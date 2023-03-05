using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository;

namespace MOBY_API_Core6.Controllers
{

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
        [Route("api/blog/all")]
        public async Task<IActionResult> getAllBlog()
        {
            try
            {
                List<Blog> ListBlog = BlogDAO.getAllBlog();

                return Ok(ListBlog);

            }
            catch
            {
                return BadRequest(ReturnMessage.create("error at getAllBlog"));
            }
        }

        [HttpGet]
        [Route("api/blog")]
        public async Task<IActionResult> getBlogByQuery([FromQuery] BlogGetVM blogGetVM)
        {
            try
            {
                List<BlogVM> ListBlog = new List<BlogVM>();
                if (blogGetVM.categoryId != null)
                {
                    ListBlog = BlogDAO.getBlogByBlogCateID(blogGetVM.categoryId.Value);

                    return Ok(ListBlog);

                }
                else if (blogGetVM.userId != null)
                {
                    ListBlog = BlogDAO.getBlogByUserID(blogGetVM.userId.Value);

                    return Ok(ListBlog);
                }
                else if (blogGetVM.BlogId != null)
                {
                    Blog foundBlog = BlogDAO.getBlogByBlogID(blogGetVM.BlogId.Value);
                    return Ok(foundBlog);
                }
                return Ok(ReturnMessage.create("there no field so no blog"));
            }
            catch
            {
                return BadRequest(ReturnMessage.create("error at getBlogByQuery"));
            }

        }

        [Authorize]
        [HttpGet]
        [Route("api/useraccount/blog")]
        public async Task<IActionResult> getBlogByToken()
        {
            try
            {
                int UserID = await UserDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                List<BlogVM> ListBlog = BlogDAO.getBlogBySelf(UserID);
                return Ok(ListBlog);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ReturnMessage.create("error at getBlogByToken"));
            }

        }

        [Authorize]
        [HttpPost]
        [Route("api/blog/create")]
        public async Task<IActionResult> CreateBlog([FromBody] CreateBlogVM createdBlog)
        {
            try
            {
                int UserID = await UserDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);

                if (BlogDAO.CreateBlog(createdBlog, UserID))
                {
                    return Ok(ReturnMessage.create("success"));
                }
                else
                {
                    return BadRequest(ReturnMessage.create("error at CreateBlog"));
                }
            }
            catch
            {
                return BadRequest(ReturnMessage.create("error at CreateBlog"));
            }

        }

        [Authorize]
        [HttpPut]
        [Route("api/blog")]
        public async Task<IActionResult> UpdateBlog([FromBody] UpdateBlogVM UpdatedBlog)
        {
            try
            {

                int uid = await UserDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                Blog foundblog = BlogDAO.getBlogByBlogIDAndUserId(UpdatedBlog.BlogId, uid);
                if (foundblog != null)
                {
                    if (BlogDAO.UpdateBlog(foundblog, UpdatedBlog))
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
            catch
            {
                return BadRequest(ReturnMessage.create("error at UpdateBlog"));
            }

        }

        [Authorize]
        [HttpPatch]
        [Route("api/blog/accept")]
        public async Task<IActionResult> AcceptBlog([FromBody] BlogIdVM blogId)
        {
            try
            {

                Blog foundblog = BlogDAO.getBlogByBlogID(blogId.BlogId);
                if (foundblog != null)
                {
                    if (BlogDAO.ConfirmBlog(foundblog, 1))
                    {
                        return Ok(ReturnMessage.create("success"));
                    }
                }

                else
                {
                    return BadRequest(ReturnMessage.create("found no blog"));
                }
                return BadRequest(ReturnMessage.create("error at AcceptBlog"));
            }
            catch
            {
                return BadRequest(ReturnMessage.create("error at AcceptBlog"));
            }

        }

        [Authorize]
        [HttpPatch]
        [Route("api/blog/deny")]
        public async Task<IActionResult> DenyBlog([FromBody] BlogIdVM blogId)
        {
            try
            {

                Blog foundblog = BlogDAO.getBlogByBlogID(blogId.BlogId);
                if (foundblog != null)
                {
                    if (BlogDAO.ConfirmBlog(foundblog, 2))
                    {
                        return Ok(ReturnMessage.create("success"));
                    }
                }

                else
                {
                    return BadRequest(ReturnMessage.create("found no blog"));
                }
                return BadRequest(ReturnMessage.create("error at DenyBlog"));
            }
            catch
            {
                return BadRequest(ReturnMessage.create("error at DenyBlog"));
            }

        }

        [Authorize]
        [HttpPatch]
        [Route("api/blog/delete")]
        public async Task<IActionResult> DeleteBlog([FromBody] BlogIdVM blogId)
        {
            try
            {

                int uid = await UserDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                Blog foundblog = BlogDAO.getBlogByBlogIDAndUserId(blogId.BlogId, uid);
                if (foundblog != null)
                {
                    if (BlogDAO.ConfirmBlog(foundblog, 3))
                    {
                        return Ok(ReturnMessage.create("success"));
                    }
                }

                else
                {
                    return BadRequest(ReturnMessage.create("found no blog"));
                }
                return BadRequest(ReturnMessage.create("error at DeleteBlog"));
            }
            catch
            {
                return BadRequest(ReturnMessage.create("error at DeleteBlog"));
            }

        }

    }
}
