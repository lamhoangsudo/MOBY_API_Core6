using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Data_View_Model
{
    public class RequestVM
    {
        public int RequestId { get; set; }
        public int UserId { get; set; }
        public int ItemQuantity { get; set; }
        public int ItemId { get; set; }
        public string Address { get; set; } = null!;
        public string? Note { get; set; }
        public DateTime DateCreate { get; set; }
        public int Status { get; set; }
        public DateTime? DateChangeStatus { get; set; }

        public ItemVM? ItemVM { get; set; }
        public UserVM? UserVM { get; set; }
        public static RequestVM RequestToVewModel(Request request)
        {
            var requestVM = new RequestVM
            {
                RequestId = request.RequestId,
                UserId = request.UserId,
                ItemQuantity = request.ItemQuantity,
                ItemId = request.ItemId,
                Address = request.Address,
                Note = request.Note,
                DateCreate = request.DateCreate,
                DateChangeStatus = request.DateChangeStatus,
                Status = request.Status,
            };
            var item = request.Item;
            requestVM.ItemVM = ItemVM.ItemToViewModel(item);

            var user = request.User;
            requestVM.UserVM = UserVM.UserAccountToVewModel(user);


            return requestVM;
        }
    }
}
