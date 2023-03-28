using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Data_View_Model
{
    public class OrderDetailBriefVM
    {
        public int Quantity { get; set; }
        public double Price { get; set; }
        public ItemVM? itemVM { get; set; }

        public static OrderDetailBriefVM OrderDetailBriefToViewModel(OrderDetail orderDetail)
        {
            var orderDetailBrief = new OrderDetailBriefVM();
            orderDetailBrief.Quantity = orderDetail.Quantity;
            orderDetailBrief.Price = orderDetail.Price;

            var item = orderDetail.Item;
            orderDetailBrief.itemVM = ItemVM.ItemForRequestOrderToViewModel(item);

            return orderDetailBrief;
        }

    }
}
