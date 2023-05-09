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
        public static string? ErrorMessage { get; set; }
        public CategoryRepository(MOBYContext context)
        {
            _context = context;
        }
        //done
        public async Task<CategoryVM?> GetCategoryByID(int categoryID)
        {
            try
            {
                CategoryVM? checkCategory = await _context.Categories
                    .Where(ct => ct.CategoryId == categoryID)
                    .Select(ct => new CategoryVM(
                        ct.CategoryId,
                        ct.CategoryName,
                        ct.CategoryImage,
                        ct.CategoryStatus
                        ))
                    .FirstOrDefaultAsync();
                return checkCategory;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        //done
        public async Task<bool> CreateCategory(CreateCategoryVM categoryVM)
        {
            var checkCategory = await _context.Categories.Where(ct => ct.CategoryName.Equals(categoryVM.CategoryName)).FirstOrDefaultAsync();
            if (checkCategory == null)
            {
                Models.Category category = new()
                {
#pragma warning disable CS8601 // Possible null reference assignment.
                    CategoryName = categoryVM.CategoryName,
#pragma warning restore CS8601 // Possible null reference assignment.
                    CategoryImage = categoryVM.CategoryImage,
                    CategoryStatus = true
                };
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        //done
        public async Task<bool> UpdateCategory(UpdateCategoryVM categoryVM)
        {
            try
            {
                Models.Category? updateCategory = await _context.Categories.Where(ct => ct.CategoryId == categoryVM.CategoryID).FirstOrDefaultAsync();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8601 // Possible null reference assignment.
                updateCategory.CategoryName = categoryVM.CategoryName;
#pragma warning restore CS8601 // Possible null reference assignment.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                updateCategory.CategoryImage = categoryVM.CategoryImage;
                updateCategory.CategoryStatus = true;
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
        public async Task<bool> DeleteCategory(DeleteCategoryVM categoryVM)
        {
            try
            {
                Models.Category? deleteCategory = await _context.Categories.Where(ct => ct.CategoryId == categoryVM.CategoryID).FirstOrDefaultAsync();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                deleteCategory.CategoryStatus = false;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
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
        public async Task<List<CategoryVM>?> GetAllCategoriesAndSubCategory()
        {
            try
            {
                List<CategoryVM> categoryVMs = new();
                categoryVMs = await _context.Categories
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
                return categoryVMs;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        //done
        public async Task<List<CategoryVM>?> GetCategoriesByStatus(bool categoryStatus)
        {
            var listCategory = await _context.Categories
                .Where(ct => ct.CategoryStatus == categoryStatus)
                .Select(ct => new CategoryVM
                (
                    ct.CategoryId,
                    ct.CategoryName,
                    ct.CategoryImage,
                    ct.CategoryStatus
                ))
                .ToListAsync();
            return listCategory;
        }
        //done
        public async Task<List<CategoryVM>?> GetCategoriesByName(string categoryName)
        {
            var listCategory = await _context.Categories
                .Where(ct => ct.CategoryName.Contains(categoryName))
                .Select(category => new CategoryVM
                (
                    category.CategoryId,
                    category.CategoryName,
                    category.CategoryImage,
                    category.CategoryStatus
                ))
                .ToListAsync();
            return listCategory;
        }
    }
}
