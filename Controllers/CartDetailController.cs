using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository;

namespace MOBY_API_Core6.Controllers
{

    [ApiController]
    public class CartDetailController : ControllerBase
    {
        private readonly IUserRepository userDAO;
        private readonly ICartRepository cartDAO;
        private readonly ICartDetailRepository cartDetailDAO;
        private readonly IItemRepository itemDAO;
        public CartDetailController(ICartDetailRepository cartDetailDAO, ICartRepository cartDAO, IUserRepository userDAO, IItemRepository itemDAO)
        {
            this.userDAO = userDAO;
            this.cartDetailDAO = cartDetailDAO;
            this.cartDAO = cartDAO;
            this.itemDAO = itemDAO;
        }

        [Authorize]
        [HttpGet]
        [Route("api/cartdetail")]
        public async Task<IActionResult> GetListCartDetailByListCartDetailid([FromBody] ListCartDetailID listCartDetailid)
        {

            if (listCartDetailid.listCartDetailID == null || listCartDetailid.listCartDetailID.Count == 0)
            {
                return BadRequest(ReturnMessage.create("there no CartDetailid"));
            }
            try
            {
                List<CartDetailVM> result = await cartDetailDAO.GetListCartDetailByListID(listCartDetailid);
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest(ReturnMessage.create("error at GetListCartDetailByListCartDetailid"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/cartdetail/create")]
        public async Task<IActionResult> CreateCartDetail([FromBody] CreateCartDetailVM createdRequestDetail)
        {

            try
            {
                if (await cartDetailDAO.CheclExistCartDetail(createdRequestDetail))
                {
                    return Ok(ReturnMessage.create("Success"));
                }
                if (await cartDetailDAO.CreateCartDetail(createdRequestDetail))
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
        [Route("api/cartdetail")]
        public async Task<IActionResult> UpdateCartDetail([FromBody] UpdateCartDetailVM updatedCartDetail)
        {
            try
            {

                CartDetail? currentCartDetail = await cartDetailDAO.GetCartDetailByCartDetailID(updatedCartDetail.CartDetailId);
                if (currentCartDetail != null)
                {
                    if (updatedCartDetail.ItemQuantity <= 0)
                    {
                        if (await cartDetailDAO.DeleteCartDetail(currentCartDetail))
                        {
                            return Ok(ReturnMessage.create("Success"));
                        }
                    }
                    String result = await cartDetailDAO.UpdateCartDetail(currentCartDetail, updatedCartDetail);
                    if (result.Equals("success"))
                    {
                        return Ok(ReturnMessage.create("Success"));
                    }
                    else if (result.Contains("item available ammout"))
                    {
                        return Ok(ReturnMessage.create(result));
                    }
                    else
                    {
                        return BadRequest(ReturnMessage.create("error at UpdateRequestDetail"));
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
        [Route("api/cartdetail")]
        public async Task<IActionResult> DeleteCartDetail([FromQuery] CartDetailIdVM cartDetailid)
        {
            try
            {
                CartDetail? requestDetail = await cartDetailDAO.GetCartDetailByCartDetailID(cartDetailid.CartDetailid);
                if (requestDetail != null)
                {
                    if (await cartDetailDAO.DeleteCartDetail(requestDetail))
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
        [Route("api/cartdetail/confirm")]
        public async Task<IActionResult> ConfirmCartDetail([FromBody] ListCartDetailidToConfirm listCartDetailID)
        {

            try
            {
                var currentUid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (await cartDetailDAO.ConfirmCartDetail(listCartDetailID, currentUid))
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
