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
        public ItemVM? ItemVM { get; set; }
        public UserVM? UserVM { get; set; }
        public static CartDetailVM CartDetailToVewModel(CartDetail cartdetail)
        {
            var CartDetailVM = new CartDetailVM
            {
                CartDetailId = cartdetail.CartDetailId,
                CartId = cartdetail.CartId,
                ItemId = cartdetail.ItemId,
                CartDetailDateCreate = cartdetail.CartDetailDateCreate,
                CartDetailDateUpdate = cartdetail.CartDetailDateUpdate,
                CartDetailItemQuantity = cartdetail.CartDetailItemQuantity,

            };
            var item = cartdetail.Item;
            CartDetailVM.ItemVM = ItemVM.ItemToViewModel(item);
            var user = cartdetail.Item.User;
            CartDetailVM.UserVM = UserVM.UserAccountToVewModel(user);
            return CartDetailVM;

        }
    }
}
