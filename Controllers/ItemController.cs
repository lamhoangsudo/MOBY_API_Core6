using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository;
using MOBY_API_Core6.Repository.IRepository;

namespace Item.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        private readonly IUserRepository _userRepository;

        public ItemController(IItemRepository itemRepository, IUserRepository userRepository)
        {
            _itemRepository = itemRepository;
            _userRepository = userRepository;
        }

        [HttpPost("CreateItem")]
        public async Task<IActionResult> CreateItem([FromBody] CreateItemVM itemVM)
        {
            try
            {
                bool checkCreate = await _itemRepository.CreateItem(itemVM);
                if (checkCreate)
                {
                    return Ok(ReturnMessage.Create(itemVM + " đã được thêm vào thành công."));
                }
                else
                {
#pragma warning disable CS8604 // Possible null reference argument.
                    return BadRequest(ReturnMessage.Create(ItemRepository.ErrorMessage));
#pragma warning restore CS8604 // Possible null reference argument.
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("GetAllBriefItemAndBriefRequest")]
        public async Task<IActionResult> GetAllBriefItem(bool share, bool status, int pageNumber, int pageSize)
        {
            try
            {
                List<BriefItem>? listBriefItem = await _itemRepository.GetAllBriefItemAndBriefRequest(share, status, pageNumber, pageSize);
                if (listBriefItem == null)
                {
                    return BadRequest(ItemRepository.ErrorMessage);
                }
                else
                {
                    return Ok(listBriefItem);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetItemDetail")]
        public async Task<IActionResult> GetItemDetail(int itemID)
        {
            try
            {
                DetailItemVM? itemDetail = await _itemRepository.GetItemDetail(itemID);
                if (itemDetail == null)
                {
                    return NotFound(ReturnMessage.Create("sản phẩm này không tồn tại"));
                }
                else
                {
                    return Ok(itemDetail);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetRequestDetail")]
        public async Task<IActionResult> GetRequestDetail(int itemID)
        {
            try
            {
                DetailItemRequestVM? itemRequestDetail = await _itemRepository.GetRequestDetail(itemID);
                if (itemRequestDetail == null)
                {
                    return NotFound(ReturnMessage.Create("yêu cầu này không tồn tại"));
                }
                else
                {
                    return Ok(itemRequestDetail);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetBriefItemByOrBriefRequestUserID")]
        public async Task<IActionResult> GetBriefItemByOrBriefRequestUserID(int userID, bool status, bool share, int pageNumber, int pageSize)
        {
            try
            {
                List<BriefItem>? listBriefItemByUserID = await _itemRepository.GetBriefItemByOrBriefRequestUserID(userID, status, share, pageNumber, pageSize);
                if (listBriefItemByUserID == null)
                {
                    return BadRequest(ItemRepository.ErrorMessage);
                }
                else
                {
                    return Ok(listBriefItemByUserID);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("SearchBriefItemByTitle")]
        public async Task<IActionResult> SearchBriefItemByTitle(string itemTitle, bool status)
        {
            try
            {
                List<BriefItem>? listBriefItemByItemTitle = await _itemRepository.SearchBriefItemByTitle(itemTitle, status);
                if (listBriefItemByItemTitle == null)
                {
                    return BadRequest(ItemRepository.ErrorMessage);
                }
                else
                {
                    return Ok(listBriefItemByItemTitle);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("SearchBriefItemByOrBriefRequestBySubCategoryID")]
        public async Task<IActionResult> SearchBriefItemByOrBriefRequestBySubCategoryID(int subCategoryID, bool status, bool share, int pageNumber, int pageSize)
        {
            try
            {
                List<BriefItem>? listBriefItemBySubCategoryID = await _itemRepository.SearchBriefItemByOrBriefRequestBySubCategoryID(subCategoryID, status, share, pageNumber, pageSize);
                if (listBriefItemBySubCategoryID == null)
                {
                    return BadRequest(ItemRepository.ErrorMessage);
                }
                else
                {
                    return Ok(listBriefItemBySubCategoryID);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("SearchBriefItemOrBriefRequestByCategoryID")]
        public async Task<IActionResult> SearchBriefItemOrBriefRequestByCategoryID(int categoryID, bool status, bool share, int pageNumber, int pageSize)
        {
            try
            {
                List<BriefItem>? listBriefItemByCategoryID = await _itemRepository.SearchBriefItemOrBriefRequestByCategoryID(categoryID, status, share, pageNumber, pageSize);
                if (listBriefItemByCategoryID == null)
                {
                    return BadRequest(ItemRepository.ErrorMessage);
                }
                else
                {
                    return Ok(listBriefItemByCategoryID);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("DeleteItem")]
        public async Task<IActionResult> DeleteItem([FromBody] DeleteItemVM itemVM)
        {
            try
            {
                bool checkDelete = await _itemRepository.DeleteItem(itemVM);
                if (checkDelete)
                {
                    return Ok(ReturnMessage.Create("sản phẩm này đã xóa thành công"));
                }
                else
                {
#pragma warning disable CS8604 // Possible null reference argument.
                    return NotFound(ReturnMessage.Create(ItemRepository.ErrorMessage));
#pragma warning restore CS8604 // Possible null reference argument.
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
                bool checkUpdate = await _itemRepository.UpdateItem(itemVM);
                if (checkUpdate)
                {
                    return Ok(ReturnMessage.Create("sản phẩm này đã được cập nhập thành công"));
                }
                else
                {
#pragma warning disable CS8604 // Possible null reference argument.
                    return BadRequest(ReturnMessage.Create(ItemRepository.ErrorMessage));
#pragma warning restore CS8604 // Possible null reference argument.
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetAllMyBriefItemAndBriefRequest")]
        public async Task<IActionResult> GetAllBriefItemAndBriefRequestByUserID(int userID, bool share)
        {
            try
            {
                List<BriefItem>? listBriefItemAndBriefRequestByUserID = await _itemRepository.GetAllMyBriefItemAndBriefRequest(userID, share);
                if (listBriefItemAndBriefRequestByUserID == null)
                {
                    return BadRequest(ItemRepository.ErrorMessage);
                }
                else
                {
                    return Ok(listBriefItemAndBriefRequestByUserID);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetAllShareRecently")]
        public async Task<IActionResult> GetAllShareRecently(int pageNumber, int pageSize)
        {
            try
            {
                int? userID = null;
                //userID = await _userRepository.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                ListVM<BriefItem>? listAllShareRecently = await _itemRepository.GetAllShareRecently(pageNumber, pageSize, userID);
                if (listAllShareRecently == null)
                {
#pragma warning disable CS8604 // Possible null reference argument.
                    return BadRequest(ReturnMessage.Create(ItemRepository.ErrorMessage));
#pragma warning restore CS8604 // Possible null reference argument.
                }
                else
                {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    if (listAllShareRecently.List.Count > 0)
                    {
                        return Ok(listAllShareRecently);
                    }
                    else
                    {
#pragma warning disable CS8604 // Possible null reference argument.
                        return NotFound(ReturnMessage.Create(ItemRepository.ErrorMessage));
#pragma warning restore CS8604 // Possible null reference argument.
                    }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetAllShareFree")]
        public async Task<IActionResult> GetAllShareFree(int pageNumber, int pageSize)
        {
            try
            {
                ListVM<BriefItem>? listAllShareFree = await _itemRepository.GetAllShareFree(pageNumber, pageSize);
                if (listAllShareFree == null)
                {
#pragma warning disable CS8604 // Possible null reference argument.
                    return BadRequest(ReturnMessage.Create(ItemRepository.ErrorMessage));
#pragma warning restore CS8604 // Possible null reference argument.
                }
                else
                {
                    return Ok(listAllShareFree);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpGet("GetAllMyShareAndRequest")]
        public async Task<IActionResult> GetAllMyShareAndRequest(bool share, bool status, int pageNumber, int pageSize)
        {
            try
            {
                int userID = await _userRepository.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (userID == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                ListVM<BriefItem>? listAllMyShareAndRequest = await _itemRepository.GetAllMyShareAndRequest(userID, share, status, pageNumber, pageSize);
                if (listAllMyShareAndRequest == null)
                {
#pragma warning disable CS8604 // Possible null reference argument.
                    return BadRequest(ReturnMessage.Create(ItemRepository.ErrorMessage));
#pragma warning restore CS8604 // Possible null reference argument.
                }
                else
                {
                    return Ok(listAllMyShareAndRequest);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpGet("GetAllShareNearYou")]
        public async Task<IActionResult> GetAllShareNearYou(int pageNumber, int pageSize)
        {
            try
            {
                int userID = await _userRepository.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (userID == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                UserAccount? user = await _userRepository.FindUserByUid(userID);
                if (user == null)
                {
                    return BadRequest(ReturnMessage.Create("tài khoảng không tồn tại"));
                }
                ListVM<BriefItem>? listAllShareNearYou = await _itemRepository.GetAllShareNearYou(user.UserAddress, pageNumber, pageSize, userID);
                if (listAllShareNearYou == null)
                {
#pragma warning disable CS8604 // Possible null reference argument.
                    return BadRequest(ReturnMessage.Create(ItemRepository.ErrorMessage));
#pragma warning restore CS8604 // Possible null reference argument.
                }
                else
                {
                    return Ok(listAllShareNearYou);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("GetItemDynamicFilters")]
        public async Task<IActionResult> GetItemDynamicFilters([FromBody] DynamicFilterItemVM dynamicFilterVM)
        {
            try
            {
                ListVM<BriefItem>? listVM = await _itemRepository.GetItemDynamicFilters(dynamicFilterVM);
                if (listVM == null)
                {
                    return BadRequest(ItemRepository.ErrorMessage);
                }
                else
                {
                    return Ok(listVM);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpGet("GetListAllOtherPersonRequestItem")]
        public async Task<IActionResult> GetListAllOtherPersonRequestItem(bool share, bool status, int pageNumber, int pageSize)
        {
            int userID = await _userRepository.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
            if (userID == 0)
            {
                return BadRequest(ReturnMessage.Create("Account has been suspended"));
            }
            try
            {
                ListVM<BriefItem>? listAllOtherPersonRequestItem = await _itemRepository.GetListAllOtherPersonRequestItem(share, status, userID, pageNumber, pageSize);
                if (listAllOtherPersonRequestItem != null)
                {

                    return Ok(listAllOtherPersonRequestItem);
                }
                else
                {
                    return BadRequest(ItemRepository.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpGet("GetListAllMyRequestItem")]
        public async Task<IActionResult> GetListAllMyRequestItem(bool share, bool status, int pageNumber, int pageSize)
        {
            int userID = await _userRepository.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
            if (userID == 0)
            {
                return BadRequest(ReturnMessage.Create("Account has been suspended"));
            }
            try
            {
                ListVM<BriefItem>? listAllMyRequestItem = await _itemRepository.GetListAllMyRequestItem(share, status, userID, pageNumber, pageSize);
                if (listAllMyRequestItem != null)
                {
                    return Ok(listAllMyRequestItem);
                }
                else
                {
                    return BadRequest(ItemRepository.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpPost("CreateRecordUserSearch")]
        public async Task<IActionResult> CreateRecordUserSearch([FromBody] RecordSearchVM recordSearchVM)
        {
            int userID = await _userRepository.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
            if (userID == 0)
            {
                return BadRequest(ReturnMessage.Create("Account has been suspended"));
            }
            try
            {
                recordSearchVM.UserId = 5;
                bool createRecordUserSearch = await _itemRepository.RecordUserSearch(recordSearchVM);
                if (createRecordUserSearch)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(ItemRepository.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpPost("GetListRecommend")]
        public async Task<IActionResult> GetListRecommend(int pageNumber, int pageSize)
        {
            int userID = await _userRepository.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
            if (userID == 0)
            {
                return BadRequest(ReturnMessage.Create("Account has been suspended"));
            }
            try
            {
                ListVM<BriefItem>? listRecommendItem = await _itemRepository.GetListRecommend(userID, pageNumber, pageSize);
                if (listRecommendItem != null)
                {
                    return Ok(listRecommendItem);
                }
                else
                {
                    return BadRequest(ItemRepository.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpGet("GetListRecommendByBaby")]
        public async Task<IActionResult> GetListRecommendByBaby(int babyID, int pageNumber, int pageSize, bool age, bool weight, bool height)
        {
            int userID = await _userRepository.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
            if (userID == 0)
            {
                return BadRequest(ReturnMessage.Create("Account has been suspended"));
            }
            try
            {
                ListVM<BriefItem>? listRecommendItemByBaby = await _itemRepository.GetListRecommendByBaby(babyID, userID, pageNumber, pageSize, age, weight, height);
                if (listRecommendItemByBaby != null)
                {
                    return Ok(listRecommendItemByBaby);
                }
                else
                {
                    return BadRequest(ItemRepository.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
