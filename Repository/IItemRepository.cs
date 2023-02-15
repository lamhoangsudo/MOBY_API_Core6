using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public interface IItemRepository
    {
        Task<bool> CreateItem(int userId, int subCategoryId, string itemTitle, string itemDetailedDescription, double itemMass, bool itemSize, string itemQuanlity, double itemEstimateValue, double itemSalePrice, int itemShareAmount, bool itemSponsoredOrderShippingFee, string itemShippingAddress, string image, string stringDateTimeExpired, bool share);

        Task<bool> UpdateItem(int userId, int itemID, int subCategoryId, string itemTitle, string itemDetailedDescription, double itemMass, bool itemSize, string itemQuanlity, double itemEstimateValue, double itemSalePrice, int itemShareAmount, bool itemSponsoredOrderShippingFee, string itemShippingAddress, string image, string stringDateTimeExpired, bool share);

        Task<List<BriefItem>> GetAllBriefItemAndBriefRequest(bool share);

        Task<List<BriefItem>> GetBriefItemByUserID(int userID);

        Task<List<BriefItem>> SearchBriefItemByTitle(string itemTitle);

        Task<List<BriefItem>> SearchBriefItemBySubCategoryID(int subCategoryID);

        Task<List<BriefItem>> SearchBriefItemByCategoryID(int categoryID);

        Task<DetailItem> GetItemDetail(int itemID);

        Task<DetailItemRequest> GetRequestDetail(int itemID);

        Task<bool> DeleteItem(int itemID, int userID);

        Task<List<BriefItem>> GetAllBriefItemAndBriefRequestByUserID(int userID, bool share);
    }
}
