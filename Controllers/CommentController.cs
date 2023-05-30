using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Log4Net;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Controllers
{

    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IUserService UserRepository;
        private readonly ICommentService CommentRepository;
        private readonly Logger4Net _logger4Net;
        public CommentController(IUserService userRepository, ICommentService CommentRepository, Logger4Net _logger4Net)
        {
            this.UserRepository = userRepository;
            this.CommentRepository = CommentRepository;
            this._logger4Net = _logger4Net;
        }

        [HttpGet]
        [Route("api/comment/all")]
        public async Task<IActionResult> GetAllComment()
        {
            try
            {
                List<CommentVM> listAllComment = await CommentRepository.GetAllComment();
                return Ok(listAllComment);
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("api/comment")]
        public async Task<IActionResult> GetCommentByQuery([FromQuery] GetCommentVM id)
        {
            try
            {
                if (id.BlogId != null)
                {
                    List<CommentVM> listAllComment = await CommentRepository.GetCommentByBlogID(id.BlogId.Value);
                    return Ok(listAllComment);
                }
                else if (id.ItemId != null)
                {
                    List<CommentVM> listAllComment = await CommentRepository.GetCommentByItemID(id.ItemId.Value);
                    return Ok(listAllComment);
                }
                else if (id.CommentId != null)
                {
                    CommentVM? commentFound = await CommentRepository.GetCommentByCommentID(id.CommentId.Value);
                    return Ok(commentFound);
                }
                return BadRequest(ReturnMessage.Create("no BlogID either ItemID or CommentID"));
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
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
                int uid = await UserRepository.GetUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (uid == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                if (uid == -1)
                {
                    return BadRequest(ReturnMessage.Create("Account not found"));
                }
                if (await CommentRepository.CreateComment(cmt, uid))
                {
                    return Ok(ReturnMessage.Create("success"));
                }

                return BadRequest(ReturnMessage.Create("error at CreateComment"));
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
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
                int uid = await UserRepository.GetUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (uid == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                if (uid == -1)
                {
                    return BadRequest(ReturnMessage.Create("Account not found"));
                }
                if (await CommentRepository.UpdateComment(cmt, uid))
                {
                    return Ok(ReturnMessage.Create("success"));
                }
                return BadRequest(ReturnMessage.Create("error at UpdateComment"));
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
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
                int uid = await UserRepository.GetUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (uid == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                if (uid == -1)
                {
                    return BadRequest(ReturnMessage.Create("Account not found"));
                }
                if (await CommentRepository.DeleteComment(cmtid, uid))
                {
                    return Ok(ReturnMessage.Create("success"));
                }
                return BadRequest(ReturnMessage.Create("error at DeleteComment"));
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
