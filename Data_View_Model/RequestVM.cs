using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Data_View_Model
{
    public class RequestVM
    {
        public int RequestId { get; set; }
        public int? UserId { get; set; }
        public List<RequestDetailVM>? cartDetailList { get; set; }


        public static RequestVM CartToVewModel(Request request)
        {
            var cartvm = new RequestVM
            {
                RequestId = request.RequestId,
                UserId = request.UserId
            };
            var ListCartDetail = request.RequestDetails.Select(cd => RequestDetailVM.CartDetailToVewModel(cd)).ToList();
            cartvm.cartDetailList = ListCartDetail;
            return cartvm;
        }

    }
}
