using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository;

namespace MOBY_API_Core6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogCategoryRepository BlogCateDAO;
        private readonly IBlogRepository BlogDAO;
        private readonly IUserRepository UserDAO;

        public BlogController(IBlogRepository BlogDAO, IBlogCategoryRepository BlogCateDAO, IUserRepository userDAO)
        {
            this.BlogDAO = BlogDAO;
            this.BlogCateDAO = BlogCateDAO;
            UserDAO = userDAO;
        }

        [HttpGet]
        [Route("api/BlogController/getAllBlog")]
        public async Task<IActionResult> getAllBlog()
        {
            try
            {
                List<BlogVM> ListBlog = await BlogDAO.getAllBlog();
                if (ListBlog.Count > 0)
                {
                    return Ok(ListBlog);
                }
                else
                {
                    return Ok(ReturnMessage.create("there no Blog"));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ReturnMessage.create("error at getAllBlog"));
            }
        }

        [HttpGet]
        [Route("api/BlogController/getBlogByBlogCate")]
        public async Task<IActionResult> getBlogByBlogCate(int blogCateID)
        {
            try
            {
                List<BlogVM> ListBlog = await BlogDAO.getBlogByBlogCateID(blogCateID);
                if (ListBlog.Count > 0)
                {
                    return Ok(ListBlog);
                }
                else
                {
                    return Ok(ReturnMessage.create("there no Blog"));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ReturnMessage.create("error at getBlogByBlogCate"));
            }

        }
        [Authorize]
        [HttpGet]
        [Route("api/BlogController/getBlogByUserID")]
        public async Task<IActionResult> getBlogByUserID()
        {
            try
            {
                int UserID = await UserDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                List<BlogVM> ListBlog = await BlogDAO.getBlogByUserID(UserID);
                if (ListBlog.Count > 0)
                {
                    return Ok(ListBlog);
                }
                else
                {
                    return Ok(ReturnMessage.create("there no Blog"));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ReturnMessage.create("error at getBlogByUserID"));
            }

        }

        [Authorize]
        [HttpPost]
        [Route("api/BlogController/CreateBlog")]
        public async Task<IActionResult> CreateBlog([FromBody] CreateBlogVM createdBlog)
        {
            try
            {
                int UserID = await UserDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);

                if (await BlogDAO.CreateBlog(createdBlog, UserID))
                {
                    return Ok(ReturnMessage.create("success"));
                }
                else
                {
                    return BadRequest(ReturnMessage.create("error at CreateBlog"));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ReturnMessage.create("error at CreateBlog"));
            }

        }

        [Authorize]
        [HttpPost]
        [Route("api/BlogController/UpdateBlog")]
        public async Task<IActionResult> UpdateBlog([FromBody] UpdateBlogVM UpdatedBlog)
        {
            try
            {

                Blog foundblog = await BlogDAO.getBlogByBlogID(UpdatedBlog.BlogId);
                if (foundblog != null)
                {
                    if (await BlogDAO.UpdateBlog(foundblog, UpdatedBlog))
                    {
                        return Ok(ReturnMessage.create("success"));
                    }
                }

                else
                {
                    return BadRequest(ReturnMessage.create("error at UpdateBlog"));
                }
                return BadRequest(ReturnMessage.create("error at UpdateBlog"));
            }
            catch (Exception ex)
            {
                return BadRequest(ReturnMessage.create("error at UpdateBlog"));
            }

        }

        [Authorize]
        [HttpPost]
        [Route("api/BlogController/AcceptBlog")]
        public async Task<IActionResult> AcceptBlog([FromBody] BlogIdVM blogId)
        {
            try
            {

                Blog foundblog = await BlogDAO.getBlogByBlogID(blogId.BlogId);
                if (foundblog != null)
                {
                    if (await BlogDAO.ConfirmBlog(foundblog, true))
                    {
                        return Ok(ReturnMessage.create("success"));
                    }
                }

                else
                {
                    return BadRequest(ReturnMessage.create("error at AcceptBlog"));
                }
                return BadRequest(ReturnMessage.create("error at AcceptBlog"));
            }
            catch (Exception ex)
            {
                return BadRequest(ReturnMessage.create("error at AcceptBlog"));
            }

        }

        [Authorize]
        [HttpPost]
        [Route("api/BlogController/DenyBlog")]
        public async Task<IActionResult> DenyBlog([FromBody] BlogIdVM blogId)
        {
            try
            {

                Blog foundblog = await BlogDAO.getBlogByBlogID(blogId.BlogId);
                if (foundblog != null)
                {
                    if (await BlogDAO.ConfirmBlog(foundblog, false))
                    {
                        return Ok(ReturnMessage.create("success"));
                    }
                }

                else
                {
                    return BadRequest(ReturnMessage.create("error at DenyBlog"));
                }
                return BadRequest(ReturnMessage.create("error at DenyBlog"));
            }
            catch (Exception ex)
            {
                return BadRequest(ReturnMessage.create("error at DenyBlog"));
            }

        }

    }
}
