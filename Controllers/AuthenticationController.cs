using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Log4Net;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Controllers
{

    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService userDAO;
        private readonly Logger4Net _logger4Net;
        public AuthenticationController(IUserService userDao)
        {
            this.userDAO = userDao;
            _logger4Net = new Logger4Net();
        }

        [HttpPost]
        [Route("api/useraccount/login")]
        public async Task<IActionResult> PostGoogleAuthentication()
        {


            //var firebaseApp = FirebaseApp.GetInstance;

            try
            {
                string result = await userDAO.CheckExistedUser(this.User.Claims.First(i => i.Type == "user_id").Value);
                return Ok(ReturnMessage.Create(result));
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
