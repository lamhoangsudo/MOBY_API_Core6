using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository.IRepository
{
    public interface ICartDetailRepository
    {
        Task<CartDetail?> GetCartDetailByCartDetailID(int cartDetailID);
        Task<string> CheclExistCartDetail(CreateCartDetailVM createdcartDetail);
        Task<string> CreateCartDetail(CreateCartDetailVM createdRequestDetail, int uid);
        Task<string> UpdateCartDetail(CartDetail cartDetail, UpdateCartDetailVM updatedcartDetail);
        Task<int> DeleteCartDetail(CartDetail cartDetail);
        Task<CartDetailVM?> GetCartDetailByID(int id);
        Task<List<Order>> GetListOrder(int uid);
        Task<int> CartDetailFreeCount(int[] listCartDetailID);
        Task<bool> ConfirmCartDetail(ListCartDetailidToConfirm cartDetailIDList, int uid);
    }
}
