using Category.Data_View_Model;
using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly MOBYContext _context;
        public SubCategoryRepository(MOBYContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateSubCategory(int categoryID, string SubCategoryName)
        {
            var checkCategory = _context.Categories.FromSqlInterpolated($"EXEC get_Category_By_ID {categoryID}").ToList().SingleOrDefault();
            if (checkCategory == null || SubCategoryName == null || SubCategoryName.Equals(""))
            {
                return false;
            }
            else
            {
                var checkSubCategory = _context.SubCategories.FromSqlInterpolated($"EXEC get_SubCategory_By_Name {SubCategoryName}").ToList().SingleOrDefault();
                if (checkSubCategory == null)
                {
                    var checkCreate = _context.Database.ExecuteSqlInterpolated($"EXEC create_SubCategory {categoryID}, {SubCategoryName}");
                    if (checkCreate != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
        }

        public async Task<bool> DeleteSubCategory(int subCategoryID)
        {
            var checkDelete = _context.Database.ExecuteSqlInterpolated($"EXEC delete_SubCategory {subCategoryID}");
            if (checkDelete != 0)
            {
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<SubCategoryVM>> GetSubCategoriesByName(string subCategoryName)
        {
            subCategoryName = "%" + subCategoryName + "%";
            var listSubCategory = _context.SubCategories.FromSqlInterpolated($"EXEC search_SubCategory_By_Name {subCategoryName}").ToList().Select(subCategory => new SubCategoryVM
            {
                SubCategoryId = subCategory.SubCategoryId,
                SubCategoryName = subCategory.SubCategoryName,
                CategoryId = subCategory.CategoryId,
                SubCategoryStatus = subCategory.SubCategoryStatus,
            });
            return listSubCategory.ToList();
        }

        public async Task<List<SubCategoryVM>> GetAllSubCategory(int categoryID)
        {
            var checkCategory = _context.Categories.FromSqlInterpolated($"EXEC get_SubCategory_By_ID {categoryID}").ToList().SingleOrDefault();
            if (checkCategory == null)
            {
                return null;
            }
            else
            {
                var listSubCategory = _context.SubCategories.FromSqlInterpolated($"EXEC get_All_SubCategory {categoryID}").ToList().Select(subCategory => new SubCategoryVM
                {
                    SubCategoryId = subCategory.SubCategoryId,
                    SubCategoryName = subCategory.SubCategoryName,
                    SubCategoryStatus = subCategory.SubCategoryStatus,
                    CategoryId = subCategory.CategoryId,
                });
                return listSubCategory.ToList();
            }
        }

        public async Task<SubCategoryVM> GetSubCategoryByID(int subCategoryID)
        {
            var checkSubCategory = _context.SubCategories.FromSqlInterpolated($"EXEC get_SubCategory_By_ID {subCategoryID}").ToList().SingleOrDefault();
            if (checkSubCategory != null)
            {
                return new SubCategoryVM
                {
                    SubCategoryId = checkSubCategory.SubCategoryId,
                    SubCategoryName = checkSubCategory.SubCategoryName,
                    SubCategoryStatus = checkSubCategory.SubCategoryStatus,
                    CategoryId = checkSubCategory.CategoryId,
                };
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> UpdateSubCategory(int subCategoryID, string SubCategoryName, int categoryID)
        {
            var checkUpdateSubCategory = _context.Categories.FromSqlInterpolated($"EXEC get_Category_By_ID {categoryID}").ToList().SingleOrDefault();
            if (checkUpdateSubCategory == null)
            {
                var checkSubCategory = _context.Categories.FromSqlInterpolated($"EXEC get_Category_By_ID {categoryID}").ToList().SingleOrDefault();
                if (checkSubCategory == null || SubCategoryName == null || SubCategoryName.Equals(""))
                {
                    return false;
                }
                else
                {
                    var checkUpdate = _context.Database.ExecuteSqlInterpolated($"EXEC update_SubCategory {subCategoryID},{SubCategoryName},{categoryID}");
                    if (checkUpdate != 0)
                    {
                        _context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            else
            {
                return false;
            }
        }
    }
}
