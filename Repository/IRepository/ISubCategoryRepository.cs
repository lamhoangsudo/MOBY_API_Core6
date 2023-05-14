using Category.Data_View_Model;
using MOBY_API_Core6.Data_View_Model;

namespace MOBY_API_Core6.Repository.IRepository
{
    public interface ISubCategoryRepository
    {
        Task<int> CreateSubCategory(CreateSubCategoryVM subCategoryVM);
        Task<int> DeleteSubCategory(DeleteSubCategoryVM subCategoryVM);
        Task<List<SubCategoryVM>?> GetSubCategoriesByName(string subCategoryName);
        Task<List<SubCategoryVM>?> GetAllSubCategory(int categoryID);
        Task<SubCategoryVM?> GetSubCategoryByID(int subCategoryID);
        Task<int> UpdateSubCategory(UpdateSubCategoryVM subCategoryVM);
    }
}
