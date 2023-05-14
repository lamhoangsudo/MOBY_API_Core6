using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Controllers
{

    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService BlogDAO;
        private readonly IUserService UserDAO;

        public BlogController(IBlogService BlogDAO, IUserService userDAO)
        {
            this.BlogDAO = BlogDAO;
            UserDAO = userDAO;
        }

        [HttpGet]
        [Route("api/blog/all")]
        public async Task<IActionResult> GetAllBlog([FromQuery] PaggingVM pagging)
        {
            try
            {
                List<BlogSimpleVM> ListBlog = await BlogDAO.GetAllBlog(pagging);
                int totalBlog = await BlogDAO.GetAllBlogCount();
                PaggingReturnVM<BlogSimpleVM> result = new(ListBlog, pagging, totalBlog);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



        [HttpGet]
        [Route("api/admin/blog")]
        public async Task<IActionResult> GetAllUncheckBlog([FromQuery] PaggingVM pagging, [FromQuery] BlogStatusVM blogStatusVM)
        {
            try
            {
                List<BlogBriefVM> ListBlog = await BlogDAO.GetAllUncheckBlog(pagging, blogStatusVM);
                int totalBlog = await BlogDAO.GetAllUncheckBlogcount(blogStatusVM);
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
        public async Task<IActionResult> GetBlogByQuery([FromQuery] BlogGetVM blogGetVM, [FromQuery] PaggingVM pagging)
        {
            try
            {
                List<BlogSimpleVM> ListBlog = new List<BlogSimpleVM>();
                if (blogGetVM.CategoryId != null)
                {
                    ListBlog = await BlogDAO.GetBlogByBlogCateID(blogGetVM.CategoryId.Value, pagging);
                    int totalBlog = await BlogDAO.GetBlogByCateCount(blogGetVM.CategoryId.Value);
                    PaggingReturnVM<BlogSimpleVM> result = new PaggingReturnVM<BlogSimpleVM>(ListBlog, pagging, totalBlog);
                    return Ok(result);

                }
                else if (blogGetVM.UserId != null)
                {
                    ListBlog = await BlogDAO.GetBlogByUserID(blogGetVM.UserId.Value, pagging);
                    int totalBlog = await BlogDAO.GetBlogByUserIDCount(blogGetVM.UserId.Value);
                    PaggingReturnVM<BlogSimpleVM> result = new PaggingReturnVM<BlogSimpleVM>(ListBlog, pagging, totalBlog);
                    return Ok(result);
                }
                else if (blogGetVM.Tittle != null)
                {
                    ListBlog = await BlogDAO.SearchlBlog(pagging, blogGetVM.Tittle);
                    int totalBlog = await BlogDAO.GetSearchBlogCount(blogGetVM.Tittle);
                    PaggingReturnVM<BlogSimpleVM> result = new PaggingReturnVM<BlogSimpleVM>(ListBlog, pagging, totalBlog);
                    return Ok(result);
                }
                else if (blogGetVM.BlogId != null)
                {
                    BlogVM? foundBlog = await BlogDAO.GetBlogVMByBlogID(blogGetVM.BlogId.Value);
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
        public async Task<IActionResult> GetBlogByToken([FromQuery] PaggingVM pagging)
        {
            try
            {
                int UserID = await UserDAO.GetUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (UserID == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                if (UserID == -1)
                {
                    return BadRequest(ReturnMessage.Create("Account not found"));
                }
                List<BlogSimpleVM> ListBlog = await BlogDAO.GetBlogBySelf(UserID, pagging);
                int totalBlog = await BlogDAO.GetBlogByBySelfCount(UserID);
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
                int UserID = await UserDAO.GetUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (UserID == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                if (UserID == -1)
                {
                    return BadRequest(ReturnMessage.Create("Account not found"));
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

                int uid = await UserDAO.GetUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (uid == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                if (uid == -1)
                {
                    return BadRequest(ReturnMessage.Create("Account not found"));
                }
                Blog? foundblog = await BlogDAO.GetBlogByBlogIDAndUserId(UpdatedBlog.BlogId, uid);
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

                Blog? foundblog = await BlogDAO.GetBlogByBlogID(blogId.BlogId);
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

                Blog? foundblog = await BlogDAO.GetBlogByBlogID(blogId.BlogId);
                if (foundblog != null)
                {
                    if (blogId.Reason != null && !blogId.Reason.Equals(""))
                    {
                        if (await BlogDAO.DenyBlog(foundblog, blogId.Reason))
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

                int uid = await UserDAO.GetUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (uid == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                if (uid == -1)
                {
                    return BadRequest(ReturnMessage.Create("Account not found"));
                }
                Blog? foundblog = await BlogDAO.GetBlogByBlogIDAndUserId(blogId.BlogId, uid);
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
