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
        [Route("api/UserController/CreateAccount")]
        public async Task<String> PostGoogleAuthenticationCreated(String address, String phone, bool sex, String dateOfBirth)
        {
            try
            {
                if (await userDAO.CreateUser(this.User.Claims, address, phone, sex, dateOfBirth))
                {
                    return "add new user";
                }
            }
            catch
            {
                return "Error while create new account";
            }

            return "user already existed";
        }

        [Authorize]
        [HttpPost]
        [Route("api/UserController/UpdateAccount")]
        public async Task<String> UpdateUserProfile(String userName, String picture, String address, String phone, bool sex, String dateOfBirth, String User_More_Information)
        {
            //UserAccounts currentUser = new UserAccounts();
            UserAccount currentUser = await userDAO.FindUserByCode(this.User.Claims.First(i => i.Type == "user_id").Value);

            try
            {
                if (await userDAO.EditUser(currentUser, userName, picture, address, phone, sex, dateOfBirth, User_More_Information))
                {
                    return "success";
                }
            }
            catch (Exception e)
            {
                return "lol";
            }

            return "erro at edit user";
        }

        [Authorize]
        [HttpPost]
        [Route("api/UserController/BanAccount")]
        public async Task<String> BanUser(String uid)
        {
            //UserAccounts currentUser = new UserAccounts();
            // UserAccount currentUser = await userDAO.FindUserByID(this.User.Claims.First(i => i.Type == "user_id").Value);


            return await userDAO.BanUser(uid);
        }

        [Authorize]
        [HttpPost]
        [Route("api/UserController/UnBanAccount")]
        public async Task<String> UnBanUser(String uid)
        {
            //UserAccounts currentUser = new UserAccounts();
            //UserAccount currentUser = await userDAO.FindUserByID(this.User.Claims.First(i => i.Type == "user_id").Value);


            return await userDAO.BanUser(uid);
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
    }
}
