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

        public BlogCategoryController(IBlogCategoryRepository BlogCateDAO)
        {
            this.BlogCateDAO = BlogCateDAO;
        }


        [HttpGet]
        [Route("api/blogcategory/all")]
        public async Task<IActionResult> GetALlBlogCategory()
        {
            List<BlogCategoryVM> BlogCateList = await BlogCateDAO.GetAllBlogCategory();
            return Ok(BlogCateList);

        }
        [HttpGet]
        [Route("api/blogcategory")]
        public async Task<IActionResult> GetBlogCategoryNameByID([FromQuery] BlogCateGetVM blogCateGetVM)
        {
            String BlogCateName = await BlogCateDAO.GetBlogCateNameByID(blogCateGetVM.BlogCategoryId);

            return Ok(BlogCateName);

        }
        [HttpPost]
        [Route("api/blogcategory/create")]
        public async Task<IActionResult> CreateBlogCategory([FromBody] CreateBlogCategoryName blogCateName)
        {

            if (await BlogCateDAO.checkNameBlogCategory(blogCateName.BlogCategoryName))
            {
                return BadRequest(ReturnMessage.create("this name already existed"));
            }
            else if (await BlogCateDAO.createBlogCategory(blogCateName.BlogCategoryName))
            {
                return Ok(ReturnMessage.create("success"));
            }
            else
            {
                return BadRequest(ReturnMessage.create("error at create Blog Category"));
            }

        }

    }
}
