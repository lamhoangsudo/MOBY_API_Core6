using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly MOBYContext _context;
        public ItemRepository(MOBYContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateItem(int userId, int subCategoryId, string itemTitle, string itemDetailedDescription, double itemMass,
            bool itemSize, string itemStatus, double itemEstimateValue, double itemSalePrice, int itemShareAmount,
            bool itemSponsoredOrderShippingFee, string itemShippingAddress, int imageId, string stringDateTimeExpired)
        {
            try
            {
                var checkUserExist = _context.CheckUserExists.FromSqlInterpolated($"EXEC check_UserExists {userId}").ToList().SingleOrDefault();
                var checkSubCategoryExists = _context.CheckSubCategoryExists.FromSqlInterpolated($"EXEC check_SubCategoryExistsFromItem {subCategoryId}").ToList().SingleOrDefault();
                var checkImageExists = _context.CheckImageExists.FromSqlInterpolated($"EXEC check_ImangeExists {imageId}").ToList().SingleOrDefault();
                if (checkSubCategoryExists == null || checkImageExists == null || checkUserExist == null)
                {
                    return false;
                }
                else
                {
                    var checkImage = _context.ItemCheckImageDulicates.FromSqlInterpolated($"EXEC check_ImangeFromItem {imageId}").ToList().SingleOrDefault();
                    if (checkImage != null)
                    {
                        return false;
                    }
                    else
                    {
                        DateTime dateTimeCreate = DateTime.Now;
                        DateTime dateTimeExpired = DateTime.Parse(stringDateTimeExpired);
                        bool check = dateTimeExpired > dateTimeCreate;
                        if (check == false)
                        {
                            return false;
                        }
                        string itemCode =  Guid.NewGuid().ToString();
                        var checkCreate = _context.Database.ExecuteSqlInterpolated($"EXEC create_Item {itemCode}, {userId}, {subCategoryId}, {itemTitle}, {itemDetailedDescription}, {itemMass}, {itemSize}, {itemStatus}, {itemEstimateValue}, {itemSalePrice}, {itemShareAmount}, {itemSponsoredOrderShippingFee}, {dateTimeExpired}, {itemShippingAddress}, {dateTimeCreate}, {imageId}");
                        if (checkCreate != 0)
                        {
                            _context.SaveChanges();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<BriefItem>> GetAllBriefItem()
        {
            try
            {
                var listBriefItem = _context.BriefItems.FromSqlInterpolated($"SELECT * FROM [BriefItem];").ToList();
                if (listBriefItem == null || listBriefItem.Count() == 0)
                {
                    return null;
                }
                else
                {
                    return listBriefItem;
                }
            }
            catch 
            {
                return null;
            }
        }

        public async Task<List<BriefItem>> SearchBriefItemByTitle(string itemTitle)
        {
            try
            {
                var listBriefItemByTitle = _context.BriefItems.FromSqlInterpolated($"EXEC search_BriefItemByTitle {itemTitle}").ToList();
                if (listBriefItemByTitle == null || listBriefItemByTitle.Count() == 0)
                {
                    return null;
                }
                else
                {
                    return listBriefItemByTitle;
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<BriefItem>> GetBriefItemByUserID(int userID)
        {
            try
            {
                var listBriefItemByUserID = _context.BriefItems.FromSqlInterpolated($"EXEC get_BriefItemByUserID {userID}").ToList();
                if (listBriefItemByUserID == null || listBriefItemByUserID.Count() == 0)
                {
                    return null;
                }
                else
                {
                    return listBriefItemByUserID;
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<DetailItem> GetItemDetail(int itemID)
        {
            try
            {
                DetailItem itemDetail = _context.DetailItems.FromSqlInterpolated($"EXEC get_DetailItem {itemID}").ToList().FirstOrDefault();
                if (itemDetail == null)
                {
                    return null;
                }
                else
                {
                    return itemDetail;
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<BriefItem>> SearchBriefItemBySubCategoryID(int subCategoryID)
        {
            try
            {
                var listBriefItemBySubCategoryID = _context.BriefItems.FromSqlInterpolated($"EXEC search_BriefItemBySubCategoryID {subCategoryID}").ToList();
                if (listBriefItemBySubCategoryID == null || listBriefItemBySubCategoryID.Count() == 0)
                {
                    return null;
                }
                else
                {
                    return listBriefItemBySubCategoryID;
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<BriefItem>> SearchBriefItemByCategoryID(int categoryID)
        {
            try
            {
                var listBriefItemByCategoryID = _context.BriefItems.FromSqlInterpolated($"EXEC search_BriefItemByCategoryID {categoryID}").ToList();
                if (listBriefItemByCategoryID == null || listBriefItemByCategoryID.Count() == 0)
                {
                    return null;
                }
                else
                {
                    return listBriefItemByCategoryID;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
