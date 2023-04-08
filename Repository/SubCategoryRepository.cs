using Category.Data_View_Model;
using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        public static string? errorMessage;
        private readonly MOBYContext _context;
        public SubCategoryRepository(MOBYContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateSubCategory(CreateSubCategoryVM subCategoryVM)
        {
            try
            {
                var checkCategory = await _context.Categories
                                .Where(ct => ct.CategoryId == subCategoryVM.categoryID)
                                .FirstOrDefaultAsync();
                if (checkCategory == null || subCategoryVM.subCategoryName == null || subCategoryVM.subCategoryName.Equals(""))
                {
                    errorMessage = "Category không tồn tại";
                    return false;
                }
                else
                {
                    var checkSubCategory = await _context.SubCategories
                        .Where(sc => sc.SubCategoryName.Equals(subCategoryVM.subCategoryName))
                        .FirstOrDefaultAsync();
                    if (checkSubCategory == null)
                    {
                        SubCategory subCategory = new SubCategory();
                        subCategory.SubCategoryName = subCategoryVM.subCategoryName;
                        subCategory.SubCategoryStatus = true;
                        subCategory.CategoryId = subCategoryVM.categoryID;
                        await _context.SubCategories.AddAsync(subCategory);
                        await _context.SaveChangesAsync();
                        return true;
                    }
                    else
                    {
                        errorMessage = "SubCategory không tồn tại";
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public async Task<bool> DeleteSubCategory(DeleteSubCategoryVM subCategoryVM)
        {
            try
            {
                SubCategory? subCategoryDelete = await _context.SubCategories.Where(sc => sc.SubCategoryId == subCategoryVM.subCategoryID).FirstOrDefaultAsync();
                if (subCategoryDelete == null)
                {
                    errorMessage = "không tìm thấy";
                    return false;
                }
                subCategoryDelete.SubCategoryStatus = false;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }

        }

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
                errorMessage = ex.Message;
                return null;
            }

        }

        public async Task<List<SubCategoryVM>?> GetAllSubCategory(int categoryID)
        {
            try
            {
                var checkCategory = await _context.Categories
                                .Where(ct => ct.CategoryId == categoryID)
                                .FirstOrDefaultAsync();
                if (checkCategory == null)
                {
                    errorMessage = "Category không tồn tại";
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
                errorMessage = ex.Message;
                return null;
            }
        }

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
                errorMessage = ex.Message;
                return null;
            }

        }

        public async Task<bool> UpdateSubCategory(UpdateSubCategoryVM subCategoryVM)
        {
            try
            {
                var checkCategory = await _context.Categories
                                .Where(ct => ct.CategoryId == subCategoryVM.categoryID)
                                .FirstOrDefaultAsync();
                if (checkCategory == null || subCategoryVM.subCategoryName == null || subCategoryVM.subCategoryName.Equals(""))
                {
                    return false;
                }
                else
                {
                    SubCategory? subCategoryUpdate = await _context.SubCategories
                        .Where(sc => sc.SubCategoryId == subCategoryVM.subCategoryID)
                        .FirstOrDefaultAsync();
                    if (subCategoryUpdate == null)
                    {
                        return false;
                    }
                    subCategoryUpdate.SubCategoryName = subCategoryVM.subCategoryName;
                    subCategoryUpdate.CategoryId = subCategoryVM.categoryID;
                    subCategoryUpdate.SubCategoryStatus = true;
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }

        }
    }
}
