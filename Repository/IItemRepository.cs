using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public interface IItemRepository
    {
        Task<bool> CreateItem(CreateItemVM itemVM);

        Task<bool> UpdateItem(UpdateItemVM itemVM);

        Task<List<BriefItem>?> GetAllBriefItemAndBriefRequest(bool share, bool status);

        Task<List<BriefItem>?> GetBriefItemByAndBriefRequestUserID(int userID, bool status);

        Task<List<BriefItem>?> SearchBriefItemByTitle(string itemTitle, bool status);

        Task<List<BriefItem>?> SearchBriefItemBySubCategoryID(int subCategoryID, bool status);

        Task<List<BriefItem>?> SearchBriefItemByCategoryID(int categoryID, bool status);

        Task<DetailItem?> GetItemDetail(int itemID);

        Task<DetailItemRequest?> GetRequestDetail(int itemID);

        Task<bool> DeleteItem(DeleteItemVM itemVM);

        Task<List<BriefItem>?> GetAllMyBriefItemAndBriefRequest(int userID, bool share);

        Task<List<BriefItem>?> GetAllMyBriefItemAndBriefRequestActiveandUnActive(int userID, bool share, bool status);
    }
}
