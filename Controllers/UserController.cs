﻿using Microsoft.AspNetCore.Authorization;
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
        private readonly ICartRepository cartDAO;
        public UserController(IUserRepository userDao, ICartRepository cartDAO)
        {
            this.userDAO = userDao;
            this.cartDAO = cartDAO;
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
                        return Ok(ReturnMessage.create("success"));
                        //int uid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                        /*if (await cartDAO.CreateCart(uid))
                        {
                            
                        }*/

                    }
                }
                else
                {
                    return BadRequest(ReturnMessage.create("this user already exist"));
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
            try
            {
                //UserAccounts currentUser = new UserAccounts();
                UserAccount? currentUser = await userDAO.FindUserByCode(this.User.Claims.First(i => i.Type == "user_id").Value);

                if (currentUser != null)
                {
                    if (await userDAO.EditUser(currentUser, accountVM))
                    {
                        return Ok(ReturnMessage.create("success"));
                    }
                }

                return BadRequest(ReturnMessage.create("No User Found"));


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ReturnMessage.create("error at UpdateUserProfile"));

            }



        }

        [Authorize]
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
                    return Ok(ReturnMessage.create("success"));
                }
                return BadRequest(ReturnMessage.create("error at BanUser"));

            }
            catch
            {
                return BadRequest(ReturnMessage.create("error at BanUser"));
            }
        }

        [Authorize]
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
                    return Ok(ReturnMessage.create("success"));
                }
                return BadRequest(ReturnMessage.create("error at UnbanUser"));

            }
            catch
            {
                return BadRequest(ReturnMessage.create("error at UnbanUser"));
            }
        }
        [Authorize]
        [HttpGet]
        [Route("api/useraccount/all")]
        public async Task<IActionResult> GetAllUser([FromQuery] PaggingVM pagging)
        {
            try
            {
                List<UserVM> list = await userDAO.GetAllUser(pagging);

                //UserAccounts currentUser = new UserAccounts();
                //UserAccount currentUser = await userDAO.FindUserByID(this.User.Claims.First(i => i.Type == "user_id").Value);


                return Ok(list); ;
            }
            catch
            {
                return BadRequest(ReturnMessage.create("error at GetAllUser"));
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
                    return NotFound(ReturnMessage.create("account not found"));
                }
                Cart? cart = currentUser.Carts.FirstOrDefault();




                //return Ok(UserAccountVM.UserAccountToVewModel(currentUser, cart.CartId));
                return Ok(currentUser);
            }
            catch
            {
                return BadRequest(ReturnMessage.create("error at GetUserInfo"));
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/useraccount")]
        public async Task<IActionResult> GetUserInfoByQuery([FromQuery] UserUidVM uid)
        {
            try
            {
                UserAccount? currentUser = await userDAO.FindUserByUid(uid.UserId);
                if (currentUser == null)
                {
                    return BadRequest(ReturnMessage.create("account not found"));
                }
                return Ok(UserAccountVM.UserAccountToVewModel(currentUser));

            }
            catch
            {
                return BadRequest(ReturnMessage.create("error at GetUserInfoByQuery"));
            }
        }
    }
}
