using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using Newtonsoft.Json;
using NodaTime.Extensions;
using NodaTime;
using MOBY_API_Core6.Repository.IRepository;
using MOBY_API_Core6.Service;
using MOBY_API_Core6.Log4Net;

namespace MOBY_API_Core6.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly MOBYContext _context;
        private readonly JsonToObj _JsonToObj;
        private readonly Logger4Net _logger4Net;
        public ItemRepository(MOBYContext context, JsonToObj jsonToObj)
        {
            _JsonToObj = jsonToObj;
            _context = context;
            _logger4Net = new Logger4Net();
        }
        public async Task<int> CreateItem(CreateItemVM itemVM, DateTime dateTimeCreate, DateTime? dateTimeExpired)
        {
            var checkSubCategoryExists = await _context.SubCategories
            .Where(sc => sc.SubCategoryId == itemVM.SubCategoryId)
            .FirstOrDefaultAsync();
            if (checkSubCategoryExists != null)
            {
                string itemCode = Guid.NewGuid().ToString();
#pragma warning disable CS8601 // Possible null reference assignment.
                Models.Item item = new()
                {
                    UserId = itemVM.UserId,
                    ItemCode = itemCode,
                    SubCategoryId = itemVM.SubCategoryId,
                    ItemTitle = itemVM.ItemTitle,
                    ItemDetailedDescription = itemVM.ItemDetailedDescription,
                    ItemMass = itemVM.ItemMass,
                    ItemSize = itemVM.ItemSize,
                    ItemEstimateValue = itemVM.ItemEstimateValue,
                    ItemSalePrice = itemVM.ItemSalePrice,
                    ItemShareAmount = itemVM.ItemShareAmount,
                    ItemExpiredTime = dateTimeExpired,
                    ItemShippingAddress = _JsonToObj.TransformLocation(itemVM.ItemShippingAddress),
                    MaxAge = itemVM.MaxAge,
                    MinAge = itemVM.MinAge,
                    MaxHeight = itemVM.MaxHeight,
                    MinHeight = itemVM.MinHeight,
                    MaxWeight = itemVM.MaxWeight,
                    MinWeight = itemVM.MinWeight,
                    ItemDateCreated = dateTimeCreate,
                    ItemStatus = true,
                    Share = itemVM.Share,
                    Image = itemVM.Image
                };
#pragma warning restore CS8601 // Possible null reference assignment.
                await _context.sadas.AddAsync(item);
                return await _context.SaveChangesAsync();
            }
            throw new KeyNotFoundException();

        }
        public async Task<List<int>> GetListItemIDByUserID(int userId)
        {
            return await _context.sadas.Where(i => i.UserId == userId).Select(i => i.ItemId).ToListAsync();
        }
        public async Task<int> GetQuantityByItemID(int itemID)
        {
            return await _context.sadas.Where(i => i.ItemId == itemID).Select(i => i.ItemShareAmount).FirstOrDefaultAsync();
        }
        public async Task<List<BriefItem>?> GetAllBriefItemAndBriefRequest(bool share, bool status, int pageNumber, int pageSize)
        {
            int itemsToSkip = (pageNumber - 1) * pageSize;
            return await _context.BriefItems
                .Where(bf => bf.Share == share && bf.ItemStatus == status)
                .OrderByDescending(bf => bf.ItemDateCreated)
                .Skip(itemsToSkip)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<List<BriefItem>?> SearchBriefItemByTitle(string itemTitle, bool status)
        {
            return await _context.BriefItems
                .Where(bf => bf.ItemTitle.Equals(itemTitle) && bf.ItemStatus == status)
                .ToListAsync();
        }
        public async Task<List<BriefItem>?> GetBriefItemByOrBriefRequestUserID(int userID, bool status, bool share, int pageNumber, int pageSize)
        {
            int itemsToSkip = (pageNumber - 1) * pageSize;
            return await _context.BriefItems
                .Where(bf => bf.UserId == userID && bf.ItemStatus == status && bf.Share == share)
                .Skip(itemsToSkip)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<List<BriefItem>?> GetBriefItemByShare(bool share, bool status)
        {
            return await _context.BriefItems
                .Where(bf => bf.Share == share && bf.ItemStatus == status)
                .ToListAsync();
        }
        public async Task<DetailItemVM?> GetItemDetail(int itemID)
        {
#pragma warning disable CS8601 // Possible null reference assignment.
            return await _context.DetailItems
                .Where(di => di.ItemId == itemID)
                .Select(di => new DetailItemVM
                {
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
#pragma warning restore CS8601 // Possible null reference assignment.
        }
        public async Task<DetailItemRequestVM?> GetRequestDetail(int itemID)
        {
#pragma warning disable CS8601 // Possible null reference assignment.
            return await _context.DetailItemRequests
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
#pragma warning restore CS8601 // Possible null reference assignment.
        }
        public async Task<List<BriefItem>?> SearchBriefItemByOrBriefRequestBySubCategoryID(int subCategoryID, bool status, bool share, int pageNumber, int pageSize)
        {
            int itemsToSkip = (pageNumber - 1) * pageSize;
            return await _context.BriefItems
                .Where(bf => bf.SubCategoryId == subCategoryID
                && bf.ItemStatus == status
                && bf.Share == share)
                .Skip(itemsToSkip)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<List<BriefItem>?> SearchBriefItemOrBriefRequestByCategoryID(int categoryID, bool status, bool share, int pageNumber, int pageSize)
        {
            int itemsToSkip = (pageNumber - 1) * pageSize;
            return await _context.BriefItems
                .Where(bf => bf.CategoryId == categoryID
                && bf.ItemStatus == status
                && bf.Share == share)
                .Skip(itemsToSkip)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<int> DeleteItem(DeleteItemVM itemVM)
        {
            bool check = await _context.Orders.Where(or => or.ItemId == itemVM.ItemID && or.Status < 3).AnyAsync();
            if (!check)
            {
                var item = await _context.sadas.Where(it => it.ItemId == itemVM.ItemID
                && it.UserId == itemVM.UserID)
                    .FirstOrDefaultAsync();
                if (item != null)
                {
                    item.ItemStatus = null;
                    return await _context.SaveChangesAsync();
                }
                throw new KeyNotFoundException();
            }
            throw new InvalidDataException();
        }
        public async Task<int> UpdateItem(UpdateItemVM itemVM, DateTime dateTimeUpdate, DateTime? dateTimeExpired)
        {
            Models.Item? currentItem = await _context.sadas
                .Where(it => it.ItemId == itemVM.ItemID && it.UserId == itemVM.UserId && it.ItemStatus != null)
                .FirstOrDefaultAsync();
            var checkSubCategoryExists = await _context.SubCategories
                .Where(sc => sc.SubCategoryId == itemVM.SubCategoryId)
                .FirstOrDefaultAsync();
            if (currentItem != null && checkSubCategoryExists != null)
            {
                currentItem.SubCategoryId = itemVM.SubCategoryId;
                currentItem.ItemTitle = itemVM.ItemTitle;
                currentItem.ItemDetailedDescription = itemVM.ItemDetailedDescription;
                currentItem.ItemMass = itemVM.ItemMass;
                currentItem.ItemSize = itemVM.ItemSize;
                currentItem.ItemStatus = true;
                currentItem.ItemEstimateValue = itemVM.ItemEstimateValue;
                currentItem.ItemSalePrice = itemVM.ItemSalePrice;
                currentItem.ItemShareAmount = itemVM.ItemShareAmount;
                currentItem.ItemShippingAddress = itemVM.ItemShippingAddress;
                currentItem.MaxAge = itemVM.MaxAge;
                currentItem.MinAge = itemVM.MinAge;
                currentItem.MaxHeight = itemVM.MaxHeight;
                currentItem.MinHeight = itemVM.MinHeight;
                currentItem.MaxWeight = itemVM.MaxWeight;
                currentItem.MinWeight = itemVM.MinWeight;
                currentItem.Image = itemVM.Image;
                currentItem.ItemExpiredTime = dateTimeExpired;
                currentItem.ItemDateUpdate = dateTimeUpdate;
                currentItem.Share = itemVM.Share;
                return await _context.SaveChangesAsync();
            }
            throw new KeyNotFoundException();
        }
        public async Task<List<BriefItem>?> GetAllMyBriefItemAndBriefRequest(int userID, bool share)
        {
            return await _context.BriefItems
                .Where(bf => bf.Share == share
                && bf.UserId == userID
                && bf.ItemStatus != null)
                .ToListAsync();
        }
        public async Task<List<BriefItem>?> GetAllMyBriefItemAndBriefRequestActiveandUnActive(int userID, bool share, bool status)
        {
            return await _context.BriefItems
                .Where(bf => bf.Share == share
                && bf.UserId == userID
                && bf.ItemStatus == status)
                .ToListAsync();
        }
        public async Task<ListVM<BriefItem>?> GetAllMyShareAndRequest(int userID, bool share, bool status, int pageNumber, int pageSize)
        {
            int itemsToSkip = (pageNumber - 1) * pageSize;
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
            return new(total, totalPage, await query.Skip(itemsToSkip).Take(pageSize).ToListAsync());
        }
        public async Task<ListVM<BriefItem>?> GetAllShareFree(int pageNumber, int pageSize)
        {
            int itemsToSkip = (pageNumber - 1) * pageSize;
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
            return new(total, totalPage, await query.Skip(itemsToSkip).Take(pageSize).Select(bfus => bfus.bf).ToListAsync());
        }
        public async Task<ListVM<BriefItem>?> GetAllShareRecently(int pageNumber, int pageSize)
        {
            int itemsToSkip = (pageNumber - 1) * pageSize;
            var query = _context.BriefItems
                .Join(_context.UserAccounts, bf => bf.UserId, us => us.UserId, (bf, us) => new { bf, us })
                .Where(bfus => bfus.bf.Share == true
                && bfus.bf.ItemStatus == true
                && bfus.us.UserStatus == true);
            query = query.OrderByDescending(bfus => bfus.bf.ItemDateCreated);
            int total = query.Count();
            int totalPage = total / pageSize;
            if (total % pageSize != 0)
            {
                ++totalPage;
            }
            return new(total, totalPage, await query.Skip(itemsToSkip).Take(pageSize).Select(bfus => bfus.bf).ToListAsync());
        }
        public async Task<ListVM<BriefItem>?> GetAllShareNearYou(string city, int pageNumber, int pageSize, int userID)
        {
            int itemsToSkip = (pageNumber - 1) * pageSize;
            var query = _context.BriefItems
                .Join(_context.sadas, bf => bf.ItemId, it => it.ItemId, (bf, it) => new { bf, it })
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
            return new(total, totalPage, await query.Skip(itemsToSkip).Take(pageSize).Select(bfitus => bfitus.bfit.bf).ToListAsync());
        }
        public async Task<bool> DeactivateAllExpiredProducts()
        {
            try
            {
                DateTime dateTimeNow = DateTime.Now;
                List<Models.Item> items = await _context.sadas.Where(it => it.ItemExpiredTime != null).ToListAsync();
                if (items != null)
                {
                    foreach (Models.Item item in items)
                    {
                        TimeSpan distance = (TimeSpan)(item.ItemExpiredTime - dateTimeNow);
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
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return false;
            }
        }
        public async Task<ListVM<BriefItem>?> GetItemDynamicFilters(DynamicFilterItemVM dynamicFilterVM)
        {
            int itemsToSkip = (dynamicFilterVM.PageNumber - 1) * dynamicFilterVM.PageSize;
            var query = _context.BriefItems
                .Join(_context.sadas, bf => bf.ItemId, it => it.ItemId, (bf, it) => new { bf, it })
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
            if (!string.IsNullOrEmpty(dynamicFilterVM.TitleName) && !string.IsNullOrWhiteSpace(dynamicFilterVM.TitleName))
            {
                query = query.Where(query => query.bfit.bf.ItemTitle.Contains(dynamicFilterVM.TitleName));
            }
            if (dynamicFilterVM.Location != null)
            {
                string? locationString = _JsonToObj.TransformLocation(dynamicFilterVM.Location);
                if (locationString != null)
                {
                    query = query.Where(query => query.bfit.it.ItemShippingAddress.StartsWith(locationString));
                }
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
            return new(total, totalPage, await query.Skip(itemsToSkip).Take(dynamicFilterVM.PageSize).Select(bfitus => bfitus.bfit.bf).ToListAsync());
        }
        public async Task<ListVM<BriefItem>?> GetListAllOtherPersonRequestItem(bool share, bool status, int userID, int pageNumber, int pageSize)
        {
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
            return new(total, totalPage, await query.Select(bfus => bfus.bf).Skip(itemsToSkip).Take(pageSize).ToListAsync());
        }
        public async Task<ListVM<BriefItem>?> GetListAllMyRequestItem(bool share, bool status, int userID, int pageNumber, int pageSize)
        {
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
            return new(total, totalPage, await query.Skip(itemsToSkip).Take(pageSize).ToListAsync());
        }
        public async Task<int> RecordUserSearch(RecordSearchVM recordSearchVM)
        {
            RecordSearch? recordSearch = null;
            var query = _context.RecordSearches
                .Where(rs => rs.UserId == recordSearchVM.UserId
                && rs.CategoryId == recordSearchVM.CategoryId
                && rs.SubCategoryId == recordSearchVM.SubCategoryId);
            if (!string.IsNullOrEmpty(recordSearchVM.TitleName) && !string.IsNullOrWhiteSpace(recordSearchVM.TitleName))
            {
                query = query.Where(rs => rs.TitleName == recordSearchVM.TitleName.Trim());
            }
            else
            {
                recordSearchVM.TitleName = null;
                query = query.Where(rs => rs.TitleName == recordSearchVM.TitleName);
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
            return await _context.SaveChangesAsync();
        }
        public async Task<ListVM<BriefItem>?> GetListRecommend(int userID, int pageNumber, int pageSize)
        {
            RecordSearch? recordSearch = await _context.RecordSearches.Where(rs => rs.UserId == userID).OrderByDescending(rs => rs.Count).FirstOrDefaultAsync();
            if (recordSearch != null)
            {
                int itemsToSkip = (pageNumber - 1) * pageSize;
                var query = _context.BriefItems
                .Where(bf => bf.Share == true
                && bf.ItemStatus == true
                && bf.UserId != userID
                && bf.ItemStatus != false);
                if (recordSearch.CategoryId != null)
                {
                    query = query.Where(bf => bf.CategoryId == recordSearch.CategoryId);
                }
                if (recordSearch.SubCategoryId != null)
                {
                    query = query.Where(bf => bf.SubCategoryId == recordSearch.SubCategoryId);
                }
                if (recordSearch.TitleName != null)
                {
                    query = query.Where(bf => bf.ItemTitle.Contains(recordSearch.TitleName.Trim()));
                }
                int total = query.Count();
                int totalPage = total / pageSize;
                if (total % pageSize != 0)
                {
                    ++totalPage;
                }
                return new(total, totalPage, await query.OrderByDescending(bf => bf.ItemDateCreated).Skip(itemsToSkip).Take(pageSize).ToListAsync());
            }
            return null;
        }
        public async Task<ListVM<BriefItem>?> GetListRecommendByBaby(int babyID, int userID, int pageNumber, int pageSize, bool age, bool weight, bool height)
        {
            Baby? baby = await _context.Babies.Where(bb => bb.Idbaby == babyID && bb.UserId == userID).FirstOrDefaultAsync();
            if (baby != null)
            {
                int itemsToSkip = (pageNumber - 1) * pageSize;
                var query = _context.BriefItems
                .Join(_context.sadas, bf => bf.ItemId, it => it.ItemId, (bf, it) => new { bf, it })
                .Where(bfit => bfit.bf.Share == true
                && bfit.bf.ItemStatus == true
                && bfit.bf.UserId != userID);
                if (age)
                {
                    LocalDateTime now = DateTime.Now.ToLocalDateTime();
                    LocalDateTime babyBirth = baby.DateOfBirth.ToLocalDateTime();
                    Period period = Period.Between(babyBirth, now, PeriodUnits.AllDateUnits);
                    double monthsAge = period.Months;
                    if (monthsAge == 0)
                    {
                        double dayAge = period.Days;
                        monthsAge = (double)dayAge / 30;
                    }
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
                return new(total, totalPage, await query.OrderByDescending(bfit => bfit.bf.ItemDateCreated).Select(bfit => bfit.bf).Skip(itemsToSkip).Take(pageSize).ToListAsync());
            }
            return null;
        }
    }
}
