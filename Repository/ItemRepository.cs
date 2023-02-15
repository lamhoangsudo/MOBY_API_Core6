using MOBY_API_Core6.Data_View_Model;
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
            bool itemSize, string itemQuanlity, double itemEstimateValue, double itemSalePrice, int itemShareAmount,
            bool itemSponsoredOrderShippingFee, string itemShippingAddress, string image, string stringDateTimeExpired, bool share)
        {
            try
            {
                var checkUserExist = _context.UserAccounts.Where(u => u.UserId == userId).ToList().SingleOrDefault();
                var checkSubCategoryExists = _context.SubCategories.Where(sc => sc.SubCategoryId == subCategoryId).ToList().SingleOrDefault();
                if (checkSubCategoryExists == null || checkUserExist == null)
                {
                    return false;
                }
                else
                {
                    DateTime dateTimeCreate = DateTime.Now;
                    DateTime? dateTimeExpired = null;
                    if (stringDateTimeExpired != null || !(stringDateTimeExpired.Equals("")))
                    {
                        dateTimeExpired = DateTime.Parse(stringDateTimeExpired);
                        bool check = dateTimeExpired > dateTimeCreate;
                        if (check == false)
                        {
                            return false;
                        }
                    }
                    string itemCode = Guid.NewGuid().ToString();
                    Models.Item item = new Models.Item();
                    item.UserId = userId;
                    item.ItemCode = itemCode;
                    item.SubCategoryId = subCategoryId;
                    item.ItemTitle = itemTitle;
                    item.ItemDetailedDescription = itemDetailedDescription;
                    item.ItemMass = itemMass;
                    item.ItemSize = itemSize;
                    item.ItemQuanlity = itemQuanlity;
                    item.ItemEstimateValue = itemEstimateValue;
                    item.ItemSalePrice = itemSalePrice;
                    item.ItemShareAmount = itemShareAmount;
                    item.ItemSponsoredOrderShippingFee = itemSponsoredOrderShippingFee;
                    item.ItemExpiredTime = dateTimeExpired;
                    item.ItemShippingAddress = itemShippingAddress;
                    item.ItemDateCreated = dateTimeCreate;
                    item.ItemStatus = true;
                    item.Share = share;
                    item.Image = image;
                    _context.Items.Add(item);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<BriefItem>> GetAllBriefItemAndBriefRequest(bool share)
        {
            try
            {
                var listBriefItem = _context.BriefItems.Where(bf => bf.Share == share).ToList();
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
                var listBriefItemByTitle = _context.BriefItems.Where(bf => bf.ItemTitle.Equals(itemTitle)).ToList();
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
                var listBriefItemByUserID = _context.BriefItems.Where(bf => bf.UserId == userID).ToList();
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

        public async Task<List<BriefItem>> GetBriefItemByShare(bool share)
        {
            try
            {
                var listBriefItemByUserID = _context.BriefItems.Where(bf => bf.Share == share).ToList();
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
                DetailItem itemDetail = _context.DetailItems.Where(di => di.ItemId == itemID).ToList().FirstOrDefault();
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

        public async Task<DetailItemRequest> GetRequestDetail(int itemID)
        {
            try
            {
                DetailItemRequest detailItemRequest = _context.DetailItemRequests.Where(dir => dir.ItemId == itemID).FirstOrDefault();
                if (detailItemRequest == null)
                {
                    return null;
                }
                else
                {
                    return detailItemRequest;
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
                var listBriefItemBySubCategoryID = _context.BriefItems.Where(bf => bf.SubCategoryId == subCategoryID).ToList();
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
                var listBriefItemByCategoryID = _context.BriefItems.Where(bf => bf.CategoryId == categoryID).ToList();
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

        public async Task<bool> DeleteItem(int itemID, int userID)
        {
            try
            {
                bool check = _context.CartDetails.Where(cd => cd.ItemId == itemID).Any();
                if (check)
                {
                    return false;
                }
                else
                {
                    var item = _context.Items.Where(it => it.ItemId == itemID && it.UserId == userID).ToList().SingleOrDefault();
                    if (item != null)
                    {
                        item.ItemStatus = false;
                        _context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return false;
            }

        }

        public async Task<bool> UpdateItem(int userID, int itemID, int subCategoryId, string itemTitle, string itemDetailedDescription, double itemMass,
            bool itemSize, string itemQuanlity, double itemEstimateValue, double itemSalePrice, int itemShareAmount,
            bool itemSponsoredOrderShippingFee, string itemShippingAddress, string image, string stringDateTimeExpired, bool share)
        {
            try
            {
                bool checkCurrentItem = _context.CartDetails.Where(cd => cd.ItemId == itemID).Any();
                if (checkCurrentItem)
                {
                    return false;
                }
                else
                {
                    Models.Item currentItem = _context.Items.Where(it => it.ItemId == itemID && it.UserId == userID).ToList().SingleOrDefault();
                    var checkSubCategoryExists = _context.SubCategories.Where(sc => sc.SubCategoryId == subCategoryId).ToList().SingleOrDefault();
                    if (currentItem == null || currentItem.ItemStatus == false || checkSubCategoryExists == null)
                    {
                        return false;
                    }
                    else
                    {
                        DateTime dateTimeUpdate = DateTime.Now;
                        DateTime? dateTimeExpired = null;
                        if (stringDateTimeExpired != null || !(stringDateTimeExpired.Equals("")))
                        {
                            dateTimeExpired = DateTime.Parse(stringDateTimeExpired);
                            bool checkDate = dateTimeExpired > dateTimeUpdate;
                            if (checkDate == false)
                            {
                                return false;
                            }
                        }
                        currentItem.SubCategoryId = subCategoryId;
                        currentItem.ItemTitle = itemTitle;
                        currentItem.ItemDetailedDescription = itemDetailedDescription;
                        currentItem.ItemMass= itemMass;
                        currentItem.ItemSize= itemSize;
                        currentItem.ItemQuanlity= itemQuanlity;
                        currentItem.ItemEstimateValue= itemEstimateValue;
                        currentItem.ItemSalePrice= itemSalePrice;
                        currentItem.ItemShareAmount= itemShareAmount;
                        currentItem.ItemSponsoredOrderShippingFee= itemSponsoredOrderShippingFee;
                        currentItem.ItemShippingAddress= itemShippingAddress;
                        currentItem.Image = image;
                        currentItem.ItemExpiredTime = dateTimeExpired;
                        currentItem.ItemDateUpdate = dateTimeUpdate;
                        currentItem.Share = share;
                        _context.SaveChanges();
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<BriefItem>> GetAllBriefItemAndBriefRequestByUserID(int userID, bool share)
        {
            try
            {
                var listBriefItemAndBriefRequestByUserID = _context.BriefItems.Where(bf => bf.Share == share && bf.UserId == userID).ToList();
                if (listBriefItemAndBriefRequestByUserID == null || listBriefItemAndBriefRequestByUserID.Count() == 0)
                {
                    return null;
                }
                else
                {
                    return listBriefItemAndBriefRequestByUserID;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
