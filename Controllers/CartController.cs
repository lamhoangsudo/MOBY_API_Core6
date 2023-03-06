using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository;

namespace MOBY_API_Core6.Controllers
{

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
        [Route("api/cart/create")]
        public async Task<IActionResult> CreateCart()
        {
            var currentUid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
            if (await cartDAO.CheackExistedCartByUid(currentUid))
            {
                return Ok(ReturnMessage.create("this user already has a cart"));
            }
            if (await cartDAO.CreateCart(currentUid))
            {
                return Ok(ReturnMessage.create("success"));
            }
            return Ok(ReturnMessage.create("error at createCart"));
        }

        [Authorize]
        [HttpGet]
        [Route("api/useraccount/cart")]
        public async Task<IActionResult> GetCartByUid()
        {
            int currentUid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
            CartVM? currentCart = await cartDAO.GetCartByUid(currentUid);
            return Ok(currentCart);
        }
    }
}
