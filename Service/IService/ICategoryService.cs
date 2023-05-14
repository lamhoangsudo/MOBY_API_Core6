using Category.Data_View_Model;
using MOBY_API_Core6.Data_View_Model;

namespace MOBY_API_Core6.Service.IService
{
    public interface ICategoryService
    {
        Task<List<CategoryVM>?> GetAllCategoriesAndSubCategory();
        Task<List<CategoryVM>?> GetCategoriesByStatus(bool categoryStatus);
        Task<List<CategoryVM>?> GetCategoriesByName(string categoryName);
        Task<bool> CreateCategory(CreateCategoryVM categoryVM);
        Task<CategoryVM?> GetCategoryByID(int categoryID);
        Task<bool> UpdateCategory(UpdateCategoryVM categoryVM);
        Task<bool> DeleteCategory(DeleteCategoryVM categoryVM);
    }
}
