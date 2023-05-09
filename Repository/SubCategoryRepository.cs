using Category.Data_View_Model;
using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;

namespace MOBY_API_Core6.Repository
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly MOBYContext _context;

        public static string? ErrorMessage { get; set; }

        public SubCategoryRepository(MOBYContext context)
        {
            _context = context;
        }
        //done
        public async Task<bool> CreateSubCategory(CreateSubCategoryVM subCategoryVM)
        {
            try
            {
                if (!(String.IsNullOrEmpty(subCategoryVM.SubCategoryName) || String.IsNullOrWhiteSpace(subCategoryVM.SubCategoryName)))
                {
                    var checkCategory = await _context.Categories
                                    .Where(ct => ct.CategoryId == subCategoryVM.CategoryID)
                                    .FirstOrDefaultAsync();
                    if (checkCategory != null)
                    {
                        var checkSubCategory = await _context.SubCategories
                            .Where(sc => sc.SubCategoryName.Equals(subCategoryVM.SubCategoryName))
                            .FirstOrDefaultAsync();
                        if (checkSubCategory == null)
                        {
                            SubCategory subCategory = new()
                            {
                                SubCategoryName = subCategoryVM.SubCategoryName,
                                SubCategoryStatus = true,
                                CategoryId = subCategoryVM.CategoryID
                            };
                            await _context.SubCategories.AddAsync(subCategory);
                            await _context.SaveChangesAsync();
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }
        //done
        public async Task<bool> DeleteSubCategory(DeleteSubCategoryVM subCategoryVM)
        {
            try
            {
                SubCategory? subCategoryDelete = await _context.SubCategories.Where(sc => sc.SubCategoryId == subCategoryVM.SubCategoryID).FirstOrDefaultAsync();
                if (subCategoryDelete == null)
                {
                    ErrorMessage = "không tìm thấy";
                    return false;
                }
                subCategoryDelete.SubCategoryStatus = false;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }

        }
        //done
        public async Task<List<SubCategoryVM>?> GetSubCategoriesByName(string subCategoryName)
        {
            try
            {
                List<SubCategoryVM> listSubCategory = await _context.SubCategories
                                .Where(sc => sc.SubCategoryName.Contains(subCategoryName)).Select(subCategory => new SubCategoryVM
                                (
                                    subCategory.SubCategoryId,
                                    subCategory.CategoryId,
                                    subCategory.SubCategoryName,
                                    subCategory.SubCategoryStatus
                                )).ToListAsync();
                return listSubCategory;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }

        }
        //done
        public async Task<List<SubCategoryVM>?> GetAllSubCategory(int categoryID)
        {
            try
            {
                var checkCategory = await _context.Categories
                                .Where(ct => ct.CategoryId == categoryID)
                                .FirstOrDefaultAsync();
                if (checkCategory == null)
                {
                    ErrorMessage = "Category không tồn tại";
                    return null;
                }
                else
                {
                    var listSubCategory = await _context.SubCategories
                        .Where(sc => sc.CategoryId == categoryID)
                        .Select(subCategory => new SubCategoryVM
                        (
                             subCategory.SubCategoryId,
                             subCategory.SubCategoryName,
                             subCategory.SubCategoryStatus
                        )).ToListAsync();
                    return listSubCategory;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        //done
        public async Task<SubCategoryVM?> GetSubCategoryByID(int subCategoryID)
        {
            try
            {
                var checkSubCategory = await _context.SubCategories
                                .Where(sc => sc.SubCategoryId == subCategoryID)
                                .FirstOrDefaultAsync();
                if (checkSubCategory != null)
                {
                    return new SubCategoryVM
                    (
                         checkSubCategory.SubCategoryId,
                         checkSubCategory.CategoryId,
                         checkSubCategory.SubCategoryName,
                         checkSubCategory.SubCategoryStatus
                    );
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }

        }
        //done
        public async Task<bool> UpdateSubCategory(UpdateSubCategoryVM subCategoryVM)
        {
            try
            {
                if (!(String.IsNullOrEmpty(subCategoryVM.SubCategoryName) || String.IsNullOrWhiteSpace(subCategoryVM.SubCategoryName)))
                {
                    var checkCategory = await _context.Categories
                                .Where(ct => ct.CategoryId == subCategoryVM.CategoryID)
                                .FirstOrDefaultAsync();
                    if (checkCategory != null)
                    {
                        SubCategory? subCategoryUpdate = await _context.SubCategories
                            .Where(sc => sc.SubCategoryId == subCategoryVM.SubCategoryID)
                            .FirstOrDefaultAsync();
                        if (subCategoryUpdate != null)
                        {
                            subCategoryUpdate.SubCategoryName = subCategoryVM.SubCategoryName;
                            subCategoryUpdate.CategoryId = subCategoryVM.CategoryID;
                            subCategoryUpdate.SubCategoryStatus = true;
                            await _context.SaveChangesAsync();
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }
    }
}
