using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public class ItemRepository : IItemRepository
    {
        public static string? errorMessage;
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
                    return await Task.FromResult(false);
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
                                return await Task.FromResult(false);
                            }
                        }
                        catch
                        {
                            errorMessage = "bạn đã nhập sai format ngày, fotmat của chúng tôi là yyyy/mm/dd";
                            return await Task.FromResult(false);
                        }

                    }
                    string itemCode = Guid.NewGuid().ToString();
                    Models.Item item = new()
                    {
                        UserId = itemVM.userId,
                        ItemCode = itemCode,
                        SubCategoryId = itemVM.subCategoryId,
                        ItemTitle = itemVM.itemTitle,
                        ItemDetailedDescription = itemVM.itemDetailedDescription,
                        ItemMass = itemVM.itemMass,
                        ItemSize = itemVM.itemSize,
                        ItemQuanlity = itemVM.itemQuanlity,
                        ItemEstimateValue = itemVM.itemEstimateValue,
                        ItemSalePrice = itemVM.itemSalePrice,
                        ItemShareAmount = itemVM.itemShareAmount,
                        ItemSponsoredOrderShippingFee = itemVM.itemSponsoredOrderShippingFee,
                        ItemExpiredTime = dateTimeExpired,
                        ItemShippingAddress = itemVM.itemShippingAddress,
                        ItemDateCreated = dateTimeCreate,
                        ItemStatus = true,
                        Share = itemVM.share,
                        Image = itemVM.image
                    };
                    _context.Items.Add(item);
                    _context.SaveChanges();
                    return await Task.FromResult(true);
                }
            }
            catch (Exception ex)
            {
                errorMessage = "có lỗi khi tạo sản phẩm này" + ex.Message;
                return await Task.FromResult(false);
            }
        }

        public async Task<List<BriefItem>?> GetAllBriefItemAndBriefRequest(bool share, bool status)
        {
            try
            {
                var listBriefItem = _context.BriefItems.Where(bf => bf.Share == share && bf.ItemStatus == status).ToList();
                if (listBriefItem == null || listBriefItem.Count == 0)
                {
                    return null;
                }
                else
                {
                    return await Task.FromResult(listBriefItem);
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<BriefItem>?> SearchBriefItemByTitle(string itemTitle, bool status)
        {
            try
            {
                var listBriefItemByTitle = _context.BriefItems.Where(bf => bf.ItemTitle.Equals(itemTitle)).ToList();
                if (listBriefItemByTitle == null || listBriefItemByTitle.Count == 0)
                {
                    return null;
                }
                else
                {
                    return await Task.FromResult(listBriefItemByTitle);
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<BriefItem>?> GetBriefItemByAndBriefRequestUserID(int userID, bool status)
        {
            try
            {
                var listBriefItemByUserID = _context.BriefItems.Where(bf => bf.UserId == userID && bf.ItemStatus == status).ToList();
                if (listBriefItemByUserID == null || listBriefItemByUserID.Count == 0)
                {
                    return null;
                }
                else
                {
                    return await Task.FromResult(listBriefItemByUserID);
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<BriefItem>?> GetBriefItemByShare(bool share, bool status)
        {
            try
            {
                var listBriefItemByUserID = _context.BriefItems.Where(bf => bf.Share == share && bf.ItemStatus == status).ToList();
                if (listBriefItemByUserID == null || listBriefItemByUserID.Count == 0)
                {
                    return null;
                }
                else
                {
                    return await Task.FromResult(listBriefItemByUserID);
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<DetailItem?> GetItemDetail(int itemID)
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
                    return await Task.FromResult(itemDetail);
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<DetailItemRequest?> GetRequestDetail(int itemID)
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
                    return await Task.FromResult(detailItemRequest);
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<BriefItem>?> SearchBriefItemBySubCategoryID(int subCategoryID, bool status)
        {
            try
            {
                var listBriefItemBySubCategoryID = _context.BriefItems.Where(bf => bf.SubCategoryId == subCategoryID && bf.ItemStatus == status).ToList();
                if (listBriefItemBySubCategoryID == null || listBriefItemBySubCategoryID.Count == 0)
                {
                    return null;
                }
                else
                {
                    return await Task.FromResult(listBriefItemBySubCategoryID);
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<BriefItem>?> SearchBriefItemByCategoryID(int categoryID, bool status)
        {
            try
            {
                var listBriefItemByCategoryID = _context.BriefItems.Where(bf => bf.CategoryId == categoryID && bf.ItemStatus == status).ToList();
                if (listBriefItemByCategoryID == null || listBriefItemByCategoryID.Count == 0)
                {
                    return null;
                }
                else
                {
                    return await Task.FromResult(listBriefItemByCategoryID);
                }
            }
            catch
            {
                return null;
            }
        }

        public Task<bool> DeleteItem(DeleteItemVM itemVM)
        {
            try
            {
                bool check = _context.CartDetails.Where(cd => cd.ItemId == itemVM.itemID).Any();
                if (check)
                {
                    errorMessage = "sản phẩm của bạn đang có người muốn nhận nên bạn không thể cập nhật sản phẩm này";
                    return Task.FromResult(false);
                }
                else
                {
                    var item = _context.Items.Where(it => it.ItemId == itemVM.itemID && it.UserId == itemVM.userID).ToList().SingleOrDefault();
                    if (item != null)
                    {
                        item.ItemStatus = false;
                        _context.SaveChanges();
                        return Task.FromResult(true);
                    }
                    else
                    {
                        errorMessage = "sản phẩm này của bạn không còn tồn tại trong dữ liệu";
                        return Task.FromResult(false);
                    }
                }
            }
            catch
            {
                return Task.FromResult(false);
            }

        }

        public async Task<bool> UpdateItem(UpdateItemVM itemVM)
        {
            try
            {
                bool checkCurrentItem = _context.CartDetails.Where(cd => cd.ItemId == itemVM.itemID).Any();
                if (checkCurrentItem)
                {
                    errorMessage = "sản phẩm của bạn đang có người muốn nhận nên bạn không thể cập nhật sản phẩm này";
                    return false;
                }
                else
                {
                    Models.Item currentItem = _context.Items.Where(it => it.ItemId == itemVM.itemID && it.UserId == itemVM.userId).ToList().SingleOrDefault();
                    var checkSubCategoryExists = _context.SubCategories.Where(sc => sc.SubCategoryId == itemVM.subCategoryId).ToList().SingleOrDefault();
                    if (currentItem == null || currentItem.ItemStatus == false)
                    {
                        errorMessage = "sản phẩm này của bạn không còn tồn tại trong dữ liệu";
                        return false;
                    }
                    if (checkSubCategoryExists == null)
                    {
                        errorMessage = "danh mục bạn chọn không còn tồn tại trong dữ liệu, mong bạn chọn danh mục khác";
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
                        await Task.CompletedTask;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = "có lỗi khi cập nhật sản phẩm này" + ex.Message;
                return false;
            }
        }

        public Task<List<BriefItem>?> GetAllMyBriefItemAndBriefRequest(int userID, bool share)
        {
            try
            {
                var listBriefItemAndBriefRequestByUserID = _context.BriefItems.Where(bf => bf.Share == share && bf.UserId == userID).ToList();
                if (listBriefItemAndBriefRequestByUserID == null || listBriefItemAndBriefRequestByUserID.Count == 0)
                {
                    return null;
                }
                else
                {
                    return Task.FromResult(listBriefItemAndBriefRequestByUserID);
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
