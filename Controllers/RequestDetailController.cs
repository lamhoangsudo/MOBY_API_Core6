using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository;

namespace MOBY_API_Core6.Controllers
{

    [ApiController]
    public class RequestDetailController : ControllerBase
    {
        private readonly IUserRepository userDAO;
        private readonly IRequestRepository requestDAO;
        private readonly IRequestDetailRepository requestDetailDAO;
        private readonly IItemRepository itemDAO;
        public RequestDetailController(IRequestDetailRepository cartDetailDAO, IRequestRepository cartDAO, IUserRepository userDAO, IItemRepository itemDAO)
        {
            this.userDAO = userDAO;
            this.requestDetailDAO = cartDetailDAO;
            this.requestDAO = cartDAO;
            this.itemDAO = itemDAO;
        }
        /*        [Authorize]
                [HttpGet]
                [Route("api/useraccount/request/requestdetail")]
                public async Task<IActionResult> GetAllRequestDetail()
                {
                    try
                    {
                        List<RequestDetailVM> listCartDetail = new List<RequestDetailVM>();
                        int uid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                        RequestVM? request = await requestDAO.GetRequestByUid(uid);
                        if (request != null)
                        {
                            listCartDetail = await requestDetailDAO.GetAllRequestDetail(request.RequestId);
                        } //clean architecture - 
                        return Ok(listCartDetail);
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                    }

                }*/

        [Authorize]
        [HttpGet]
        [Route("api/useraccount/item/requestdetail")]
        public async Task<IActionResult> GetAllRequestDetailByItem()
        {
            try
            {
                int uid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                List<int> itemIDList = await itemDAO.getListItemIDByUserID(uid);
                List<RequestDetailVM> result = new List<RequestDetailVM>();
                foreach (int itemID in itemIDList)
                {
                    List<RequestDetailVM> requestDetailOf1ItemList = await requestDetailDAO.getRequestDetailByItemID(itemID);
                    foreach (RequestDetailVM requestDetailVM in requestDetailOf1ItemList)
                    {
                        result.Add(requestDetailVM);
                    }
                }

                //for item in listItem 
                //requestdetail nao co item == item nguoi ban



                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        /*[Authorize]
        [HttpGet]
        [Route("api/useraccount/item/cartdetail")]
        public async Task<IActionResult> GetRequestDetailOfItemForOwner()
        {
            int uid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
            List<BriefItem> listItemByUserId = await itemDAO.get(uid, true, true);
            List<CartDetailVM> listCartDetailResult = new List<CartDetailVM>();
            List<CartDetailVM> listcartDetailVMByItemID = new List<CartDetailVM>();
            foreach (BriefItem item in listItemByUserId)
            {
                listcartDetailVMByItemID = await cartDetailDAO.GetCartDetailByItemID(item.ItemId);
                foreach (CartDetailVM vm in listcartDetailVMByItemID)
                {
                    listCartDetailResult.Add(vm);
                }
            }

            return Ok(listCartDetailResult);

        }*/

        [Authorize]
        [HttpPost]
        [Route("api/requestdetail/create")]
        public async Task<IActionResult> CreateRequestDetail([FromBody] CreateRequestDetailVM createdRequestDetail)
        {

            try
            {
                if (await requestDetailDAO.CreateRequestDetail(createdRequestDetail))
                {
                    return Ok(ReturnMessage.create("Success"));
                }
                return BadRequest(ReturnMessage.create("error at CreateRequestDetail"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [Authorize]
        [HttpPut]
        [Route("api/requestdetail")]
        public async Task<IActionResult> UpdateRequestDetail([FromBody] UpdateRequestDetailVM updatedRequestDetail)
        {
            try
            {
                RequestDetail? requestDetail = await requestDetailDAO.GetRequestDetailByRequestDetailID(updatedRequestDetail.RequestDetailId);
                if (requestDetail != null)
                {

                    if (await requestDetailDAO.UpdateRequestDetail(requestDetail, updatedRequestDetail.ItemQuantity))
                    {
                        return Ok(ReturnMessage.create("Success"));
                    }
                }
                return BadRequest(ReturnMessage.create("error at UpdateRequestDetail"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("api/requestdetail")]
        public async Task<IActionResult> DeleteRequestDetail([FromBody] RequestDetailIdVM requestDetailid)
        {
            try
            {
                RequestDetail? requestDetail = await requestDetailDAO.GetRequestDetailByRequestDetailID(requestDetailid.RequestDetailId);
                if (requestDetail != null)
                {
                    if (await requestDetailDAO.DeleteRequestDetail(requestDetail))
                    {
                        return Ok(ReturnMessage.create("Success"));
                    }
                }
                return BadRequest(ReturnMessage.create("error at DeleteRequestDetail"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [Authorize]
        [HttpPatch]
        [Route("api/requestdetail/accept")]
        public async Task<IActionResult> AcceptRequestDetail([FromBody] RequestDetailIdVM requestDetailid)
        {

            try
            {
                if (await requestDetailDAO.AcceptRequestDetail(requestDetailid))
                {
                    RequestDetail? foundRequestDetail = await requestDetailDAO
                        .GetRequestDetailByRequestDetailID(requestDetailid.RequestDetailId);

                    if (foundRequestDetail != null)
                    {
                        int itemQuantity = await itemDAO.getQuantityByItemID(foundRequestDetail.ItemId);
                        List<RequestDetailVM> otherRequestDetail = await requestDetailDAO
                        .getRequestDetailByItemID(foundRequestDetail.ItemId);
                        foreach (RequestDetailVM RequestDetail in otherRequestDetail)
                        {
                            if (RequestDetail.ItemQuantity > itemQuantity)
                            {
                                await requestDetailDAO.DenyRequestDetail(RequestDetail.RequestDetailId);
                            }
                        }


                        return Ok(ReturnMessage.create("Success"));
                    }

                }
                return BadRequest(ReturnMessage.create("error at AcceptRequestDetail"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpPatch]
        [Route("api/requestdetail/deny")]
        public async Task<IActionResult> DenyRequestDetail([FromBody] RequestDetailIdVM CartDetailID)
        {
            try
            {
                if (await requestDetailDAO.DenyRequestDetail(CartDetailID.RequestDetailId))
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

        [Authorize]
        [HttpPatch]
        [Route("api/requestdetail/confirm")]
        public async Task<IActionResult> ConfirmRequestDetail([FromBody] ListRequestDetailID listRequestDetailID)
        {

            try
            {
                if (await requestDetailDAO.ConfirmRequestDetail(listRequestDetailID))
                {
                    return Ok(ReturnMessage.create("Success"));
                }
                return BadRequest(ReturnMessage.create("error at ConfirmRequestDetail"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
