using MOBY_API_Core6.Data_View_Model;

namespace MOBY_API_Core6.Repository
{
    public interface IRequestRepository
    {
        public Task<bool> CreateRequest(int userID);
        public Task<RequestVM?> GetRequestByUid(int userID);
        public Task<bool> CheackExistedRequestByUid(int userID);
    }
}
