using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public class CartDetailRepository : ICartDetailRepository
    {
        private readonly MOBYContext context;

        public CartDetailRepository(MOBYContext context)
        {
            this.context = context;
        }
        public async Task<List<RequestDetailVM>> GetAllCartDetail(int cartID)
        {
            List<RequestDetailVM> listCartDetailMV = await context.RequestDetails
                .Where(cd => cd.CartId == cartID)
                .Include(cd => cd.Item)
                .ThenInclude(item => item.User)
                .Select(cd => RequestDetailVM.CartDetailToVewModel(cd))
                .ToListAsync();
            /*List<CartDetail> listCartDetail = new List<CartDetail>();
            List<CartDetailVM> listCartDetailMV = new List<CartDetailVM>();
            var cart = context.Carts.Where(c => c.CartId == cartID)
                .Include(thisCart => thisCart.CartDetails).FirstOrDefault();
            //listCartDetail = context.CartDetails.Where(u => u.CartId == cartID).ToList();
            listCartDetail = cart.CartDetails.ToList();
            CartDetailVM crmv = new CartDetailVM();
            foreach (var item in listCartDetail)
            {
                crmv = CartDetailVM.CartDetailToVewModel(item);
                listCartDetailMV.Add(crmv);
            }*/
            return listCartDetailMV;
        }

        public async Task<CartDetail?> GetCartDetailByCartDetailID(int CartDetail)
        {

            CartDetail? foundCartDetail = await context.CartDetails
                .Where(cd => cd.CartDetailId == CartDetail)
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

        public async Task<bool> CreateCartDetail(CreateCartDetailVM createdCartDetail)
        {

            CartDetail newCartDetail = new CartDetail();
            newCartDetail.CartId = createdCartDetail.CartId;
            newCartDetail.ItemId = createdCartDetail.ItemId;
            newCartDetail.CartDetailDateCreate = DateTime.Now;
            newCartDetail.CartDetailItemQuantity = createdCartDetail.CartDetailItemQuantity;

            await context.CartDetails.AddAsync(newCartDetail);
            if (await context.SaveChangesAsync() != 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateCartDetail(CartDetail cartDetail, int quantity)
        {

            cartDetail.CartDetailItemQuantity = quantity;
            cartDetail.CartDetailDateUpdate = DateTime.Now;

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




    }

}



