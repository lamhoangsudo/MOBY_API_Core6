using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository;

namespace MOBY_API_Core6.Controllers
{

    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IUserRepository userDAO;
        private readonly ICartRepository cartDAO;
        private readonly ICartDetailRepository cartDetailDAO;
        private readonly IItemRepository itemDAO;
        private readonly IRequestRepository requestDAO;
        public RequestController(ICartDetailRepository cartDetailDAO, ICartRepository cartDAO, IUserRepository userDAO, IItemRepository itemDAO, IRequestRepository requestDAO)
        {
            this.userDAO = userDAO;
            this.cartDetailDAO = cartDetailDAO;
            this.cartDAO = cartDAO;
            this.itemDAO = itemDAO;
            this.requestDAO = requestDAO;
        }

        [Authorize]
        [HttpGet]
        [Route("api/useraccount/item/request/sharer")]
        public async Task<IActionResult> GetAllRequestByItem()
        {
            try
            {
                int uid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                List<int> itemIDList = await itemDAO.getListItemIDByUserID(uid);
                List<RequestVM> result = new List<RequestVM>();
                foreach (int itemID in itemIDList)
                {
                    List<RequestVM> requestDetailOf1ItemList = await requestDAO.getRequestByItemID(itemID);

                    result.AddRange(requestDetailOf1ItemList);
                }


                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [Authorize]
        [HttpGet]
        [Route("api/useraccount/request/reciever")]
        public async Task<IActionResult> GetAllRequestByUserid()
        {
            try
            {
                int uid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);


                List<RequestVM> ListRequest = await requestDAO.getRequestByUserID(uid);

                return Ok(ListRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [Authorize]
        [HttpGet]
        [Route("api/request")]
        public async Task<IActionResult> GetRequestByRequestID([FromQuery] RequestIDVM requestIDVM)
        {
            try
            {


                RequestVM? result = await requestDAO.getRequestVMByRequestID(requestIDVM.RequestId);
                if (result != null)
                {
                    Ok(result);
                }


                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [Authorize]
        [HttpPatch]
        [Route("api/requestdetail/accept")]
        public async Task<IActionResult> AcceptRequest([FromBody] RequestIDVM requestIDVM)
        {

            try
            {
                Request? foundRequestDetail = await requestDAO.getRequestByRequestID(requestIDVM.RequestId);
                if (foundRequestDetail == null)
                {
                    return BadRequest(ReturnMessage.create("error at request not found"));
                }
                if (await requestDAO.AcceptRequestDetail(foundRequestDetail))
                {
                    await requestDAO.DenyOtherRequestWhichPassItemQuantity(foundRequestDetail);
                    return Ok(ReturnMessage.create("Success"));
                }
                else
                {
                    return BadRequest(ReturnMessage.create("error at AcceptRequestDetail"));
                }


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpPatch]
        [Route("api/requestdetail/deny")]
        public async Task<IActionResult> DenyRequest([FromBody] RequestIDVM requestIDVM)
        {
            try
            {
                Request? foundRequestDetail = await requestDAO.getRequestByRequestID(requestIDVM.RequestId);
                if (foundRequestDetail == null)
                {
                    return BadRequest(ReturnMessage.create("error at request not found"));
                }
                if (await requestDAO.DenyRequestDetail(foundRequestDetail))
                {
                    return Ok(ReturnMessage.create("Success"));
                }
                return BadRequest(ReturnMessage.create("error at DenyRequestDetail"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}
