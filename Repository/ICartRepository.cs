using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public interface ICartRepository
    {
        public Task<bool> CreateCart(int userID);
        public Task<Cart> GetCartByUid(int userID);
        public Task<bool> CheackExistedCartByUid(int userID);
    }
}
