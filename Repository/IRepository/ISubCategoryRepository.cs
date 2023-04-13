using Category.Data_View_Model;
using MOBY_API_Core6.Data_View_Model;

namespace MOBY_API_Core6.Repository.IRepository
{
    public interface ISubCategoryRepository
    {
        Task<List<SubCategoryVM>?> GetAllSubCategory(int categoryID);
        Task<bool> CreateSubCategory(CreateSubCategoryVM subCategoryVM);
        Task<bool> UpdateSubCategory(UpdateSubCategoryVM subCategoryVM);
        Task<SubCategoryVM?> GetSubCategoryByID(int subCategoryID);
        Task<bool> DeleteSubCategory(DeleteSubCategoryVM subCategoryVM);
        Task<List<SubCategoryVM>?> GetSubCategoriesByName(string subCategoryName);
    }
}
