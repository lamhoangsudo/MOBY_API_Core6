using Category.Data_View_Model;
using MOBY_API_Core6.Data_View_Model;

namespace MOBY_API_Core6.Repository.IRepository
{
    public interface ICategoryRepository
    {
        Task<CategoryVM?> GetCategoryByID(int categoryID);
        Task<int> CreateCategory(CreateCategoryVM categoryVM);
        Task<int> UpdateCategory(UpdateCategoryVM categoryVM);
        Task<int> DeleteCategory(DeleteCategoryVM categoryVM);
        Task<List<CategoryVM>?> GetAllCategoriesAndSubCategory();
        Task<List<CategoryVM>?> GetCategoriesByStatus(bool categoryStatus);
        Task<List<CategoryVM>?> GetCategoriesByName(string categoryName);

    }
}
