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
        [Authorize]
        [HttpGet]
        [Route("api/useraccount/request/requestdetail/all")]
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
                }
                return Ok(listCartDetail);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
        /*
        [Authorize]
        [HttpGet]
        [Route("api/useraccount/item/cartdetail")]
        public async Task<IActionResult> GetCartDetailOfItemForOwner()
        {
            int uid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
            List<BriefItem> listItemByUserId = await itemDAO.GetAllMyBriefItemAndBriefRequestActiveandUnActive(uid, true, true);
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

        }
        */
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
                RequestDetail? requestDetail = await requestDetailDAO.GetRequestDetailByRequestDetailID(requestDetailid.CartDetailId);
                if (requestDetail != null)
                {
                    if (await requestDetailDAO.DeleteRequestDetail(requestDetail))
                    {
                        return Ok(ReturnMessage.create("Success"));
                    }
                }
                return BadRequest(ReturnMessage.create("error at UpdateCartDetail"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /*
        [Authorize]
        [HttpPatch]
        [Route("api/cartdetail/accept")]
        public async Task<IActionResult> AcceptCartDetail([FromBody] CartDetailIdVM CartDetailID)
        {
            var cacrtDetail = await cartDetailDAO.GetCartDetailByCartDetailID(CartDetailID.CartDetailId);
            if (await cartDetailDAO.UpdateCartDetail(cacrtDetail, 2))
            {
                return Ok(ReturnMessage.create("Success"));
            }
            return BadRequest(ReturnMessage.create("error at AcceptCartDetail"));
        }

        [Authorize]
        [HttpPatch]
        [Route("api/cartdetail/cancel")]
        public async Task<IActionResult> CancelCartDetail([FromBody] CartDetailIdVM CartDetailID)
        {
            var cacrtDetail = await cartDetailDAO.GetCartDetailByCartDetailID(CartDetailID.CartDetailId);
            if (await cartDetailDAO.UpdateCartDetail(cacrtDetail, 3))
            {
                return Ok(ReturnMessage.create("Success"));
            }
            return BadRequest(ReturnMessage.create("error at CancelCartDetail"));

        }

        [Authorize]
        [HttpPatch]
        [Route("api/cartdetail/confirm")]
        public async Task<IActionResult> ConfirmCartDetail([FromBody] CartDetailIdVM CartDetailID)
        {
            var cacrtDetail = await cartDetailDAO.GetCartDetailByCartDetailID(CartDetailID.CartDetailId);
            if (await cartDetailDAO.UpdateCartDetail(cacrtDetail, 4))
            {
                return Ok(ReturnMessage.create("Success"));
            }
            return BadRequest(ReturnMessage.create("error at UpdateCartDetail"));
        }
        */
    }
}
