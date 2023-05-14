using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository.IRepository
{
    public interface IItemRepository
    {
        Task<int> CreateItem(CreateItemVM itemVM, DateTime dateTimeCreate, DateTime? dateTimeExpired);
        Task<List<int>> GetListItemIDByUserID(int userId);
        Task<int> GetQuantityByItemID(int itemID);
        Task<List<BriefItem>?> GetAllBriefItemAndBriefRequest(bool share, bool status, int pageNumber, int pageSize);
        Task<List<BriefItem>?> SearchBriefItemByTitle(string itemTitle, bool status);
        Task<List<BriefItem>?> GetBriefItemByOrBriefRequestUserID(int userID, bool status, bool share, int pageNumber, int pageSize);
        Task<List<BriefItem>?> GetBriefItemByShare(bool share, bool status);
        Task<DetailItemVM?> GetItemDetail(int itemID);
        Task<DetailItemRequestVM?> GetRequestDetail(int itemID);
        Task<List<BriefItem>?> SearchBriefItemByOrBriefRequestBySubCategoryID(int subCategoryID, bool status, bool share, int pageNumber, int pageSize);
        Task<List<BriefItem>?> SearchBriefItemOrBriefRequestByCategoryID(int categoryID, bool status, bool share, int pageNumber, int pageSize);
        Task<int> DeleteItem(DeleteItemVM itemVM);
        Task<int> UpdateItem(UpdateItemVM itemVM, DateTime dateTimeUpdate, DateTime? dateTimeExpired);
        Task<List<BriefItem>?> GetAllMyBriefItemAndBriefRequest(int userID, bool share);
        Task<List<BriefItem>?> GetAllMyBriefItemAndBriefRequestActiveandUnActive(int userID, bool share, bool status);
        Task<ListVM<BriefItem>?> GetAllMyShareAndRequest(int userID, bool share, bool status, int pageNumber, int pageSize);
        Task<ListVM<BriefItem>?> GetAllShareFree(int pageNumber, int pageSize);
        Task<ListVM<BriefItem>?> GetAllShareRecently(int pageNumber, int pageSize);
        Task<ListVM<BriefItem>?> GetAllShareNearYou(string city, int pageNumber, int pageSize, int userID);
        Task<bool> DeactivateAllExpiredProducts();
        Task<ListVM<BriefItem>?> GetItemDynamicFilters(DynamicFilterItemVM dynamicFilterVM);
        Task<ListVM<BriefItem>?> GetListAllOtherPersonRequestItem(bool share, bool status, int userID, int pageNumber, int pageSize);
        Task<ListVM<BriefItem>?> GetListAllMyRequestItem(bool share, bool status, int userID, int pageNumber, int pageSize);
        Task<int> RecordUserSearch(RecordSearchVM recordSearchVM);
        Task<ListVM<BriefItem>?> GetListRecommend(int userID, int pageNumber, int pageSize);
        Task<ListVM<BriefItem>?> GetListRecommendByBaby(int babyID, int userID, int pageNumber, int pageSize, bool age, bool weight, bool height);
    }
}
