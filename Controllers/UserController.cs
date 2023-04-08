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
        private readonly IEmailRepository emailDAO;
        public UserController(IUserRepository userDao, IEmailRepository emailDAO)
        {
            this.userDAO = userDao;
            this.emailDAO = emailDAO;
        }
        [Authorize]
        [HttpPost]
        [Route("api/useraccount/create")]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountVM createUserVM)

        {
            try
            {
                if (!(await userDAO.CheckExistedUser(this.User.Claims.First(i => i.Type == "user_id").Value)))
                {
                    if (await userDAO.CreateUser(this.User.Claims, createUserVM))
                    {
                        return Ok(ReturnMessage.Create("success"));
                        //int uid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                        /*if (await cartDAO.CreateCart(uid))
                        {
                            
                        }*/

                    }
                }
                else
                {
                    return BadRequest(ReturnMessage.Create("this user already exist"));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return BadRequest(ReturnMessage.Create("error at CreateAccount"));
        }

        [Authorize]
        [HttpPut]
        [Route("api/useraccount")]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UpdateAccountVM accountVM)
        {
            try
            {
                //UserAccounts currentUser = new UserAccounts();
                UserAccount? currentUser = await userDAO.FindUserByCode(this.User.Claims.First(i => i.Type == "user_id").Value);

                if (currentUser != null)
                {
                    if (await userDAO.EditUser(currentUser, accountVM))
                    {
                        return Ok(ReturnMessage.Create("success"));
                    }
                    return BadRequest(ReturnMessage.Create("error at updateUserAccount"));
                }

                return BadRequest(ReturnMessage.Create("No User Found"));


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //[Authorize]
        [HttpPatch]
        [Route("api/useraccount/ban")]
        public async Task<IActionResult> BanUser([FromBody] UserUidVM uid)
        {
            //UserAccounts currentUser = new UserAccounts();
            // UserAccount currentUser = await userDAO.FindUserByID(this.User.Claims.First(i => i.Type == "user_id").Value);

            try
            {
                if (await userDAO.BanUser(uid))
                {
                    return Ok(ReturnMessage.Create("success"));
                }
                return BadRequest(ReturnMessage.Create("error at BanUser"));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //[Authorize]
        [HttpPatch]
        [Route("api/useraccount/unban")]
        public async Task<IActionResult> UnBanUser([FromBody] UserUidVM uid)
        {
            //UserAccounts currentUser = new UserAccounts();
            //UserAccount currentUser = await userDAO.FindUserByID(this.User.Claims.First(i => i.Type == "user_id").Value);
            try
            {
                if (await userDAO.UnbanUser(uid))
                {
                    return Ok(ReturnMessage.Create("success"));
                }
                return BadRequest(ReturnMessage.Create("error at UnbanUser"));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [Authorize]
        [HttpGet]
        [Route("api/useraccount/all")]
        public async Task<IActionResult> GetAllUser([FromQuery] PaggingVM pagging, [FromQuery] UserAccountFilterVM userAccountFilterVM)
        {
            try
            {
                if (userAccountFilterVM.UserName == null)
                {
                    userAccountFilterVM.UserName = "";
                }
                if (userAccountFilterVM.UserGmail == null)
                {
                    userAccountFilterVM.UserGmail = "";
                }
                List<UserVM> listUser = await userDAO.GetAllUser(pagging, userAccountFilterVM);
                int totalUser = await userDAO.GetAllUserCount();
                PaggingReturnVM<UserVM> result = new PaggingReturnVM<UserVM>(listUser, pagging, totalUser);
                //UserAccounts currentUser = new UserAccounts();
                //UserAccount currentUser = await userDAO.FindUserByID(this.User.Claims.First(i => i.Type == "user_id").Value);


                return Ok(result); ;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/useraccount/token")]
        public async Task<IActionResult> GetUserInfo()
        {
            try
            {
                UserAccount? currentUser = await userDAO.FindUserByCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (currentUser == null)
                {
                    return NotFound(ReturnMessage.Create("account not found"));
                }
                Cart? currentcart = currentUser.Carts.FirstOrDefault();
                if (currentcart == null)
                {
                    return NotFound(ReturnMessage.Create("account not found cart"));
                }
                var userAccountVM = UserAccountVM.UserAccountToVewModel(currentUser, currentcart.CartId);


                //return Ok(UserAccountVM.UserAccountToVewModel(currentUser, cart.CartId));
                return Ok(userAccountVM);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("api/useraccount")]
        public async Task<IActionResult> GetUserInfoByQuery([FromQuery] UserUidVM uid)
        {
            try
            {
                UserAccount? currentUser = await userDAO.FindUserByUidWithoutStatus(uid.UserId);
                if (currentUser == null)
                {
                    return BadRequest(ReturnMessage.Create("account not found"));
                }
                Cart? currentcart = currentUser.Carts.FirstOrDefault();
                if (currentcart == null)
                {
                    return NotFound(ReturnMessage.Create("account not found cart"));
                }
                var userAccountVM = UserAccountVM.UserAccountToVewModel(currentUser, currentcart.CartId);

                return Ok(userAccountVM);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
