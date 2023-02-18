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
                    return Ok("da tao thanh cong");
                }
                else
                {
                    return BadRequest(ReturnMessage.create(ItemRepository.errorMessage));
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet("GetAllBriefItemAndBriefRequest")]
        public async Task<IActionResult> GetAllBriefItem(bool share)
        {
            try
            {
                List<BriefItem> listBriefItem = await _itemRepository.GetAllBriefItemAndBriefRequest(share);
                if (listBriefItem == null || listBriefItem.Count() == 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                else
                {
                    return Ok(listBriefItem);
                }
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,e);
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
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                else
                {
                    return Ok(itemDetail);
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("GetBriefItemByUserId")]
        public async Task<IActionResult> GetBriefItemByUserID(int userID)
        {
            try
            {
                List<BriefItem> listBriefItemByUserID = await _itemRepository.GetBriefItemByUserID(userID);
                if (listBriefItemByUserID == null || listBriefItemByUserID.Count() == 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                else
                {
                    return Ok(listBriefItemByUserID);
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("SearchBriefItemByUserId")]
        public async Task<IActionResult> SearchBriefItemByTitle(string itemTitle)
        {
            try
            {
                List<BriefItem> listBriefItemByItemTitle = await _itemRepository.SearchBriefItemByTitle(itemTitle);
                if (listBriefItemByItemTitle == null || listBriefItemByItemTitle.Count() == 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                else
                {
                    return Ok(listBriefItemByItemTitle);
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("SearchBriefItemBySubCategoryID")]
        public async Task<IActionResult> SearchBriefItemBySubCategoryID(int subCategoryID)
        {
            try
            {
                List<BriefItem> listBriefItemBySubCategoryID = await _itemRepository.SearchBriefItemBySubCategoryID(subCategoryID);
                if (listBriefItemBySubCategoryID == null || listBriefItemBySubCategoryID.Count() == 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                else
                {
                    return Ok(listBriefItemBySubCategoryID);
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("SearchBriefItemByCategoryID")]
        public async Task<IActionResult> SearchBriefItemByCategoryID(int categoryID)
        {
            try
            {
                List<BriefItem> listBriefItemByCategoryID = await _itemRepository.SearchBriefItemByCategoryID(categoryID);
                if (listBriefItemByCategoryID == null || listBriefItemByCategoryID.Count() == 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                else
                {
                    return Ok(listBriefItemByCategoryID);
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("DeleteItem")]
        public async Task<IActionResult> DeleteItem([FromBody] DeleteItemVM itemVM)
        {
            try
            {
                bool checkDelete = await _itemRepository.DeleteItem(itemVM);
                if (checkDelete)
                {
                    return Ok("da xoa thanh cong");
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("UpdateItem")]
        public async Task<IActionResult> UpdateItem([FromBody] UpdateItemVM itemVM)
        {
            try
            {
                bool checkUpdate = await _itemRepository.UpdateItem(itemVM);
                if (checkUpdate)
                {
                    return Ok("da update thanh cong");
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("GetAllBriefItemAndBriefRequestByUserID")]
        public async Task<IActionResult> GetAllBriefItemAndBriefRequestByUserID(int userID, bool share)
        {
            try
            {
                List<BriefItem> listBriefItemAndBriefRequestByUserID = await _itemRepository.GetAllBriefItemAndBriefRequestByUserID(userID, share);
                if (listBriefItemAndBriefRequestByUserID == null || listBriefItemAndBriefRequestByUserID.Count() == 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                else
                {
                    return Ok(listBriefItemAndBriefRequestByUserID);
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
