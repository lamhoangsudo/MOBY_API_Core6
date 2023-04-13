using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;

namespace MOBY_API_Core6.Controllers
{

    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IRequestRepository requestRepository;
        private readonly IRequestDetailRepository requestDetailRepository;
        public RequestController(IUserRepository userRepository, IRequestRepository requestRepository, IOrderRepository orderRepository, IRequestDetailRepository requestDetailRepository)
        {

            this.userRepository = userRepository;
            this.orderRepository = orderRepository;
            this.requestRepository = requestRepository;
            this.requestDetailRepository = requestDetailRepository;
        }

        [Authorize]
        [HttpGet]
        [Route("api/useraccount/item/request/sharer")]
        public async Task<IActionResult> GetAllRequestBySharerID([FromQuery] RequestStatusVM requestStatus)
        {
            try
            {

                int uid = await userRepository.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (uid == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                List<RequestVM> requestDetailOf1ItemList = await requestRepository.getRequestBySharerID(uid, requestStatus.requestStatus);
                /*List<int> itemIDList = await itemDAO.getListItemIDByUserID(uid);
                List<RequestVM> result = new List<RequestVM>();
                foreach (int itemID in itemIDList)
                {


                    result.AddRange(requestDetailOf1ItemList);
                }*/


                return Ok(requestDetailOf1ItemList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [Authorize]
        [HttpGet]
        [Route("api/useraccount/request/reciever")]
        public async Task<IActionResult> GetAllRequestByUserid([FromQuery] RequestStatusVM requestStatus)
        {
            try
            {
                int uid = await userRepository.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (uid == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }

                List<RequestVM> ListRequest = await requestRepository.getRequestByRecieverID(uid, requestStatus.requestStatus);

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


                RequestVM? result = await requestRepository.getRequestVMByRequestID(requestIDVM.RequestId);
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
        [HttpPost]
        [Route("api/request/confirm")]
        public async Task<IActionResult> ConfirmRequest([FromBody] RequestConfirmVM requestConfirmVM)
        {

            try
            {

                Request? foundRequest = await requestRepository.getRequestByRequestID(requestConfirmVM.RequestID);
                if (foundRequest == null)
                {
                    return BadRequest(ReturnMessage.Create("error at request not found"));
                }

                List<RequestDetail> requestDetailList = foundRequest.RequestDetails.ToList();
                List<RequestDetail> acceptedRequestDetail = new List<RequestDetail>();

                foreach (RequestDetail requestDetail in requestDetailList)
                {
                    if (requestConfirmVM.ListRequestDetailID.Contains(requestDetail.RequestDetailId))
                    {
                        //accept requestDetail + autoCheckDeny
                        if (requestDetailRepository.AcceptRequestDetail(requestDetail))
                        {
                            await requestDetailRepository.DenyOtherRequestWhichPassItemQuantity(requestDetail);
                            acceptedRequestDetail.Add(requestDetail);
                        }
                        else
                        {
                            return BadRequest(ReturnMessage.Create("Item not enought for request Detail"));
                        }

                    }
                    else
                    {
                        //denyrequestDetail
                        requestDetailRepository.DenyRequestDetail(requestDetail);
                    }
                }
                foundRequest.DateChangeStatus = DateTime.Now;
                foundRequest.Status = 1;
                await requestRepository.SaveRequest();
                if (await orderRepository.CreateOrder(foundRequest.UserId, foundRequest.Address, foundRequest.Note, acceptedRequestDetail))
                {
                    return Ok(ReturnMessage.Create("success"));
                }
                return BadRequest(ReturnMessage.Create("error at ConfirmRequest"));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/request/deny")]
        public async Task<IActionResult> DenyRequest([FromBody] RequestIDVM requestIDVM)
        {

            try
            {

                Request? foundRequest = await requestRepository.getRequestByRequestID(requestIDVM.RequestId);
                if (foundRequest == null)
                {
                    return BadRequest(ReturnMessage.Create("error at request not found"));
                }
                if (await requestRepository.DenyRequest(foundRequest))
                {
                    return Ok(ReturnMessage.Create("success"));
                }
                return BadRequest(ReturnMessage.Create("error at DenyRequest"));


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
