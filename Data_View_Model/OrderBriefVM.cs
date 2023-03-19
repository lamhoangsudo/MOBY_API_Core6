using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Data_View_Model
{
    public class OrderBriefVM
    {

        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int ItemId { get; set; }
        public int Quanlity { get; set; }
        public int Status { get; set; }
        public DateTime DateCreate { get; set; }
        public ItemVM? ItemVM { get; set; }
        public UserVM? UserVM { get; set; }

        public static OrderBriefVM OrderToBriefVewModel(Order order)
        {
            var orderBriefVM = new OrderBriefVM
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                Quanlity = order.Quanlity,
                ItemId = order.ItemId,
                DateCreate = order.DateCreate,

                Status = order.Status,
            };
            var item = order.Item;
            orderBriefVM.ItemVM = ItemVM.ItemToViewModel(item);

            var user = order.User;
            orderBriefVM.UserVM = UserVM.UserAccountToVewModel(user);


            return orderBriefVM;
        }
    }
}