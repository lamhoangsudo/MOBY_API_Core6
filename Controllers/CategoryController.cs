using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Log4Net;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Service;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly Logger4Net _logger4Net;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _logger4Net = new Logger4Net();
        }

        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryVM categoryVM)
        {
            try
            {
                bool checkCreate = await _categoryService.CreateCategory(categoryVM);
                if (checkCreate)
                {
                    return Ok(ReturnMessage.Create("da tao thanh cong"));
                }
                else
                {
                    return BadRequest(ReturnMessage.Create(CategoryService.ErrorMessage));
                }
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryVM categoryVM)
        {
            try
            {
                var category = await _categoryService.GetCategoryByID(categoryVM.CategoryID);
                if (category == null)
                {
                    return NotFound(ReturnMessage.Create("khong tim thay"));
                }
                else
                {
                    bool check = await _categoryService.UpdateCategory(categoryVM);
                    if (check == true)
                    {
                        return Ok(ReturnMessage.Create("da edit thanh cong"));
                    }
                    else
                    {
                        return BadRequest(ReturnMessage.Create(CategoryService.ErrorMessage));
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
        public async Task<IActionResult> DeleteCategory([FromBody] DeleteCategoryVM categoryVM)
        {
            try
            {
                var category = await _categoryService.GetCategoryByID(categoryVM.CategoryID);
                if (category == null || category.CategoryStatus == false)
                {
                    return NotFound(ReturnMessage.Create("khong tim thay"));
                }
                else
                {
                    bool check = await _categoryService.DeleteCategory(categoryVM);
                    if (check == true)
                    {
                        return Ok(ReturnMessage.Create("da update thanh cong"));
                    }
                    else
                    {
                        return BadRequest(ReturnMessage.Create(CategoryService.ErrorMessage));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetAllCategory/")]
        public async Task<IActionResult> GetAllCategory()
        {
            try
            {
                var listCategory = await _categoryService.GetAllCategoriesAndSubCategory();

                if (listCategory == null)
                {
#pragma warning disable CS8604 // Possible null reference argument.
                    return BadRequest(ReturnMessage.Create(CategoryService.ErrorMessage));
#pragma warning restore CS8604 // Possible null reference argument.
                }
                if (listCategory.Count == 0)
                {
#pragma warning disable CS8604 // Possible null reference argument.
                    return NotFound(ReturnMessage.Create(CategoryService.ErrorMessage));
#pragma warning restore CS8604 // Possible null reference argument.
                }
                else
                {
                    return Ok(listCategory);
                }
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetStatus")]
        public async Task<IActionResult> GetCategoryByStatus(bool categoryStatus)
        {
            try
            {
                var listCategory = await _categoryService.GetCategoriesByStatus(categoryStatus);
                if (listCategory == null || listCategory.Count == 0)
                {
                    return NotFound(ReturnMessage.Create(CategoryService.ErrorMessage));
                }
                else
                {
                    return Ok(listCategory);
                }
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("SearchCategoryName")]
        public async Task<IActionResult> GetCategoryByName(string categoryName)
        {
            try
            {
                if (categoryName == null || categoryName.Equals(""))
                {
                    var listCategory = await _categoryService.GetAllCategoriesAndSubCategory();
                    if (listCategory == null || listCategory.Count == 0)
                    {
                        return NotFound(ReturnMessage.Create(CategoryService.ErrorMessage));
                    }
                    else
                    {
                        return Ok(listCategory);
                    }
                }
                else
                {
                    var listCategory = await _categoryService.GetCategoriesByName(categoryName);
                    if (listCategory == null || listCategory.Count == 0)
                    {
                        return NotFound(ReturnMessage.Create(CategoryService.ErrorMessage));
                    }
                    else
                    {
                        return Ok(listCategory);
                    }
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
