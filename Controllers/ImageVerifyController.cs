using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Controllers
{

    [ApiController]
    public class ImageVerifyController : ControllerBase
    {
        private readonly IImageVerifyService imageVerifyRepository;
        public ImageVerifyController(IImageVerifyService imageVerifyRepository)
        {
            this.imageVerifyRepository = imageVerifyRepository;
        }

        [Authorize]
        [HttpPost]
        [Route("api/image/verify")]
        public async Task<IActionResult> ImageVerify([FromBody] ImageVerifyVM imageVerifyVM)
        {
            string result = await imageVerifyRepository.Verify(imageVerifyVM.ImageURL);
            return Ok(ReturnMessage.Create(result));
        }
    }
}
