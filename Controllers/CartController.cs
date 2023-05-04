using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;

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
            if (currentUid == 0)
            {
                return BadRequest(ReturnMessage.Create("Account has been suspended"));
            }
            if (currentUid == -1)
            {
                return BadRequest(ReturnMessage.Create("Account not found"));
            }
            if (await cartDAO.CheackExistedCartByUid(currentUid))
            {
                return Ok(ReturnMessage.Create("this user already has a cart"));
            }
            if (await cartDAO.CreateCart(currentUid))
            {
                return Ok(ReturnMessage.Create("success"));
            }
            return Ok(ReturnMessage.Create("error at CreateCart"));
        }

        [Authorize]
        [HttpPatch]
        [Route("api/cart")]
        public async Task<IActionResult> UpdateCart([FromBody] UpdateCartVM updatedCart)
        {
            var currentUid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
            if (currentUid == 0)
            {
                return BadRequest(ReturnMessage.Create("Account has been suspended"));
            }
            if (currentUid == -1)
            {
                return BadRequest(ReturnMessage.Create("Account not found"));
            }
            var currenCart = await cartDAO.GetCartByUid(currentUid);
            if (currenCart == null)
            {
                return Ok(ReturnMessage.Create("Cart Not Found"));
            }
            if (await cartDAO.UpdateCart(currenCart, updatedCart))
            {
                return Ok(ReturnMessage.Create("success"));
            }
            return Ok(ReturnMessage.Create("error at UpdateCart"));
        }

        [Authorize]
        [HttpGet]
        [Route("api/useraccount/cart")]
        public async Task<IActionResult> GetCartByUid()
        {
            int currentUid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
            if (currentUid == 0)
            {
                return BadRequest(ReturnMessage.Create("Account has been suspended"));
            }
            if (currentUid == -1)
            {
                return BadRequest(ReturnMessage.Create("Account not found"));
            }
            Data_View_Model.CartVM? currentCart = await cartDAO.GetCartVMByUid(currentUid);
            return Ok(currentCart);
        }
    }
}
