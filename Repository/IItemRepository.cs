using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public interface IItemRepository
    {
        Task<bool> CreateItem(CreateItemVM itemVM);

        Task<bool> UpdateItem(UpdateItemVM itemVM);

        Task<List<BriefItem>?> GetAllBriefItemAndBriefRequest(bool share, bool status, int pageNumber, int pageSize);

        Task<List<BriefItem>?> GetBriefItemByOrBriefRequestUserID(int userID, bool status, bool share, int pageNumber, int pageSize);

        Task<List<BriefItem>?> SearchBriefItemByTitle(string itemTitle, bool status);

        Task<List<BriefItem>?> SearchBriefItemByOrBriefRequestBySubCategoryID(int subCategoryID, bool status, bool share, int pageNumber, int pageSize);

        Task<List<BriefItem>?> SearchBriefItemOrBriefRequestByCategoryID(int categoryID, bool status, bool share, int pageNumber, int pageSize);

        Task<DetailItem?> GetItemDetail(int itemID);

        Task<DetailItemRequest?> GetRequestDetail(int itemID);

        Task<bool> DeleteItem(DeleteItemVM itemVM);

        Task<List<BriefItem>?> GetAllMyBriefItemAndBriefRequest(int userID, bool share);

        Task<List<BriefItem>?> GetAllMyBriefItemAndBriefRequestActiveandUnActive(int userID, bool share, bool status);

        Task<List<BriefItem>?> GetAllShareRecently(int pageNumber, int pageSize, int userID);

        Task<List<BriefItem>?> GetAllShareFree(int pageNumber, int pageSize, int userID);

        Task<List<BriefItem>?> GetAllMyShareAndRequest(int userID, bool share, bool status, int pageNumber, int pageSize);

        Task<List<BriefItem>?> GetAllShareNearYou(string location, int pageNumber, int pageSize, int userID);

        Task<List<BriefItem>?> GetItemDynamicFilters(int pageNumber, int pageSize, DynamicFilterVM dynamicFilterVM);

        Task<List<BriefItem>?> GetListAllOtherPersonRequestItem(bool share, bool status, int userID, int pageNumber, int pageSize);

        Task<List<BriefItem>?> GetListAllMyRequestItem(bool share, bool status, int userID, int pageNumber, int pageSize);
        public Task<List<int>> getListItemIDByUserID(int userId);
        public Task<int> getQuantityByItemID(int itemID);
    }
}
