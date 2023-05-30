using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Log4Net;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Controllers
{

    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IUserService userDAO;
        private readonly ICartService cartDAO;
        private readonly Logger4Net _logger4Net;
        public CartController(IUserService userDao, ICartService cartDAO)
        {
            this.userDAO = userDao;
            this.cartDAO = cartDAO;
            _logger4Net = new Logger4Net();
        }
        [Authorize]
        [HttpPost]
        [Route("api/cart/create")]
        public async Task<IActionResult> CreateCart()
        {
            try
            {
                var currentUid = await userDAO.GetUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
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
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpPatch]
        [Route("api/cart")]
        public async Task<IActionResult> UpdateCart([FromBody] UpdateCartVM updatedCart)
        {

            try
            {
                var currentUid = await userDAO.GetUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
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
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/useraccount/cart")]
        public async Task<IActionResult> GetCartByUid()
        {

            try
            {
                int currentUid = await userDAO.GetUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
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
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
