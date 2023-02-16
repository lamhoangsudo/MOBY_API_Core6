using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository;

namespace MOBY_API_Core6.Controllers
{
    [Route("api/[controller]")]
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
        [Route("api/CartDetailController/GetAllCartDetail")]
        public async Task<IActionResult> GetAllCartDetail()
        {
            int uid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
            CartVM cart = await cartDAO.GetCartByUid(uid);
            List<CartDetailVM> listCartDetail = await cartDetailDAO.GetAllCartDetail(cart.CartId);
            if (listCartDetail != null)
            {
                return Ok(listCartDetail);
            }
            else
            {
                return BadRequest(ReturnMessage.create("no CartDetail Found"));
            }

        }
        [Authorize]
        [HttpGet]
        [Route("api/CartDetailController/GetCartDetailOfItemForOwner")]
        public async Task<IActionResult> GetCartDetailOfItemForOwner()
        {
            int uid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
            List<BriefItem> listItem = await itemDAO.GetBriefItemByUserID(uid);
            List<CartDetailVM> listCartDetailResult = new List<CartDetailVM>();
            List<CartDetailVM> listcartDetailVMByItemID = new List<CartDetailVM>();
            foreach (BriefItem item in listItem)
            {
                listcartDetailVMByItemID = await cartDetailDAO.GetCartDetailByItemID(item.ItemId);
                foreach (CartDetailVM vm in listcartDetailVMByItemID)
                {
                    listCartDetailResult.Add(vm);
                }
            }
            if (listCartDetailResult.Count > 0)
            {
                return Ok(listCartDetailResult);
            }
            else
            {
                return Ok(ReturnMessage.create("this user didn't has any request"));
            }


        }
        [Authorize]
        [HttpPost]
        [Route("api/CartDetailController/CreateCartDetail/{cartID} {itemID} {quantity}")]
        public async Task<IActionResult> CreateCartDetail(int cartID, int itemID, int quantity)
        {
            int uid = await userDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
            CartVM cart = await cartDAO.GetCartByUid(uid);
            if (await cartDetailDAO.CreateCartDetail(cartID, itemID, quantity))
            {
                return Ok(ReturnMessage.create("Success"));
            }
            return BadRequest(ReturnMessage.create("error at CreateCartDetail"));
        }

        [Authorize]
        [HttpPost]
        [Route("api/CartDetailController/AcceptCartDetail/{cartDetailID}")]
        public async Task<IActionResult> AcceptCartDetail(int cartDetailID)
        {
            var cacrtDetail = await cartDetailDAO.GetCartDetailByCartDetailID(cartDetailID);
            if (await cartDetailDAO.UpdateCartDetail(cacrtDetail, 2))
            {
                return Ok(ReturnMessage.create("Success"));
            }
            return BadRequest(ReturnMessage.create("error at AcceptCartDetail"));
        }

        [Authorize]
        [HttpPost]
        [Route("api/CartDetailController/CancelCartDetail/{cartDetailID}")]
        public async Task<IActionResult> CancelCartDetail(int cartDetailID)
        {
            var cacrtDetail = await cartDetailDAO.GetCartDetailByCartDetailID(cartDetailID);
            if (await cartDetailDAO.UpdateCartDetail(cacrtDetail, 3))
            {
                return Ok(ReturnMessage.create("Success"));
            }
            return BadRequest(ReturnMessage.create("error at CancelCartDetail"));

        }

        [Authorize]
        [HttpPost]
        [Route("api/CartDetailController/ConfirmCartDetail/{cartDetailID}")]
        public async Task<IActionResult> ConfirmCartDetail(int cartDetailID)
        {
            var cacrtDetail = await cartDetailDAO.GetCartDetailByCartDetailID(cartDetailID);
            if (await cartDetailDAO.UpdateCartDetail(cacrtDetail, 4))
            {
                return Ok(ReturnMessage.create("Success"));
            }
            return BadRequest(ReturnMessage.create("error at UpdateCartDetail"));
        }
    }
}
