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
        public SubCategoryRepository(MOBYContext context)
        {
            _context = context;
        }
        public async Task<int> CreateSubCategory(CreateSubCategoryVM subCategoryVM)
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
                    return await _context.SaveChangesAsync();
                }
            }
            throw new KeyNotFoundException();
        }
        public async Task<int> DeleteSubCategory(DeleteSubCategoryVM subCategoryVM)
        {
            SubCategory? subCategoryDelete = await _context.SubCategories.Where(sc => sc.SubCategoryId == subCategoryVM.SubCategoryID).FirstOrDefaultAsync();
            if (subCategoryDelete != null)
            {
                subCategoryDelete.SubCategoryStatus = false;
                return await _context.SaveChangesAsync();
            }
            throw new KeyNotFoundException();

        }
        public async Task<List<SubCategoryVM>?> GetSubCategoriesByName(string subCategoryName)
        {
            return await _context.SubCategories
                            .Where(sc => sc.SubCategoryName.Contains(subCategoryName)).Select(subCategory => new SubCategoryVM
                            (
                                subCategory.SubCategoryId,
                                subCategory.CategoryId,
                                subCategory.SubCategoryName,
                                subCategory.SubCategoryStatus
                            )).ToListAsync();
        }
        public async Task<List<SubCategoryVM>?> GetAllSubCategory(int categoryID)
        {
            var checkCategory = await _context.Categories
                            .Where(ct => ct.CategoryId == categoryID)
                            .FirstOrDefaultAsync();
            if (checkCategory != null)
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
            throw new KeyNotFoundException();
        }
        public async Task<SubCategoryVM?> GetSubCategoryByID(int subCategoryID)
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
            throw new KeyNotFoundException();

        }
        public async Task<int> UpdateSubCategory(UpdateSubCategoryVM subCategoryVM)
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
                    return await _context.SaveChangesAsync();
                }
            }
            throw new KeyNotFoundException();
        }
    }
}
