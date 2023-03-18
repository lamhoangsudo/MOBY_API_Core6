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

        public async Task<bool> CreateCartDetail(CreateCartDetailVM createdRequestDetail)
        {
            ItemVM? itemfound = await context.Items.Where(i => i.ItemId == createdRequestDetail.ItemId)
                .Select(i => ItemVM.ItemToViewModel(i)).FirstOrDefaultAsync();
            if (itemfound == null)
            {
                return false;
            }

            CartDetail newCartDetail = new CartDetail();
            newCartDetail.CartId = createdRequestDetail.CartId;
            newCartDetail.ItemId = createdRequestDetail.ItemId;
            newCartDetail.ItemQuantity = 1;

            await context.CartDetails.AddAsync(newCartDetail);

            await context.SaveChangesAsync();
            return true;

            return false;
        }

        public async Task<bool> UpdateCartDetail(CartDetail cartDetail, UpdateCartDetailVM updatedcartDetail)
        {
            int quantityleft = await context.Items.Where(i => i.ItemId == cartDetail.ItemId)
                .Select(i => i.ItemShareAmount)
                .FirstOrDefaultAsync();
            if (quantityleft < updatedcartDetail.ItemQuantity)
            {
                return false;
            }
            cartDetail.ItemQuantity = updatedcartDetail.ItemQuantity;


            if (await context.SaveChangesAsync() != 0)
            {
                return true;
            }
            return false;
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

        public async Task<List<CartDetailVM>> GetListCartDetailByListID(ListCartDetailID requestDetailIDList)
        {

            List<CartDetailVM> listCartDetail = new List<CartDetailVM>();

            if (requestDetailIDList.listCartDetailID == null || requestDetailIDList.listCartDetailID.Count == 0)
            {
                return listCartDetail;
            }

            foreach (int id in requestDetailIDList.listCartDetailID)
            {
                CartDetailVM? currentRequestDetail = await context.CartDetails
                    .Where(cd => cd.CartDetailId == id)
                    .Include(cd => cd.Item)
                    .ThenInclude(i => i.User)
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

                CartDetail? currentRequestDetail = await context.CartDetails.Where(cd => cd.CartDetailId == id).FirstOrDefaultAsync();

                if (currentRequestDetail != null)
                {
                    Request newRequest = new Request();
                    newRequest.UserId = uid;
                    newRequest.ItemId = currentRequestDetail.ItemId;
                    newRequest.ItemQuantity = currentRequestDetail.ItemQuantity;
                    newRequest.Address = address;
                    newRequest.Note = requestDetailIDList.note;
                    newRequest.DateCreate = DateTime.Now;
                    newRequest.Status = 0;
                    context.Requests.Add(newRequest);
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



