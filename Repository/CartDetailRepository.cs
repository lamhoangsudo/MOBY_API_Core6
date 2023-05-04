using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;

namespace MOBY_API_Core6.Repository
{
    public class CartDetailRepository : ICartDetailRepository
    {
        private readonly MOBYContext context;
        private readonly IEmailRepository emailDAO;

        public CartDetailRepository(MOBYContext context, IEmailRepository emailDAO)
        {
            this.context = context;
            this.emailDAO = emailDAO;
        }


        public async Task<CartDetail?> GetCartDetailByCartDetailID(int cartDetailID)
        {

            CartDetail? foundCartDetail = await context.CartDetails
                .Where(cd => cd.CartDetailId == cartDetailID)
                .Include(cd => cd.Item)
                .FirstOrDefaultAsync();

            return foundCartDetail;
        }
        public async Task<String> CheclExistCartDetail(CreateCartDetailVM createdcartDetail)
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
        /*public List<CartDetailVM> GetCartDetailByItemID(int itemID)
        {

            List<CartDetail> foundCartDetail = context.CartDetails.Where(cd => cd.ItemId == itemID).ToList();
            List<CartDetailVM> CartDetailToVM = new List<CartDetailVM>();
            CartDetailVM cdmv = new CartDetailVM();
            foreach (var item in foundCartDetail)
            {
                cdmv = CartDetailVM.CartDetailToVewModel(item);
                CartDetailToVM.Add(cdmv);
            }
            return CartDetailToVM;
        }*/

        public async Task<String> CreateCartDetail(CreateCartDetailVM createdRequestDetail, int uid)
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

            CartDetail newCartDetail = new CartDetail();
            newCartDetail.CartId = createdRequestDetail.CartId;
            newCartDetail.ItemId = createdRequestDetail.ItemId;
            newCartDetail.ItemQuantity = 1;
            await context.CartDetails.AddAsync(newCartDetail);

            if (await context.SaveChangesAsync() != 0)
            {
                return "success";
            }


            return "error at CreateRequestDetail";

        }

        public async Task<String> UpdateCartDetail(CartDetail cartDetail, UpdateCartDetailVM updatedcartDetail)
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

        public async Task<bool> DeleteCartDetail(CartDetail cartDetail)
        {

            context.CartDetails.Remove(cartDetail);

            if (await context.SaveChangesAsync() != 0)
            {
                return true;
            }
            return false;
        }

        public async Task<List<CartDetailVM>> GetListCartDetailByListID(int[] listOfIds)
        {

            List<CartDetailVM> listCartDetail = new List<CartDetailVM>();

            if (listOfIds == null || listOfIds.Length == 0)
            {
                return listCartDetail;
            }

            foreach (int id in listOfIds)
            {
                CartDetailVM? currentRequestDetail = await context.CartDetails
                    .Where(cd => cd.CartDetailId == id)
                    .Include(cd => cd.Item)
                    .ThenInclude(i => i.User)
                    .Include(cd => cd.Item)
                    .ThenInclude(i => i.SubCategory)
                    .Select(cd => CartDetailVM.RequestDetailToVewModel(cd))
                    .FirstOrDefaultAsync();

                if (currentRequestDetail != null)
                {
                    listCartDetail.Add(currentRequestDetail);
                }
            }


            return listCartDetail;
        }


        public async Task<String> CheckCartDetail(int[] listCartDetailID, int uid)
        {

            //int cartid = await context.Carts.Where(c => c.UserId == uid).Select(c=> c.CartId).FirstOrDefaultAsync();

            int countLeft = 7;
            List<Order> listOrder = await context.Orders.Where(o => o.Price == 0 && o.UserId == uid)
                .OrderByDescending(o => o.OrderId).Take(7).ToListAsync();
            foreach (Order order in listOrder)
            {
                TimeSpan date = DateTime.Now - order.DateCreate;
                if (date.TotalDays <= 7)
                {
                    countLeft--;
                }
            }
            if (countLeft <= 0)
            {
                return "Bạn đã vượt quá giới hạn lượt nhận trong tuần, vui lòng thử lại khi khác";
            }
            if (countLeft == 7)
            {
                return "success";
            }
            int cartDetailFree = await context.CartDetails
                .Include(cd => cd.Item)
                .Where(cd => listCartDetailID.Contains(cd.CartDetailId) && cd.Item.ItemSalePrice == 0)
                .CountAsync();
            if (cartDetailFree > countLeft)
            {
                return "Bạn đã nhận " + (7 - countLeft) + "/7 sản phẩm trong tuần, bạn không thể nhận thêm " + cartDetailFree + " sản phẩm, vui lòng thử lại khi khác";
            }


            return "success";
        }


        public async Task<bool> ConfirmCartDetail(ListCartDetailidToConfirm cartDetailIDList, int uid)
        {

            String? address;
            String? note = cartDetailIDList.note;
            if (cartDetailIDList.address == null || cartDetailIDList.address == "")
            {
                address = await context.Carts.Where(c => c.UserId == uid)
                .Select(c => c.Address).FirstOrDefaultAsync();
            }
            else
            {
                address = cartDetailIDList.address;
            }

            if (address == null || address == "")
            {
                return false;
            }
            List<UserAccount> itemOwner = new List<UserAccount>();
            IDictionary<int, String> itemOwnerDic = new Dictionary<int, String>();
            List<CartDetail> currentCartDetails = await context.CartDetails.Where(cd => cartDetailIDList.listCartDetailID!.Contains(cd.CartDetailId))
                .Include(cd => cd.Item)
                .ThenInclude(i => i.User)
                .ToListAsync();
            foreach (CartDetail cartDetail in currentCartDetails)
            {
                if (cartDetail.Item.ItemShareAmount < cartDetail.ItemQuantity)
                {
                    return false;
                }
                Order newOrder = new Order();
                newOrder.UserId = uid;
                newOrder.ItemId = cartDetail.ItemId;
                newOrder.Address = address;
                newOrder.Note = note;
                newOrder.Status = 0;
                newOrder.DateCreate = DateTime.Now;
                newOrder.Quantity = cartDetail.ItemQuantity;
                newOrder.Price = cartDetail.Item.ItemSalePrice!.Value;
                newOrder.TransactionNo = cartDetailIDList.vnp_TransactionNo;
                newOrder.TransactionDate = cartDetailIDList.TransactionDate;
                newOrder.CardType = cartDetailIDList.vnp_CardType;
                newOrder.BankCode = cartDetailIDList.vnp_BankCode;
                context.Orders.Add(newOrder);
                cartDetail.Item.ItemShareAmount -= cartDetail.ItemQuantity;

                if (!(itemOwnerDic.ContainsKey(cartDetail.Item.UserId)))
                {
                    itemOwnerDic.Add(cartDetail.Item.UserId, cartDetail.Item.User.UserGmail);
                }
                context.CartDetails.Remove(cartDetail);
            }
            foreach (var owner in itemOwnerDic)
            {
                Email newEmail = new Email();
                newEmail.To = owner.Value;
                newEmail.Subject = "you have an order";
                newEmail.Body = "you have an order";
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



