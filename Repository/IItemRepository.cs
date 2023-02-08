using Item.Models;
using MOBY_API_Core6.Models;

namespace Item.Repository
{
    public interface IItemRepository
    {
        bool CreateItem(int userId, int subCategoryId, string itemTitle, string itemDetailedDescription, double itemMass, bool itemSize, string itemStatus, double itemEstimateValue, double itemSalePrice, int itemShareAmount, bool itemSponsoredOrderShippingFee, string itemShippingAddress, int imageId, string stringDateTimeExpired);

        List<BriefItem> GetAllBriefItem();

        List<BriefItem> GetBriefItemByUserID(int userID);

        List<BriefItem> SearchBriefItemByTitle(string itemTitle);

        List<BriefItem> SearchBriefItemBySubCategoryID(int subCategoryID);

        List<BriefItem> SearchBriefItemByCategoryID(int categoryID);

        DetailItem GetItemDetail(int itemID);
    }
}
