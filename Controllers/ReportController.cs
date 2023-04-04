using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpPost("CreateReport")]
        public async Task<IActionResult> CreateReport([FromBody] CreateReportVM reportVM)
        {
            try
            {
                int userID = int.Parse(this.User.Claims.First(i => i.Type == "user_id").Value);
                reportVM.userID = userID;
                bool checkCreate = false;
                if (reportVM.itemID != null)
                {
                    checkCreate = await _reportRepository.CreateItemReport(reportVM);
                }
                else if (reportVM.orderID != null)
                {
                    checkCreate = await _reportRepository.CreateOrderReport(reportVM);
                }
                else if (reportVM.commentID != null)
                {
                    checkCreate = await _reportRepository.CreateCommentReport(reportVM);
                }
                else if (reportVM.replyID != null)
                {
                    checkCreate = await _reportRepository.CreateReplyReport(reportVM);
                }
                else if (reportVM.blogID != null)
                {
                    checkCreate = await _reportRepository.CreateBlogReport(reportVM);
                }

                if (checkCreate)
                {
                    return Ok(ReturnMessage.Create("đã tạo thành công"));
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ReportRepository.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpPut("UpdateReport")]
        public async Task<IActionResult> UpdateReport([FromBody] UpdateReportVM reportVM)
        {
            try
            {
                bool checkUpdate = await _reportRepository.UpdateReport(reportVM);
                if (checkUpdate)
                {
                    return Ok(ReturnMessage.Create("đã cập nhập thành công"));
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ReportRepository.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("ApprovedReport")]
        public async Task<IActionResult> ApprovedReport([FromBody] ApprovedReportVM reportVM)
        {
            try
            {
                bool checkDelete = await _reportRepository.ApprovedReport(reportVM);
                if (checkDelete)
                {
                    return Ok(ReturnMessage.Create("đã duyệt"));
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ReportRepository.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("DenyReport")]
        public async Task<IActionResult> DenyReport([FromBody] DenyReportVM reportVM)
        {
            try
            {
                bool checkDelete = await _reportRepository.DenyReport(reportVM);
                if (checkDelete)
                {
                    return Ok(ReturnMessage.Create("đã từ chối"));
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ReportRepository.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("DeleteReport")]
        public async Task<IActionResult> DeleteReport([FromBody] DeleteReportVM reportVM)
        {
            try
            { 
                bool checkDelete = await _reportRepository.DeleteReport(reportVM);
                if (checkDelete)
                {
                    return Ok(ReturnMessage.Create("đã hủy"));
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ReportRepository.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("GetAllReport")]
        public async Task<IActionResult> GetReport([FromBody] DynamicFilterReportVM dynamicFilterReportVM)
        {
            try
            {
                ListVM<ViewReport>? reports = await _reportRepository.GetReports(dynamicFilterReportVM);
                if (reports != null)
                {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    if (reports.List.Count == 0 || reports.List == null)
                    {
                        return NotFound(ReturnMessage.Create("không có report nào"));
                    }
                    else
                    {
                        return Ok(reports);
                    }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                }
                else
                {
#pragma warning disable CS8604 // Possible null reference argument.
                    return BadRequest(ReturnMessage.Create(ReportRepository.ErrorMessage));
#pragma warning restore CS8604 // Possible null reference argument.
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize]
        [HttpPost("GetAllReportByUser")]
        public async Task<IActionResult> GetAllReportByUser([FromBody] DynamicFilterReportVM dynamicFilterReportVM)
        {
            try
            {
                int userID = int.Parse(this.User.Claims.First(i => i.Type == "user_id").Value);
                dynamicFilterReportVM.UserID = userID;
                ListVM<ViewReport>? reports = await _reportRepository.GetReports(dynamicFilterReportVM);
                if (reports != null)
                {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    if (reports.List.Count == 0 || reports.List == null)
                    {
                        return NotFound(ReturnMessage.Create("không có report nào"));
                    }
                    else
                    {
                        return Ok(reports);
                    }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                }
                else
                {
#pragma warning disable CS8604 // Possible null reference argument.
                    return BadRequest(ReturnMessage.Create(ReportRepository.ErrorMessage));
#pragma warning restore CS8604 // Possible null reference argument.
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetDetailReport")]
        public async Task<IActionResult> DetaiGetDetailReport(int report, bool item, bool blog, bool comment, bool reply, bool order)
        {
            try
            {
                if(item)
                {
                    ViewReportItem? viewReportItem = await _reportRepository.ItemReportDetail(report);
                    if (viewReportItem != null)
                    {
                        return Ok(viewReportItem);
                    }
                    else
                    {
                        return NotFound(ReturnMessage.Create("report không tồn tại"));
                    }
                }
                else if (blog)
                {
                    ViewReportBlog? viewReportBlog = await _reportRepository.BlogReportDetail(report);
                    if (viewReportBlog != null)
                    {
                        return Ok(viewReportBlog);
                    }
                    else
                    {
                        return NotFound(ReturnMessage.Create("report không tồn tại"));
                    }
                }
                else if (comment)
                {
                    ViewReportComment? viewReportCommnent = await _reportRepository.CommentReportDetail(report);
                    if (viewReportCommnent != null)
                    {
                        return Ok(viewReportCommnent);
                    }
                    else
                    {
                        return NotFound(ReturnMessage.Create("report không tồn tại"));
                    }
                }
                else if (reply)
                {
                    ViewReportReply? viewReportReply = await _reportRepository.ReplyReportDetail(report);
                    if (viewReportReply != null)
                    {
                        return Ok(viewReportReply);
                    }
                    else
                    {
                        return NotFound(ReturnMessage.Create("report không tồn tại"));
                    }
                }
                else if (order)
                {
                    ViewReportOrder? viewReportOrder = await _reportRepository.OrderReportDetail(report);
                    if (viewReportOrder != null)
                    {
                        return Ok(viewReportOrder);
                    }
                    else
                    {
                        return NotFound(ReturnMessage.Create("report không tồn tại"));
                    }
                }
#pragma warning disable CS8604 // Possible null reference argument.
                return BadRequest(ReturnMessage.Create(ReportRepository.ErrorMessage));
#pragma warning restore CS8604 // Possible null reference argument.
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
