using Microsoft.AspNetCore.Mvc;
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
        [HttpPost]
        [Route("api/AuthenticationController/CheckLogin")]
        public async Task<String> PostGoogleAuthentication()
        {


            //var firebaseApp = FirebaseApp.GetInstance;

            try
            {
                if (await userDAO.CheckExistedUser(this.User.Claims))
                {
                    //user existed 
                    return "user existed";
                }
                else
                {
                    //quang qua trang dien thong tin
                    return "new user";
                }


            }
            catch
            {
                return "Error token not found";
            }

            return "Error token not found";
        }
    }
}
