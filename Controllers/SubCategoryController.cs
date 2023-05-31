using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Log4Net;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryService _subCategoryService;
        private readonly Logger4Net _logger4Net;
        public SubCategoryController(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
            _logger4Net = new Logger4Net();
        }

        [HttpPost("createSubCategory")]
        public async Task<IActionResult> createSubCategory([FromBody] CreateSubCategoryVM subCategoryVM)
        {
            try
            {
                bool checkCreate = await _subCategoryService.CreateSubCategory(subCategoryVM);
                if (checkCreate)
                {
                    return Ok(ReturnMessage.Create("da tao thanh cong"));
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetAllSubCategory")]
        public async Task<IActionResult> GetAllSubCategory(int categoryID)
        {
            try
            {
                var listSubCategory = await _subCategoryService.GetAllSubCategory(categoryID);
                if (listSubCategory == null || listSubCategory.Count == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                else
                {
                    return Ok(listSubCategory);
                }
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("updateSubCategory")]
        public async Task<IActionResult> updateSubCategory([FromBody] UpdateSubCategoryVM subCategoryVM)
        {
            try
            {
                var subCategory = await _subCategoryService.GetSubCategoryByID(subCategoryVM.SubCategoryID);
                if (subCategory == null || subCategory.SubCategoryStatus == false)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                else
                {
                    bool check = await _subCategoryService.UpdateSubCategory(subCategoryVM);
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
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory([FromBody] DeleteSubCategoryVM subCategoryVM)
        {
            try
            {
                var subCategory = await _subCategoryService.GetSubCategoryByID(subCategoryVM.SubCategoryID);
                if (subCategory == null || subCategory.SubCategoryStatus == false)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                else
                {
                    bool check = await _subCategoryService.DeleteSubCategory(subCategoryVM);
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
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("SearchSubCategoryName")]
        public async Task<IActionResult> GetSubCategoryByName(string subCategoryName)
        {
            try
            {
                var listSubCategory = await _subCategoryService.GetSubCategoriesByName(subCategoryName);
                if (listSubCategory == null || listSubCategory.Count == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                else
                {
                    return Ok(listSubCategory);
                }
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
