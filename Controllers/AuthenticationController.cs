using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository;

namespace MOBY_API_Core6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRepository userDAO;
        public AuthenticationController(IUserRepository userDao)
        {
            this.userDAO = userDao;
        }
        [HttpGet]
        [Route("api/AuthenticationController/CheckLogin")]
        public async Task<IActionResult> PostGoogleAuthentication()
        {


            //var firebaseApp = FirebaseApp.GetInstance;

            try
            {
                if (await userDAO.CheckExistedUser(this.User.Claims))
                {
                    //user existed 
                    return Ok(ReturnMessage.create("User existed"));
                }
                else
                {
                    //quang qua trang dien thong tin
                    return Ok(ReturnMessage.create("new user"));
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ReturnMessage.create(ex.Message));
            }


        }
    }
}
