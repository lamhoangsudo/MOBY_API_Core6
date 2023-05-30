using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Log4Net;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Controllers
{

    [ApiController]
    public class ReplyController : ControllerBase
    {
        private readonly IUserService UserRepository;
        private readonly IReplyService ReplyRepository;
        private readonly Logger4Net _logger4Net;

        public ReplyController(IUserService userRepository, IReplyService ReplyRepository)
        {
            this.UserRepository = userRepository;
            this.ReplyRepository = ReplyRepository;
            _logger4Net = new Logger4Net();
        }

        [HttpGet]
        [Route("api/reply")]
        public async Task<IActionResult> GetReplyByID([FromQuery] ReplyIDVM replyIDVM)
        {
            try
            {
                ReplyVM? replyFound = await ReplyRepository.GetReplyByReplyID(replyIDVM.ReplyId);
                return Ok(replyFound);
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/Reply/create")]
        public async Task<IActionResult> CreateReply([FromBody] CreateReplyVM rep)
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
                if (await ReplyRepository.CreateReply(rep, uid))
                {
                    return Ok(ReturnMessage.Create("success"));
                }
                return BadRequest(ReturnMessage.Create("error at CreateReply"));
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("api/reply")]
        public async Task<IActionResult> UpdateReply([FromBody] UpdateReplyVM rep)
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
                if (await ReplyRepository.UpdateReply(rep, uid))
                {
                    return Ok(ReturnMessage.Create("success"));
                }
                return BadRequest(ReturnMessage.Create("error at UpdateReply"));
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("api/reply/delete")]
        public async Task<IActionResult> DeleteReply([FromQuery] GetReplyIDVM cmtid)
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
                if (await ReplyRepository.DeleteReply(cmtid, uid))
                {
                    return Ok(ReturnMessage.Create("success"));
                }
                return BadRequest(ReturnMessage.Create("error at DeleteReply"));
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
