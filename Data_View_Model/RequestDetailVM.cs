using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Data_View_Model
{
    public class RequestDetailVM
    {
        public int RequestDetailId { get; set; }
        public int RequestId { get; set; }
        public int ItemId { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public int ItemQuantity { get; set; }
        public int Status { get; set; }
        public bool? SponsoredOrderShippingFee { get; set; }
        public string? Note { get; set; }

        public ItemVM? ItemVM { get; set; }
        public UserVM? UserVM { get; set; }
        public static RequestDetailVM RequestDetailToVewModel(RequestDetail requestDetail)
        {
            var RequestDetailVM = new RequestDetailVM
            {
                RequestDetailId = requestDetail.RequestDetailId,
                RequestId = requestDetail.RequestId,
                ItemId = requestDetail.ItemId,
                DateCreate = requestDetail.DateCreate,
                DateUpdate = requestDetail.DateUpdate,
                ItemQuantity = requestDetail.ItemQuantity,
                Status = requestDetail.Status,
                SponsoredOrderShippingFee = requestDetail.SponsoredOrderShippingFee,
                Note = requestDetail.Note,

            };
            var item = requestDetail.Item;
            RequestDetailVM.ItemVM = ItemVM.ItemToViewModel(item);
            if (requestDetail.Item.User != null)
            {
                var user = requestDetail.Item.User;
                RequestDetailVM.UserVM = UserVM.UserAccountToVewModel(user);
            }

            return RequestDetailVM;

        }

        public static RequestDetailVM RequestDetailToVewModel(RequestDetail requestDetail, Models.Item item, UserAccount user)
        {
            var RequestDetailVM = new RequestDetailVM
            {
                RequestDetailId = requestDetail.RequestDetailId,
                RequestId = requestDetail.RequestId,
                ItemId = requestDetail.ItemId,
                DateCreate = requestDetail.DateCreate,
                DateUpdate = requestDetail.DateUpdate,
                ItemQuantity = requestDetail.ItemQuantity,
                Status = requestDetail.Status,

            };

            RequestDetailVM.ItemVM = ItemVM.ItemToViewModel(item);

            RequestDetailVM.UserVM = UserVM.UserAccountToVewModel(user);
            return RequestDetailVM;

        }
    }
}
