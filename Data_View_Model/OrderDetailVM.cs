using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Data_View_Model
{
    public class OrderDetailVM
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public ItemVM? itemVM { get; set; }
        public static OrderDetailVM OrderDetailToViewModel(OrderDetail orderDetail)
        {
            var orderDetailVM = new OrderDetailVM();
            orderDetailVM.OrderDetailId = orderDetail.OrderDetailId;
            orderDetailVM.OrderId = orderDetail.OrderId;
            orderDetailVM.ItemId = orderDetail.ItemId;
            orderDetailVM.Quantity = orderDetail.Quantity;
            orderDetailVM.Price = orderDetail.Price;

            var item = orderDetail.Item;
            orderDetailVM.itemVM = ItemVM.ItemForRequestOrderToViewModel(item);

            return orderDetailVM;
        }
    }
}
