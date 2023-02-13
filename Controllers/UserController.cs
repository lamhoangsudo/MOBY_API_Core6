using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository;

namespace MOBY_API_Core6.Controllers
{
    [Route("api/[controller]")]
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
        [Route("api/UserController/CreateAccount/{Adress} {phone} {sex} {dateOfBirth}")]
        public async Task<IActionResult> PostGoogleAuthenticationCreated(String address, String phone, bool sex, String dateOfBirth)
        {
            try
            {
                if (await userDAO.CreateUser(this.User.Claims, address, phone, sex, dateOfBirth))
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
        [HttpPost]
        [Route("api/UserController/UpdateAccount/{userName} {picture} {Adress} {phone} {sex} {dateOfBirth} {User_More_Information}")]
        public async Task<IActionResult> UpdateUserProfile(String userName, String picture, String address, String phone, bool sex, String dateOfBirth, String User_More_Information)
        {
            //UserAccounts currentUser = new UserAccounts();
            UserAccount currentUser = await userDAO.FindUserByCode(this.User.Claims.First(i => i.Type == "user_id").Value);

            try
            {
                if (await userDAO.EditUser(currentUser, userName, picture, address, phone, sex, dateOfBirth, User_More_Information))
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
        [HttpPost]
        [Route("api/UserController/BanAccount/{UserID}")]
        public async Task<IActionResult> BanUser(String uid)
        {
            //UserAccounts currentUser = new UserAccounts();
            // UserAccount currentUser = await userDAO.FindUserByID(this.User.Claims.First(i => i.Type == "user_id").Value);

            if (await userDAO.BanUser(uid))
            {
                return Ok(ReturnMessage.create("success"));
            }

            return BadRequest(ReturnMessage.create("error at UpdateUserProfile"));
        }

        [Authorize]
        [HttpPost]
        [Route("api/UserController/UnBanAccount/{UserID}")]
        public async Task<String> UnBanUser(String uid)
        {
            //UserAccounts currentUser = new UserAccounts();
            //UserAccount currentUser = await userDAO.FindUserByID(this.User.Claims.First(i => i.Type == "user_id").Value);
            if (await userDAO.BanUser(uid))
            {

            }

            return;
        }
        [Authorize]
        [HttpGet]
        [Route("api/UserController/GetAllUser")]
        public async Task<List<UserAccount>> GetAllUser(String uid)
        {
            List<UserAccount> list = new List<UserAccount>();
            list = await userDAO.GetAllUser();

            //UserAccounts currentUser = new UserAccounts();
            //UserAccount currentUser = await userDAO.FindUserByID(this.User.Claims.First(i => i.Type == "user_id").Value);


            return list;
        }

        [Authorize]
        [HttpGet]
        [Route("api/UserController/GetUserInfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            UserAccount currentUser = await userDAO.FindUserByCode(this.User.Claims.First(i => i.Type == "user_id").Value);



            if (currentUser == null)
            {
                return NotFound(ReturnMessage.create("account not found"));
            }


            return Ok(currentUser);
        }
    }
}
