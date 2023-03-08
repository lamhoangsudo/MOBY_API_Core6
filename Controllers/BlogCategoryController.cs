using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository;

namespace MOBY_API_Core6.Controllers
{

    [ApiController]
    public class BlogCategoryController : ControllerBase
    {
        private readonly IBlogCategoryRepository BlogCateDAO;
        private readonly IBlogRepository BlogDAO;

        public BlogCategoryController(IBlogCategoryRepository BlogCateDAO, IBlogRepository blogDAO)
        {
            this.BlogCateDAO = BlogCateDAO;
            BlogDAO = blogDAO;
        }


        [HttpGet]
        [Route("api/blogcategory/all")]
        public async Task<IActionResult> GetALlBlogCategory()
        {

            try
            {
                List<BlogCategoryVM> BlogCateList = await BlogCateDAO.GetAllBlogCategory();
                return Ok(BlogCateList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("api/blogcategory")]
        public async Task<IActionResult> GetBlogCategoryByID([FromQuery] BlogCateGetVM blogCateGetVM)
        {

            try
            {
                BlogCategoryVM? BlogCateName = await BlogCateDAO.GetBlogCateByID(blogCateGetVM.BlogCategoryId);

                return Ok(BlogCateName);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        [Route("api/blogcategory/create")]
        public async Task<IActionResult> CreateBlogCategory([FromBody] CreateBlogCategoryName blogCateName)
        {
            try
            {
                if (await BlogCateDAO.createBlogCategory(blogCateName.BlogCategoryName))
                {
                    return Ok(ReturnMessage.create("success"));
                }
                else
                {
                    return BadRequest(ReturnMessage.create("this name already existed"));

                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
