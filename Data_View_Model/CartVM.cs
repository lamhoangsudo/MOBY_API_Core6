using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Data_View_Model
{
    public class CartVM
    {
        public int CartId { get; set; }
        public int? UserId { get; set; }
        public string Address { get; set; } = null!;
        public List<CartDetailVM>? cartDetailList { get; set; }


        public static CartVM CartToVewModel(Cart cart)
        {
            var cartvm = new CartVM
            {
                CartId = cart.CartId,
                UserId = cart.UserId,
                Address = cart.Address,
            };
            var ListCartDetail = cart.CartDetails.Select(cd => CartDetailVM.RequestDetailToVewModel(cd)).ToList();
            cartvm.cartDetailList = ListCartDetail;
            return cartvm;
        }

    }
}
