using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("/CreateItem")]
        public async Task<IActionResult> CreateItem(int userId, int subCategoryId, string itemTitle, string itemDetailedDescription, double itemMass, bool itemSize, string itemQuanlity, double itemEstimateValue, double itemSalePrice, int itemShareAmount, bool itemSponsoredOrderShippingFee, string itemShippingAddress, string image, string stringDateTimeExpired, bool share)
        {
            try
            {
                bool checkCreate = await _itemRepository.CreateItem(userId, subCategoryId, itemTitle, itemDetailedDescription, itemMass, itemSize, itemQuanlity, itemEstimateValue, itemSalePrice, itemShareAmount, itemSponsoredOrderShippingFee, itemShippingAddress, image, stringDateTimeExpired, share);
                if (checkCreate)
                {
                    return Ok("da tao thanh cong");
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
        [HttpGet("/GetAllBriefItemAndBriefRequest")]
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

        [HttpGet("/GetItemDetail")]
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

        [HttpGet("/GetBriefItemByUserId")]
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

        [HttpGet("/SearchBriefItemByUserId")]
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

        [HttpGet("/SearchBriefItemBySubCategoryID")]
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

        [HttpGet("/SearchBriefItemByCategoryID")]
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

        [HttpPost("/DeleteItem")]
        public async Task<IActionResult> DeleteItem(int itemID, int userID)
        {
            try
            {
                bool checkDelete = await _itemRepository.DeleteItem(itemID, userID);
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

        [HttpPost("/Update")]
        public async Task<IActionResult> UpdateItem(int userID, int itemID, int subCategoryId, string itemTitle, string itemDetailedDescription, double itemMass, bool itemSize, string itemQuanlity, double itemEstimateValue, double itemSalePrice, int itemShareAmount, bool itemSponsoredOrderShippingFee, string itemShippingAddress, string image, string stringDateTimeExpired, bool share)
        {
            try
            {
                bool checkUpdate = await _itemRepository.UpdateItem(userID, itemID, subCategoryId, itemTitle, itemDetailedDescription, itemMass, itemSize, itemQuanlity, itemEstimateValue, itemSalePrice, itemShareAmount, itemSponsoredOrderShippingFee, itemShippingAddress, image, stringDateTimeExpired, share);
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

        [HttpGet("/GetAllBriefItemAndBriefRequestByUserID")]
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
