using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Data_View_Model
{
    public class CartDetailVM
    {
        public int CartDetailId { get; set; }
        public int CartId { get; set; }
        public int ItemId { get; set; }
        public int ItemQuantity { get; set; }

        public ItemVM? ItemVM { get; set; }
        public UserVM? UserVM { get; set; }
        public static CartDetailVM RequestDetailToVewModel(CartDetail cartDetail)
        {
            var CartDetailVM = new CartDetailVM
            {
                CartDetailId = cartDetail.CartDetailId,
                CartId = cartDetail.CartId,
                ItemId = cartDetail.ItemId,
                ItemQuantity = cartDetail.ItemQuantity,

            };
            var item = cartDetail.Item;
            CartDetailVM.ItemVM = ItemVM.ItemToViewModel(item);
            if (cartDetail.Item.User != null)
            {
                var user = cartDetail.Item.User;
                CartDetailVM.UserVM = UserVM.UserAccountToVewModel(user);
            }

            return CartDetailVM;

        }

        public static CartDetailVM RequestDetailToVewModel(CartDetail cartDetail, Models.Item item, UserAccount user)
        {
            var RequestDetailVM = new CartDetailVM
            {
                CartDetailId = cartDetail.CartDetailId,
                CartId = cartDetail.CartId,
                ItemId = cartDetail.ItemId,
                ItemQuantity = cartDetail.ItemQuantity,

            };

            RequestDetailVM.ItemVM = ItemVM.ItemToViewModel(item);

            RequestDetailVM.UserVM = UserVM.UserAccountToVewModel(user);
            return RequestDetailVM;

        }
    }
}
