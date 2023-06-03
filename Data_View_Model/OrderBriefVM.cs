using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Data_View_Model
{
    public class OrderBriefVM
    {

        public int OrderId { get; set; }
        public int Status { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DatePackage { get; set; }
        public DateTime? DateReceived { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string? ReasonCancel { get; set; }
        public DateTime? DateCancel { get; set; }
        public UserVM? UserRecieverVM { get; set; }
        public UserVM? UserSharerVM { get; set; }
        public ItemVMForOrderBriefVM? ItemVM { get; set; }

        public static OrderBriefVM OrderToBriefVewModel(Order order)
        {
            var orderBriefVM = new OrderBriefVM
            {
                OrderId = order.OrderId,
                DateCreate = order.DateCreate,
                DatePackage = order.DatePackage,
                DateReceived = order.DateReceived,
                Quantity = order.Quantity,
                Price = order.Price,
                Status = order.Status,
                ReasonCancel = order.ReasonCancel,
                DateCancel = order.DateCancel,

            };


            var userOwner = order.Item.User;
            orderBriefVM.UserSharerVM = UserVM.UserAccountToVewModel(userOwner);

            var user = order.User;
            orderBriefVM.UserRecieverVM = UserVM.UserAccountToVewModel(user);

            var item = order.Item;
            orderBriefVM.ItemVM = ItemVMForOrderBriefVM.ItemForOrderToViewModel(item);
            return orderBriefVM;
        }
    }
}