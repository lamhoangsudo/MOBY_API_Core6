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
        private IUserAddressService userAddressDAO;
        private readonly IUserService userDAO;
        public MyAddressController(IUserAddressService userAddressDAO, IUserService userDAO)
        {
            this.userAddressDAO = userAddressDAO;
            this.userDAO = userDAO;
        }

        [Authorize]
        [HttpPost("api/useraddress")]
        public async Task<IActionResult> createNewAddress([FromBody] CreateMyAddressVM createMyAddressVM)
        {
            try
            {
                int uid = await userDAO.GetUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (uid == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                if (uid == -1)
                {
                    return BadRequest(ReturnMessage.Create("Account not found"));
                }

                if (await userAddressDAO.CheckExitedAddress(createMyAddressVM, uid))
                {
                    return BadRequest(ReturnMessage.Create("this address already existed"));
                }

                if (await userAddressDAO.CreateNewAddress(createMyAddressVM, uid))
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
        public async Task<IActionResult> myListAddress()
        {
            try
            {
                int uid = await userDAO.GetUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (uid == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                if (uid == -1)
                {
                    return BadRequest(ReturnMessage.Create("Account not found"));
                }
                List<MyAddressVM>? addresses = await userAddressDAO.GetMylistAddress(uid);

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
                int uid = await userDAO.GetUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (uid == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                if (uid == -1)
                {
                    return BadRequest(ReturnMessage.Create("Account not found"));
                }
                UserAddress? currentUserAddress = await userAddressDAO.FindUserAddressByUserAddressID(updateMyAddressVM.UserAddressID, uid);
                if (currentUserAddress != null)
                {
                    if (await userAddressDAO.UpdateUserAddress(updateMyAddressVM, currentUserAddress))
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
        public async Task<IActionResult> deleteNewAddress([FromQuery] MyAddressIdVM myAddressIdVM)
        {
            try
            {
                int uid = await userDAO.GetUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (uid == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                if (uid == -1)
                {
                    return BadRequest(ReturnMessage.Create("Account not found"));
                }
                UserAddress? currentUserAddress = await userAddressDAO.FindUserAddressByUserAddressID(myAddressIdVM.UserAddressID, uid);
                if (currentUserAddress != null)
                {
                    if (await userAddressDAO.DeleteMyAddress(currentUserAddress))
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
