using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public interface IRequestRepository
    {
        public Task<bool> AcceptRequestDetail(Request request);
        public Task<bool> DenyRequestDetail(Request request);
        public Task<List<RequestVM>> getRequestByItemID(int itemid);
        public Task<Request?> getRequestByRequestID(int requestID);
        public Task<RequestVM?> getRequestVMByRequestID(int requestID);
        public Task<bool> DenyOtherRequestWhichPassItemQuantity(Request request);
    }
}
