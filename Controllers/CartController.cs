using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Repository;

namespace MOBY_API_Core6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IUserRepository userDAO;
        private readonly ICartRepository cartDAO;
        public CartController(IUserRepository userDao, ICartRepository cartDAO)
        {
            this.userDAO = userDao;
            this.cartDAO = cartDAO;
        }
        [Authorize]
        [HttpPost]
        [Route("api/CartController/CreateCart")]
        public async Task<String> CreateCart()
        {
            var currentUid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
            if (await cartDAO.CheackExistedCartByUid(currentUid))
            {
                return "false";
            }
            if (await cartDAO.CreateCart(currentUid))
            {
                return "success";
            }
            return "false";
        }

        [Authorize]
        [HttpGet]
        [Route("api/CartController/getCartByUid")]
        public async Task<CartVM> GetCartByUid()
        {
            int currentUid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
            CartVM currentCart = await cartDAO.GetCartByUid(currentUid);
            return currentCart;


        }
    }
}
