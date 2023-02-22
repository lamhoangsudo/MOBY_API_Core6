using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository;

namespace Item.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;

        public ItemController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpPost("CreateItem")]
        public async Task<IActionResult> CreateItem([FromBody] CreateItemVM itemVM)
        {
            try
            {
                bool checkCreate = await _itemRepository.CreateItem(itemVM);
                if (checkCreate)
                {
                    return Ok(ReturnMessage.create(itemVM + " đã được thêm vào thành công."));
                }
                else
                {
                    return BadRequest(ReturnMessage.create(ItemRepository.errorMessage));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("GetAllBriefItemAndBriefRequest")]
        public async Task<IActionResult> GetAllBriefItem(bool share, bool status)
        {
            try
            {
                List<BriefItem> listBriefItem = await _itemRepository.GetAllBriefItemAndBriefRequest(share, status);
                if (listBriefItem == null)
                {
                    return BadRequest(ItemRepository.errorMessage);
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
                DetailItem itemDetail = await _itemRepository.GetItemDetail(itemID);
                if (itemDetail == null)
                {
                    return NotFound(ReturnMessage.create("sản phẩm này không tồn tại"));
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
                DetailItemRequest itemRequestDetail = await _itemRepository.GetRequestDetail(itemID);
                if (itemRequestDetail == null)
                {
                    return NotFound(ReturnMessage.create("yêu cầu này không tồn tại"));
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

        [HttpGet("GetBriefItemByAndBriefRequestUserID")]
        public async Task<IActionResult> GetBriefItemByUserID(int userID, bool status)
        {
            try
            {
                List<BriefItem> listBriefItemByUserID = await _itemRepository.GetBriefItemByAndBriefRequestUserID(userID, status);
                if (listBriefItemByUserID == null)
                {
                    return BadRequest(ItemRepository.errorMessage);
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
                List<BriefItem> listBriefItemByItemTitle = await _itemRepository.SearchBriefItemByTitle(itemTitle, status);
                if (listBriefItemByItemTitle == null)
                {
                    return BadRequest(ItemRepository.errorMessage);
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

        [HttpGet("SearchBriefItemBySubCategoryID")]
        public async Task<IActionResult> SearchBriefItemBySubCategoryID(int subCategoryID, bool status)
        {
            try
            {
                List<BriefItem> listBriefItemBySubCategoryID = await _itemRepository.SearchBriefItemBySubCategoryID(subCategoryID, status);
                if (listBriefItemBySubCategoryID == null)
                {
                    return BadRequest(ItemRepository.errorMessage);
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

        [HttpGet("SearchBriefItemByCategoryID")]
        public async Task<IActionResult> SearchBriefItemByCategoryID(int categoryID, bool status)
        {
            try
            {
                List<BriefItem> listBriefItemByCategoryID = await _itemRepository.SearchBriefItemByCategoryID(categoryID, status);
                if (listBriefItemByCategoryID == null)
                {
                    return BadRequest(ItemRepository.errorMessage);
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
                    return Ok(ReturnMessage.create("sản phẩm này đã xóa thành công"));
                }
                else
                {
                    return NotFound(ReturnMessage.create(ItemRepository.errorMessage));
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
                    return Ok(ReturnMessage.create("sản phẩm này đã được cập nhập thành công"));
                }
                else
                {
                    return BadRequest(ReturnMessage.create(ItemRepository.errorMessage));
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
                List<BriefItem> listBriefItemAndBriefRequestByUserID = await _itemRepository.GetAllMyBriefItemAndBriefRequest(userID, share);
                if (listBriefItemAndBriefRequestByUserID == null)
                {
                    return BadRequest(ItemRepository.errorMessage);
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
    }
}
