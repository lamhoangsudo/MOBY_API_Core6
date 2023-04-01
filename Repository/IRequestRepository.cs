using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public interface IRequestRepository
    {
        //public Task<String> AcceptRequest(Request request);
        public Task<bool> DenyRequest(Request request);
        public Task<List<RequestVM>> getRequestBySharerID(int itemid, int status);
        public Task<List<RequestVM>> getRequestByRecieverID(int userid, int status);
        public Task<Request?> getRequestByRequestID(int requestID);
        public Task<RequestVM?> getRequestVMByRequestID(int requestID);
        public Task<bool> SaveRequest();
        //public Task<bool> ConfirmRequest(Request request);
        //public Task<bool> DenyOtherRequestWhichPassItemQuantity(Request request);
    }
}
