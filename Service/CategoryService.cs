using Category.Data_View_Model;
using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly MOBYContext _context;
        private readonly ICategoryRepository _categoryRepository;
        public static string? ErrorMessage { get; set; }
        public CategoryService(MOBYContext context, ICategoryRepository categoryRepository)
        {
            _context = context;
            _categoryRepository = categoryRepository;
        }
        public async Task<CategoryVM?> GetCategoryByID(int categoryID)
        {
            try
            {
                CategoryVM? checkCategory = await _categoryRepository.GetCategoryByID(categoryID);
                return checkCategory;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        public async Task<bool> CreateCategory(CreateCategoryVM categoryVM)
        {
            try
            {
                int check = await _categoryRepository.CreateCategory(categoryVM);
                if (check <= 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }
        public async Task<bool> UpdateCategory(UpdateCategoryVM categoryVM)
        {
            try
            {
                int check = await _categoryRepository.UpdateCategory(categoryVM);
                if (check <= 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }
        public async Task<bool> DeleteCategory(DeleteCategoryVM categoryVM)
        {
            try
            {
                int check = await _categoryRepository.DeleteCategory(categoryVM);
                if (check <= 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }
        public async Task<List<CategoryVM>?> GetAllCategoriesAndSubCategory()
        {
            try
            {
                List<CategoryVM>? categoryVMs = new();
                categoryVMs = await _categoryRepository.GetAllCategoriesAndSubCategory();
                return categoryVMs;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        public async Task<List<CategoryVM>?> GetCategoriesByStatus(bool categoryStatus)
        {
            try
            {
                List<CategoryVM>? listCategory = new();
                listCategory = await _categoryRepository.GetCategoriesByStatus(categoryStatus);
                return listCategory;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }

        }
        public async Task<List<CategoryVM>?> GetCategoriesByName(string categoryName)
        {
            try
            {
                List<CategoryVM>? listCategory = new();
                listCategory = await _categoryRepository.GetCategoriesByName(categoryName);
                return listCategory;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
    }
}
