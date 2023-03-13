using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository;

namespace MOBY_API_Core6.Controllers
{

    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IUserRepository userDAO;
        private readonly IRequestRepository cartDAO;
        public RequestController(IUserRepository userDao, IRequestRepository cartDAO)
        {
            this.userDAO = userDao;
            this.cartDAO = cartDAO;
        }
        [Authorize]
        [HttpPost]
        [Route("api/request/create")]
        public async Task<IActionResult> CreateRequest()
        {
            var currentUid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
            if (await cartDAO.CheackExistedRequestByUid(currentUid))
            {
                return Ok(ReturnMessage.create("this user already has a cart"));
            }
            if (await cartDAO.CreateRequest(currentUid))
            {
                return Ok(ReturnMessage.create("success"));
            }
            return Ok(ReturnMessage.create("error at CreateRequest"));
        }

        [Authorize]
        [HttpGet]
        [Route("api/useraccount/request")]
        public async Task<IActionResult> GetRequestByUid()
        {
            int currentUid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
            RequestVM? currentCart = await cartDAO.GetRequestByUid(currentUid);
            return Ok(currentCart);
        }
    }
}
