using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Repository;

namespace MOBY_API_Core6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartDetailController : ControllerBase
    {
        private readonly IUserRepository userDAO;
        private readonly ICartRepository cartDAO;
        private readonly ICartDetailRepository cartDetailDAO;
        public CartDetailController(ICartDetailRepository cartDetailDAO, ICartRepository cartDAO, IUserRepository userDAO)
        {
            this.userDAO = userDAO;
            this.cartDetailDAO = cartDetailDAO;
            this.cartDAO = cartDAO;
        }
        [Authorize]
        [HttpGet]
        [Route("api/CartDetailController/GetAllCartDetail")]
        public async Task<List<CartDetailVM>> GetAllCartDetail()
        {
            int uid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
            CartVM cart = await cartDAO.GetCartByUid(uid);
            List<CartDetailVM> listCartDetail = await cartDetailDAO.GetAllCartDetail(cart.CartId);
            return listCartDetail;
        }
        [Authorize]
        [HttpPost]
        [Route("api/CartDetailController/CreateCartDetail/{cartID} {itemID} {quantity}")]
        public async Task<bool> CreateCartDetail(int cartID, int itemID, int quantity)
        {
            int uid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
            CartVM cart = await cartDAO.GetCartByUid(uid);
            if (await cartDetailDAO.CreateCartDetail(cartID, itemID, quantity))
            {
                return true;
            }
            return false;
        }

        [Authorize]
        [HttpPost]
        [Route("api/CartDetailController/UpdateCratDetail/{cartDetailID} {quantity}")]
        public async Task<bool> UpdateCratDetail(int cartDetailID, int quantity)
        {
            var cacrtDetail = await cartDetailDAO.GetCartDetailByCartDetailID(cartDetailID);
            if (await cartDetailDAO.UpdateCartDetail(cacrtDetail, quantity))
            {
                return true;
            }
            return false;
        }

        [Authorize]
        [HttpPost]
        [Route("api/CartDetailController/DeleteCratDetail/{cartDetailID}")]
        public async Task<bool> DeleteCratDetail(int cartDetailID)
        {
            var cacrtDetail = await cartDetailDAO.GetCartDetailByCartDetailID(cartDetailID);
            if (await cartDetailDAO.DeleteCartDetail(cacrtDetail))
            {
                return true;
            }
            return false;
        }
    }
}
