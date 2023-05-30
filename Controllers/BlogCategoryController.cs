using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Log4Net;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Controllers
{

    [ApiController]
    public class BlogCategoryController : ControllerBase
    {
        private readonly IBlogCategoryService BlogCateDAO;
        private readonly Logger4Net _logger4Net;
        public BlogCategoryController(IBlogCategoryService BlogCateDAO, Logger4Net _logger4Net)
        {
            this.BlogCateDAO = BlogCateDAO;
            this._logger4Net = _logger4Net;
        }
        [HttpGet]
        [Route("api/blogcategory/all")]
        public async Task<IActionResult> GetALlBlogCategory()
        {

            try
            {
                List<BlogCategoryOnlyVM> BlogCateList = await BlogCateDAO.GetAllBlogCategory(0);
                return Ok(BlogCateList);
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        [Route("api/admin/blogcategory/all")]
        public async Task<IActionResult> GetALlBlogCategoryForAdmin([FromQuery] BlogCategoryStatusVM blogCategoryStatusVM)
        {

            try
            {
                int status;
                if (blogCategoryStatusVM.blogCateStatus == true)
                {
                    status = 0;
                }
                else
                {
                    status = 1;
                }
                List<BlogCategoryOnlyVM> BlogCateList = await BlogCateDAO.GetAllBlogCategory(status);
                return Ok(BlogCateList);
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
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
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [Authorize]
        [HttpPost]
        [Route("api/blogcategory/create")]
        public async Task<IActionResult> CreateBlogCategory([FromBody] CreateBlogCategoryName blogCateName)
        {
            try
            {
                if (await BlogCateDAO.CreateBlogCategory(blogCateName.BlogCategoryName))
                {
                    return Ok(ReturnMessage.Create("success"));
                }
                else
                {

                    return BadRequest(ReturnMessage.Create("this name already existed"));
                }
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [Authorize]
        [HttpPut]
        [Route("api/blogcategory")]
        public async Task<IActionResult> UpdateBlogCategory([FromBody] UpdateBlogCategoryVM updateBlogCategoryVM)
        {
            try
            {
                if (await BlogCateDAO.UpdateBlogCategory(updateBlogCategoryVM))
                {
                    return Ok(ReturnMessage.Create("success"));
                }
                else
                {
                    return BadRequest(ReturnMessage.Create("blog category not found"));

                }
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [Authorize]
        [HttpPatch]
        [Route("api/blogcategory")]
        public async Task<IActionResult> DeleteBlogCategory([FromBody] BlogCateGetVM blogCateGetVM)
        {
            try
            {
                if (await BlogCateDAO.DeleteBlogCategory(blogCateGetVM))
                {
                    return Ok(ReturnMessage.Create("success"));
                }
                else
                {
                    return BadRequest(ReturnMessage.Create("blog category not found"));

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
