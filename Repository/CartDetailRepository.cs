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
        public List<CartDetailVM> GetAllCartDetail(int cartID)
        {

            List<CartDetail> listCartDetail = new List<CartDetail>();
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
            }
            return listCartDetailMV;
        }

        public CartDetail GetCartDetailByCartDetailID(int CartDetail)
        {

            CartDetail foundCartDetail = context.CartDetails.Where(cd => cd.CartDetailId == CartDetail).FirstOrDefault();

            return foundCartDetail;
        }
        public List<CartDetailVM> GetCartDetailByItemID(int itemID)
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
        }

        public bool CreateCartDetail(CreateCartDetailVM createdCartDetail)
        {
            try
            {
                CartDetail newCartDetail = new CartDetail();
                newCartDetail.CartId = createdCartDetail.CartId;
                newCartDetail.ItemId = createdCartDetail.ItemId;
                newCartDetail.CartDetailDateCreate = DateTime.Now;
                newCartDetail.CartDetailItemQuantity = createdCartDetail.CartDetailItemQuantity;

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

        public bool UpdateCartDetail(CartDetail cartDetail, int quantity)
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




    }

}



