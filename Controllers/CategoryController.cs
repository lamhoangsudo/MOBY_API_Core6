using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("CreateCategory/{categoryName}")]
        public IActionResult CreateCategory(string categoryName, string categoryImange)
        {
            try
            {
                bool checkCreate = _categoryRepository.CreateCategory(categoryName, categoryImange);
                if (checkCreate)
                {
                    return Ok(categoryName);
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

        [HttpPut("UpdateCategory/{categoryID} {categoryName}")]
        public IActionResult UpdateCategory(int categoryID, string categoryName, string categoryImage)
        {
            try
            {
                var category = _categoryRepository.GetCategoryByID(categoryID);
                if (category == null || category.CategoryStatus == false)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                else
                {
                    bool check = _categoryRepository.UpdateCategory(categoryID, categoryName, categoryImage);
                    if (check == true)
                    {
                        return Ok(categoryName);
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

        [HttpPut("DeleteCategory/{categoryID}")]
        public IActionResult DeleteCategory(int categoryID)
        {
            try
            {
                var category = _categoryRepository.GetCategoryByID(categoryID);
                if (category == null || category.CategoryStatus == false)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                else
                {
                    bool check = _categoryRepository.DeleteCategory(categoryID);
                    if (check == true)
                    {
                        return Ok(categoryID);
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
        public IActionResult GetAllCategory()
        {
            try
            {
                var listCategory = _categoryRepository.GetAllCategories();
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

        [HttpGet("GetStatus/{categoryStatus}")]
        public IActionResult GetCategoryByStatus(bool categoryStatus)
        {
            try
            {
                var listCategory = _categoryRepository.GetCategoriesByStatus(categoryStatus);
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

        [HttpGet("SearchCategoryName/{categoryName}")]
        public IActionResult GetCategoryByName(string categoryName)
        {
            try
            {
                if (categoryName == null || categoryName.Equals(""))
                {
                    var listCategory = _categoryRepository.GetAllCategories();
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
                    var listCategory = _categoryRepository.GetCategoriesByName(categoryName);
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
