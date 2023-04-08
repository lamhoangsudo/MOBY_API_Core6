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
        public async Task<IActionResult> GetAllBlog([FromQuery] PaggingVM pagging)
        {
            try
            {
                List<BlogSimpleVM> ListBlog = await BlogDAO.getAllBlog(pagging);
                int totalBlog = await BlogDAO.getAllBlogCount();
                PaggingReturnVM<BlogSimpleVM> result = new PaggingReturnVM<BlogSimpleVM>(ListBlog, pagging, totalBlog);

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
                List<BlogBriefVM> ListBlog = await BlogDAO.getAllUncheckBlog(pagging, blogStatusVM);
                int totalBlog = await BlogDAO.getAllUncheckBlogcount(blogStatusVM);
                PaggingReturnVM<BlogBriefVM> result = new PaggingReturnVM<BlogBriefVM>(ListBlog, pagging, totalBlog);

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
                List<BlogSimpleVM> ListBlog = new List<BlogSimpleVM>();
                if (blogGetVM.categoryId != null)
                {
                    ListBlog = await BlogDAO.getBlogByBlogCateID(blogGetVM.categoryId.Value, pagging);
                    int totalBlog = await BlogDAO.getBlogByCateCount(blogGetVM.categoryId.Value);
                    PaggingReturnVM<BlogSimpleVM> result = new PaggingReturnVM<BlogSimpleVM>(ListBlog, pagging, totalBlog);
                    return Ok(result);

                }
                else if (blogGetVM.userId != null)
                {
                    ListBlog = await BlogDAO.getBlogByUserID(blogGetVM.userId.Value, pagging);
                    int totalBlog = await BlogDAO.getBlogByUserIDCount(blogGetVM.userId.Value);
                    PaggingReturnVM<BlogSimpleVM> result = new PaggingReturnVM<BlogSimpleVM>(ListBlog, pagging, totalBlog);
                    return Ok(result);
                }
                else if (blogGetVM.tittle != null)
                {
                    ListBlog = await BlogDAO.SearchlBlog(pagging, blogGetVM.tittle);
                    int totalBlog = await BlogDAO.getSearchBlogCount(blogGetVM.tittle);
                    PaggingReturnVM<BlogSimpleVM> result = new PaggingReturnVM<BlogSimpleVM>(ListBlog, pagging, totalBlog);
                    return Ok(result);
                }
                else if (blogGetVM.BlogId != null)
                {
                    BlogVM? foundBlog = await BlogDAO.getBlogVMByBlogID(blogGetVM.BlogId.Value);
                    if (foundBlog != null)
                    {
                        return Ok(foundBlog);
                    }
                    return Ok(foundBlog);
                }
                return Ok(ReturnMessage.Create("there no field so no blog"));
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
                if (UserID == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                List<BlogSimpleVM> ListBlog = await BlogDAO.getBlogBySelf(UserID, pagging);
                int totalBlog = await BlogDAO.getBlogByBySelfCount(UserID);
                PaggingReturnVM<BlogSimpleVM> result = new PaggingReturnVM<BlogSimpleVM>(ListBlog, pagging, totalBlog);
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
                if (UserID == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                if (await BlogDAO.CreateBlog(createdBlog, UserID))
                {
                    return Ok(ReturnMessage.Create("success"));
                }
                else
                {
                    return BadRequest(ReturnMessage.Create("error at CreateBlog"));
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
                if (uid == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                Blog? foundblog = await BlogDAO.getBlogByBlogIDAndUserId(UpdatedBlog.BlogId, uid);
                if (foundblog != null)
                {
                    if (await BlogDAO.UpdateBlog(foundblog, UpdatedBlog))
                    {
                        return Ok(ReturnMessage.Create("success"));
                    }
                }
                return BadRequest(ReturnMessage.Create("error at UpdateBlog"));
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
                        return Ok(ReturnMessage.Create("success"));
                    }
                }
                else
                {
                    return BadRequest(ReturnMessage.Create("found no blog"));
                }
                return BadRequest(ReturnMessage.Create("error at AcceptBlog"));
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
                if (foundblog != null)
                {
                    if (blogId.reason != null && blogId.reason.Equals(""))
                    {
                        if (await BlogDAO.DenyBlog(foundblog, 2, blogId.reason))
                        {
                            return Ok(ReturnMessage.Create("success"));
                        }
                    }
                    else
                    {
                        return BadRequest(ReturnMessage.Create("must have reason deny"));
                    }
                }
                else
                {
                    return BadRequest(ReturnMessage.Create("found no blog"));
                }
                return BadRequest(ReturnMessage.Create("error at DenyBlog"));
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
                if (uid == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                Blog? foundblog = await BlogDAO.getBlogByBlogIDAndUserId(blogId.BlogId, uid);
                if (foundblog != null)
                {
                    if (await BlogDAO.ConfirmBlog(foundblog, 3))
                    {
                        return Ok(ReturnMessage.Create("success"));
                    }
                }
                else
                {
                    return BadRequest(ReturnMessage.Create("found no blog"));
                }
                return BadRequest(ReturnMessage.Create("error at DeleteBlog"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

    }
}
