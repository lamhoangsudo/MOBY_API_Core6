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
        [Route("api/useraccount/cartdetail/all")]
        public async Task<IActionResult> GetAllCartDetail()
        {
            int uid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
            CartVM? cart = await cartDAO.GetCartByUid(uid);
            if (cart != null)
            {
                List<CartDetailVM> listCartDetail = await cartDetailDAO.GetAllCartDetail(cart.CartId);
                if (listCartDetail != null)
                {
                    return Ok(listCartDetail);
                }

            }
            return BadRequest(ReturnMessage.create("no CartDetail Found"));
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
        [Route("api/cartdetail/create")]
        public async Task<IActionResult> CreateCartDetail([FromBody] CreateCartDetailVM createdCartDetail)
        {

            if (await cartDetailDAO.CreateCartDetail(createdCartDetail))
            {
                return Ok(ReturnMessage.create("Success"));
            }
            return BadRequest(ReturnMessage.create("error at CreateCartDetail"));
        }


        [Authorize]
        [HttpPut]
        [Route("api/cartdetail")]
        public async Task<IActionResult> UpdateCartDetail([FromBody] UpdateCartDetailVM CartDetail)
        {
            CartDetail? cacrtDetail = await cartDetailDAO.GetCartDetailByCartDetailID(CartDetail.CartDetailId);
            if (cacrtDetail != null)
            {
                if (await cartDetailDAO.UpdateCartDetail(cacrtDetail, CartDetail.CartDetailItemQuantity))
                {
                    return Ok(ReturnMessage.create("Success"));
                }
            }
            return BadRequest(ReturnMessage.create("error at UpdateCartDetail"));
        }

        [Authorize]
        [HttpDelete]
        [Route("api/cartdetail")]
        public async Task<IActionResult> DeleteCartDetail([FromBody] CartDetailIdVM CartDetailid)
        {
            CartDetail? cacrtDetail = await cartDetailDAO.GetCartDetailByCartDetailID(CartDetailid.CartDetailId);
            if (cacrtDetail != null)
            {
                if (await cartDetailDAO.DeleteCartDetail(cacrtDetail))
                {
                    return Ok(ReturnMessage.create("Success"));
                }
            }
            return BadRequest(ReturnMessage.create("error at UpdateCartDetail"));
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
