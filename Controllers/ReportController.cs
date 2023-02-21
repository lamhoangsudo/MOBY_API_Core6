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
        public ReportController(IReportRepository reportRepository)
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
                    return Ok(ReturnMessage.create("đã tạo thành công"));
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ReportRepository.errorMessage);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut("/UpdateReport")]
        public async Task<IActionResult> UpdateReport([FromBody] UpdateReportVM reportVM)
        {
            try
            {
                bool checkUpdate = await _reportRepository.UpdateReport(reportVM);
                if (checkUpdate)
                {
                    return Ok(ReturnMessage.create("đã cập nhập thành công"));
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ReportRepository.errorMessage);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPatch("/ApprovedReport")]
        public async Task<IActionResult> ApprovedReport([FromBody] ApprovedReportVM reportVM)
        {
            try
            {
                bool checkDelete = await _reportRepository.ApprovedReport(reportVM);
                if (checkDelete)
                {
                    return Ok(ReturnMessage.create("đã duyệt"));
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ReportRepository.errorMessage);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("/GetReport/Status?={status}")]
        public async Task<IActionResult> GetAllReportByStatus(int status)
        {
            try
            {
                List<Report> reports = await _reportRepository.GetAllReportByStatus(status);
                if (reports.Count != 0)
                {
                    return Ok(reports);
                }
                else
                {
                    return NotFound(ReturnMessage.create("không có report nào"));
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}
