using Category.Data_View_Model;
using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;

namespace MOBY_API_Core6.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MOBYContext _context;
        public CategoryRepository(MOBYContext context)
        {
            _context = context;
        }
        public async Task<CategoryVM?> GetCategoryByID(int categoryID)
        {
            CategoryVM? checkCategory = await _context.Categories
                .Where(ct => ct.CategoryId == categoryID)
                .Select(ct => new CategoryVM(ct.CategoryId, ct.CategoryName, ct.CategoryImage, ct.CategoryStatus)).FirstOrDefaultAsync();
            if (checkCategory != null)
            {
                return checkCategory;
            }
            throw new NullReferenceException();
        }
        public async Task<int> CreateCategory(CreateCategoryVM categoryVM)
        {
            var checkCategory = await _context.Categories.Where(ct => ct.CategoryName.Equals(categoryVM.CategoryName)).FirstOrDefaultAsync();
            if (checkCategory == null)
            {
                Models.Category category = new()
                {
                    CategoryName = categoryVM.CategoryName,
                    CategoryImage = categoryVM.CategoryImage,
                    CategoryStatus = true
                };
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
                return 1;
            }
            throw new DuplicateWaitObjectException();
        }
        public async Task<int> UpdateCategory(UpdateCategoryVM categoryVM)
        {
            Models.Category? updateCategory = await _context.Categories.Where(ct => ct.CategoryId == categoryVM.CategoryID).FirstOrDefaultAsync();
            if (updateCategory != null)
            {
                updateCategory.CategoryName = categoryVM.CategoryName;
                updateCategory.CategoryImage = categoryVM.CategoryImage;
                updateCategory.CategoryStatus = true;
                await _context.SaveChangesAsync();
                return 1;
            }
            throw new KeyNotFoundException();
        }
        public async Task<int> DeleteCategory(DeleteCategoryVM categoryVM)
        {
            Models.Category? deleteCategory = await _context.Categories.Where(ct => ct.CategoryId == categoryVM.CategoryID).FirstOrDefaultAsync();
            if (deleteCategory != null)
            {
                deleteCategory.CategoryStatus = false;
                return await _context.SaveChangesAsync();
            }
            throw new KeyNotFoundException();
        }
        public async Task<List<CategoryVM>?> GetAllCategoriesAndSubCategory()
        {
            return await _context.Categories
                .Include(ct => ct.SubCategories)
                .Select(ct => new CategoryVM(
                    ct.CategoryId,
                    ct.CategoryName,
                    ct.CategoryImage,
                    ct.CategoryStatus,
                    ct.SubCategories.Select(sc => new SubCategoryVM(
                        sc.SubCategoryId,
                        sc.SubCategoryName,
                        sc.SubCategoryStatus
                        )).ToList()
                )).ToListAsync();
        }
        public async Task<List<CategoryVM>?> GetCategoriesByStatus(bool categoryStatus)
        {
            return await _context.Categories
                .Where(ct => ct.CategoryStatus == categoryStatus)
                .Select(ct => new CategoryVM
                (
                    ct.CategoryId,
                    ct.CategoryName,
                    ct.CategoryImage,
                    ct.CategoryStatus
                ))
                .ToListAsync();
        }
        public async Task<List<CategoryVM>?> GetCategoriesByName(string categoryName)
        {
            return await _context.Categories
                .Where(ct => ct.CategoryName.Contains(categoryName))
                .Select(category => new CategoryVM
                (
                    category.CategoryId,
                    category.CategoryName,
                    category.CategoryImage,
                    category.CategoryStatus
                ))
                .ToListAsync();
        }
    }
}
