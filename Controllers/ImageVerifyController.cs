using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Repository.IRepository;

namespace MOBY_API_Core6.Controllers
{

    [ApiController]
    public class ImageVerifyController : ControllerBase
    {
        private readonly IImageVerifyRepository imageVerifyRepository;
        public ImageVerifyController(IImageVerifyRepository imageVerifyRepository)
        {
            this.imageVerifyRepository = imageVerifyRepository;
        }

        [Authorize]
        [HttpPost]
        [Route("api/image/verify")]
        public async Task<IActionResult> ImageVerify([FromBody] ImageVerifyVM imageVerifyVM)
        {
            bool result = await imageVerifyRepository.verify(imageVerifyVM.imageURL);
            return Ok(result);
        }
    }
}
