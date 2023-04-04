using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
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

        [HttpPost("createSubCategory")]
        public async Task<IActionResult> createSubCategory([FromBody] CreateSubCategoryVM subCategoryVM)
        {
            try
            {
                bool checkCreate = await _subCategoryRepository.CreateSubCategory(subCategoryVM);
                if (checkCreate)
                {
                    return Ok(ReturnMessage.Create("da tao thanh cong"));
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

        [HttpGet("GetAllSubCategory")]
        public async Task<IActionResult> GetAllSubCategory(int categoryID)
        {
            try
            {
                var listSubCategory = await _subCategoryRepository.GetAllSubCategory(categoryID);
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

        [HttpPut("updateSubCategory")]
        public async Task<IActionResult> updateSubCategory([FromBody] UpdateSubCategoryVM subCategoryVM)
        {
            try
            {
                var subCategory = await _subCategoryRepository.GetSubCategoryByID(subCategoryVM.subCategoryID);
                if (subCategory == null || subCategory.SubCategoryStatus == false)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                else
                {
                    bool check = await _subCategoryRepository.UpdateSubCategory(subCategoryVM);
                    if (check == true)
                    {
                        return Ok(ReturnMessage.Create("da edit thanh cong"));
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

        [HttpPut("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory([FromBody] DeleteSubCategoryVM subCategoryVM)
        {
            try
            {
                var subCategory = await _subCategoryRepository.GetSubCategoryByID(subCategoryVM.subCategoryID);
                if (subCategory == null || subCategory.SubCategoryStatus == false)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                else
                {
                    bool check = await _subCategoryRepository.DeleteSubCategory(subCategoryVM);
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

        [HttpGet("SearchSubCategoryName")]
        public async Task<IActionResult> GetSubCategoryByName(string subCategoryName)
        {
            try
            {
                var listSubCategory = await _subCategoryRepository.GetSubCategoriesByName(subCategoryName);
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
