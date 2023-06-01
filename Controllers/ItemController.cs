using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Log4Net;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Service;
using MOBY_API_Core6.Service.IService;

namespace Item.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly IUserService _userService;
        private readonly Logger4Net _logger4Net;
        public ItemController(IItemService itemService, IUserService userService)
        {
            _itemService = itemService;
            _userService = userService;
            _logger4Net = new Logger4Net();
        }
        [Authorize]
        [HttpPost("CreateItem")]
        public async Task<IActionResult> CreateItem([FromBody] CreateItemVM itemVM)
        {
            try
            {
                int userID = await _userService.GetUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (userID == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                if (userID == -1)
                {
                    return BadRequest(ReturnMessage.Create("Account not found"));
                }
                itemVM.UserId = userID;
                bool checkCreate = await _itemService.CreateItem(itemVM);
                if (checkCreate)
                {
                    return Ok(ReturnMessage.Create(itemVM.ItemTitle + " đã được thêm vào thành công."));
                }
                else
                {
                    
                    return BadRequest(ReturnMessage.Create(ItemService.ErrorMessage));
                }
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("GetAllBriefItemAndBriefRequest")]
        public async Task<IActionResult> GetAllBriefItem(bool share, bool status, int pageNumber, int pageSize)
        {
            try
            {
                List<BriefItem>? listBriefItem = await _itemService.GetAllBriefItemAndBriefRequest(share, status, pageNumber, pageSize);
                if (listBriefItem == null)
                {
                    return BadRequest(ReturnMessage.Create(ItemService.ErrorMessage));
                }
                else
                {
                    return Ok(listBriefItem);
                }
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetItemDetail")]
        public async Task<IActionResult> GetItemDetail(int itemID)
        {
            try
            {
                DetailItemVM? itemDetail = await _itemService.GetItemDetail(itemID);
                if (itemDetail == null)
                {
                    return NotFound(ReturnMessage.Create(ItemService.ErrorMessage));
                }
                else
                {
                    return Ok(itemDetail);
                }
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetRequestDetail")]
        public async Task<IActionResult> GetRequestDetail(int itemID)
        {
            try
            {
                DetailItemRequestVM? itemRequestDetail = await _itemService.GetRequestDetail(itemID);
                if (itemRequestDetail == null)
                {
                    return NotFound(ReturnMessage.Create(ItemService.ErrorMessage));
                }
                else
                {
                    return Ok(itemRequestDetail);
                }
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetBriefItemByOrBriefRequestUserID")]
        public async Task<IActionResult> GetBriefItemByOrBriefRequestUserID(int userID, bool status, bool share, int pageNumber, int pageSize)
        {
            try
            {
                List<BriefItem>? listBriefItemByUserID = await _itemService.GetBriefItemByOrBriefRequestUserID(userID, status, share, pageNumber, pageSize);
                if (listBriefItemByUserID == null)
                {
                    return BadRequest(ReturnMessage.Create(ItemService.ErrorMessage));
                }
                else
                {
                    return Ok(listBriefItemByUserID);
                }
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("SearchBriefItemByTitle")]
        public async Task<IActionResult> SearchBriefItemByTitle(string itemTitle, bool status)
        {
            try
            {
                List<BriefItem>? listBriefItemByItemTitle = await _itemService.SearchBriefItemByTitle(itemTitle, status);
                if (listBriefItemByItemTitle == null)
                {
                    return BadRequest(ReturnMessage.Create(ItemService.ErrorMessage));
                }
                else
                {
                    return Ok(listBriefItemByItemTitle);
                }
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("SearchBriefItemByOrBriefRequestBySubCategoryID")]
        public async Task<IActionResult> SearchBriefItemByOrBriefRequestBySubCategoryID(int subCategoryID, bool status, bool share, int pageNumber, int pageSize)
        {
            try
            {
                List<BriefItem>? listBriefItemBySubCategoryID = await _itemService.SearchBriefItemByOrBriefRequestBySubCategoryID(subCategoryID, status, share, pageNumber, pageSize);
                if (listBriefItemBySubCategoryID == null)
                {
                    return BadRequest(ReturnMessage.Create(ItemService.ErrorMessage));
                }
                else
                {
                    return Ok(listBriefItemBySubCategoryID);
                }
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("SearchBriefItemOrBriefRequestByCategoryID")]
        public async Task<IActionResult> SearchBriefItemOrBriefRequestByCategoryID(int categoryID, bool status, bool share, int pageNumber, int pageSize)
        {
            try
            {
                List<BriefItem>? listBriefItemByCategoryID = await _itemService.SearchBriefItemOrBriefRequestByCategoryID(categoryID, status, share, pageNumber, pageSize);
                if (listBriefItemByCategoryID == null)
                {
                    return BadRequest(ReturnMessage.Create(ItemService.ErrorMessage));
                }
                else
                {
                    return Ok(listBriefItemByCategoryID);
                }
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("DeleteItem")]
        public async Task<IActionResult> DeleteItem([FromBody] DeleteItemVM itemVM)
        {
            try
            {
                bool checkDelete = await _itemService.DeleteItem(itemVM);
                if (checkDelete)
                {
                    return Ok(ReturnMessage.Create("sản phẩm này đã xóa thành công"));
                }
                else
                {
                    return NotFound(ReturnMessage.Create(ItemService.ErrorMessage));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("UpdateItem")]
        public async Task<IActionResult> UpdateItem([FromBody] UpdateItemVM itemVM)
        {
            try
            {
                bool checkUpdate = await _itemService.UpdateItem(itemVM);
                if (checkUpdate)
                {
                    return Ok(ReturnMessage.Create("sản phẩm này đã được cập nhập thành công"));
                }
                else
                {
                    return BadRequest(ReturnMessage.Create(ItemService.ErrorMessage));
                }
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetAllMyBriefItemAndBriefRequest")]
        public async Task<IActionResult> GetAllBriefItemAndBriefRequestByUserID(int userID, bool share)
        {
            try
            {
                List<BriefItem>? listBriefItemAndBriefRequestByUserID = await _itemService.GetAllMyBriefItemAndBriefRequest(userID, share);
                if (listBriefItemAndBriefRequestByUserID == null)
                {
                    return BadRequest(ReturnMessage.Create(ItemService.ErrorMessage));
                }
                else
                {
                    return Ok(listBriefItemAndBriefRequestByUserID);
                }
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetAllShareRecently")]
        public async Task<IActionResult> GetAllShareRecently(int pageNumber, int pageSize)
        {
            try
            {
                ListVM<BriefItem>? listAllShareRecently = await _itemService.GetAllShareRecently(pageNumber, pageSize);
                if (listAllShareRecently == null)
                {
                    return BadRequest(ReturnMessage.Create(ItemService.ErrorMessage));
                }
                else
                {
                    return Ok(listAllShareRecently);
                }
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetAllShareFree")]
        public async Task<IActionResult> GetAllShareFree(int pageNumber, int pageSize)
        {
            try
            {
                ListVM<BriefItem>? listAllShareFree = await _itemService.GetAllShareFree(pageNumber, pageSize);
                if (listAllShareFree == null)
                {
                    return BadRequest(ReturnMessage.Create(ItemService.ErrorMessage));
                }
                else
                {
                    return Ok(listAllShareFree);
                }
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpGet("GetAllMyShareAndRequest")]
        public async Task<IActionResult> GetAllMyShareAndRequest(bool share, bool status, int pageNumber, int pageSize)
        {
            try
            {
                int userID = await _userService.GetUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (userID == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                if (userID == -1)
                {
                    return BadRequest(ReturnMessage.Create("Account not found"));
                }
                ListVM<BriefItem>? listAllMyShareAndRequest = await _itemService.GetAllMyShareAndRequest(userID, share, status, pageNumber, pageSize);
                if (listAllMyShareAndRequest == null)
                {
                    return BadRequest(ReturnMessage.Create(ItemService.ErrorMessage));
                }
                else
                {
                    return Ok(listAllMyShareAndRequest);
                }
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpGet("GetAllShareNearYou")]
        public async Task<IActionResult> GetAllShareNearYou(int pageNumber, int pageSize)
        {
            try
            {
                int userID = await _userService.GetUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (userID == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                if (userID == -1)
                {
                    return BadRequest(ReturnMessage.Create("Account not found"));
                }
                UserAccount? user = await _userService.FindUserByUid(userID);
                if (user == null)
                {
                    return BadRequest(ReturnMessage.Create("tài khoảng không tồn tại"));
                }
                ListVM<BriefItem>? listAllShareNearYou = await _itemService.GetAllShareNearYou(user.UserAddress, pageNumber, pageSize, userID);
                if (listAllShareNearYou == null)
                {
                    return BadRequest(ReturnMessage.Create(ItemService.ErrorMessage));
                }
                else
                {
                    return Ok(listAllShareNearYou);
                }
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("GetItemDynamicFilters")]
        public async Task<IActionResult> GetItemDynamicFilters([FromBody] DynamicFilterItemVM dynamicFilterVM)
        {
            try
            {
                ListVM<BriefItem>? listVM = await _itemService.GetItemDynamicFilters(dynamicFilterVM);
                if (listVM == null)
                {
                    return BadRequest(ReturnMessage.Create(ItemService.ErrorMessage));
                }
                else
                {
                    return Ok(listVM);
                }
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpGet("GetListAllOtherPersonRequestItem")]
        public async Task<IActionResult> GetListAllOtherPersonRequestItem(bool share, bool status, int pageNumber, int pageSize)
        {
            int userID = await _userService.GetUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
            if (userID == 0)
            {
                return BadRequest(ReturnMessage.Create("Account has been suspended"));
            }
            if (userID == -1)
            {
                return BadRequest(ReturnMessage.Create("Account not found"));
            }
            try
            {
                ListVM<BriefItem>? listAllOtherPersonRequestItem = await _itemService.GetListAllOtherPersonRequestItem(share, status, userID, pageNumber, pageSize);
                if (listAllOtherPersonRequestItem != null)
                {

                    return Ok(listAllOtherPersonRequestItem);
                }
                else
                {
                    return BadRequest(ReturnMessage.Create(ItemService.ErrorMessage));
                }
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpGet("GetListAllMyRequestItem")]
        public async Task<IActionResult> GetListAllMyRequestItem(bool share, bool status, int pageNumber, int pageSize)
        {
            int userID = await _userService.GetUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
            if (userID == 0)
            {
                return BadRequest(ReturnMessage.Create("Account has been suspended"));
            }
            if (userID == -1)
            {
                return BadRequest(ReturnMessage.Create("Account not found"));
            }
            try
            {
                ListVM<BriefItem>? listAllMyRequestItem = await _itemService.GetListAllMyRequestItem(share, status, userID, pageNumber, pageSize);
                if (listAllMyRequestItem != null)
                {
                    return Ok(listAllMyRequestItem);
                }
                else
                {
                    return BadRequest(ReturnMessage.Create(ItemService.ErrorMessage));
                }
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpPost("CreateRecordUserSearch")]
        public async Task<IActionResult> CreateRecordUserSearch([FromBody] RecordSearchVM recordSearchVM)
        {
            int userID = await _userService.GetUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
            if (userID == 0)
            {
                return BadRequest(ReturnMessage.Create("Account has been suspended"));
            }
            if (userID == -1)
            {
                return BadRequest(ReturnMessage.Create("Account not found"));
            }
            try
            {
                bool createRecordUserSearch = await _itemService.RecordUserSearch(recordSearchVM);
                if (createRecordUserSearch)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(ReturnMessage.Create(ItemService.ErrorMessage));
                }
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpGet("GetListRecommend")]
        public async Task<IActionResult> GetListRecommend(int pageNumber, int pageSize)
        {
            int userID = await _userService.GetUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
            if (userID == 0)
            {
                return BadRequest(ReturnMessage.Create("Account has been suspended"));
            }
            if (userID == -1)
            {
                return BadRequest(ReturnMessage.Create("Account not found"));
            }
            try
            {
                ListVM<BriefItem>? listRecommendItem = await _itemService.GetListRecommend(userID, pageNumber, pageSize);
                if (listRecommendItem != null)
                {
                    return Ok(listRecommendItem);
                }
                else
                {
                    return BadRequest(ReturnMessage.Create(ItemService.ErrorMessage));
                }
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpGet("GetListRecommendByBaby")]
        public async Task<IActionResult> GetListRecommendByBaby(int babyID, int pageNumber, int pageSize, bool age, bool weight, bool height)
        {
            int userID = await _userService.GetUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
            if (userID == 0)
            {
                return BadRequest(ReturnMessage.Create("Account has been suspended"));
            }
            if (userID == -1)
            {
                return BadRequest(ReturnMessage.Create("Account not found"));
            }
            try
            {
                age = true;
                weight = true;
                height = true;
                ListVM<BriefItem>? listRecommendItemByBaby = await _itemService.GetListRecommendByBaby(babyID, userID, pageNumber, pageSize, age, weight, height);
                if (listRecommendItemByBaby != null)
                {
                    return Ok(listRecommendItemByBaby);
                }
                else
                {
                    return BadRequest(ReturnMessage.Create(ItemService.ErrorMessage));
                }
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
