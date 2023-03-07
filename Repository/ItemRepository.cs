using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using Nancy;
using Nancy.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
                var checkUserExist = await _context.UserAccounts
                    .Where(u => u.UserId == itemVM.userId)
                    .SingleOrDefaultAsync();
                var checkSubCategoryExists = await _context.SubCategories
                    .Where(sc => sc.SubCategoryId == itemVM.subCategoryId)
                    .SingleOrDefaultAsync();
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
                    Models.Item item = new()
                    {
                        UserId = itemVM.userId,
                        ItemCode = itemCode,
                        SubCategoryId = itemVM.subCategoryId,
                        ItemTitle = itemVM.itemTitle,
                        ItemDetailedDescription = itemVM.itemDetailedDescription,
                        ItemMass = itemVM.itemMass,
                        ItemSize = itemVM.itemSize,
#pragma warning disable CS8601 // Possible null reference assignment.
                        ItemQuanlity = itemVM.itemQuanlity,
#pragma warning disable CS8601 // Possible null reference assignment.
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
                    await _context.Items.AddAsync(item);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                errorMessage = "có lỗi khi tạo sản phẩm này" + ex.Message;
                return false;
            }
        }

        public async Task<List<BriefItem>?> GetAllBriefItemAndBriefRequest(bool share, bool status, int pageNumber, int pageSize)
        {
            try
            {
                int itemsToSkip = (pageNumber - 1) * pageSize;
                List<BriefItem> listBriefItem = new List<BriefItem>();
                listBriefItem = await _context.BriefItems
                    .Where(bf => bf.Share == share && bf.ItemStatus == status)
                    .Skip(itemsToSkip)
                    .Take(pageSize)
                    .ToListAsync();
                if (listBriefItem.Count == 0)
                {
                    errorMessage = "không có dữ liệu";
                }
                return listBriefItem;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<BriefItem>?> SearchBriefItemByTitle(string itemTitle, bool status)
        {
            try
            {
                List<BriefItem> listBriefItemByTitle = new List<BriefItem>();
                listBriefItemByTitle = await _context.BriefItems
                    .Where(bf => bf.ItemTitle.Equals(itemTitle))
                    .ToListAsync();
                if (listBriefItemByTitle.Count == 0)
                {
                    errorMessage = "không có dữ liệu";
                }
                return listBriefItemByTitle;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<BriefItem>?> GetBriefItemByOrBriefRequestUserID(int userID, bool status, bool share, int pageNumber, int pageSize)
        {
            try
            {
                int itemsToSkip = (pageNumber - 1) * pageSize;
                List<BriefItem> listBriefItemByUserID = new List<BriefItem>();
                listBriefItemByUserID = await _context.BriefItems
                    .Where(bf => bf.UserId == userID && bf.ItemStatus == status && bf.Share == share)
                    .Skip(itemsToSkip)
                    .Take(pageSize)
                    .ToListAsync();
                if (listBriefItemByUserID.Count == 0)
                {
                    errorMessage = "không có dữ liệu";
                }
                return listBriefItemByUserID;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<BriefItem>?> GetBriefItemByShare(bool share, bool status)
        {
            try
            {
                List<BriefItem> listBriefItemByUserID = new List<BriefItem>();
                listBriefItemByUserID = await _context.BriefItems
                    .Where(bf => bf.Share == share && bf.ItemStatus == status)
                    .ToListAsync();
                if (listBriefItemByUserID.Count == 0)
                {
                    errorMessage = "không có dữ liệu";
                }
                return listBriefItemByUserID;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }

        public async Task<DetailItem?> GetItemDetail(int itemID)
        {
            try
            {
                DetailItem? itemDetail = await _context.DetailItems
                    .Where(di => di.ItemId == itemID)
                    .FirstOrDefaultAsync();
                return itemDetail;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }

        public async Task<DetailItemRequest?> GetRequestDetail(int itemID)
        {
            try
            {
                DetailItemRequest? detailItemRequest = await _context.DetailItemRequests
                    .Where(dir => dir.ItemId == itemID)
                    .FirstOrDefaultAsync();
                return detailItemRequest;

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<BriefItem>?> SearchBriefItemBySubCategoryID(int subCategoryID, bool status)
        {
            try
            {
                List<BriefItem> listBriefItemBySubCategoryID = new List<BriefItem>();
                listBriefItemBySubCategoryID = await _context.BriefItems.Where(bf => bf.SubCategoryId == subCategoryID
                && bf.ItemStatus == status)
                    .ToListAsync();
                if (listBriefItemBySubCategoryID.Count == 0)
                {
                    errorMessage = "không có dữ liệu";
                }
                return listBriefItemBySubCategoryID;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<BriefItem>?> SearchBriefItemOrBriefRequestByCategoryID(int categoryID, bool status, bool share, int pageNumber, int pageSize)
        {
            try
            {
                int itemsToSkip = (pageNumber - 1) * pageSize;
                List<BriefItem> listBriefItemByCategoryID = new List<BriefItem>();
                listBriefItemByCategoryID = await _context.BriefItems
                    .Where(bf => bf.CategoryId == categoryID 
                    && bf.ItemStatus == status
                    && bf.Share == share)
                    .Skip(itemsToSkip)
                    .Take(pageSize)
                    .ToListAsync();
                if (listBriefItemByCategoryID.Count == 0)
                {
                    errorMessage = "không có dữ liệu";
                }
                return listBriefItemByCategoryID;
            }
            catch (Exception ex)
            {
                errorMessage += ex.Message;
                return null;
            }
        }

        public async Task<bool> DeleteItem(DeleteItemVM itemVM)
        {
            try
            {
                bool check = await _context.CartDetails.Where(cd => cd.ItemId == itemVM.itemID)
                    .AnyAsync();
                if (check)
                {
                    errorMessage = "sản phẩm của bạn đang có người muốn nhận nên bạn không thể xóa sản phẩm này";
                    return false;
                }
                else
                {
                    var item = await _context.Items.Where(it => it.ItemId == itemVM.itemID
                    && it.UserId == itemVM.userID)
                        .SingleOrDefaultAsync();
                    if (item != null)
                    {
                        item.ItemStatus = false;
                        _context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        errorMessage = "sản phẩm này của bạn không còn tồn tại trong dữ liệu";
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
                bool checkCurrentItem = await _context.CartDetails
                    .Where(cd => cd.ItemId == itemVM.itemID)
                    .AnyAsync();
                if (checkCurrentItem)
                {
                    errorMessage = "sản phẩm của bạn đang có người muốn nhận nên bạn không thể cập nhật sản phẩm này";
                    return false;
                }
                else
                {
                    Models.Item? currentItem = await _context.Items
                        .Where(it => it.ItemId == itemVM.itemID && it.UserId == itemVM.userId)
                        .SingleOrDefaultAsync();
                    var checkSubCategoryExists = await _context.SubCategories
                        .Where(sc => sc.SubCategoryId == itemVM.subCategoryId)
                        .SingleOrDefaultAsync();
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
#pragma warning disable CS8601 // Possible null reference assignment.
                        currentItem.ItemQuanlity = itemVM.itemQuanlity;
#pragma warning restore CS8601 // Possible null reference assignment.
                        currentItem.ItemEstimateValue = itemVM.itemEstimateValue;
                        currentItem.ItemSalePrice = itemVM.itemSalePrice;
                        currentItem.ItemShareAmount = itemVM.itemShareAmount;
                        currentItem.ItemSponsoredOrderShippingFee = itemVM.itemSponsoredOrderShippingFee;
                        currentItem.ItemShippingAddress = itemVM.itemShippingAddress;
                        currentItem.Image = itemVM.image;
                        currentItem.ItemExpiredTime = dateTimeExpired;
                        currentItem.ItemDateUpdate = dateTimeUpdate;
                        currentItem.Share = itemVM.share;
                        await _context.SaveChangesAsync();
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

        public async Task<List<BriefItem>?> GetAllMyBriefItemAndBriefRequest(int userID, bool share)
        {
            try
            {
                List<BriefItem> listBriefItemAndBriefRequestByUserID = new List<BriefItem>();
                listBriefItemAndBriefRequestByUserID = await _context.BriefItems
                    .Where(bf => bf.Share == share
                    && bf.UserId == userID)
                    .ToListAsync();
                if (listBriefItemAndBriefRequestByUserID.Count == 0)
                {
                    errorMessage = "không có dữ liệu";
                }
                return listBriefItemAndBriefRequestByUserID;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<BriefItem>?> GetAllMyBriefItemAndBriefRequestActiveandUnActive(int userID, bool share, bool status)
        {
            try
            {
                List<BriefItem> listBriefItemAndBriefRequestByUserID = new List<BriefItem>();
                listBriefItemAndBriefRequestByUserID = await _context.BriefItems
                    .Where(bf => bf.Share == share
                    && bf.UserId == userID
                    && bf.ItemStatus == status)
                    .ToListAsync();
                if (listBriefItemAndBriefRequestByUserID.Count == 0)
                {
                    errorMessage = "không có dữ liệu";
                }
                return listBriefItemAndBriefRequestByUserID;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<BriefItem>?> GetAllMyShareAndRequest(int userID, bool share, bool status, int pageNumber, int pageSize)
        {
            try
            {
                DateTime dateTimeNow = DateTime.Now;
                int itemsToSkip = (pageNumber - 1) * pageSize;
                List<BriefItem> listMyShareAndRequest = new List<BriefItem>();
                listMyShareAndRequest = await _context.BriefItems
                    .Where(bf => bf.Share == share
                    && bf.UserId == userID
                    && bf.ItemStatus == status)
                    .Skip(itemsToSkip)
                    .Take(pageSize)
                    .ToListAsync();
                if (listMyShareAndRequest.Count == 0)
                {
                    errorMessage = "không có dữ liệu";
                }
                return listMyShareAndRequest;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<BriefItem>?> GetAllShareFree(int pageNumber, int pageSize, int userID)
        {
            try
            {
                int itemsToSkip = (pageNumber - 1) * pageSize;
                List<BriefItem> listMyShareAndRequest = new List<BriefItem>();
                listMyShareAndRequest = await _context.BriefItems
                    .Where(bf => bf.Share == true
                    && bf.ItemStatus == true
                    && bf.ItemSalePrice == 0
                    && bf.UserId != userID)
                    .Skip(itemsToSkip)
                    .Take(pageSize)
                    .ToListAsync();
                if (listMyShareAndRequest.Count == 0)
                {
                    errorMessage = "không có dữ liệu";
                }
                return listMyShareAndRequest;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<BriefItem>?> GetAllShareRecently(int pageNumber, int pageSize, int userID)
        {
            try
            {
                int itemsToSkip = (pageNumber - 1) * pageSize;
                List<BriefItem> listShareRecently = new List<BriefItem>();
                listShareRecently = await _context.BriefItems
                    .Where(bf => bf.Share == true
                    && bf.ItemStatus == true
                    && bf.UserId != userID)
                    .Join(_context.Items
                    , bf => bf.ItemId
                    , it => it.ItemId
                    , (bf, it) => new { bf, it })
                    .OrderByDescending(bfit => bfit.it.ItemDateCreated)
                    .Skip(itemsToSkip)
                    .Take(pageSize)
                    .Select(bfit => new BriefItem
                    {
                        CategoryId = bfit.bf.CategoryId,
                        CategoryName = bfit.bf.CategoryName,
                        CategoryStatus = bfit.bf.CategoryStatus,
                        SubCategoryId = bfit.bf.SubCategoryId,
                        ItemId = bfit.bf.ItemId,
                        Image = bfit.bf.Image,
                        ItemCode = bfit.bf.ItemCode,
                        ItemSalePrice = bfit.bf.ItemSalePrice,
                        ItemStatus = bfit.bf.ItemStatus,
                        ItemTitle = bfit.bf.ItemTitle,
                        Share = true,
                        SubCategoryName = bfit.bf.SubCategoryName,
                        SubCategoryStatus = bfit.bf.SubCategoryStatus,
                        UserId = bfit.bf.UserId,
                        UserName = bfit.bf.UserName
                    }).ToListAsync();
                if (listShareRecently.Count == 0)
                {
                    errorMessage = "không có dữ liệu";
                }
                return listShareRecently;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<BriefItem>?> GetAllShareNearYou(string location, int pageNumber, int pageSize, int userID)
        {
            try
            {
                string[] words = location.Trim().Split(",");
                string city = String.Concat(words[0].Where(c => !Char.IsWhiteSpace(c))).Replace("}", "");
                int itemsToSkip = (pageNumber - 1) * pageSize;
                List<BriefItem> listShareRecently = new List<BriefItem>();
                listShareRecently = await _context.BriefItems
                    .Join(_context.Items, bf => bf.ItemId, it => it.ItemId, (bf, it) => new { bf, it })
                    .Where(bfit => bfit.it.ItemShippingAddress.StartsWith(city)
                    && bfit.bf.Share == true
                    && bfit.bf.ItemStatus == true
                    && bfit.bf.UserId != userID)
                    .Skip(itemsToSkip)
                    .Take(pageSize)
                    .Select(bfit => new BriefItem
                    {
                        CategoryId = bfit.bf.CategoryId,
                        CategoryName = bfit.bf.CategoryName,
                        CategoryStatus = bfit.bf.CategoryStatus,
                        SubCategoryId = bfit.bf.SubCategoryId,
                        ItemId = bfit.bf.ItemId,
                        Image = bfit.bf.Image,
                        ItemCode = bfit.bf.ItemCode,
                        ItemSalePrice = bfit.bf.ItemSalePrice,
                        ItemStatus = bfit.bf.ItemStatus,
                        ItemTitle = bfit.bf.ItemTitle,
                        Share = true,
                        SubCategoryName = bfit.bf.SubCategoryName,
                        SubCategoryStatus = bfit.bf.SubCategoryStatus,
                        UserId = bfit.bf.UserId,
                        UserName = bfit.bf.UserName
                    }).ToListAsync();
                if (listShareRecently.Count == 0)
                {
                    errorMessage = "không có dữ liệu";
                }
                return listShareRecently;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }

        public async Task<bool> DeactivateAllExpiredProducts()
        {
            try
            {
                DateTime dateTimeNow = DateTime.Now;
                List<Models.Item> items = await _context.Items.Where(it => it.ItemExpiredTime != null).ToListAsync();
                if (items != null)
                {
                    foreach (Models.Item item in items)
                    {
#pragma warning disable CS8629 // Nullable value type may be null.
                        TimeSpan distance = (TimeSpan)(item.ItemExpiredTime - dateTimeNow);
#pragma warning restore CS8629 // Nullable value type may be null.
                        if (distance.TotalDays == 1)
                        {
                            item.ItemStatus = false;
                        }
                    }
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<BriefItem>?> GetItemDynamicFilters(int pageNumber, int pageSize, DynamicFilterVM dynamicFilterVM)
        {
            int itemsToSkip = (pageNumber - 1) * pageSize;
            List<BriefItem> listItemDynamicFilters = new List<BriefItem>();
            var query = _context.BriefItems.Join(_context.Items, bf => bf.ItemId, it => it.ItemId, (bf, it) => new { bf, it });
            if (dynamicFilterVM.categoryID != 0)
            {
                query = query.Where(query => query.bf.CategoryId == dynamicFilterVM.categoryID);
            }
            if (!String.IsNullOrEmpty(dynamicFilterVM.titleName) && !String.IsNullOrWhiteSpace(dynamicFilterVM.titleName))
            {
                query = query.Where(query => query.bf.ItemTitle.Contains(dynamicFilterVM.titleName));
            }
            if (dynamicFilterVM.location != null)
            {
                string locationString = String.Concat(dynamicFilterVM.location.Where(c => !Char.IsWhiteSpace(c))).Replace("}", "");
                query = query.Where(query => query.it.ItemShippingAddress.StartsWith(locationString));
            }
            if (dynamicFilterVM.maxPrice != null && dynamicFilterVM.minPrice != null && dynamicFilterVM.maxPrice >= dynamicFilterVM.minPrice)
            {
                query = query.Where(query => query.it.ItemSalePrice <= dynamicFilterVM.maxPrice && query.it.ItemSalePrice >= dynamicFilterVM.minPrice);
            }
            if (dynamicFilterVM.maxPrice == null && dynamicFilterVM.minPrice != null)
            {
                query = query.Where(query => query.it.ItemSalePrice >= dynamicFilterVM.minPrice);
            }
            if (dynamicFilterVM.minPrice == null && dynamicFilterVM.maxPrice != null)
            {
                query = query.Where(query => query.it.ItemSalePrice >= dynamicFilterVM.maxPrice);
            }
            query = query.Where(query => query.it.ItemSalePrice <= dynamicFilterVM.maxUsable && query.it.ItemSalePrice >= dynamicFilterVM.minUsable);
            listItemDynamicFilters = await query
                .Skip(itemsToSkip)
                .Take(pageSize)
                .Select(bfit => new BriefItem
                {
                    CategoryId = bfit.bf.CategoryId,
                    CategoryName = bfit.bf.CategoryName,
                    CategoryStatus = bfit.bf.CategoryStatus,
                    SubCategoryId = bfit.bf.SubCategoryId,
                    ItemId = bfit.bf.ItemId,
                    Image = bfit.bf.Image,
                    ItemCode = bfit.bf.ItemCode,
                    ItemSalePrice = bfit.bf.ItemSalePrice,
                    ItemStatus = bfit.bf.ItemStatus,
                    ItemTitle = bfit.bf.ItemTitle,
                    Share = bfit.bf.Share,
                    SubCategoryName = bfit.bf.SubCategoryName,
                    SubCategoryStatus = bfit.bf.SubCategoryStatus,
                    UserId = bfit.bf.UserId,
                    UserName = bfit.bf.UserName
                }).ToListAsync();
            if (listItemDynamicFilters.Count == 0)
            {
                errorMessage = "không có dữ liệu";
            }
            return listItemDynamicFilters;
        }

        public async Task<List<BriefItem>?> GetListAllOtherPersonRequestItem(bool share, bool status, int userID, int pageNumber, int pageSize)
        {
            try
            {
                List<BriefItem> listBriefItemByUserID = new List<BriefItem>();
                int itemsToSkip = (pageNumber - 1) * pageSize;
                listBriefItemByUserID = await _context.BriefItems
                    .Where(bf => bf.Share == share && bf.ItemStatus == status && bf.UserId != userID)
                    .Skip(itemsToSkip)
                    .Take(pageSize)
                    .ToListAsync();
                if (listBriefItemByUserID.Count == 0)
                {
                    errorMessage = "không có dữ liệu";
                }
                return listBriefItemByUserID;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<BriefItem>?> GetListAllMyRequestItem(bool share, bool status, int userID, int pageNumber, int pageSize)
        {
            try
            {
                List<BriefItem> listBriefItemByUserID = new List<BriefItem>();
                int itemsToSkip = (pageNumber - 1) * pageSize;
                listBriefItemByUserID = await _context.BriefItems
                    .Where(bf => bf.Share == share && bf.ItemStatus == status && bf.UserId == userID)
                    .Skip(itemsToSkip)
                    .Take(pageSize)
                    .ToListAsync();
                if (listBriefItemByUserID.Count == 0)
                {
                    errorMessage = "không có dữ liệu";
                }
                return listBriefItemByUserID;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }
    }
}

