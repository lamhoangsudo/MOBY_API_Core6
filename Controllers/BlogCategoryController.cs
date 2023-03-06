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
                Console.WriteLine(ex.Message);
                return BadRequest(ReturnMessage.create("erro at GetALlBlogCategory"));
            }
        }

        [HttpGet]
        [Route("api/blogcategory/blog/new")]
        public async Task<IActionResult> getNewBlogByBlogCategoryID(BlogCateGetVM blogCateID)
        {
            try
            {
                List<BlogVM> ListBlog = await BlogDAO.getNewBlogByBlogCateID(blogCateID.BlogCategoryId);

                return Ok(ListBlog);

            }
            catch
            {
                return BadRequest(ReturnMessage.create("error at getNewBlogByBlogCategoryID"));
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
                Console.WriteLine(ex.Message);
                return BadRequest(ReturnMessage.create("erro at GetBlogCategoryByID"));
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
                Console.WriteLine(ex);
                return BadRequest(ReturnMessage.create("error at create Blog Category"));
            }
        }

    }
}
