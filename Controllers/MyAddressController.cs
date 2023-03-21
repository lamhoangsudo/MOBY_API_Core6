using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository;

namespace MOBY_API_Core6.Controllers
{

    [ApiController]
    public class MyAddressController : ControllerBase
    {
        private IUserAddressRepository userAddressDAO;
        private readonly IUserRepository userDAO;
        public MyAddressController(IUserAddressRepository userAddressDAO, IUserRepository userDAO)
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
                int uid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);

                if (await userAddressDAO.CheckExitedAddress(createMyAddressVM, uid))
                {
                    return BadRequest(ReturnMessage.create("this address already existed"));
                }

                if (await userAddressDAO.createNewAddress(createMyAddressVM, uid))
                {
                    return Ok(ReturnMessage.create("success"));
                }
                else
                {

                    return BadRequest(ReturnMessage.create("error at createNewAddress"));

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
                int uid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                List<MyAddressVM>? addresses = await userAddressDAO.getMylistAddress(uid);

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
                int uid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                UserAddress? currentUserAddress = await userAddressDAO.FindUserAddressByUserAddressID(updateMyAddressVM.userAddressID, uid);
                if (currentUserAddress != null)
                {
                    if (await userAddressDAO.UpdateUserAddress(updateMyAddressVM, currentUserAddress))
                    {
                        return Ok(ReturnMessage.create("success"));
                    }
                    return BadRequest(ReturnMessage.create("error at UpdateAddress"));
                }
                return BadRequest(ReturnMessage.create("UserAddress not found"));
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
                int uid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                UserAddress? currentUserAddress = await userAddressDAO.FindUserAddressByUserAddressID(myAddressIdVM.userAddressID, uid);
                if (currentUserAddress != null)
                {
                    if (await userAddressDAO.deleteMyAddress(currentUserAddress))
                    {
                        return Ok(ReturnMessage.create("success"));
                    }
                    return BadRequest(ReturnMessage.create("error at deleteNewAddress"));
                }
                return BadRequest(ReturnMessage.create("UserAddress not found"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
