using Category.Data_View_Model;

namespace MOBY_API_Core6.Repository
{
    public interface ICategoryRepository
    {
        Task<List<CategoryVM>> GetAllCategories();
        Task<List<CategoryVM>> GetCategoriesByStatus(bool categoryStatus);
        Task<List<CategoryVM>> GetCategoriesByName(string categoryName);
        Task<bool> CreateCategory(string categoryName, string categoryImage);
        Task<CategoryVM> GetCategoryByID(int categoryID);
        Task<bool> UpdateCategory(int categoryID, string categoryName, string categoryImage);
        Task<bool> DeleteCategory(int categoryID);
    }
}
