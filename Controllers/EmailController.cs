using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailRepository;

        public EmailController(IEmailService emailRepository)
        {
            _emailRepository = emailRepository;
        }

        [HttpPost("SendEmail")]
        public IActionResult SendEmail(Email email)
        {
            try
            {
                _emailRepository.SendEmai(email);
                return Ok(email);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

    }
}
