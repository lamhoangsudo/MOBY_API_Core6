using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Controllers
{

    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService userDAO;
        public AuthenticationController(IUserService userDao)
        {
            this.userDAO = userDao;
        }
        [HttpPost]
        [Route("api/useraccount/login")]
        public async Task<IActionResult> PostGoogleAuthentication()
        {


            //var firebaseApp = FirebaseApp.GetInstance;

            try
            {
                if (await userDAO.CheckExistedUser(this.User.Claims.First(i => i.Type == "user_id").Value))
                {
                    //user existed 
                    return Ok(ReturnMessage.Create("User existed"));
                }
                else
                {
                    //quang qua trang dien thong tin
                    return Ok(ReturnMessage.Create("new user"));
                }


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }


        }
    }
}
