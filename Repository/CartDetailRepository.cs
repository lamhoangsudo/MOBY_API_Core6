using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public class CartDetailRepository : ICartDetailRepository
    {
        private readonly MOBYContext context;
        private readonly IOrderRepository orderDAO;

        public CartDetailRepository(MOBYContext context, IOrderRepository orderDAO)
        {
            this.context = context;
            this.orderDAO = orderDAO;
        }


        public async Task<CartDetail?> GetCartDetailByCartDetailID(int cartDetailID)
        {

            CartDetail? foundCartDetail = await context.CartDetails
                .Where(cd => cd.CartDetailId == cartDetailID)
                .Include(cd => cd.Item)
                .FirstOrDefaultAsync();

            return foundCartDetail;
        }
        public async Task<bool> CheclExistCartDetail(CreateCartDetailVM createdcartDetail)
        {

            CartDetail? foundCartDetail = await context.CartDetails
                .Where(cd => cd.CartId == createdcartDetail.CartId && cd.ItemId == createdcartDetail.ItemId)
                .FirstOrDefaultAsync();
            if (foundCartDetail != null)
            {
                foundCartDetail.ItemQuantity += 1;
                await context.SaveChangesAsync();
                return true;
            }
            return false;
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


        public async Task<bool> ConfirmCartDetail(ListCartDetailidToConfirm requestDetailIDList, int uid)
        {
            if (requestDetailIDList.listCartDetailID == null || requestDetailIDList.listCartDetailID.Count == 0)
            {
                return false;
            }
            String? address = await context.Carts.Where(c => c.UserId == uid)
                .Select(c => c.Address).FirstOrDefaultAsync();
            if (address == null || address == "")
            {
                return false;
            }
            foreach (int id in requestDetailIDList.listCartDetailID)
            {

                CartDetail? currentCartDetail = await context.CartDetails.Where(cd => cd.CartDetailId == id).Include(cd => cd.Item).FirstOrDefaultAsync();

                if (currentCartDetail != null)
                {
                    Request newRequest = new Request();
                    newRequest.UserId = uid;
                    newRequest.ItemId = currentCartDetail.ItemId;
                    newRequest.ItemQuantity = currentCartDetail.ItemQuantity;
                    if (currentCartDetail.Item.ItemSalePrice != null)
                    {
                        newRequest.Price = (double)currentCartDetail.Item.ItemSalePrice;
                    }
                    else
                    {
                        newRequest.Price = 0;
                    }
                    newRequest.Address = address;
                    newRequest.Note = requestDetailIDList.note;
                    newRequest.DateCreate = DateTime.Now;
                    newRequest.Status = 0;
                    context.Requests.Add(newRequest);
                    context.CartDetails.Remove(currentCartDetail);
                }

            }

            if (await context.SaveChangesAsync() != 0)
            {
                return true;
            }
            return false;
        }




    }

}



