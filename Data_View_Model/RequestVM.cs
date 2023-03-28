using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Data_View_Model
{
    public class RequestVM
    {
        public int RequestId { get; set; }
        public int UserId { get; set; }
        public string Address { get; set; } = null!;
        public string? Note { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateChangeStatus { get; set; }
        public int Status { get; set; }

        public List<RequestDetailVM>? requestDetailVM { get; set; }
        public UserVM? ItemOwner { get; set; }
        public UserVM? userVM { get; set; }
        public static RequestVM RequestToVewModel(Request request)
        {
            var requestVM = new RequestVM
            {
                RequestId = request.RequestId,
                UserId = request.UserId,
                Address = request.Address,
                Note = request.Note,
                DateCreate = request.DateCreate,
                DateChangeStatus = request.DateChangeStatus,
                Status = request.Status,
            };
            var requestDetail = request.RequestDetails.Select(rd => RequestDetailVM.RequestDetailToVewModel(rd)).ToList();
            requestVM.requestDetailVM = requestDetail;


            var ItemOwner = request.RequestDetails.First().Item.User;
            requestVM.ItemOwner = UserVM.UserAccountToVewModel(ItemOwner);
            var user = request.User;
            requestVM.userVM = UserVM.UserAccountToVewModel(user);


            return requestVM;
        }
    }
}
