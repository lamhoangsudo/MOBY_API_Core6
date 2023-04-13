using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;

namespace MOBY_API_Core6.Controllers
{

    [ApiController]
    public class RequestDetailController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        private readonly IRequestRepository requestRepository;
        private readonly IRequestDetailRepository requestDetailRepository;
        public RequestDetailController(IUserRepository userRepository, IRequestRepository requestRepository, IRequestDetailRepository requestDetailRepository)
        {

            this.userRepository = userRepository;

            this.requestRepository = requestRepository;
            this.requestDetailRepository = requestDetailRepository;
        }
        [Authorize]
        [HttpGet]
        [Route("api/useraccount/item/requestdetail/reciever")]
        public async Task<IActionResult> GetAllRequestDetailByRecieverID([FromQuery] RequestStatusVM requestStatus)
        {
            try
            {
                int uid = await userRepository.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (uid == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                List<RequestDetailVM> requestDetailOf1ItemList = await requestDetailRepository.getRequestDetailByRecieverID(uid, requestStatus.requestStatus);

                return Ok(requestDetailOf1ItemList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }



    }
}
