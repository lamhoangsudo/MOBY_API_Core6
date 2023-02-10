using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Repository;

namespace Item.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;
        public ImageController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        [HttpPost("CreateImage/{image1} {image2} {image3} {image4} {image5}")]
        public async Task<IActionResult> createImage(string image1, string image2, string image3, string image4, string image5)
        {
            try
            {
                bool checkCreate = await _imageRepository.CreateImage(image1, image2, image3, image4, image5);
                if (checkCreate)
                {
                    return Ok("da tao thanh cong");
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
