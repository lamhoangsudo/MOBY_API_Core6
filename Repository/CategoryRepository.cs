using Category.Data_View_Model;
using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
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

        public async Task<bool> CreateCategory(CreateCategoryVM categoryVM)
        {
            var checkCategory = _context.Categories.FromSqlInterpolated($"EXEC get_Category_By_Name {categoryVM.categoryName}").ToList().SingleOrDefault();
            if (checkCategory == null)
            {
                var checkCreate = _context.Database.ExecuteSqlInterpolated($"EXEC create_Category {categoryVM.categoryName}, {categoryVM.categoryImage}");
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

        public async Task<bool> UpdateCategory(UpdateCategoryVM categoryVM)
        {
            var checkUpdateCategory = _context.Categories.FromSqlInterpolated($"EXEC get_Category_By_Name {categoryVM.categoryName}").ToList().SingleOrDefault();
            if (checkUpdateCategory == null)
            {
                var checkUpdate = _context.Database.ExecuteSqlInterpolated($"EXEC update_Category {categoryVM.categoryID},{categoryVM.categoryName},{categoryVM.categoryImage}");
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

        public async Task<bool> DeleteCategory(DeleteCategoryVM categoryVM)
        {
            var checkDelete = _context.Database.ExecuteSqlInterpolated($"EXEC delete_Category {categoryVM.categoryID}");
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

        public async Task<List<CategoryVM>> GetAllCategoriesAndSubCategory()
        {
            List<CategoryVM> categoryVMs = new List<CategoryVM>();
            var listCategory = _context.Categories.FromSqlRaw($"EXEC get_All_Category").ToList();
            foreach (var category in listCategory)
            {
                CategoryVM vM = new CategoryVM();
                vM.CategoryId = category.CategoryId;
                vM.CategoryName = category.CategoryName;
                vM.CategoryStatus = category.CategoryStatus;
                vM.CategoryImage = category.CategoryImage;
                var listSubCategory = _context.SubCategories.FromSqlInterpolated($"EXEC get_All_SubCategory {vM.CategoryId}").ToList().Select(subCategory => new SubCategoryVM
                {
                    SubCategoryId = subCategory.SubCategoryId,
                    SubCategoryName = subCategory.SubCategoryName,
                    SubCategoryStatus = subCategory.SubCategoryStatus,
                    CategoryId = subCategory.CategoryId,
                });
                vM.subCategoryVMs = listSubCategory.ToList();
                categoryVMs.Add(vM);
            }
            return categoryVMs;
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
