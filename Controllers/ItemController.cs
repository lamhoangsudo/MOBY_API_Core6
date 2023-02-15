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

        [HttpPost("/CreateItem/{userId},{subCategoryId},{itemTitle},{itemDetailedDescription},{itemMass},{itemSize},{itemQuanlity},{itemEstimateValue},{itemSalePrice},{itemShareAmount},{itemSponsoredOrderShippingFee},{itemShippingAddress},{image},{stringDateTimeExpired},{share}")]
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
        [HttpGet("/GetAllBriefItem")]
        public async Task<IActionResult> GetAllBriefItem()
        {
            try
            {
                List<BriefItem> listBriefItem = await _itemRepository.GetAllBriefItem();
                if (listBriefItem != null || listBriefItem.Count() > 0)
                {
                    return Ok(listBriefItem);
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

        [HttpGet("/GetItemDetail/{itemID}")]
        public async Task<IActionResult> GetItemDetail(int itemID)
        {
            try
            {
                DetailItem itemDetail = await _itemRepository.GetItemDetail(itemID);
                if (itemDetail != null)
                {
                    return Ok(itemDetail);
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

        [HttpGet("/GetBriefItemByUserId/{userID}")]
        public async Task<IActionResult> GetBriefItemByUserID(int userID)
        {
            try
            {
                List<BriefItem> listBriefItemByUserID = await _itemRepository.GetBriefItemByUserID(userID);
                if (listBriefItemByUserID != null || listBriefItemByUserID.Count() > 0)
                {
                    return Ok(listBriefItemByUserID);
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

        [HttpGet("/SearchBriefItemByUserId/{itemTitle}")]
        public async Task<IActionResult> SearchBriefItemByTitle(string itemTitle)
        {
            try
            {
                List<BriefItem> listBriefItemByItemTitle = await _itemRepository.SearchBriefItemByTitle(itemTitle);
                if (listBriefItemByItemTitle != null || listBriefItemByItemTitle.Count() > 0)
                {
                    return Ok(listBriefItemByItemTitle);
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

        [HttpGet("/SearchBriefItemBySubCategoryID/{subCategoryID}")]
        public async Task<IActionResult> SearchBriefItemBySubCategoryID(int subCategoryID)
        {
            try
            {
                List<BriefItem> listBriefItemBySubCategoryID = await _itemRepository.SearchBriefItemBySubCategoryID(subCategoryID);
                if (listBriefItemBySubCategoryID != null || listBriefItemBySubCategoryID.Count() > 0)
                {
                    return Ok(listBriefItemBySubCategoryID);
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

        [HttpGet("/SearchBriefItemByCategoryID/{categoryID}")]
        public async Task<IActionResult> SearchBriefItemByCategoryID(int categoryID)
        {
            try
            {
                List<BriefItem> listBriefItemByCategoryID = await _itemRepository.SearchBriefItemByCategoryID(categoryID);
                if (listBriefItemByCategoryID != null || listBriefItemByCategoryID.Count() > 0)
                {
                    return Ok(listBriefItemByCategoryID);
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
        [HttpPost("/DeleteItem/{itemID}")]
        public async Task<IActionResult> DeleteItem(int itemID)
        {
            try
            {
                bool checkDelete = await _itemRepository.DeleteItem(itemID);
                if(checkDelete)
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
        [HttpPost("/Update/{itemID},{subCategoryId},{itemTitle},{itemDetailedDescription},{itemMass},{itemSize},{itemQuanlity},{itemEstimateValue},{itemSalePrice},{itemShareAmount},{itemSponsoredOrderShippingFee},{itemShippingAddress},{image},{stringDateTimeExpired},{share}")]
        public async Task<IActionResult> UpdateItem(int itemID, int subCategoryId, string itemTitle, string itemDetailedDescription, double itemMass, bool itemSize, string itemQuanlity, double itemEstimateValue, double itemSalePrice, int itemShareAmount, bool itemSponsoredOrderShippingFee, string itemShippingAddress, string image, string stringDateTimeExpired, bool share)
        {
            try
            {
                bool checkUpdate = await _itemRepository.UpdateItem(itemID, subCategoryId, itemTitle, itemDetailedDescription, itemMass, itemSize, itemQuanlity, itemEstimateValue, itemSalePrice, itemShareAmount, itemSponsoredOrderShippingFee, itemShippingAddress, image, stringDateTimeExpired, share);
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
    }
}
