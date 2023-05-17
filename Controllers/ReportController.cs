using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Service;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        public readonly IReportService _reportService;
        public readonly IUserService _userService;
        public readonly IRecordPenaltyService _recordPenaltyService;

        public ReportController(IReportService reportService, IUserService userService, IRecordPenaltyService recordPenaltyService)
        {
            _reportService = reportService;
            _userService = userService;
            _recordPenaltyService = recordPenaltyService;
        }

        [Authorize]
        [HttpPost("CreateReport")]
        public async Task<IActionResult> CreateReport([FromBody] CreateReportVM reportVM)
        {
            try
            {
                int userID = await _userService.GetUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (userID == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                if (userID == -1)
                {
                    return BadRequest(ReturnMessage.Create("Account not found"));
                }
                UserAccount? user = await _userService.FindUserByUid(userID);
                if (user == null)
                {
                    return BadRequest(ReturnMessage.Create("tài khoảng không tồn tại"));
                }
                reportVM.UserID = userID;
                bool checkCreate = await _reportService.CreateReport(reportVM);
                if (checkCreate)
                {
                    return Ok(ReturnMessage.Create("đã tạo thành công"));
                }
                else
                {
                    return BadRequest(ReturnMessage.Create(ReportService.ErrorMessage));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut("UpdateReport")]
        public async Task<IActionResult> UpdateReport([FromBody] UpdateReportVM reportVM)
        {
            try
            {
                bool checkUpdate = await _reportService.UpdateReport(reportVM);
                if (checkUpdate)
                {
                    return Ok(ReturnMessage.Create("đã cập nhập thành công"));
                }
                else
                {
                    return BadRequest(ReturnMessage.Create(ReportService.ErrorMessage));
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
                bool checkDelete = await _reportService.ApprovedReport(reportVM);
                if (checkDelete)
                {
                    return Ok(ReturnMessage.Create("đã duyệt"));
                }
                else
                {
                    return BadRequest(ReturnMessage.Create(ReportService.ErrorMessage));
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
                bool checkDelete = await _reportService.DenyReport(reportVM);
                if (checkDelete)
                {
                    return Ok(ReturnMessage.Create("đã từ chối"));
                }
                else
                {
                    return BadRequest(ReturnMessage.Create(ReportService.ErrorMessage));
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
                bool checkDelete = await _reportService.DeleteReport(reportVM);
                if (checkDelete)
                {
                    return Ok(ReturnMessage.Create("đã hủy"));
                }
                else
                {
                    return BadRequest(ReturnMessage.Create(ReportService.ErrorMessage));
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
                ListVM<ViewReport>? reports = await _reportService.GetReports(dynamicFilterReportVM);
                if (reports != null)
                {
                    return Ok(reports);
                }
                else
                {
                    return BadRequest(ReturnMessage.Create(ReportService.ErrorMessage));
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
                int userID = await _userService.GetUserIDByUserCode(this.User.Claims.First(i => i.Type == "user_id").Value);
                if (userID == 0)
                {
                    return BadRequest(ReturnMessage.Create("Account has been suspended"));
                }
                if (userID == -1)
                {
                    return BadRequest(ReturnMessage.Create("Account not found"));
                }
                UserAccount? user = await _userService.FindUserByUidWithoutStatus(userID);
                if (user == null)
                {
                    return BadRequest(ReturnMessage.Create("tài khoảng không tồn tại"));
                }
                dynamicFilterReportVM.UserID = userID;
                ListVM<ViewReport>? reports = await _reportService.GetReports(dynamicFilterReportVM);
                if (reports != null)
                {
                        return Ok(reports);
                }
                else
                {
                    return BadRequest(ReturnMessage.Create(ReportService.ErrorMessage));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [Authorize]
        [HttpGet("GetDetailReport")]
        public async Task<IActionResult> GetDetailReport(int report, int type)
        {
            try
            {
                switch (type)
                {
                    case 0:
                        ViewReportItem? viewReportItem = await _reportService.ItemReportDetail(report);
                        if (viewReportItem != null)
                        {
                            return Ok(viewReportItem);
                        }
                        else
                        {
                            return NotFound(ReturnMessage.Create("report không tồn tại"));
                        }
                    case 4:
                        ViewReportBlog? viewReportBlog = await _reportService.BlogReportDetail(report);
                        if (viewReportBlog != null)
                        {
                            return Ok(viewReportBlog);
                        }
                        else
                        {
                            return NotFound(ReturnMessage.Create("report không tồn tại"));
                        }
                    case 2:
                        ViewReportComment? viewReportCommnent = await _reportService.CommentReportDetail(report);
                        if (viewReportCommnent != null)
                        {
                            return Ok(viewReportCommnent);
                        }
                        else
                        {
                            return NotFound(ReturnMessage.Create("report không tồn tại"));
                        }
                    case 3:
                        ViewReportReply? viewReportReply = await _reportService.ReplyReportDetail(report);
                        if (viewReportReply != null)
                        {
                            return Ok(viewReportReply);
                        }
                        else
                        {
                            return NotFound(ReturnMessage.Create("report không tồn tại"));
                        }
                    case 1:
                        ViewReportOrder? viewReportOrder = await _reportService.OrderReportDetail(report);
                        if (viewReportOrder != null)
                        {
                            return Ok(viewReportOrder);
                        }
                        else
                        {
                            return NotFound(ReturnMessage.Create("report không tồn tại"));
                        }
                }
                return BadRequest(ReturnMessage.Create(ReportService.ErrorMessage));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPatch("HiddenObject")]
        public async Task<IActionResult> HideObject([FromBody] HiddenAndPunish hiddenAndPunish)
        {
            try
            {
                bool checkHidden = await _reportService.HiddenOject(hiddenAndPunish);
                if (checkHidden)
                {
                    return Ok(ReturnMessage.Create("đã ẩn đối tượng thành công"));
                }
                else
                {
                    return BadRequest(ReturnMessage.Create(ReportService.ErrorMessage));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut("PunishViolators")]
        public async Task<IActionResult> PunishViolators([FromBody] HiddenAndPunish hideAndPunish)
        {
            try
            {
                bool checkHide = await _reportService.PunishViolators(hideAndPunish);
                if (checkHide)
                {
                    return Ok(ReturnMessage.Create("đã trừ điểm user thành công"));
                }
                else
                {
                    return BadRequest(ReturnMessage.Create(ReportService.ErrorMessage));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("GetStatusAndReasonHiden")]
        public async Task<IActionResult> GetStatusAndReasonHiden([FromQuery] int id, [FromQuery] int type)
        {
            try
            {
                StatusAndReasonHidenViewModel? statusAndReasonHidenViewModel = await _reportService.GetStatusAndReasonHiden(id, type);
                if (statusAndReasonHidenViewModel != null)
                {
                    return Ok(statusAndReasonHidenViewModel);
                }
                else
                {
                    return BadRequest(ReturnMessage.Create(ReportService.ErrorMessage));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("GetRecordPenaltyPointsByUser")]
        public async Task<IActionResult> GetRecordPenaltyPointsByUser([FromQuery] int userID)
        {
            try
            {
                List<RecordPenaltyPoint>? recordPenaltyPoints = await _recordPenaltyService.GetRecordPenaltyPointsByUserID(userID);
                if (recordPenaltyPoints != null)
                {
                    return Ok(recordPenaltyPoints);
                }
                else
                {
                    return BadRequest(ReturnMessage.Create(RecordPenaltyService.ErrorMessage));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
