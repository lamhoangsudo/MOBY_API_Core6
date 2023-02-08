using Category.Data_View_Model;

namespace MOBY_API_Core6.Repository
{
    public interface ISubCategoryRepository
    {
        List<SubCategoryVM> GetAllSubCategory(int categoryID);
        bool CreateSubCategory(int categoryID, String SubCategoryName);
        bool UpdateSubCategory(int subCategoryID, String SubCategoryName, int categoryID);
        SubCategoryVM GetSubCategoryByID(int subCategoryID);
        bool DeleteSubCategory(int subCategoryID);
        List<SubCategoryVM> GetSubCategoriesByName(string subCategoryName);
    }
}
