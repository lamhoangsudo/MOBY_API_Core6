using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository;

namespace MOBY_API_Core6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryVM categoryVM)
        {
            try
            {
                bool checkCreate = await _categoryRepository.CreateCategory(categoryVM);
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

        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryVM categoryVM)
        {
            try
            {
                var category = await _categoryRepository.GetCategoryByID(categoryVM.categoryID);
                if (category == null)
                {
                    return NotFound(ReturnMessage.Create("khong tim thay"));
                }
                else
                {
                    bool check = await _categoryRepository.UpdateCategory(categoryVM);
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
        public async Task<IActionResult> DeleteCategory([FromBody] DeleteCategoryVM categoryVM)
        {
            try
            {
                var category = await _categoryRepository.GetCategoryByID(categoryVM.categoryID);
                if (category == null || category.CategoryStatus == false)
                {
                    return NotFound(ReturnMessage.Create("khong tim thay"));
                }
                else
                {
                    bool check = await _categoryRepository.DeleteCategory(categoryVM);
                    if (check == true)
                    {
                        return Ok(ReturnMessage.Create("da update thanh cong"));
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

        [HttpGet("GetAllCategory/")]
        public async Task<IActionResult> GetAllCategory()
        {
            try
            {
                var listCategory = await _categoryRepository.GetAllCategoriesAndSubCategory();

                if (listCategory == null)
                {
#pragma warning disable CS8604 // Possible null reference argument.
                    return BadRequest(ReturnMessage.Create(CategoryRepository.errorMessage));
#pragma warning restore CS8604 // Possible null reference argument.
                }
                if (listCategory.Count == 0)
                {
#pragma warning disable CS8604 // Possible null reference argument.
                    return NotFound(ReturnMessage.Create(CategoryRepository.errorMessage));
#pragma warning restore CS8604 // Possible null reference argument.
                }
                else
                {
                    return Ok(listCategory);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetStatus")]
        public async Task<IActionResult> GetCategoryByStatus(bool categoryStatus)
        {
            try
            {
                var listCategory = await _categoryRepository.GetCategoriesByStatus(categoryStatus);
                if (listCategory == null || listCategory.Count == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                else
                {
                    return Ok(listCategory);
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("SearchCategoryName")]
        public async Task<IActionResult> GetCategoryByName(string categoryName)
        {
            try
            {
                if (categoryName == null || categoryName.Equals(""))
                {
                    var listCategory = await _categoryRepository.GetAllCategoriesAndSubCategory();
                    if (listCategory == null || listCategory.Count == 0)
                    {
                        return StatusCode(StatusCodes.Status404NotFound);
                    }
                    else
                    {
                        return Ok(listCategory);
                    }
                }
                else
                {
                    var listCategory = await _categoryRepository.GetCategoriesByName(categoryName);
                    if (listCategory == null || listCategory.Count == 0)
                    {
                        return StatusCode(StatusCodes.Status404NotFound);
                    }
                    else
                    {
                        return Ok(listCategory);
                    }
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
