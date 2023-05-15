using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository.IRepository
{
    public interface ICartRepository
    {
        public Task<int> CreateCart(int userID);
        public Task<int> UpdateCart(Cart cart, UpdateCartVM updatedCart);
        public Task<CartVM?> GetCartVMByUid(int userID);
        public Task<Cart?> CheackExistedCartByUid(int userID);
        public Task<Cart?> GetCartByUid(int userID);
    }
}
