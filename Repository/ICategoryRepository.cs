using Category.Data_View_Model;

namespace MOBY_API_Core6.Repository
{
    public interface ICategoryRepository
    {
        List<CategoryVM> GetAllCategories();
        List<CategoryVM> GetCategoriesByStatus(bool categoryStatus);
        List<CategoryVM> GetCategoriesByName(string categoryName);
        bool CreateCategory(string categoryName, string categoryImage);
        CategoryVM GetCategoryByID(int categoryID);
        bool UpdateCategory(int categoryID, string categoryName, string categoryImage);
        bool DeleteCategory(int categoryID);
    }
}
