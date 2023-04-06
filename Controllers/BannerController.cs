using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository;

namespace MOBY_API_Core6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannerController : ControllerBase
    {
        private readonly IBannerRepository _bannerRepository;
        public readonly IUserRepository _userRepository;

        public BannerController(IBannerRepository bannerRepository, IUserRepository userRepository)
        {
            _bannerRepository = bannerRepository;
            _userRepository = userRepository;
        }

        [Authorize]
        [HttpPost("CreateBanner")]
        public async Task<IActionResult> CreateBanner(string link, string image) 
        {
            try
            {
                bool checkCreate = await _bannerRepository.CreateBanner(link);
                if (checkCreate)
                {
                    return Ok(ReturnMessage.Create("đã tạo thành công"));
                }
                else
                {
#pragma warning disable CS8604 // Possible null reference argument.
                    return BadRequest(ReturnMessage.Create(BannerRepository.ErrorMessage));
#pragma warning restore CS8604 // Possible null reference argument.
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpPut("UpdateBanner")]
        public async Task<IActionResult> UpdateBanner([FromBody] BannerVM bannerVM)
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
                    return BadRequest(ReturnMessage.Create(BannerRepository.ErrorMessage));
#pragma warning restore CS8604 // Possible null reference argument.
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
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
                    return BadRequest(ReturnMessage.Create(BannerRepository.ErrorMessage));
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
                    return NotFound(ReturnMessage.Create(BannerRepository.ErrorMessage));
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
