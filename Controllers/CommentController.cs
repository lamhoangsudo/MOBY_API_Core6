using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository;

namespace MOBY_API_Core6.Controllers
{

    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IUserRepository UserDAO;
        private readonly ICommentRepository CmtDAO;
        public CommentController(IUserRepository userRepository, ICommentRepository CmtDAO)
        {
            this.UserDAO = userRepository;
            this.CmtDAO = CmtDAO;
        }

        [HttpGet]
        [Route("api/comment/all")]
        public async Task<IActionResult> getAllComment()
        {
            try
            {
                List<CommentVM> listAllComment = await CmtDAO.GetAllComment();
                return Ok(listAllComment);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("api/comment")]
        public async Task<IActionResult> getCommentByQuery([FromQuery] GetCommentVM id)
        {
            try
            {
                if (id.BlogId != null)
                {
                    List<CommentVM> listAllComment = await CmtDAO.GetCommentByBlogID(id.BlogId.Value);
                    return Ok(listAllComment);
                }
                else if (id.ItemId != null)
                {
                    List<CommentVM> listAllComment = await CmtDAO.GetCommentByItemID(id.ItemId.Value);
                    return Ok(listAllComment);
                }
                return BadRequest(ReturnMessage.create("no BlogID either ItemID"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/comment/create")]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentVM cmt)
        {
            try
            {
                int uid = await UserDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);

                if (await CmtDAO.CreateComment(cmt, uid))
                {
                    return Ok(ReturnMessage.create("success"));
                }

                return BadRequest(ReturnMessage.create("error at CreateComment"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("api/comment")]
        public async Task<IActionResult> UpdateComment([FromBody] UpdateCommentVM cmt)
        {
            try
            {
                int uid = await UserDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (await CmtDAO.UpdateComment(cmt, uid))
                {
                    return Ok(ReturnMessage.create("success"));
                }
                return BadRequest(ReturnMessage.create("error at UpdateComment"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("api/comment/delete")]
        public async Task<IActionResult> DeleteComment([FromQuery] GetCommentIDVM cmtid)
        {
            try
            {
                int uid = await UserDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (await CmtDAO.DeleteComment(cmtid, uid))
                {
                    return Ok(ReturnMessage.create("success"));
                }
                return BadRequest(ReturnMessage.create("error at DeleteComment"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
