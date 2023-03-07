using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository;

namespace MOBY_API_Core6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyAddressController : ControllerBase
    {
        private IUserAddressRepository _userAddressRepository;

        public MyAddressController(IUserAddressRepository userAddressRepository)
        {
            _userAddressRepository = userAddressRepository;
        }

        [Authorize]
        [HttpPost("NewAddress")]
        public async Task<IActionResult> createNewAddress([FromBody] string location) 
        {
            try
            {
                int userID = int.Parse(this.User.Claims.First(i => i.Type == "user_id").Value);
                MyAddressVM myAddressVM = new MyAddressVM(location, userID);
                bool check = await _userAddressRepository.createNewAddress(myAddressVM);
                if (check)
                {
                    return Ok(ReturnMessage.create("đã thêm địa chỉ mới"));
                }
                else
                {
#pragma warning disable CS8604 // Possible null reference argument.
                    return BadRequest(ReturnMessage.create(UserAddressRepository.errorMessage));
#pragma warning restore CS8604 // Possible null reference argument.
                }
            }catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpGet("MyAddress")]
        public async Task<IActionResult> myListAddress()
        {
            try
            {
                int userID = int.Parse(this.User.Claims.First(i => i.Type == "user_id").Value);
                List<string> addresses = await _userAddressRepository.getMylistAddress(userID);
                if (addresses != null)
                {
                    return Ok(addresses);
                }
                else
                {
#pragma warning disable CS8604 // Possible null reference argument.
                    return BadRequest(ReturnMessage.create(UserAddressRepository.errorMessage));
#pragma warning restore CS8604 // Possible null reference argument.
                }
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("DeleteAddress")]
        public async Task<IActionResult> deleteNewAddress([FromBody] string location)
        {
            try
            {
                int userID = int.Parse(this.User.Claims.First(i => i.Type == "user_id").Value);
                MyAddressVM myAddressVM = new MyAddressVM(location, userID);
                bool check = await _userAddressRepository.deleteMyAddress(myAddressVM);
                if (check)
                {
                    return Ok(ReturnMessage.create("đã xóa địa chỉ"));
                }
                else
                {
#pragma warning disable CS8604 // Possible null reference argument.
                    return BadRequest(ReturnMessage.create(UserAddressRepository.errorMessage));
#pragma warning restore CS8604 // Possible null reference argument.
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
