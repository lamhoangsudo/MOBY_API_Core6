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
        public async Task<IActionResult> getAllBlog([FromQuery] PaggingVM pagging)
        {
            try
            {
                List<BlogVM> ListBlog = await BlogDAO.getAllBlog(pagging);
                int totalBlog = await BlogDAO.getAllBlogCount();
                PaggingReturnVM<BlogVM> result = new PaggingReturnVM<BlogVM>(ListBlog, pagging, totalBlog);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("api/admin/blog")]
        public async Task<IActionResult> getAllUncheckBlog([FromQuery] PaggingVM pagging, [FromQuery] BlogStatusVM blogStatusVM)
        {
            try
            {
                List<BlogVM> ListBlog = await BlogDAO.getAllUncheckBlog(pagging, blogStatusVM);
                int totalBlog = await BlogDAO.getAllUncheckBlogcount(blogStatusVM);
                PaggingReturnVM<BlogVM> result = new PaggingReturnVM<BlogVM>(ListBlog, pagging, totalBlog);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("api/blog")]
        public async Task<IActionResult> getBlogByQuery([FromQuery] BlogGetVM blogGetVM, [FromQuery] PaggingVM pagging)
        {
            try
            {
                List<BlogVM> ListBlog = new List<BlogVM>();
                if (blogGetVM.categoryId != null)
                {
                    ListBlog = await BlogDAO.getBlogByBlogCateID(blogGetVM.categoryId.Value, pagging);
                    int totalBlog = await BlogDAO.getAllBlogCount(blogGetVM.categoryId.Value);
                    PaggingReturnVM<BlogVM> result = new PaggingReturnVM<BlogVM>(ListBlog, pagging, totalBlog);
                    return Ok(result);

                }
                else if (blogGetVM.userId != null)
                {
                    ListBlog = await BlogDAO.getBlogByUserID(blogGetVM.userId.Value, pagging);
                    int totalBlog = await BlogDAO.getBlogByUserIDCount(blogGetVM.userId.Value);
                    PaggingReturnVM<BlogVM> result = new PaggingReturnVM<BlogVM>(ListBlog, pagging, totalBlog);
                    return Ok(result);
                }
                else if (blogGetVM.BlogId != null)
                {
                    Blog? foundBlog = await BlogDAO.getBlogByBlogID(blogGetVM.BlogId.Value);
                    if (foundBlog != null)
                    {
                        return Ok(BlogVM.BlogToVewModel(foundBlog));
                    }
                    return Ok(foundBlog);
                }
                return Ok(ReturnMessage.create("there no field so no blog"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [Authorize]
        [HttpGet]
        [Route("api/useraccount/blog")]
        public async Task<IActionResult> getBlogByToken([FromQuery] PaggingVM pagging)
        {
            try
            {
                int UserID = await UserDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                List<BlogVM> ListBlog = await BlogDAO.getBlogBySelf(UserID, pagging);
                int totalBlog = await BlogDAO.getBlogByBySelfCount(UserID);
                PaggingReturnVM<BlogVM> result = new PaggingReturnVM<BlogVM>(ListBlog, pagging, totalBlog);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
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
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
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
                Blog? foundblog = await BlogDAO.getBlogByBlogIDAndUserId(UpdatedBlog.BlogId, uid);
                if (foundblog != null)
                {
                    if (await BlogDAO.UpdateBlog(foundblog, UpdatedBlog))
                    {
                        return Ok(ReturnMessage.create("success"));
                    }
                }
                return BadRequest(ReturnMessage.create("error at UpdateBlog"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [Authorize]
        [HttpPatch]
        [Route("api/blog/accept")]
        public async Task<IActionResult> AcceptBlog([FromBody] BlogIdVM blogId)
        {
            try
            {

                Blog? foundblog = await BlogDAO.getBlogByBlogID(blogId.BlogId);
                if (foundblog != null)
                {
                    if (await BlogDAO.ConfirmBlog(foundblog, 1))
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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [Authorize]
        [HttpPatch]
        [Route("api/blog/deny")]
        public async Task<IActionResult> DenyBlog([FromBody] BlogIdDenyVM blogId)
        {
            try
            {

                Blog? foundblog = await BlogDAO.getBlogByBlogID(blogId.BlogId);
                if (foundblog != null && blogId.reason != null)
                {
                    if (await BlogDAO.DenyBlog(foundblog, 2, blogId.reason))
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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
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
                Blog? foundblog = await BlogDAO.getBlogByBlogIDAndUserId(blogId.BlogId, uid);
                if (foundblog != null)
                {
                    if (await BlogDAO.ConfirmBlog(foundblog, 3))
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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

    }
}
