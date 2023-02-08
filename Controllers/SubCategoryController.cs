using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Repository;

namespace MOBY_API_Core6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryRepository _subCategoryRepository;
        public SubCategoryController(ISubCategoryRepository subCategoryRepository)
        {
            _subCategoryRepository = subCategoryRepository;
        }

        [HttpPost("createSubCategory/{subCategoryName}")]
        public IActionResult createSubCategory(int categoryID, string subCategoryName)
        {
            try
            {
                bool checkCreate = _subCategoryRepository.CreateSubCategory(categoryID, subCategoryName);
                if (checkCreate)
                {
                    return Ok(subCategoryName);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("GetAllSubCategory/{categoryID}")]
        public IActionResult GetAllSubCategory(int categoryID)
        {
            try
            {
                var listSubCategory = _subCategoryRepository.GetAllSubCategory(categoryID);
                if (listSubCategory == null || listSubCategory.Count == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                else
                {
                    return Ok(listSubCategory);
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("updateSubCategory/{subCategoryName} {subCategoryID}")]
        public IActionResult updateSubCategory(int categoryID, string subCategoryName, int subCategoryID)
        {
            try
            {
                var subCategory = _subCategoryRepository.GetSubCategoryByID(subCategoryID);
                if (subCategory == null || subCategory.SubCategoryStatus == false)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                else
                {
                    bool check = _subCategoryRepository.UpdateSubCategory(subCategoryID, subCategoryName, categoryID);
                    if (check == true)
                    {
                        return Ok(subCategoryName);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status400BadRequest);
                    }
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("DeleteCategory/{subCategoryID}")]
        public IActionResult DeleteCategory(int subCategoryID)
        {
            try
            {
                var subCategory = _subCategoryRepository.GetSubCategoryByID(subCategoryID);
                if (subCategory == null || subCategory.SubCategoryStatus == false)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                else
                {
                    bool check = _subCategoryRepository.DeleteSubCategory(subCategoryID);
                    if (check == true)
                    {
                        return Ok(subCategory);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status400BadRequest);
                    }
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("SearchSubCategoryName/{subCategoryName}")]
        public IActionResult GetSubCategoryByName(string subCategoryName)
        {
            try
            {
                var listSubCategory = _subCategoryRepository.GetSubCategoriesByName(subCategoryName);
                if (listSubCategory == null || listSubCategory.Count == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                else
                {
                    return Ok(listSubCategory);
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
