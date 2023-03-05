using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public interface ICartDetailRepository
    {
        public List<CartDetailVM> GetAllCartDetail(int cartID);
        public bool CreateCartDetail(CreateCartDetailVM createdCartDetail);
        public bool UpdateCartDetail(CartDetail cartDetail, int status);
        public CartDetail GetCartDetailByCartDetailID(int CartDetailID);
        public List<CartDetailVM> GetCartDetailByItemID(int itemID);

    }
}
