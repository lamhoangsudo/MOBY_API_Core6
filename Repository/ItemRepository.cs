using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public class ItemRepository : IItemRepository
    {
        public static string errorMessage;
        private readonly MOBYContext _context;
        public ItemRepository(MOBYContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateItem(CreateItemVM itemVM)
        {
            try
            {
                var checkUserExist = _context.UserAccounts.Where(u => u.UserId == itemVM.userId).ToList().SingleOrDefault();
                var checkSubCategoryExists = _context.SubCategories.Where(sc => sc.SubCategoryId == itemVM.subCategoryId).ToList().SingleOrDefault();
                if (checkSubCategoryExists == null || checkUserExist == null)
                {
                    errorMessage = "danh mục or tài khoản bạn nhập không tồn tại";
                    return false;
                }
                else
                {
                    DateTime dateTimeCreate = DateTime.Now;
                    DateTime? dateTimeExpired = null;
                    if (!string.IsNullOrEmpty(itemVM.stringDateTimeExpired) && !string.IsNullOrWhiteSpace(itemVM.stringDateTimeExpired))
                    {
                        try
                        {
                            dateTimeExpired = DateTime.Parse(itemVM.stringDateTimeExpired);
                            bool check = dateTimeExpired > dateTimeCreate;
                            if (check == false)
                            {
                                errorMessage = "bạn đã nhập ngày hết hạn sau ngày tạo";
                                return false;
                            }
                        }
                        catch
                        {
                            errorMessage = "bạn đã nhập sai format ngày, fotmat của chúng tôi là yyyy/mm/dd";
                            return false;
                        }

                    }
                    string itemCode = Guid.NewGuid().ToString();
                    Models.Item item = new Models.Item();
                    item.UserId = itemVM.userId;
                    item.ItemCode = itemCode;
                    item.SubCategoryId = itemVM.subCategoryId;
                    item.ItemTitle = itemVM.itemTitle;
                    item.ItemDetailedDescription = itemVM.itemDetailedDescription;
                    item.ItemMass = itemVM.itemMass;
                    item.ItemSize = itemVM.itemSize;
                    item.ItemQuanlity = itemVM.itemQuanlity;
                    item.ItemEstimateValue = itemVM.itemEstimateValue;
                    item.ItemSalePrice = itemVM.itemSalePrice;
                    item.ItemShareAmount = itemVM.itemShareAmount;
                    item.ItemSponsoredOrderShippingFee = itemVM.itemSponsoredOrderShippingFee;
                    item.ItemExpiredTime = dateTimeExpired;
                    item.ItemShippingAddress = itemVM.itemShippingAddress;
                    item.ItemDateCreated = dateTimeCreate;
                    item.ItemStatus = true;
                    item.Share = itemVM.share;
                    item.Image = itemVM.image;
                    _context.Items.Add(item);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                errorMessage = "có lỗi khi tạo sản phẩm này" + ex.Message;
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

        public async Task<bool> DeleteItem(DeleteItemVM itemVM)
        {
            try
            {
                bool check = _context.CartDetails.Where(cd => cd.ItemId == itemVM.itemID).Any();
                if (check)
                {
                    return false;
                }
                else
                {
                    var item = _context.Items.Where(it => it.ItemId == itemVM.itemID && it.UserId == itemVM.userID).ToList().SingleOrDefault();
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

        public async Task<bool> UpdateItem(UpdateItemVM itemVM)
        {
            try
            {
                bool checkCurrentItem = _context.CartDetails.Where(cd => cd.ItemId == itemVM.itemID).Any();
                if (checkCurrentItem)
                {
                    errorMessage = "danh mục or tài khoản bạn nhập không tồn tại";
                    return false;
                }
                else
                {
                    Models.Item currentItem = _context.Items.Where(it => it.ItemId == itemVM.itemID && it.UserId == itemVM.userId).ToList().SingleOrDefault();
                    var checkSubCategoryExists = _context.SubCategories.Where(sc => sc.SubCategoryId == itemVM.subCategoryId).ToList().SingleOrDefault();
                    if (currentItem == null || currentItem.ItemStatus == false || checkSubCategoryExists == null)
                    {
                        return false;
                    }
                    else
                    {
                        DateTime dateTimeUpdate = DateTime.Now;
                        DateTime? dateTimeExpired = null;
                        try
                        {
                            if (!string.IsNullOrEmpty(itemVM.stringDateTimeExpired) && !string.IsNullOrWhiteSpace(itemVM.stringDateTimeExpired))
                            {
                                dateTimeExpired = DateTime.Parse(itemVM.stringDateTimeExpired);
                                bool checkDate = dateTimeExpired > dateTimeUpdate;
                                if (checkDate == false)
                                {
                                    errorMessage = "bạn đã nhập ngày hết hạn sau ngày cập nhật";
                                    return false;
                                }
                            }
                        }
                        catch
                        {
                            errorMessage = "bạn đã nhập sai format ngày, fotmat của chúng tôi là yyyy/mm/dd";
                            return false;
                        }
                        currentItem.SubCategoryId = itemVM.subCategoryId;
                        currentItem.ItemTitle = itemVM.itemTitle;
                        currentItem.ItemDetailedDescription = itemVM.itemDetailedDescription;
                        currentItem.ItemMass = itemVM.itemMass;
                        currentItem.ItemSize = itemVM.itemSize;
                        currentItem.ItemQuanlity = itemVM.itemQuanlity;
                        currentItem.ItemEstimateValue = itemVM.itemEstimateValue;
                        currentItem.ItemSalePrice = itemVM.itemSalePrice;
                        currentItem.ItemShareAmount = itemVM.itemShareAmount;
                        currentItem.ItemSponsoredOrderShippingFee = itemVM.itemSponsoredOrderShippingFee;
                        currentItem.ItemShippingAddress = itemVM.itemShippingAddress;
                        currentItem.Image = itemVM.image;
                        currentItem.ItemExpiredTime = dateTimeExpired;
                        currentItem.ItemDateUpdate = dateTimeUpdate;
                        currentItem.Share = itemVM.share;
                        _context.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = "có lỗi khi tạo sản phẩm này" + ex.Message;
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
