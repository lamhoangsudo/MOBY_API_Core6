using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository;

namespace MOBY_API_Core6.Controllers
{

    [ApiController]
    public class ReplyController : ControllerBase
    {
        private readonly IUserRepository UserDAO;
        private readonly IReplyRepository RepDAO;

        public ReplyController(IUserRepository userRepository, IReplyRepository RepDAO)
        {
            this.UserDAO = userRepository;
            this.RepDAO = RepDAO;
        }

        [Authorize]
        [HttpPost]
        [Route("api/Reply/create")]
        public async Task<IActionResult> CreateReply([FromBody] CreateReplyVM rep)
        {
            try
            {
                int uid = await UserDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (await RepDAO.CreateReply(rep, uid))
                {
                    return Ok(ReturnMessage.create("success"));
                }
                return BadRequest(ReturnMessage.create("error at CreateReply"));
            }
            catch (Exception ex)
            {
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
                int uid = await UserDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (await RepDAO.UpdateReply(rep, uid))
                {
                    return Ok(ReturnMessage.create("success"));
                }
                return BadRequest(ReturnMessage.create("error at UpdateReply"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("api/reply/delete")]
        public async Task<IActionResult> DeleteReply([FromBody] GetReplyIDVM cmtid)
        {
            try
            {
                int uid = await UserDAO.getUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (await RepDAO.DeleteReply(cmtid, uid))
                {
                    return Ok(ReturnMessage.create("success"));
                }
                return BadRequest(ReturnMessage.create("error at DeleteReply"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
