using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public interface IItemRepository
    {
        Task<bool> CreateItem(int userId, int subCategoryId, string itemTitle, string itemDetailedDescription, double itemMass, bool itemSize, string itemStatus, double itemEstimateValue, double itemSalePrice, int itemShareAmount, bool itemSponsoredOrderShippingFee, string itemShippingAddress, string image, string stringDateTimeExpired);

        Task<List<BriefItem>> GetAllBriefItem();

        Task<List<BriefItem>> GetBriefItemByUserID(int userID);

        Task<List<BriefItem>> SearchBriefItemByTitle(string itemTitle);

        Task<List<BriefItem>> SearchBriefItemBySubCategoryID(int subCategoryID);

        Task<List<BriefItem>> SearchBriefItemByCategoryID(int categoryID);

        Task<DetailItem> GetItemDetail(int itemID);
    }
}
