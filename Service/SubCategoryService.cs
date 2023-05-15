using Category.Data_View_Model;
using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Service
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly MOBYContext _context;
        private readonly ISubCategoryRepository _subCategoryRepository;
        public static string? ErrorMessage { get; set; }

        public SubCategoryService(MOBYContext context, ISubCategoryRepository subCategoryRepository)
        {
            _context = context;
            _subCategoryRepository = subCategoryRepository;
        }
        public async Task<bool> CreateSubCategory(CreateSubCategoryVM subCategoryVM)
        {
            try
            {
                if (!(string.IsNullOrEmpty(subCategoryVM.SubCategoryName) || string.IsNullOrWhiteSpace(subCategoryVM.SubCategoryName)))
                {
                    int check = await _subCategoryRepository.CreateSubCategory(subCategoryVM);
                    if (check != 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }
        public async Task<bool> DeleteSubCategory(DeleteSubCategoryVM subCategoryVM)
        {
            try
            {
                int check = await _subCategoryRepository.DeleteSubCategory(subCategoryVM);
                if (check != 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }

        }
        public async Task<List<SubCategoryVM>?> GetSubCategoriesByName(string subCategoryName)
        {
            try
            {
                List<SubCategoryVM>? listSubCategory = await _subCategoryRepository.GetSubCategoriesByName(subCategoryName);
                if (listSubCategory == null)
                {
                    listSubCategory = new List<SubCategoryVM>();
                }
                return listSubCategory;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }

        }
        public async Task<List<SubCategoryVM>?> GetAllSubCategory(int categoryID)
        {
            try
            {
                return await _subCategoryRepository.GetAllSubCategory(categoryID);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        public async Task<SubCategoryVM?> GetSubCategoryByID(int subCategoryID)
        {
            try
            {
                return await _subCategoryRepository.GetSubCategoryByID(subCategoryID);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }

        }
        public async Task<bool> UpdateSubCategory(UpdateSubCategoryVM subCategoryVM)
        {
            try
            {
                if (!(string.IsNullOrEmpty(subCategoryVM.SubCategoryName) || string.IsNullOrWhiteSpace(subCategoryVM.SubCategoryName)))
                {
                    int check = await _subCategoryRepository.UpdateSubCategory(subCategoryVM);
                    if (check > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }
    }
}
