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
                List<BlogVM> ListBlog = await BlogDAO.getAllBlog();

                return Ok(ListBlog);


            }
            catch (Exception ex)
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
                if (blogGetVM.categoryId != null)
                {
                    List<BlogVM> ListBlog = await BlogDAO.getBlogByBlogCateID(blogGetVM.categoryId.Value);

                    return Ok(ListBlog);

                }
                else if (blogGetVM.userId != null)
                {
                    List<BlogVM> ListBlog = await BlogDAO.getBlogByUserID(blogGetVM.userId.Value);

                    return Ok(ListBlog);

                }

                return BadRequest(ReturnMessage.create("missiong query at get blog"));

            }
            catch (Exception ex)
            {
                return BadRequest(ReturnMessage.create("error at getBlogByBlogCate"));
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
                List<BlogVM> ListBlog = await BlogDAO.getBlogByUserID(UserID);

                return Ok(ListBlog);

            }
            catch (Exception ex)
            {
                return BadRequest(ReturnMessage.create("error at getBlogByUserID"));
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
        [HttpPut]
        [Route("api/blog")]
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
        [HttpPatch]
        [Route("api/blog/accept")]
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
        [HttpPatch]
        [Route("api/blog/deny")]
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
