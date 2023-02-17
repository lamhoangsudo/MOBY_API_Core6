using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository;

namespace MOBY_API_Core6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogCategoryController : ControllerBase
    {
        private readonly IBlogCategoryRepository BlogCateDAO;

        public BlogCategoryController(IBlogCategoryRepository BlogCateDAO)
        {
            this.BlogCateDAO = BlogCateDAO;
        }


        [HttpGet]
        [Route("api/BlogCategoryController/GetALlBlogCategory")]
        public async Task<IActionResult> GetALlBlogCategory()
        {
            List<BlogCategoryVM> BlogCateList = await BlogCateDAO.GetAllBlogCategory();
            if (BlogCateList.Count == 0)
            {
                return Ok(ReturnMessage.create("there no Blog Category"));
            }
            else

            {
                return Ok(BlogCateList);
            }
        }
        [HttpGet]
        [Route("api/BlogCategoryController/GetBlogCategoryNameByID")]
        public async Task<IActionResult> GetBlogCategoryNameByID(int id)
        {
            String BlogCateName = await BlogCateDAO.GetBlogCateNameByID(id);
            if (BlogCateName == null)
            {
                return Ok(ReturnMessage.create("there no Blog Category"));
            }
            else

            {
                return Ok(BlogCateName);
            }
        }
        [HttpPost]
        [Route("api/BlogCategoryController/CreateBlogCategory")]
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
