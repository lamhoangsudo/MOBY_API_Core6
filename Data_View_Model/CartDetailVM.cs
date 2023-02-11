using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Data_View_Model
{
    public class CartDetailVM
    {
        public int CartDetailId { get; set; }
        public int CartId { get; set; }
        public int ItemId { get; set; }
        public DateTime CartDetailDateCreate { get; set; }
        public DateTime? CartDetailDateUpdate { get; set; }
        public int CartDetailItemQuantity { get; set; }

        public static CartDetailVM CartDetailToVewModel(CartDetail cartd)
        {
            return new CartDetailVM
            {
                CartDetailId = cartd.CartDetailId,
                CartId = cartd.CartId,
                ItemId = cartd.ItemId,
                CartDetailDateCreate = cartd.CartDetailDateCreate,
                CartDetailDateUpdate = cartd.CartDetailDateUpdate,
                CartDetailItemQuantity = cartd.CartDetailItemQuantity

            };
        }
    }
}
