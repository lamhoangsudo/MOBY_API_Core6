using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Data_View_Model
{
    public class RequestDetailVM
    {
        public int RequestDetailId { get; set; }
        public int RequestId { get; set; }
        public int ItemId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int Status { get; set; }
        public ItemVM? ItemVM { get; set; }

        public static RequestDetailVM RequestDetailToVewModel(RequestDetail requestDetail)
        {
            var requestDetailVM = new RequestDetailVM
            {
                RequestDetailId = requestDetail.RequestId,
                RequestId = requestDetail.RequestId,
                ItemId = requestDetail.ItemId,
                Price = requestDetail.Price,
                Quantity = requestDetail.Quantity,

                Status = requestDetail.Status,
            };
            var item = requestDetail.Item;
            requestDetailVM.ItemVM = ItemVM.ItemForRequestOrderToViewModel(item);


            return requestDetailVM;
        }
    }
}
