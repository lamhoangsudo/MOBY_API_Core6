﻿using Category.Data_View_Model;
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

        public async Task<CategoryVM?> GetCategoryByID(int categoryID)
        {
            var checkCategory = await _context.Categories.FromSqlInterpolated($"EXEC get_Category_By_ID {categoryID}").SingleOrDefaultAsync();
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
            var checkCategory = await _context.Categories.FromSqlInterpolated($"EXEC get_Category_By_Name {categoryVM.categoryName}").SingleOrDefaultAsync();
            if (checkCategory == null)
            {
                var checkCreate = await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC create_Category {categoryVM.categoryName}, {categoryVM.categoryImage}");
                if (checkCreate != 0)
                {
                    await _context.SaveChangesAsync();
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
            var checkUpdateCategory = await _context.Categories.FromSqlInterpolated($"EXEC get_Category_By_Name {categoryVM.categoryName}").SingleOrDefaultAsync();
            if (checkUpdateCategory == null)
            {
                var checkUpdate = await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC update_Category {categoryVM.categoryID},{categoryVM.categoryName},{categoryVM.categoryImage}");
                if (checkUpdate != 0)
                {
                    await _context.SaveChangesAsync();
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
            var checkDelete = await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC delete_Category {categoryVM.categoryID}");
            if (checkDelete != 0)
            {
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<CategoryVM>?> GetAllCategoriesAndSubCategory()
        {
            List<CategoryVM> categoryVMs = new List<CategoryVM>();
            var listCategory = await _context.Categories
                .FromSqlInterpolated($"EXEC get_All_Category")
                .ToListAsync();
            foreach (var category in listCategory)
            {
                CategoryVM vM = new CategoryVM();
                vM.CategoryId = category.CategoryId;
                vM.CategoryName = category.CategoryName;
                vM.CategoryStatus = category.CategoryStatus;
                vM.CategoryImage = category.CategoryImage;
                var listSubCategory = await _context.SubCategories
                    .FromSqlInterpolated($"EXEC get_All_SubCategory {vM.CategoryId}")
                    .Select(subCategory => new SubCategoryVM
                {
                    SubCategoryId = subCategory.SubCategoryId,
                    SubCategoryName = subCategory.SubCategoryName,
                    SubCategoryStatus = subCategory.SubCategoryStatus,
                    CategoryId = subCategory.CategoryId,
                }).ToListAsync();
                vM.subCategoryVMs = listSubCategory;
                categoryVMs.Add(vM);
            }
            return categoryVMs;
        }

        public async Task<List<CategoryVM>?> GetCategoriesByStatus(bool categoryStatus)
        {
            var listCategory = await _context.Categories
                .FromSqlInterpolated($"EXEC get_Category_By_Status {categoryStatus}")
                .Select(category => new CategoryVM { 
                    CategoryId = category.CategoryId, 
                    CategoryName = category.CategoryName, 
                    CategoryImage = category.CategoryImage, 
                    CategoryStatus = category.CategoryStatus })
                .ToListAsync();
            return listCategory;
        }

        public async Task<List<CategoryVM>?> GetCategoriesByName(string categoryName)
        {
            categoryName = "%" + categoryName + "%";
            var listCategory = await _context.Categories
                .FromSqlInterpolated($"EXEC search_Category_By_Name {categoryName}")
                .Select(category => new CategoryVM
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                CategoryImage = category.CategoryImage,
                CategoryStatus = category.CategoryStatus,
            }).ToListAsync();
            return listCategory;
        }
    }
}
