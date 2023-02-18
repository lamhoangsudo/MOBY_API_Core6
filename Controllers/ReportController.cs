using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository;

namespace MOBY_API_Core6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        public readonly IReportRepository _reportRepository;
        public ReportController (IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }
        [HttpPost("/CreateReport")]
        public async Task<IActionResult> CreateReport([FromBody] CreateReportVM reportVM)
        {
            try
            {
                bool checkCreate = await _reportRepository.CreateReport(reportVM);
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
        [HttpPost("/UpdateReport")]
        public async Task<IActionResult> UpdateReport([FromBody] UpdateReportVM reportVM)
        {
            try
            {
                bool checkUpdate = await _reportRepository.UpdateReport(reportVM);
                if (checkUpdate)
                {
                    return Ok("da update thanh cong");
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
        [HttpPost("/DeleteReport")]
        public async Task<IActionResult> ApprovedItem([FromBody] ApprovedReportVM reportVM)
        {
            try
            {
                bool checkDelete = await _reportRepository.ApprovedReport(reportVM);
                if (checkDelete)
                {
                    return Ok("da duyet thanh cong");
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
