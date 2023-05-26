using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Service;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannerController : ControllerBase
    {
        private readonly IBannerService _bannerRepository;
        public readonly IUserService _userRepository;

        public BannerController(IBannerService bannerRepository, IUserService userRepository)
        {
            _bannerRepository = bannerRepository;
            _userRepository = userRepository;
        }

        //[Authorize]
        [HttpPost("CreateBanner")]
        public async Task<IActionResult> CreateBanner([FromBody] CreateBannerVM bannerVM)
        {
            try
            {
                bool checkCreate = await _bannerRepository.CreateBanner(bannerVM);
                if (checkCreate)
                {
                    return Ok(ReturnMessage.Create("đã tạo thành công"));
                }
                else
                {
#pragma warning disable CS8604 // Possible null reference argument.
                    return BadRequest(ReturnMessage.Create(BannerService.ErrorMessage));
#pragma warning restore CS8604 // Possible null reference argument.
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //[Authorize]
        [HttpPut("UpdateBanner")]
        public async Task<IActionResult> UpdateBanner([FromBody] UpdateBannerVM bannerVM)
        {
            try
            {
                bool checkCreate = await _bannerRepository.UpdateBanner(bannerVM);
                if (checkCreate)
                {
                    return Ok(ReturnMessage.Create("đã cập nhập thành công"));
                }
                else
                {
#pragma warning disable CS8604 // Possible null reference argument.
                    return BadRequest(ReturnMessage.Create(BannerService.ErrorMessage));
#pragma warning restore CS8604 // Possible null reference argument.
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //[Authorize]
        [HttpDelete("DeleteBanner")]
        public async Task<IActionResult> DeleteBanner([FromBody] int id)
        {
            try
            {
                bool checkCreate = await _bannerRepository.DeleteBanner(id);
                if (checkCreate)
                {
                    return Ok(ReturnMessage.Create("đã xóa thành công"));
                }
                else
                {
#pragma warning disable CS8604 // Possible null reference argument.
                    return BadRequest(ReturnMessage.Create(BannerService.ErrorMessage));
#pragma warning restore CS8604 // Possible null reference argument.
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetAllBanner")]
        public async Task<IActionResult> GetAllBanner()
        {
            try
            {
                List<BannerVM>? bannerVMs = await _bannerRepository.GetAllBanner();
                if (bannerVMs != null && bannerVMs.Any())
                {
                    return Ok(bannerVMs);
                }
                else
                {
#pragma warning disable CS8604 // Possible null reference argument.
                    return BadRequest(ReturnMessage.Create(BannerService.ErrorMessage));
#pragma warning restore CS8604 // Possible null reference argument.
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
