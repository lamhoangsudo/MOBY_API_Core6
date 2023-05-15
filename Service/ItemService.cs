using Category.Data_View_Model;
using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;
using MOBY_API_Core6.Service.IService;
using Newtonsoft.Json;
using NodaTime;
using NodaTime.Extensions;

namespace MOBY_API_Core6.Service
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly JsonToObj _JsonToObj;

        public static string ErrorMessage { get; set; } = string.Empty;

        public ItemService(JsonToObj jsonToObj, IItemRepository itemRepository)
        {
            _JsonToObj = jsonToObj;
            _itemRepository = itemRepository;
        }
        public async Task<bool> CreateItem(CreateItemVM itemVM)
        {
            try
            {
                DateTime dateTimeCreate = DateTime.Now;
                DateTime? dateTimeExpired = null;
                if (!string.IsNullOrEmpty(itemVM.StringDateTimeExpired) && !string.IsNullOrWhiteSpace(itemVM.StringDateTimeExpired) && itemVM.Share == false)
                {
                    try
                    {
                        dateTimeExpired = DateTime.Parse(itemVM.StringDateTimeExpired);
                        bool check = dateTimeExpired > dateTimeCreate;
                        if (check == false)
                        {
                            return false;
                        }
                    }
                    catch
                    {
                        return false;
                    }
                }
                if (_JsonToObj.TransformLocation(itemVM.ItemShippingAddress) == null)
                {
                    return false;
                }
                if (itemVM.MaxAge < itemVM.MinAge || itemVM.MaxWeight < itemVM.MinWeight || itemVM.MaxHeight < itemVM.MinHeight || itemVM.MinAge < 0 || itemVM.MinWeight < 0 || itemVM.MinHeight < 0)
                {
                    return false;
                }
                else
                {
                    int check = await _itemRepository.CreateItem(itemVM, dateTimeCreate, dateTimeExpired);
                    if (check <= 0)
                    {
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }
        public async Task<List<int>> GetListItemIDByUserID(int userId)
        {
            List<int> ItemIDList = await _itemRepository.GetListItemIDByUserID(userId);
            return ItemIDList;
        }
        public async Task<int> GetQuantityByItemID(int itemID)
        {
            int itemQuantity = await _itemRepository.GetQuantityByItemID(itemID);
            return itemQuantity;
        }
        public async Task<List<BriefItem>?> GetAllBriefItemAndBriefRequest(bool share, bool status, int pageNumber, int pageSize)
        {
            try
            {
                List<BriefItem>? listBriefItem;
                listBriefItem = await _itemRepository.GetAllBriefItemAndBriefRequest(share, status, pageNumber, pageSize);
                if (listBriefItem == null) { 
                    listBriefItem = new List<BriefItem>();
                }
                return listBriefItem;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        public async Task<List<BriefItem>?> SearchBriefItemByTitle(string itemTitle, bool status)
        {
            try
            {
                List<BriefItem>? listBriefItemByTitle;
                listBriefItemByTitle = await _itemRepository.SearchBriefItemByTitle(itemTitle, status);
                if (listBriefItemByTitle == null)
                {
                    listBriefItemByTitle = new List<BriefItem>();
                }
                return listBriefItemByTitle;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        public async Task<List<BriefItem>?> GetBriefItemByOrBriefRequestUserID(int userID, bool status, bool share, int pageNumber, int pageSize)
        {
            try
            {
                List<BriefItem>? listBriefItemByUserID;
                listBriefItemByUserID = await _itemRepository.GetBriefItemByOrBriefRequestUserID(userID, status, share, pageNumber, pageSize);
                if (listBriefItemByUserID == null)
                {
                    listBriefItemByUserID = new List<BriefItem>();
                }
                return listBriefItemByUserID;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        public async Task<List<BriefItem>?> GetBriefItemByShare(bool share, bool status)
        {
            try
            {
                List<BriefItem>? listBriefItemByUserID;
                listBriefItemByUserID = await _itemRepository.GetBriefItemByShare(share, status);
                if (listBriefItemByUserID == null)
                {
                    listBriefItemByUserID = new List<BriefItem>();
                }

                return listBriefItemByUserID;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        public async Task<DetailItemVM?> GetItemDetail(int itemID)
        {
            try
            {
                DetailItemVM? itemDetail = await _itemRepository.GetItemDetail(itemID);
                if (itemDetail == null)
                {
                    throw new KeyNotFoundException();
                }
                return itemDetail;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        public async Task<DetailItemRequestVM?> GetRequestDetail(int itemID)
        {
            try
            {
                DetailItemRequestVM? detailItemRequest = await _itemRepository.GetRequestDetail(itemID);
                if (detailItemRequest == null)
                {
                    throw new KeyNotFoundException();
                }
                return detailItemRequest;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        public async Task<List<BriefItem>?> SearchBriefItemByOrBriefRequestBySubCategoryID(int subCategoryID, bool status, bool share, int pageNumber, int pageSize)
        {
            try
            {
                List<BriefItem>? listBriefItemBySubCategoryID;
                listBriefItemBySubCategoryID = await _itemRepository.SearchBriefItemByOrBriefRequestBySubCategoryID(subCategoryID, status, share, pageNumber, pageSize);
                if (listBriefItemBySubCategoryID == null)
                {
                    listBriefItemBySubCategoryID = new List<BriefItem>();
                }
                return listBriefItemBySubCategoryID;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        public async Task<List<BriefItem>?> SearchBriefItemOrBriefRequestByCategoryID(int categoryID, bool status, bool share, int pageNumber, int pageSize)
        {
            try
            {
                List<BriefItem>? listBriefItemByCategoryID;
                listBriefItemByCategoryID = await _itemRepository.SearchBriefItemOrBriefRequestByCategoryID(categoryID, status, share, pageNumber, pageSize);
                if (listBriefItemByCategoryID == null)
                {
                    listBriefItemByCategoryID = new List<BriefItem>();
                }
                return listBriefItemByCategoryID;
            }
            catch (Exception ex)
            {
                ErrorMessage += ex.Message;
                return null;
            }
        }
        public async Task<bool> DeleteItem(DeleteItemVM itemVM)
        {
            try
            {
                int check = await _itemRepository.DeleteItem(itemVM);
                if (check <= 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex) 
            {
                ErrorMessage = ex.Message;
                return false;
            }

        }
        public async Task<bool> UpdateItem(UpdateItemVM itemVM)
        {
            try
            {
                DateTime dateTimeUpdate = DateTime.Now;
                DateTime? dateTimeExpired = null;
                try
                {
                    if (!string.IsNullOrEmpty(itemVM.StringDateTimeExpired) && !string.IsNullOrWhiteSpace(itemVM.StringDateTimeExpired) && itemVM.Share == false)
                    {
                        dateTimeExpired = DateTime.Parse(itemVM.StringDateTimeExpired);
                        bool checkDate = dateTimeExpired > dateTimeUpdate;
                        if (checkDate == false)
                        {
                            return false;
                        }
                    }
                }
                catch
                {
                    return false;
                }
                if (itemVM.MaxAge < itemVM.MinAge || itemVM.MaxWeight < itemVM.MinWeight || itemVM.MaxHeight < itemVM.MinHeight || itemVM.MinAge < 0 || itemVM.MinWeight < 0 || itemVM.MinHeight < 0)
                {
                    return false;
                }
                if (_JsonToObj.TransformLocation(itemVM.ItemShippingAddress) == null)
                {
                    return false;
                }
                else
                {
                    itemVM.ItemShippingAddress = _JsonToObj.TransformLocation(itemVM.ItemShippingAddress);
                    int check = await _itemRepository.UpdateItem(itemVM, dateTimeUpdate, dateTimeExpired);
                    if (check <= 0)
                    {
                        return false;
                    }
                    return true;
                }

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }
        public async Task<List<BriefItem>?> GetAllMyBriefItemAndBriefRequest(int userID, bool share)
        {
            try
            {
                List<BriefItem>? listBriefItemAndBriefRequestByUserID;
                listBriefItemAndBriefRequestByUserID = await _itemRepository.GetAllMyBriefItemAndBriefRequest(userID, share);
                if (listBriefItemAndBriefRequestByUserID == null)
                {
                    listBriefItemAndBriefRequestByUserID = new List<BriefItem>();
                }
                return listBriefItemAndBriefRequestByUserID;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        public async Task<List<BriefItem>?> GetAllMyBriefItemAndBriefRequestActiveandUnActive(int userID, bool share, bool status)
        {
            try
            {
                List<BriefItem>? listBriefItemAndBriefRequestByUserID = new();
                listBriefItemAndBriefRequestByUserID = await _itemRepository.GetAllMyBriefItemAndBriefRequestActiveandUnActive(userID, share, status);
                return listBriefItemAndBriefRequestByUserID;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        public async Task<ListVM<BriefItem>?> GetAllMyShareAndRequest(int userID, bool share, bool status, int pageNumber, int pageSize)
        {
            try
            {
                ListVM<BriefItem>? listVM = await _itemRepository.GetAllMyShareAndRequest(userID, share, status, pageNumber, pageSize);
                if (listVM == null)
                {
                    listVM = new ListVM<BriefItem>(0, 0, new List<BriefItem>());
                }
                return listVM;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        public async Task<ListVM<BriefItem>?> GetAllShareFree(int pageNumber, int pageSize)
        {
            try
            {
                ListVM<BriefItem>? listVM = await _itemRepository.GetAllShareFree(pageNumber, pageSize);
                if (listVM == null)
                {
                    listVM = new ListVM<BriefItem>(0, 0, new List<BriefItem>());
                }
                return listVM;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        public async Task<ListVM<BriefItem>?> GetAllShareRecently(int pageNumber, int pageSize)
        {
            try
            {
                ListVM<BriefItem>? listVM = await _itemRepository.GetAllShareRecently(pageNumber, pageSize);
                if (listVM == null)
                {
                    listVM = new ListVM<BriefItem>(0,0, new List<BriefItem>());
                }
                return listVM;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        public async Task<ListVM<BriefItem>?> GetAllShareNearYou(string location, int pageNumber, int pageSize, int userID)
        {
            try
            {
                Location locationObject = JsonConvert.DeserializeObject<Location>(location);
                string city = "addressProvince:" + locationObject.AddressProvince;
                ListVM<BriefItem>? listVM = await _itemRepository.GetAllShareNearYou(city, pageNumber, pageSize, userID);
                if (listVM == null)
                {
                    listVM = new ListVM<BriefItem>(0, 0, new List<BriefItem>());
                }
                return listVM;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        public async Task<ListVM<BriefItem>?> GetItemDynamicFilters(DynamicFilterItemVM dynamicFilterVM)
        {
            try
            {
                
                ListVM<BriefItem>? listVM = await _itemRepository.GetItemDynamicFilters(dynamicFilterVM);
                if (listVM == null)
                {
                    listVM = new ListVM<BriefItem>(0, 0, new List<BriefItem>());
                }
                return listVM;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        public async Task<ListVM<BriefItem>?> GetListAllOtherPersonRequestItem(bool share, bool status, int userID, int pageNumber, int pageSize)
        {
            try
            {
                ListVM<BriefItem>? listVM = await _itemRepository.GetListAllOtherPersonRequestItem(share, status, userID, pageNumber, pageSize);
                if (listVM == null)
                {
                    listVM = new ListVM<BriefItem>(0, 0, new List<BriefItem>());
                }
                return listVM;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        public async Task<ListVM<BriefItem>?> GetListAllMyRequestItem(bool share, bool status, int userID, int pageNumber, int pageSize)
        {
            try
            {
                ListVM<BriefItem>? listVM = await _itemRepository.GetListAllMyRequestItem(share, status, userID, pageNumber, pageSize);
                if (listVM == null)
                {
                    listVM = new ListVM<BriefItem>(0, 0, new List<BriefItem>());
                }
                return listVM;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        public async Task<bool> RecordUserSearch(RecordSearchVM recordSearchVM)
        {
            try
            {
                if (!(recordSearchVM.CategoryId == null && recordSearchVM.SubCategoryId == null && string.IsNullOrEmpty(recordSearchVM.TitleName) && string.IsNullOrWhiteSpace(recordSearchVM.TitleName)))
                {
                    int check = await _itemRepository.RecordUserSearch(recordSearchVM);
                    if (check <= 0)
                    {
                        return false;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }
        public async Task<ListVM<BriefItem>?> GetListRecommend(int userID, int pageNumber, int pageSize)
        {
            try
            {
                ListVM<BriefItem>? listVM;
                listVM = await _itemRepository.GetListRecommend(userID, pageNumber, pageSize);
                if (listVM == null)
                {
                    listVM = new ListVM<BriefItem>(0, 0, new List<BriefItem>());
                }
                return listVM;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        public async Task<ListVM<BriefItem>?> GetListRecommendByBaby(int babyID, int userID, int pageNumber, int pageSize, bool age, bool weight, bool height)
        {
            try
            {
                ListVM<BriefItem>? listVM;
                listVM = await _itemRepository.GetListRecommendByBaby(babyID, userID, pageNumber, pageSize, age, weight, height);
                if (listVM == null)
                {
                    listVM = new ListVM<BriefItem>(0, 0, new List<BriefItem>());
                }
                return listVM;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
    }
}

