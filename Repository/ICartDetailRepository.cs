using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public interface ICartDetailRepository
    {
        public Task<List<RequestDetailVM>> GetAllCartDetail(int cartID);
        public Task<bool> CreateCartDetail(CreateCartDetailVM createdCartDetail);
        public Task<bool> UpdateCartDetail(CartDetail cartDetail, int status);
        public Task<CartDetail?> GetCartDetailByCartDetailID(int CartDetailID);
        public Task<bool> DeleteCartDetail(CartDetail cartDetail);
        //public Task<List<CartDetailVM>> GetCartDetailByItemID(int itemID);

    }
}
