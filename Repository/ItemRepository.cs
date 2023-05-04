using Category.Data_View_Model;
using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;
using Newtonsoft.Json;
using NodaTime;
using NodaTime.Extensions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

namespace MOBY_API_Core6.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly MOBYContext _context;
        private readonly JsonToObj _JsonToObj;

        public static string? ErrorMessage { get; set; }

        public ItemRepository(MOBYContext context, JsonToObj jsonToObj)
        {
            _JsonToObj = jsonToObj;
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
                    ErrorMessage = "danh mục or tài khoản bạn nhập không tồn tại";
                    return false;
                }
                else
                {
                    DateTime dateTimeCreate = DateTime.Now;
                    DateTime? dateTimeExpired = null;
                    if (!string.IsNullOrEmpty(itemVM.stringDateTimeExpired) && !string.IsNullOrWhiteSpace(itemVM.stringDateTimeExpired) && itemVM.share == false)
                    {
                        try
                        {
                            dateTimeExpired = DateTime.Parse(itemVM.stringDateTimeExpired);
                            bool check = dateTimeExpired > dateTimeCreate;
                            if (check == false)
                            {
                                ErrorMessage = "bạn đã nhập ngày hết hạn sau ngày tạo";
                                return false;
                            }
                        }
                        catch
                        {
                            ErrorMessage = "bạn đã nhập sai format ngày, fotmat của chúng tôi là yyyy/mm/dd";
                            return false;
                        }
                    }
                    if(_JsonToObj.TransformLocation(itemVM.itemShippingAddress) == null)
                    {
                        return false;
                    }
                    if (itemVM.MaxAge < itemVM.MinAge || itemVM.MaxWeight < itemVM.MinWeight || itemVM.MaxHeight < itemVM.MinHeight || itemVM.MinAge < 0 || itemVM.MinWeight < 0 || itemVM.MinHeight < 0)
                    {
                        return false;
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
                        ItemEstimateValue = itemVM.itemEstimateValue,
                        ItemSalePrice = itemVM.itemSalePrice,
                        ItemShareAmount = itemVM.itemShareAmount,
                        ItemExpiredTime = dateTimeExpired,
                        ItemShippingAddress = _JsonToObj.TransformLocation(itemVM.itemShippingAddress),
                        MaxAge = itemVM.MaxAge,
                        MinAge = itemVM.MinAge,
                        MaxHeight = itemVM.MaxHeight,
                        MinHeight = itemVM.MinHeight,
                        MaxWeight = itemVM.MaxWeight,
                        MinWeight = itemVM.MinWeight,
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
                ErrorMessage = "có lỗi khi tạo sản phẩm này" + ex.Message;
                return false;
            }
        }

        public async Task<List<int>> GetListItemIDByUserID(int userId)
        {
            List<int> ItemIDList = await _context.Items.Where(i => i.UserId == userId).Select(i => i.ItemId).ToListAsync();
            return ItemIDList;
        }

        public async Task<int> GetQuantityByItemID(int itemID)
        {
            int itemQuantity = await _context.Items.Where(i => i.ItemId == itemID).Select(i => i.ItemShareAmount).FirstOrDefaultAsync();
            return itemQuantity;
        }

        public async Task<List<BriefItem>?> GetAllBriefItemAndBriefRequest(bool share, bool status, int pageNumber, int pageSize)
        {
            try
            {
                int itemsToSkip = (pageNumber - 1) * pageSize;
                List<BriefItem> listBriefItem = new();
                listBriefItem = await _context.BriefItems
                    .Where(bf => bf.Share == share && bf.ItemStatus == status)
                    .OrderByDescending(bf => bf.ItemDateCreated)
                    .Skip(itemsToSkip)
                    .Take(pageSize)
                    .ToListAsync();
                if (listBriefItem.Count == 0)
                {
                    ErrorMessage = "không có dữ liệu";
                }
                return listBriefItem;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<BriefItem>?> SearchBriefItemByTitle(string itemTitle, bool status)
        {
            try
            {
                List<BriefItem> listBriefItemByTitle = new();
                listBriefItemByTitle = await _context.BriefItems
                    .Where(bf => bf.ItemTitle.Equals(itemTitle))
                    .ToListAsync();
                if (listBriefItemByTitle.Count == 0)
                {
                    ErrorMessage = "không có dữ liệu";
                }
                return listBriefItemByTitle;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<BriefItem>?> GetBriefItemByOrBriefRequestUserID(int userID, bool status, bool share, int pageNumber, int pageSize)
        {
            try
            {
                int itemsToSkip = (pageNumber - 1) * pageSize;
                List<BriefItem> listBriefItemByUserID = new();
                listBriefItemByUserID = await _context.BriefItems
                    .Where(bf => bf.UserId == userID && bf.ItemStatus == status && bf.Share == share)
                    .Skip(itemsToSkip)
                    .Take(pageSize)
                    .ToListAsync();
                if (listBriefItemByUserID.Count == 0)
                {
                    ErrorMessage = "không có dữ liệu";
                }
                return listBriefItemByUserID;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<BriefItem>?> GetBriefItemByShare(bool share, bool status)
        {
            try
            {
                List<BriefItem> listBriefItemByUserID = new();
                listBriefItemByUserID = await _context.BriefItems
                    .Where(bf => bf.Share == share && bf.ItemStatus == status)
                    .ToListAsync();
                if (listBriefItemByUserID.Count == 0)
                {
                    ErrorMessage = "không có dữ liệu";
                }
                return listBriefItemByUserID;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        public async Task<DetailItemVM?> GetItemDetail(int itemID)
        {
            try
            {
                DetailItemVM? itemDetail = await _context.DetailItems
                    .Where(di => di.ItemId == itemID)
                    .Select(di => new DetailItemVM {
                         ItemId = di.ItemId,
                         ItemCode = di.ItemCode, 
                         ItemTitle = di.ItemTitle, 
                         ItemDetailedDescription = di.ItemDetailedDescription, 
                         ItemMass = di.ItemMass, 
                         ItemSize = di.ItemSize, 
                         ItemEstimateValue = di.ItemEstimateValue, 
                         ItemSalePrice = di.ItemSalePrice, 
                         ItemShareAmount = di.ItemShareAmount, 
                         ItemExpiredTime = di.ItemExpiredTime,
                         ItemShippingAddress = _JsonToObj.TransformJsonLocation(di.ItemShippingAddress),
                         ItemDateCreated = di.ItemDateCreated, 
                         ItemStatus = di.ItemStatus, 
                         Share = di.Share, 
                         Image = di.Image, 
                         UserName = di.UserName, 
                         UserId = di.UserId, 
                         SubCategoryId = di.SubCategoryId, 
                         SubCategoryName = di.SubCategoryName, 
                         CategoryId = di.CategoryId, 
                         CategoryName = di.CategoryName, 
                         MaxAge = di.MaxAge, 
                         MinAge = di.MinAge, 
                         MaxWeight = di.MaxWeight, 
                         MinWeight = di.MinWeight, 
                         MinHeight = di.MinHeight, 
                         MaxHeight = di.MaxHeight,
                    })
                .FirstOrDefaultAsync();
                return itemDetail;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        public async Task<DetailItemRequestVM?> GetRequestDetail(int itemID)
        {
            try
            {
                DetailItemRequestVM? detailItemRequest = await _context.DetailItemRequests
                    .Where(dir => dir.ItemId == itemID)
                    .Select(di => new DetailItemRequestVM
                    {
                        ItemId = di.ItemId,
                        ItemCode = di.ItemCode,
                        ItemTitle = di.ItemTitle,
                        ItemDetailedDescription = di.ItemDetailedDescription,
                        ItemShareAmount = di.ItemShareAmount,
                        ItemExpiredTime = di.ItemExpiredTime,
                        ItemShippingAddress = _JsonToObj.TransformJsonLocation(di.ItemShippingAddress),
                        ItemDateCreated = di.ItemDateCreated,
                        ItemStatus = di.ItemStatus,
                        Share = di.Share,
                        Image = di.Image,
                        UserName = di.UserName,
                        UserId = di.UserId,
                        SubCategoryId = di.SubCategoryId,
                        SubCategoryName = di.SubCategoryName,
                        CategoryId = di.CategoryId,
                        CategoryName = di.CategoryName,
                        MaxAge = di.MaxAge,
                        MinAge = di.MinAge,
                        MaxWeight = di.MaxWeight,
                        MinWeight = di.MinWeight,
                        MinHeight = di.MinHeight,
                        MaxHeight = di.MaxHeight,
                    })
                    .FirstOrDefaultAsync();
                return detailItemRequest;

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<BriefItem>?> SearchBriefItemByOrBriefRequestBySubCategoryID(int subCategoryID, bool status, bool share, int pageNumber, int pageSize)
        {
            try
            {
                int itemsToSkip = (pageNumber - 1) * pageSize;
                List<BriefItem> listBriefItemBySubCategoryID = new();
                listBriefItemBySubCategoryID = await _context.BriefItems
                    .Where(bf => bf.SubCategoryId == subCategoryID
                    && bf.ItemStatus == status
                    && bf.Share == share)
                    .Skip(itemsToSkip)
                    .Take(pageSize)
                    .ToListAsync();
                if (listBriefItemBySubCategoryID.Count == 0)
                {
                    ErrorMessage = "không có dữ liệu";
                }
                return listBriefItemBySubCategoryID;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<BriefItem>?> SearchBriefItemOrBriefRequestByCategoryID(int categoryID, bool status, bool share, int pageNumber, int pageSize)
        {
            try
            {
                int itemsToSkip = (pageNumber - 1) * pageSize;
                List<BriefItem> listBriefItemByCategoryID = new();
                listBriefItemByCategoryID = await _context.BriefItems
                    .Where(bf => bf.CategoryId == categoryID
                    && bf.ItemStatus == status
                    && bf.Share == share)
                    .Skip(itemsToSkip)
                    .Take(pageSize)
                    .ToListAsync();
                if (listBriefItemByCategoryID.Count == 0)
                {
                    ErrorMessage = "không có dữ liệu";
                }
                return listBriefItemByCategoryID;
            }
            catch (Exception ex)
            {
                ErrorMessage += ex.Message;
                return null;
            }
        }

        public async Task<bool> DeleteItem(DeleteItemVM itemVM)
        {
            try
            {
                bool check = await _context
                    .Orders
                    .Where(or => or.ItemId == itemVM.itemID && or.Status == 0)
                    .AnyAsync();
                if (check)
                {
                    ErrorMessage = "sản phẩm của bạn đang có người muốn nhận nên bạn không thể xóa sản phẩm này";
                    return false;
                }
                else
                {
                    var item = await _context.Items.Where(it => it.ItemId == itemVM.itemID
                    && it.UserId == itemVM.userID)
                        .FirstOrDefaultAsync();
                    if (item != null)
                    {
                        item.ItemStatus = null;
                        _context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        ErrorMessage = "sản phẩm này của bạn không còn tồn tại trong dữ liệu";
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
                bool checkCurrentItem = await _context.Orders
                    .Where(or => or.ItemId == itemVM.itemID && or.Status == 0)
                    .AnyAsync();
                if (checkCurrentItem)
                {
                    ErrorMessage = "sản phẩm của bạn đang có người muốn nhận nên bạn không thể cập nhật sản phẩm này";
                    return false;
                }
                else
                {
                    Models.Item? currentItem = await _context.Items
                        .Where(it => it.ItemId == itemVM.itemID && it.UserId == itemVM.userId)
                        .FirstOrDefaultAsync();
                    var checkSubCategoryExists = await _context.SubCategories
                        .Where(sc => sc.SubCategoryId == itemVM.subCategoryId)
                        .FirstOrDefaultAsync();
                    if (currentItem == null || currentItem.ItemStatus == null)
                    {
                        ErrorMessage = "sản phẩm này của bạn không còn tồn tại trong dữ liệu";
                        return false;
                    }
                    if (checkSubCategoryExists == null)
                    {
                        ErrorMessage = "danh mục bạn chọn không còn tồn tại trong dữ liệu, mong bạn chọn danh mục khác";
                        return false;
                    }
                    if (itemVM.MaxAge < itemVM.MinAge || itemVM.MaxWeight < itemVM.MinWeight || itemVM.MaxHeight < itemVM.MinHeight || itemVM.MinAge < 0 || itemVM.MinWeight < 0 || itemVM.MinHeight < 0)
                    {
                        return false;
                    }
                    else
                    {
                        DateTime dateTimeUpdate = DateTime.Now;
                        DateTime? dateTimeExpired = null;
                        try
                        {
                            if (!string.IsNullOrEmpty(itemVM.stringDateTimeExpired) && !string.IsNullOrWhiteSpace(itemVM.stringDateTimeExpired) && itemVM.share == false)
                            {
                                dateTimeExpired = DateTime.Parse(itemVM.stringDateTimeExpired);
                                bool checkDate = dateTimeExpired > dateTimeUpdate;
                                if (checkDate == false)
                                {
                                    ErrorMessage = "bạn đã nhập ngày hết hạn sau ngày cập nhật";
                                    return false;
                                }
                            }
                        }
                        catch
                        {
                            ErrorMessage = "bạn đã nhập sai format ngày, fotmat của chúng tôi là yyyy/mm/dd";
                            return false;
                        }
                        currentItem.SubCategoryId = itemVM.subCategoryId;
                        currentItem.ItemTitle = itemVM.itemTitle;
                        currentItem.ItemDetailedDescription = itemVM.itemDetailedDescription;
                        currentItem.ItemMass = itemVM.itemMass;
                        currentItem.ItemSize = itemVM.itemSize;
                        currentItem.ItemStatus = true;
                        currentItem.ItemEstimateValue = itemVM.itemEstimateValue;
                        currentItem.ItemSalePrice = itemVM.itemSalePrice;
                        currentItem.ItemShareAmount = itemVM.itemShareAmount;
                        currentItem.ItemShippingAddress = _JsonToObj.TransformLocation(itemVM.itemShippingAddress);
                        currentItem.MaxAge = itemVM.MaxAge;
                        currentItem.MinAge = itemVM.MinAge;
                        currentItem.MaxHeight = itemVM.MaxHeight;
                        currentItem.MinHeight = itemVM.MinHeight;
                        currentItem.MaxWeight = itemVM.MaxWeight;
                        currentItem.MinWeight = itemVM.MinWeight;
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
                ErrorMessage = "có lỗi khi cập nhật sản phẩm này" + ex.Message;
                return false;
            }
        }

        public async Task<List<BriefItem>?> GetAllMyBriefItemAndBriefRequest(int userID, bool share)
        {
            try
            {
                List<BriefItem> listBriefItemAndBriefRequestByUserID = new();
                listBriefItemAndBriefRequestByUserID = await _context.BriefItems
                    .Where(bf => bf.Share == share
                    && bf.UserId == userID)
                    .ToListAsync();
                if (listBriefItemAndBriefRequestByUserID.Count == 0)
                {
                    ErrorMessage = "không có dữ liệu";
                }
                return listBriefItemAndBriefRequestByUserID;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<BriefItem>?> GetAllMyBriefItemAndBriefRequestActiveandUnActive(int userID, bool share, bool status)
        {
            try
            {
                List<BriefItem> listBriefItemAndBriefRequestByUserID = new();
                listBriefItemAndBriefRequestByUserID = await _context.BriefItems
                    .Where(bf => bf.Share == share
                    && bf.UserId == userID
                    && bf.ItemStatus == status)
                    .ToListAsync();
                if (listBriefItemAndBriefRequestByUserID.Count == 0)
                {
                    ErrorMessage = "không có dữ liệu";
                }
                return listBriefItemAndBriefRequestByUserID;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        public async Task<ListVM<BriefItem>?> GetAllMyShareAndRequest(int userID, bool share, bool status, int pageNumber, int pageSize)
        {
            try
            {
                DateTime dateTimeNow = DateTime.Now;
                int itemsToSkip = (pageNumber - 1) * pageSize;
                List<BriefItem> listMyShareAndRequest = new();
                var query = _context.BriefItems
                    .Where(bf => bf.Share == share
                    && bf.UserId == userID
                    && bf.ItemStatus == status);
                int total = query.Count();
                int totalPage = total / pageSize;
                if (total % pageSize != 0)
                {
                    ++totalPage;
                }
                listMyShareAndRequest = await query
                    .Skip(itemsToSkip)
                    .Take(pageSize)
                    .ToListAsync();
                if (listMyShareAndRequest.Count == 0)
                {
                    ErrorMessage = "không có dữ liệu";
                }
                ListVM<BriefItem> listVM = new(total, totalPage, listMyShareAndRequest);
                return listVM;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        public async Task<ListVM<BriefItem>?> GetAllShareFree(int pageNumber, int pageSize)
        {
            try
            {
                int itemsToSkip = (pageNumber - 1) * pageSize;
                List<BriefItem> listShareFree = new();
                var query = _context.BriefItems
                    .Join(_context.UserAccounts, bf => bf.UserId, us => us.UserId, (bf, us) => new { bf, us })
                    .Where(bfus => bfus.bf.Share == true
                    && bfus.bf.ItemStatus == true
                    && bfus.bf.ItemSalePrice == 0
                    && bfus.us.UserStatus == true);
                query = query.OrderByDescending(bfus => bfus.bf.ItemDateCreated);
                int total = query.Count();
                int totalPage = total / pageSize;
                if (total % pageSize != 0)
                {
                    ++totalPage;
                }
                listShareFree = await query
                    .Skip(itemsToSkip)
                    .Take(pageSize)
                    .Select(bfus => bfus.bf)
                    .ToListAsync();
                if (listShareFree.Count == 0)
                {
                    ErrorMessage = "không có dữ liệu";
                }
                ListVM<BriefItem> listVM = new(total, totalPage, listShareFree);
                return listVM;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        public async Task<ListVM<BriefItem>?> GetAllShareRecently(int pageNumber, int pageSize, int? userID)
        {
            try
            {
                int itemsToSkip = (pageNumber - 1) * pageSize;
                List<BriefItem> listShareRecently = new();
                var query = _context.BriefItems
                    .Join(_context.UserAccounts, bf => bf.UserId, us => us.UserId, (bf, us) => new { bf, us })
                    .Where(bfus => bfus.bf.Share == true
                    && bfus.bf.ItemStatus == true
                    && bfus.us.UserStatus == true);
                if (userID != null)
                {
                    query = query.Where(bfus => bfus.bf.UserId != userID);
                }
                query = query.OrderByDescending(bfus => bfus.bf.ItemDateCreated);
                int total = query.Count();
                int totalPage = total / pageSize;
                if (total % pageSize != 0)
                {
                    ++totalPage;
                }
                listShareRecently = await query
                    .Skip(itemsToSkip)
                    .Take(pageSize)
                    .Select(bfus => bfus.bf)
                    .ToListAsync();
                if (listShareRecently.Count == 0)
                {
                    ErrorMessage = "không có dữ liệu";
                }
                ListVM<BriefItem> listVM = new(total, totalPage, listShareRecently);
                return listVM;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        public async Task<ListVM<BriefItem>?> GetAllShareNearYou(string location, int pageNumber, int pageSize, int userID)
        {
            try
            {
                Location locationObject = JsonConvert.DeserializeObject<Location>(location);
                string city = "addressProvince:" + locationObject.AddressProvince;
                int itemsToSkip = (pageNumber - 1) * pageSize;
                List<BriefItem> listShareRecently = new();
                var query = _context.BriefItems
                    .Join(_context.Items, bf => bf.ItemId, it => it.ItemId, (bf, it) => new { bf, it })
                    .Join(_context.UserAccounts, bfit => bfit.it.UserId, us => us.UserId, (bfit, us) => new { bfit, us })
                    .Where(bfitus => bfitus.bfit.it.ItemShippingAddress.Trim().Contains(city)
                    && bfitus.bfit.bf.Share == true
                    && bfitus.bfit.bf.ItemStatus == true
                    && bfitus.bfit.bf.UserId != userID
                    && bfitus.us.UserStatus == true)
                    .OrderByDescending(bfitus => bfitus.bfit.bf.ItemDateCreated);
                int total = query.Count();
                int totalPage = total / pageSize;
                if (total % pageSize != 0)
                {
                    ++totalPage;
                }
                listShareRecently = await query
                    .Skip(itemsToSkip)
                    .Take(pageSize)
                    .Select(bfitus => bfitus.bfit.bf)
                    .ToListAsync();
                if (listShareRecently.Count == 0)
                {
                    total = 0;
                    totalPage = 0;
                    ErrorMessage = "không có dữ liệu";
                }
                ListVM<BriefItem> listVM = new(total, totalPage, listShareRecently);
                return listVM;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
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

        public async Task<ListVM<BriefItem>?> GetItemDynamicFilters(DynamicFilterItemVM dynamicFilterVM)
        {
            try
            {
                int itemsToSkip = (dynamicFilterVM.PageNumber - 1) * dynamicFilterVM.PageSize;
                List<BriefItem> listItemDynamicFilters = new();
                var query = _context.BriefItems
                    .Join(_context.Items, bf => bf.ItemId, it => it.ItemId, (bf, it) => new { bf, it })
                    .Join(_context.UserAccounts, bfit => bfit.it.UserId, us => us.UserId, (bfit, us) => new { bfit, us })
                    .Where(bfitus => bfitus.us.UserStatus == true
                    && bfitus.bfit.bf.CategoryStatus == true
                    && bfitus.bfit.bf.SubCategoryStatus == true);
                if (dynamicFilterVM.CategoryID != null)
                {
                    query = query.Where(query => query.bfit.bf.CategoryId == dynamicFilterVM.CategoryID);
                }
                if (dynamicFilterVM.SubCategoryID != null)
                {
                    query = query.Where(query => query.bfit.bf.SubCategoryId == dynamicFilterVM.SubCategoryID);
                }
                if (!String.IsNullOrEmpty(dynamicFilterVM.TitleName) && !String.IsNullOrWhiteSpace(dynamicFilterVM.TitleName))
                {
                    query = query.Where(query => query.bfit.bf.ItemTitle.Contains(dynamicFilterVM.TitleName));
                }
                if (dynamicFilterVM.Location != null)
                {
                    string? locationString = _JsonToObj.TransformLocation(dynamicFilterVM.Location);
                    query = query.Where(query => query.bfit.it.ItemShippingAddress.StartsWith(locationString));
                }
                if (dynamicFilterVM.MaxPrice >= dynamicFilterVM.MinPrice && dynamicFilterVM.MaxPrice != 0)
                {
                    query = query.Where(query => query.bfit.it.ItemSalePrice <= dynamicFilterVM.MaxPrice && query.bfit.it.ItemSalePrice >= dynamicFilterVM.MinPrice);
                }
                if (dynamicFilterVM.Share != null)
                {
                    query = query.Where(query => query.bfit.it.Share == dynamicFilterVM.Share);
                }
                if (dynamicFilterVM.Status != null)
                {
                    query = query.Where(query => query.bfit.bf.ItemStatus == dynamicFilterVM.Status);
                }
                if (dynamicFilterVM.MinDateCreate <= dynamicFilterVM.MaxDateCreate && dynamicFilterVM.MinDateCreate != null && dynamicFilterVM.MaxDateCreate != null)
                {
                    query = query.Where(query => query.bfit.bf.ItemDateCreated >= dynamicFilterVM.MinDateCreate && query.bfit.bf.ItemDateCreated <= dynamicFilterVM.MaxDateCreate);
                }
                if (dynamicFilterVM.MinDateUpdate <= dynamicFilterVM.MaxDateUpdate && dynamicFilterVM.MinDateUpdate != null && dynamicFilterVM.MaxDateUpdate != null)
                {
                    query = query.Where(query => query.bfit.it.ItemDateUpdate >= dynamicFilterVM.MinDateUpdate && query.bfit.it.ItemDateUpdate <= dynamicFilterVM.MaxDateUpdate);
                }
                if (dynamicFilterVM.OrderByDateCreate == true && dynamicFilterVM.OrderByDateUpdate == false && dynamicFilterVM.OrderByEstimateValue == false && dynamicFilterVM.OrderByPrice == true)
                {
                    if (dynamicFilterVM.AscendingOrDescending == true)
                    {
                        query = query.OrderBy(query => query.bfit.bf.ItemDateCreated);
                    }
                    else
                    {
                        query = query.OrderByDescending(query => query.bfit.bf.ItemDateCreated);
                    }
                }
                else if (dynamicFilterVM.OrderByDateCreate == false && dynamicFilterVM.OrderByDateUpdate == true && dynamicFilterVM.OrderByEstimateValue == false && dynamicFilterVM.OrderByPrice == true)
                {
                    if (dynamicFilterVM.AscendingOrDescending == true)
                    {
                        query = query.OrderBy(query => query.bfit.it.ItemDateUpdate);
                    }
                    else
                    {
                        query = query.OrderByDescending(query => query.bfit.it.ItemDateUpdate);
                    }
                }
                else if (dynamicFilterVM.OrderByDateCreate == false && dynamicFilterVM.OrderByDateUpdate == false && dynamicFilterVM.OrderByEstimateValue == true && dynamicFilterVM.OrderByPrice == true)
                {
                    if (dynamicFilterVM.AscendingOrDescending == true)
                    {
                        query = query.OrderBy(query => query.bfit.it.ItemEstimateValue);
                    }
                    else
                    {
                        query = query.OrderByDescending(query => query.bfit.it.ItemEstimateValue);
                    }
                }
                else if (dynamicFilterVM.OrderByDateCreate == false && dynamicFilterVM.OrderByDateUpdate == false && dynamicFilterVM.OrderByEstimateValue == true && dynamicFilterVM.OrderByPrice == true)
                {
                    if (dynamicFilterVM.AscendingOrDescending == true)
                    {
                        query = query.OrderBy(query => query.bfit.bf.ItemSalePrice);
                    }
                    else
                    {
                        query = query.OrderByDescending(query => query.bfit.bf.ItemSalePrice);
                    }
                }
                if (dynamicFilterVM.MinUsable <= dynamicFilterVM.MaxUsable && dynamicFilterVM.MinUsable != null && dynamicFilterVM.MaxUsable != null)
                {
                    query = query.Where(query => query.bfit.it.ItemEstimateValue <= dynamicFilterVM.MaxUsable && query.bfit.it.ItemEstimateValue >= dynamicFilterVM.MinUsable);
                }
                int total = query.Count();
                int totalPage = total / dynamicFilterVM.PageSize;
                if (total % dynamicFilterVM.PageSize != 0)
                {
                    ++totalPage;
                }
                listItemDynamicFilters = await query
                    .Skip(itemsToSkip)
                    .Take(dynamicFilterVM.PageSize)
                    .Select(bfitus => bfitus.bfit.bf)
                    .ToListAsync();
                if (listItemDynamicFilters.Count == 0)
                {
                    ErrorMessage = "không có dữ liệu";
                }
                ListVM<BriefItem> listVM = new(total, totalPage, listItemDynamicFilters);
                return listVM;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        public async Task<ListVM<BriefItem>?> GetListAllOtherPersonRequestItem(bool share, bool status, int userID, int pageNumber, int pageSize)
        {
            try
            {
                List<BriefItem> listBriefItemByUserID = new();
                int itemsToSkip = (pageNumber - 1) * pageSize;
                var query = _context.BriefItems
                    .Join(_context.UserAccounts, bf => bf.UserId, us => us.UserId, (bf, us) => new { bf, us })
                    .Where(bfus => bfus.bf.Share == share && bfus.bf.ItemStatus == status && bfus.bf.UserId != userID)
                    .OrderByDescending(bfus => bfus.bf.ItemDateCreated);
                int total = query.Count();
                int totalPage = total / pageSize;
                if (total % pageSize != 0)
                {
                    ++totalPage;
                }
                listBriefItemByUserID = await query
                    .Select(bfus => bfus.bf)
                    .Skip(itemsToSkip)
                    .Take(pageSize)
                    .ToListAsync();
                if (listBriefItemByUserID.Count == 0)
                {
                    ErrorMessage = "không có dữ liệu";
                }
                ListVM<BriefItem> listVM = new(total, totalPage, listBriefItemByUserID);
                return listVM;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        public async Task<ListVM<BriefItem>?> GetListAllMyRequestItem(bool share, bool status, int userID, int pageNumber, int pageSize)
        {
            try
            {
                List<BriefItem> listBriefItemByUserID = new();
                int itemsToSkip = (pageNumber - 1) * pageSize;
                var query = _context.BriefItems
                    .Where(bf => bf.Share == share && bf.ItemStatus == status && bf.UserId == userID)
                    .OrderByDescending(bf => bf.ItemDateCreated);
                int total = query.Count();
                int totalPage = total / pageSize;
                if (total % pageSize != 0)
                {
                    ++totalPage;
                }
                listBriefItemByUserID = await query
                    .Skip(itemsToSkip)
                    .Take(pageSize)
                    .ToListAsync();
                if (listBriefItemByUserID.Count == 0)
                {
                    ErrorMessage = "không có dữ liệu";
                }
                ListVM<BriefItem> listVM = new(total, totalPage, listBriefItemByUserID);
                return listVM;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        public async Task<bool> RecordUserSearch(RecordSearchVM recordSearchVM)
        {
            try
            {
                if (!(recordSearchVM.CategoryId == null && recordSearchVM.SubCategoryId == null && String.IsNullOrEmpty(recordSearchVM.TitleName) && String.IsNullOrWhiteSpace(recordSearchVM.TitleName)))
                {
                    RecordSearch? recordSearch = null;
                    var query = _context.RecordSearches.Where(rs => rs.UserId == recordSearchVM.UserId);
                    if (recordSearchVM.CategoryId != null)
                    {
                        query = query.Where(rs => rs.CategoryId == recordSearchVM.CategoryId);
                    }
                    if (recordSearchVM.SubCategoryId != null)
                    {
                        query = query.Where(rs => rs.SubCategoryId == recordSearchVM.SubCategoryId);
                    }
                    if (!String.IsNullOrEmpty(recordSearchVM.TitleName) && !String.IsNullOrWhiteSpace(recordSearchVM.TitleName))
                    {
                        query = query.Where(rs => rs.TitleName == recordSearchVM.TitleName.Trim());
                    }
                    recordSearch = await query.FirstOrDefaultAsync();
                    if (recordSearch != null)
                    {
                        recordSearch.Count++;
                    }
                    else
                    {
                        recordSearch = new()
                        {
                            UserId = recordSearchVM.UserId,
                            CategoryId = recordSearchVM.CategoryId,
                            SubCategoryId = recordSearchVM.SubCategoryId,
                            Count = 1,
                            TitleName = recordSearchVM.TitleName
                        };
                        await _context.RecordSearches.AddAsync(recordSearch);
                    }
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public async Task<ListVM<BriefItem>?> GetListRecommend(int userID, int pageNumber, int pageSize)
        {
            try
            {
                ListVM<BriefItem>? listVM = null;
                List<BriefItem> listListRecommend = new();
                int total = 0;
                int totalPage = 0;
                RecordSearch? recordSearch = await _context.RecordSearches.Where(rs => rs.UserId == userID).OrderByDescending(rs => rs.Count).FirstOrDefaultAsync();
                if (recordSearch != null)
                {
                    int itemsToSkip = (pageNumber - 1) * pageSize;
                    var query = _context.BriefItems
                    .Where(bf => bf.Share == true && bf.ItemStatus == true && bf.UserId != userID);
                    if (recordSearch.CategoryId != null)
                    {
                        query = query.Where(bf => bf.CategoryId == recordSearch.CategoryId);
                        if (recordSearch.SubCategoryId != null)
                        {
                            query = query.Where(bf => bf.SubCategoryId == recordSearch.SubCategoryId);
                        }
                    }
                    if (recordSearch.TitleName != null)
                    {
                        query = query.Where(bf => bf.ItemTitle.Contains(recordSearch.TitleName.Trim()));
                    }
                    total = query.Count();
                    totalPage = total / pageSize;
                    if (total % pageSize != 0)
                    {
                        ++totalPage;
                    }
                    listListRecommend = await query
                        .OrderByDescending(bf => bf.ItemDateCreated)
                        .Skip(itemsToSkip)
                        .Take(pageSize)
                        .ToListAsync();
                }
                listVM = new(total, totalPage, listListRecommend);
                return listVM;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        public async Task<ListVM<BriefItem>?> GetListRecommendByBaby(int babyID, int userID, int pageNumber, int pageSize, bool age, bool weight, bool height)
        {
            try
            {
                ListVM<BriefItem>? listVM = null;
                Baby? baby = await _context.Babies.Where(bb => bb.Idbaby == babyID && bb.UserId == userID).FirstOrDefaultAsync();
                if (baby != null)
                {
                    List<BriefItem> listListRecommend = new();
                    int itemsToSkip = (pageNumber - 1) * pageSize;
                    var query = _context.BriefItems
                    .Join(_context.Items, bf => bf.ItemId, it => it.ItemId, (bf, it) => new {bf, it})
                    .Where(bfit => bfit.bf.Share == true 
                    && bfit.bf.ItemStatus == true 
                    && bfit.bf.UserId != userID);
                    if(age)
                    {
                        LocalDateTime now = DateTime.Now.ToLocalDateTime();
                        LocalDateTime babyBirth = baby.DateOfBirth.ToLocalDateTime();
                        Period period = Period.Between(babyBirth, now, PeriodUnits.Months);
                        int monthsAge = period.Months;
                        query = query.Where(bfit => bfit.it.MaxAge >= monthsAge && bfit.it.MinAge <= monthsAge);
                    }
                    if (weight)
                    {
                        query = query.Where(bfit => bfit.it.MaxWeight >= baby.Weight && bfit.it.MinWeight <= baby.Weight);
                    }
                    if (height)
                    {
                        query = query.Where(bfit => bfit.it.MaxHeight >= baby.Height && bfit.it.MinHeight <= baby.Height);
                    }
                    int total = query.Count();
                    int totalPage = total / pageSize;
                    if (total % pageSize != 0)
                    {
                        ++totalPage;
                    }
                    listListRecommend = await query
                        .OrderByDescending(bfit => bfit.bf.ItemDateCreated)
                        .Select(bfit => bfit.bf)
                        .Skip(itemsToSkip)
                        .Take(pageSize)
                        .ToListAsync();
                    listVM = new(total, totalPage, listListRecommend);
                }
                return listVM;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
    }
}

