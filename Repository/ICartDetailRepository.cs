using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public interface ICartDetailRepository
    {
        //public Task<List<RequestDetailVM>> GetAllRequestDetail(int requestDetailID);
        public Task<bool> CreateCartDetail(CreateCartDetailVM createdcartDetail);
        public Task<bool> UpdateCartDetail(CartDetail cartDetail, UpdateCartDetailVM updatedCartDetail);
        public Task<CartDetail?> GetCartDetailByCartDetailID(int cartDetailID);
        public Task<bool> DeleteCartDetail(CartDetail cartDetail);
        public Task<List<CartDetailVM>> GetListCartDetailByListID(ListCartDetailID cartDetailIDList);
        public Task<bool> ConfirmCartDetail(ListCartDetailidToConfirm requestDetailIDList, int uid);
        //public Task<List<CartDetailVM>> GetCartDetailByItemID(int itemID);

    }
}
