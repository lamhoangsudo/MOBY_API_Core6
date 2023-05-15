using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Repository
{
    public class CartDetailRepository : ICartDetailRepository
    {
        private readonly MOBYContext context;
        private readonly IEmailService emailDAO;

        public CartDetailRepository(MOBYContext context, IEmailService emailDAO)
        {
            this.context = context;
            this.emailDAO = emailDAO;
        }

        public async Task<CartDetail?> GetCartDetailByCartDetailID(int cartDetailID)
        {
            return await context.CartDetails
                .Where(cd => cd.CartDetailId == cartDetailID)
                .Include(cd => cd.Item)
                .FirstOrDefaultAsync();
        }
        public async Task<string> CheclExistCartDetail(CreateCartDetailVM createdcartDetail)
        {
            CartDetail? foundCartDetail = await context.CartDetails
                .Where(cd => cd.CartId == createdcartDetail.CartId && cd.ItemId == createdcartDetail.ItemId)
                .Include(cd => cd.Item)
                .FirstOrDefaultAsync();
            if (foundCartDetail != null)
            {
                if (foundCartDetail.ItemQuantity >= foundCartDetail.Item.ItemShareAmount)
                {
                    return "cant not add more than share ammout";
                }
                foundCartDetail.ItemQuantity += 1;
                await context.SaveChangesAsync();
                return "succes";
            }
            return "";
        }
        public async Task<string> CreateCartDetail(CreateCartDetailVM createdRequestDetail, int uid)
        {
            ItemVM? itemfound = await context.Items.Where(i => i.ItemId == createdRequestDetail.ItemId)
                .Select(i => ItemVM.ItemToViewModel(i)).FirstOrDefaultAsync();
            if (itemfound == null)
            {
                return "item not found";
            }
            if (itemfound.UserId == uid)
            {
                return "can't add own item";
            }
            CartDetail newCartDetail = new()
            {
                CartId = createdRequestDetail.CartId,
                ItemId = createdRequestDetail.ItemId,
                ItemQuantity = 1
            };
            await context.CartDetails.AddAsync(newCartDetail);

            if (await context.SaveChangesAsync() != 0)
            {
                return "success";
            }
            return "error at CreateRequestDetail";
        }
        public async Task<string> UpdateCartDetail(CartDetail cartDetail, UpdateCartDetailVM updatedcartDetail)
        {
            int quantityleft = await context.Items.Where(i => i.ItemId == cartDetail.ItemId)
                .Select(i => i.ItemShareAmount)
                .FirstOrDefaultAsync();
            if (quantityleft < updatedcartDetail.ItemQuantity)
            {
                return "item available ammout is " + quantityleft + " ";
            }
            cartDetail.ItemQuantity = updatedcartDetail.ItemQuantity;
            if (await context.SaveChangesAsync() != 0)
            {
                return "success";
            }
            return "error";
        }
        public async Task<int> DeleteCartDetail(CartDetail cartDetail)
        {
            context.CartDetails.Remove(cartDetail);
            return await context.SaveChangesAsync();
        }
        public async Task<CartDetailVM?> GetCartDetailByID(int id)
        {
            return await context.CartDetails
                .Where(cd => cd.CartDetailId == id)
                .Include(cd => cd.Item)
                .ThenInclude(i => i.User)
                .Include(cd => cd.Item)
                .ThenInclude(i => i.SubCategory)
                .Select(cd => CartDetailVM.RequestDetailToVewModel(cd))
                .FirstOrDefaultAsync();
        }
        public async Task<List<Order>> GetListOrder(int uid)
        {
            return await context.Orders.Where(o => o.Price == 0 && o.UserId == uid).OrderByDescending(o => o.OrderId).Take(7).ToListAsync();
        }
        public async Task<int> CartDetailFreeCount(int[] listCartDetailID)
        {
            return await context.CartDetails
                .Include(cd => cd.Item)
                .Where(cd => listCartDetailID.Contains(cd.CartDetailId) && cd.Item.ItemSalePrice == 0)
                .CountAsync();
        }
        public async Task<bool> ConfirmCartDetail(ListCartDetailidToConfirm cartDetailIDList, int uid)
        {
            string? address;
            string? note = cartDetailIDList.Note;
            if (cartDetailIDList.Address == null || cartDetailIDList.Address == "")
            {
                address = await context.Carts.Where(c => c.UserId == uid)
                .Select(c => c.Address).FirstOrDefaultAsync();
            }
            else
            {
                address = cartDetailIDList.Address;
            }
            if (address == null || address == "")
            {
                return false;
            }
            List<UserAccount> itemOwner = new();
            IDictionary<int, UserAccount> itemOwnerDic = new Dictionary<int, UserAccount>();
            List<CartDetail> currentCartDetails = await context.CartDetails.Where(cd => cartDetailIDList.ListCartDetailID!.Contains(cd.CartDetailId))
                .Include(cd => cd.Item)
                .ThenInclude(i => i.User)
                .ToListAsync();
            foreach (CartDetail cartDetail in currentCartDetails)
            {
                if (cartDetail.Item.ItemShareAmount < cartDetail.ItemQuantity)
                {
                    return false;
                }
                Order newOrder = new()
                {
                    UserId = uid,
                    ItemId = cartDetail.ItemId,
                    Address = address,
                    Note = note,
                    Status = 0,
                    DateCreate = DateTime.Now,
                    Quantity = cartDetail.ItemQuantity,
                    Price = cartDetail.Item.ItemSalePrice!.Value
                };
                if (cartDetailIDList.Vnp_TransactionNo == null || cartDetailIDList.Vnp_TransactionNo.Equals(""))
                {
                    newOrder.TransactionNo = null;
                }
                else
                {
                    newOrder.TransactionNo = cartDetailIDList.Vnp_TransactionNo;
                }
                if (cartDetailIDList.TransactionDate == null || cartDetailIDList.TransactionDate.Equals(""))
                {
                    newOrder.TransactionDate = null;
                }
                else
                {
                    newOrder.TransactionDate = cartDetailIDList.TransactionDate;
                }
                if (cartDetailIDList.Vnp_CardType == null || cartDetailIDList.Vnp_CardType.Equals(""))
                {
                    newOrder.CardType = null;
                }
                else
                {
                    newOrder.CardType = cartDetailIDList.Vnp_CardType;
                }
                if (cartDetailIDList.Vnp_BankCode == null || cartDetailIDList.Vnp_BankCode.Equals(""))
                {
                    newOrder.BankCode = null;
                }
                else
                {
                    newOrder.BankCode = cartDetailIDList.Vnp_BankCode;
                }
                newOrder.DateCancel = null;
                context.Orders.Add(newOrder);
                cartDetail.Item.ItemShareAmount -= cartDetail.ItemQuantity;

                if (!itemOwnerDic.ContainsKey(cartDetail.Item.UserId))
                {
                    itemOwnerDic.Add(cartDetail.Item.UserId, cartDetail.Item.User);
                }
                context.CartDetails.Remove(cartDetail);
            }
            foreach (var owner in itemOwnerDic)
            {
                Email newEmail = new()
                {
                    To = owner.Value.UserGmail,
                    UserName = owner.Value.UserName,
                    Subject = "Bạn có đơn hàng đã được khởi tạo",
                    Obj = "Đơn hàng",
                    Link = "https://moby-customer.vercel.app/account/order?itemType=sharer&status=0"
                };
                await emailDAO.SendEmai(newEmail);
            }
            if (await context.SaveChangesAsync() != 0)
            {
                return true;
            }
            return false;
        }
    }
}
