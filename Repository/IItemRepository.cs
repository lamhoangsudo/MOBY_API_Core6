using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public interface IItemRepository
    {
        Task<bool> CreateItem(CreateItemVM itemVM);

        Task<bool> UpdateItem(UpdateItemVM itemVM);

        Task<List<BriefItem>> GetAllBriefItemAndBriefRequest(bool share);

        Task<List<BriefItem>> GetBriefItemByUserID(int userID);

        Task<List<BriefItem>> SearchBriefItemByTitle(string itemTitle);

        Task<List<BriefItem>> SearchBriefItemBySubCategoryID(int subCategoryID);

        Task<List<BriefItem>> SearchBriefItemByCategoryID(int categoryID);

        Task<DetailItem> GetItemDetail(int itemID);

        Task<DetailItemRequest> GetRequestDetail(int itemID);

        Task<bool> DeleteItem(DeleteItemVM itemVM);

        Task<List<BriefItem>> GetAllBriefItemAndBriefRequestByUserID(int userID, bool share);
    }
}
