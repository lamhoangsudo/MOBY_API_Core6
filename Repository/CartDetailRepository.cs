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
        public async Task<List<CartDetailVM>> GetAllCartDetail(int cartID)
        {

            List<CartDetail> listCartDetail = new List<CartDetail>();
            List<CartDetailVM> listCartDetailMV = new List<CartDetailVM>();
            var cart = context.Carts.Where(c => c.CartId == cartID)
                .Include(thisCart => thisCart.CartDetails).FirstOrDefault();
            listCartDetail = context.CartDetails.Where(u => u.CartId == cartID).ToList();
            listCartDetail = cart.CartDetails.ToList();
            CartDetailVM crmv = new CartDetailVM();
            foreach (var item in listCartDetail)
            {
                crmv = CartDetailVM.CartDetailToVewModel(item);
                listCartDetailMV.Add(crmv);
            }
            return listCartDetailMV;
        }

        public async Task<CartDetail> GetCartDetailByCartDetailID(int CartDetail)
        {

            CartDetail foundCartDetail = context.CartDetails.Where(cd => cd.CartDetailId == CartDetail).FirstOrDefault();

            return foundCartDetail;
        }

        public async Task<bool> CreateCartDetail(int cartID, int itemID, int quantity)
        {
            try
            {
                CartDetail newCartDetail = new CartDetail();
                newCartDetail.CartId = cartID;
                newCartDetail.ItemId = itemID;
                newCartDetail.CartDetailDateCreate = DateTime.Now;
                newCartDetail.CartDetailItemQuantity = quantity;
                context.CartDetails.Add(newCartDetail);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public async Task<bool> UpdateCartDetail(CartDetail cartDetail, int quantity)
        {
            try
            {
                cartDetail.CartDetailItemQuantity = quantity;
                cartDetail.CartDetailDateUpdate = DateTime.Now;

                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public async Task<bool> DeleteCartDetail(CartDetail cartDetail)
        {

            //List<CartDetail> listCartDetail = new List<CartDetail>();
            //listCartDetail = await context.CartDetails.Where(cd => cd.CartDetailId = cartDetailID).ToList();
            //CartDetail cartDetail = context.CartDetails.Where(cd => cd.CartDetailId == cartDetailID).FirstOrDefault();
            try
            {
                context.Remove(cartDetail);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }


    }

}



