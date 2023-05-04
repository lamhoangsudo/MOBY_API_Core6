using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository;
using MOBY_API_Core6.Repository.IRepository;

namespace MOBY_API_Core6.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userDAO;

        private readonly ITransationRepository transationRepository;

        private readonly IBabyRepository babyRepository;
        public UserController(IUserRepository userDao, ITransationRepository transationRepository, IBabyRepository babyRepository)
        {
            this.userDAO = userDao;
            this.transationRepository = transationRepository;
            this.babyRepository = babyRepository;
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

        [Authorize]
        [HttpPut]
        [Route("api/useraccount/bank")]
        public async Task<IActionResult> UpdateUserBanking([FromBody] UpdateBankAccount bankVM)
        {
            try
            {
                //UserAccounts currentUser = new UserAccounts();
                UserAccount? currentUser = await userDAO.FindUserByCode(this.User.Claims.First(i => i.Type == "user_id").Value);

                if (currentUser != null)
                {
                    if (await userDAO.EditBankAccount(currentUser, bankVM))
                    {
                        return Ok(ReturnMessage.Create("success"));
                    }
                    return BadRequest(ReturnMessage.Create("error at update banking"));
                }

                return BadRequest(ReturnMessage.Create("No User Found"));


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/useraccount/balance")]
        public async Task<IActionResult> Withdraw([FromBody] WithdrawVM bankVM)
        {
            try
            {
                if (bankVM.Balance < 20000)
                {
                    return BadRequest(ReturnMessage.Create("khoản rút tối thiểu là 20.000vnd"));
                }
                //UserAccounts currentUser = new UserAccounts();
                UserAccount? currentUser = await userDAO.FindUserByCode(this.User.Claims.First(i => i.Type == "user_id").Value);

                if (currentUser != null)
                {

                    if (await transationRepository.CreateTransationLog(currentUser, bankVM.Balance))
                    {
                        return Ok(ReturnMessage.Create("success"));
                    }
                    return BadRequest(ReturnMessage.Create("error at update banking"));
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
                var userAccountVM = UserAccountSelfVM.UserAccountToVewModel(currentUser, currentcart.CartId);


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

        [Authorize]
        [HttpPost("api/baby")]
        public async Task<IActionResult> InputInformationBaby([FromBody] CreateBabyVM babyVM)
        {
            try
            {
                int userID = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (userID == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                babyVM.UserId = userID;
                bool checkInput = await babyRepository.InputInformationBaby(babyVM);
                if (checkInput)
                {
                    return Ok(ReturnMessage.Create("success"));
                }
                else
                {
                    return BadRequest(ReturnMessage.Create(BabyRepository.ErrorMessage));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpPut("api/baby/update")]
        public async Task<IActionResult> UpdateInformationBaby([FromBody] UpdateBabyVM babyVM)
        {
            try
            {
                bool checkInput = await babyRepository.UpdateInformationBaby(babyVM);
                if (checkInput)
                {
                    return Ok(ReturnMessage.Create("success"));
                }
                else
                {
                    return BadRequest(ReturnMessage.Create(BabyRepository.ErrorMessage));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpGet("api/useraccount/baby")]
        public async Task<IActionResult> GetBabyByUserID()
        {
            try
            {
                int userID = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (userID == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                List<Baby>? babies = await babyRepository.GetBabyByUserID(userID);
                if (babies != null)
                {
                    return Ok(babies);
                }
                else
                {
                    return BadRequest(ReturnMessage.Create(BabyRepository.ErrorMessage));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
