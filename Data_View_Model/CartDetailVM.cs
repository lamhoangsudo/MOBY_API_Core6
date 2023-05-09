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


            return CartDetailVM;

        }

        public static CartDetailVM RequestDetailToVewModel(CartDetail cartDetail, Models.Item item)
        {
            var RequestDetailVM = new CartDetailVM
            {
                CartDetailId = cartDetail.CartDetailId,
                CartId = cartDetail.CartId,
                ItemId = cartDetail.ItemId,
                ItemQuantity = cartDetail.ItemQuantity,
                ItemVM = ItemVM.ItemToViewModel(item)
            };


            return RequestDetailVM;

        }
    }
}
