using Category.Data_View_Model;
using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MOBYContext _context;

        public CategoryRepository(MOBYContext context)
        {
            _context = context;
        }

        public async Task<CategoryVM> GetCategoryByID(int categoryID)
        {
            var checkCategory = _context.Categories.FromSqlInterpolated($"EXEC get_Category_By_ID {categoryID}").ToList().SingleOrDefault();
            if (checkCategory != null)
            {
                return new CategoryVM
                {
                    CategoryId = checkCategory.CategoryId,
                    CategoryName = checkCategory.CategoryName,
                    CategoryImage = checkCategory.CategoryImage,
                    CategoryStatus = checkCategory.CategoryStatus,
                };
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> CreateCategory(string categoryName, string categoryImage)
        {
            var checkCategory = _context.Categories.FromSqlInterpolated($"EXEC get_Category_By_Name {categoryName}").ToList().SingleOrDefault();
            if (checkCategory == null)
            {
                var checkCreate = _context.Database.ExecuteSqlInterpolated($"EXEC create_Category {categoryName}, {categoryImage}");
                if (checkCreate != 0)
                {
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateCategory(int categoryID, string categoryName, string categoryImage)
        {
            var checkUpdateCategory = _context.Categories.FromSqlInterpolated($"EXEC get_Category_By_Name {categoryName}").ToList().SingleOrDefault();
            if (checkUpdateCategory == null)
            {
                var checkUpdate = _context.Database.ExecuteSqlInterpolated($"EXEC update_Category {categoryID},{categoryName},{categoryImage}");
                if (checkUpdate != 0)
                {
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        public async Task<bool> DeleteCategory(int categoryID)
        {
            var checkDelete = _context.Database.ExecuteSqlInterpolated($"EXEC delete_Category {categoryID}");
            if (checkDelete != 0)
            {
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<CategoryVM>> GetAllCategories()
        {
            var listCategory = _context.Categories.FromSqlRaw($"EXEC get_All_Category").ToList().Select(category => new CategoryVM
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                CategoryImage = category.CategoryImage,
                CategoryStatus = category.CategoryStatus,
            });
            return listCategory.ToList();
        }

        public async Task<List<CategoryVM>> GetCategoriesByStatus(bool categoryStatus)
        {
            var listCategory = _context.Categories.FromSqlInterpolated($"EXEC get_Category_By_Status {categoryStatus}").ToList().Select(category => new CategoryVM
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                CategoryImage = category.CategoryImage,
                CategoryStatus = category.CategoryStatus,
            });
            return listCategory.ToList();
        }

        public async Task<List<CategoryVM>> GetCategoriesByName(string categoryName)
        {
            categoryName = "%" + categoryName + "%";
            var listCategory = _context.Categories.FromSqlInterpolated($"EXEC search_Category_By_Name {categoryName}").ToList().Select(category => new CategoryVM
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                CategoryImage = category.CategoryImage,
                CategoryStatus = category.CategoryStatus,
            });
            return listCategory.ToList();
        }
    }
}
