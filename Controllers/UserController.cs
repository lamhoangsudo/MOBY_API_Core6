using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository;

namespace MOBY_API_Core6.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userDAO;
        public UserController(IUserRepository userDao)
        {
            this.userDAO = userDao;
        }
        [Authorize]
        [HttpPost]
        [Route("api/useraccount/create")]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountVM createUserVM)

        {
            try
            {
                if (await userDAO.CreateUser(this.User.Claims, createUserVM))
                {
                    return Ok(ReturnMessage.create("success"));
                }
            }
            catch
            {

            }

            return BadRequest(ReturnMessage.create("error at CreateAccount"));
        }

        [Authorize]
        [HttpPut]
        [Route("api/useraccount")]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UpdateAccountVM accountVM)
        {
            //UserAccounts currentUser = new UserAccounts();
            UserAccount currentUser = await userDAO.FindUserByCode(this.User.Claims.First(i => i.Type == "user_id").Value);

            try
            {
                if (await userDAO.EditUser(currentUser, accountVM))
                {
                    return Ok(ReturnMessage.create("success"));
                }
            }
            catch (Exception e)
            {

            }

            return BadRequest(ReturnMessage.create("error at UpdateUserProfile"));
        }

        [Authorize]
        [HttpPatch]
        [Route("api/useraccount/ban")]
        public async Task<IActionResult> BanUser([FromBody] UserUidVM uid)
        {
            //UserAccounts currentUser = new UserAccounts();
            // UserAccount currentUser = await userDAO.FindUserByID(this.User.Claims.First(i => i.Type == "user_id").Value);

            if (await userDAO.BanUser(uid))
            {
                return Ok(ReturnMessage.create("success"));
            }

            return BadRequest(ReturnMessage.create("error at BanUser"));
        }

        [Authorize]
        [HttpPatch]
        [Route("api/useraccount/unban")]
        public async Task<IActionResult> UnBanUser([FromBody] UserUidVM uid)
        {
            //UserAccounts currentUser = new UserAccounts();
            //UserAccount currentUser = await userDAO.FindUserByID(this.User.Claims.First(i => i.Type == "user_id").Value);
            if (await userDAO.BanUser(uid))
            {
                return Ok(ReturnMessage.create("success"));
            }

            return BadRequest(ReturnMessage.create("error at UnBanUser"));
        }
        [Authorize]
        [HttpGet]
        [Route("api/useraccount/all")]
        public async Task<IActionResult> GetAllUser()
        {
            List<UserVM> list = new List<UserVM>();
            list = await userDAO.GetAllUser();

            //UserAccounts currentUser = new UserAccounts();
            //UserAccount currentUser = await userDAO.FindUserByID(this.User.Claims.First(i => i.Type == "user_id").Value);


            return Ok(list); ;
        }

        [Authorize]
        [HttpGet]
        [Route("api/useraccount/token")]
        public async Task<IActionResult> GetUserInfo()
        {
            UserAccount currentUser = await userDAO.FindUserByCode(this.User.Claims.First(i => i.Type == "user_id").Value);



            if (currentUser == null)
            {
                return NotFound(ReturnMessage.create("account not found"));
            }


            return Ok(currentUser);
        }

        [Authorize]
        [HttpGet]
        [Route("api/useraccount")]
        public async Task<IActionResult> GetUserInfoByQuery([FromQuery] UserUidVM uid)
        {
            UserAccount currentUser = await userDAO.FindUserByUid(uid.UserId);

            return Ok(currentUser);
        }
    }
}
