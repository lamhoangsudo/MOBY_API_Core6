using Category.Data_View_Model;

namespace MOBY_API_Core6.Repository
{
    public interface ISubCategoryRepository
    {
        Task <List<SubCategoryVM>> GetAllSubCategory(int categoryID);
        Task <bool> CreateSubCategory(int categoryID, String SubCategoryName);
        Task<bool> UpdateSubCategory(int subCategoryID, String SubCategoryName, int categoryID);
        Task<SubCategoryVM> GetSubCategoryByID(int subCategoryID);
        Task<bool> DeleteSubCategory(int subCategoryID);
        Task<List<SubCategoryVM>> GetSubCategoriesByName(string subCategoryName);
    }
}
