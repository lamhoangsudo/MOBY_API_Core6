using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Controllers
{

    [ApiController]
    public class MyAddressController : ControllerBase
    {
        private readonly IUserAddressService userAddressService;
        private readonly IUserService userService;
        public MyAddressController(IUserAddressService userAddressService, IUserService userService)
        {
            this.userAddressService = userAddressService;
            this.userService = userService;
        }

        [Authorize]
        [HttpPost("api/useraddress")]
        public async Task<IActionResult> CreateNewAddress([FromBody] CreateMyAddressVM createMyAddressVM)
        {
            try
            {
                int uid = await userService.GetUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (uid == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                if (uid == -1)
                {
                    return BadRequest(ReturnMessage.Create("Account not found"));
                }
                if (await userAddressService.CreateNewAddress(createMyAddressVM, uid))
                {
                    return Ok(ReturnMessage.Create("success"));
                }
                else
                {
                    return BadRequest(ReturnMessage.Create("error at createNewAddress"));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpGet("api/useraddress")]
        public async Task<IActionResult> MyListAddress()
        {
            try
            {
                int uid = await userService.GetUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (uid == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                if (uid == -1)
                {
                    return BadRequest(ReturnMessage.Create("Account not found"));
                }
                List<MyAddressVM>? addresses = await userAddressService.GetMylistAddress(uid);

                return Ok(addresses);


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpPatch("api/useraddress")]
        public async Task<IActionResult> UpdateAddress([FromBody] UpdateMyAddressVM updateMyAddressVM)
        {
            try
            {
                int uid = await userService.GetUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (uid == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                if (uid == -1)
                {
                    return BadRequest(ReturnMessage.Create("Account not found"));
                }
                UserAddress? currentUserAddress = await userAddressService.FindUserAddressByUserAddressID(updateMyAddressVM.UserAddressID, uid);
                if (currentUserAddress != null)
                {
                    if (await userAddressService.UpdateUserAddress(updateMyAddressVM, currentUserAddress))
                    {
                        return Ok(ReturnMessage.Create("success"));
                    }
                    return BadRequest(ReturnMessage.Create("error at UpdateAddress"));
                }
                return BadRequest(ReturnMessage.Create("UserAddress not found"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("api/useraddress")]
        public async Task<IActionResult> DeleteNewAddress([FromQuery] int id)
        {
            try
            {
                int uid = await userService.GetUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (uid == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                if (uid == -1)
                {
                    return BadRequest(ReturnMessage.Create("Account not found"));
                }
                MyAddressIdVM myAddressIdVM = new()
                {
                    UserAddressID = uid
                };
                UserAddress? currentUserAddress = await userAddressService.FindUserAddressByUserAddressID(myAddressIdVM.UserAddressID, uid);
                if (currentUserAddress != null)
                {
                    if (await userAddressService.DeleteMyAddress(currentUserAddress))
                    {
                        return Ok(ReturnMessage.Create("success"));
                    }
                    return BadRequest(ReturnMessage.Create("error at deleteNewAddress"));
                }
                return BadRequest(ReturnMessage.Create("UserAddress not found"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
