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
        private IUserAddressRepository _userAddressRepository;
        private readonly IUserRepository userDAO;
        public MyAddressController(IUserAddressRepository userAddressRepository, IUserRepository userDAO)
        {
            _userAddressRepository = userAddressRepository;
            this.userDAO = userDAO;
        }

        [Authorize]
        [HttpPost("api/useraddress")]
        public async Task<IActionResult> createNewAddress([FromBody] CreateMyAddressVM createMyAddressVM)
        {
            try
            {
                int uid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);

                if (await _userAddressRepository.CheckExitedAddress(createMyAddressVM, uid))
                {
                    return BadRequest(ReturnMessage.create("this address already existed"));
                }

                if (await _userAddressRepository.createNewAddress(createMyAddressVM, uid))
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
                List<MyAddressVM>? addresses = await _userAddressRepository.getMylistAddress(uid);
                if (addresses != null)
                {
                    return Ok(addresses);
                }
                else
                {

                    return BadRequest(ReturnMessage.create("error at myListAddress"));

                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("api/useraddress")]
        public async Task<IActionResult> deleteNewAddress([FromBody] CreateMyAddressVM createMyAddressVM)
        {
            try
            {
                int uid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);

                bool check = await _userAddressRepository.deleteMyAddress(createMyAddressVM, uid);
                if (check)
                {
                    return Ok(ReturnMessage.create("success"));
                }
                else
                {

                    return BadRequest(ReturnMessage.create("error at deleteNewAddress"));

                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
