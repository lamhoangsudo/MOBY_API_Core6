using MOBY_API_Core6.Data_View_Model;

namespace MOBY_API_Core6.Repository
{
    public interface IRequestRepository
    {
        public Task<bool> CreateCart(int userID);
        public Task<RequestVM?> GetCartByUid(int userID);
        public Task<bool> CheackExistedCartByUid(int userID);
    }
}
