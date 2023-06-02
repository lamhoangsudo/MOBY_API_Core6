using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Log4Net;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Service
{
    public class ReportService : IReportService
    {
        private readonly IEmailService _emailService;
        private readonly IReportRepository _reportRepository;
        private readonly Logger4Net _logger4Net;
        public static string ErrorMessage { get; set; } = string.Empty;

        public ReportService(IEmailService emailService, IReportRepository reportRepository)
        {
            _emailService = emailService;
            _reportRepository = reportRepository;
            _logger4Net = new Logger4Net();
        }
        public async Task<bool> CreateReport(CreateReportVM reportVM)
        {
            try
            {
                int type = reportVM.Type;
                int check = 0;
                switch (type)
                {
                    case 0:
                        //item
                        check = await _reportRepository.CreateItemReport(reportVM);
                        if (check > 0)
                        {
                            return true;
                        }
                        break;
                    case 1:
                        //order
                        check = await _reportRepository.CreateOrderReport(reportVM);
                        if (check > 0)
                        {
                            return true;
                        }
                        break;
                    case 2:
                        //comment
                        check = await _reportRepository.CreateCommentReport(reportVM);
                        if (check > 0)
                        {
                            return true;
                        }
                        break;
                    case 3:
                        //reply
                        check = await _reportRepository.CreateReplyReport(reportVM);
                        if (check > 0)
                        {
                            return true;
                        }
                        break;
                    case 4:
                        //blog
                        check = await _reportRepository.CreateBlogReport(reportVM);
                        if (check > 0)
                        {
                            return true;
                        }
                        break;
                }
                throw new ArgumentOutOfRangeException();
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                ErrorMessage = ex.Message;
                return false;
            }
        }
        public async Task<bool> ApprovedReport(ApprovedReportVM reportVM)
        {
            try
            {
                int check = await _reportRepository.ApprovedReport(reportVM, _emailService);
                if (check <= 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }
        public async Task<bool> DenyReport(DenyReportVM reportVM)
        {
            try
            {
                int check = await _reportRepository.DenyReport(reportVM, _emailService);
                if (check <= 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }
        public async Task<bool> UpdateReport(UpdateReportVM reportVM)
        {
            try
            {
                int check = await _reportRepository.UpdateReport(reportVM);
                if (check <= 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }
        public async Task<bool> DeleteReport(DeleteReportVM reportVM)
        {
            try
            {
                int check = await _reportRepository.DeleteReport(reportVM);
                if (check <= 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                ErrorMessage = ex.Message;
                return false;
            }
        }
        public async Task<ListVM<ViewReport>?> GetReports(DynamicFilterReportVM dynamicFilterReportVM)
        {
            try
            {
                ListVM<ViewReport>? ViewReport;
                ViewReport = await _reportRepository.GetReports(dynamicFilterReportVM);
                if (ViewReport == null)
                {
                    ViewReport = new(0, 0, new List<ViewReport>());
                }
                return ViewReport;
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                ErrorMessage = ex.Message;
                return null;
            }
        }
        public async Task<ViewReportItem?> ItemReportDetail(int reportID)
        {
            try
            {
                ViewReportItem? itemReport = await _reportRepository.ItemReportDetail(reportID);
                return itemReport;
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                ErrorMessage = ex.Message;
                return null;
            }
        }
        public async Task<ViewReportBlog?> BlogReportDetail(int reportID)
        {
            try
            {
                ViewReportBlog? itemReport = await _reportRepository.BlogReportDetail(reportID);
                return itemReport;
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                ErrorMessage = ex.Message;
                return null;
            }
        }
        public async Task<ViewReportComment?> CommentReportDetail(int reportID)
        {
            try
            {
                ViewReportComment? itemReport = await _reportRepository.CommentReportDetail(reportID);
                return itemReport;
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                ErrorMessage = ex.Message;
                return null;
            }
        }
        public async Task<ViewReportReply?> ReplyReportDetail(int reportID)
        {
            try
            {
                ViewReportReply? itemReport = await _reportRepository.ReplyReportDetail(reportID);
                return itemReport;
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                ErrorMessage = ex.Message;
                return null;
            }
        }
        public async Task<ViewReportOrder?> OrderReportDetail(int reportID)
        {
            try
            {
                ViewReportOrder? itemReport = await _reportRepository.OrderReportDetail(reportID);
                return itemReport;
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                ErrorMessage = ex.Message;
                return null;
            }
        }
        public async Task<bool> HiddenOject(HiddenAndPunish hideAndPunish)
        {
            try
            {
                int id = hideAndPunish.Id;
                int type = hideAndPunish.Type;
                bool check = false;
                switch (type)
                {
                    case 0:
                        //item
                        check = await _reportRepository.HiddenItems(hideAndPunish, _emailService);
                        if (check)
                        {
                            return check;
                        }
                        break;
                    case 1:
                        //order
                        check = await _reportRepository.HiddenOrders(hideAndPunish, _emailService);
                        if (check)
                        {
                            return check;
                        }
                        break;
                    case 2:
                        //comment
                        check = await _reportRepository.HiddenComments(hideAndPunish, _emailService);
                        if (check)
                        {
                            return check;
                        }
                        break;
                    case 3:
                        //reply
                        check = await _reportRepository.HiddenReplies(hideAndPunish, _emailService);
                        if (check)
                        {
                            return check;
                        }
                        break;
                    case 4:
                        //blog
                        check = await _reportRepository.HiddenBlogs(hideAndPunish, _emailService);
                        if (check)
                        {
                            return check;
                        }
                        break;
                }
                throw new ArgumentOutOfRangeException();
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                ErrorMessage = ex.Message;
                return false;
            }
        }
        public async Task<bool> PunishViolators(HiddenAndPunish hideAndPunish)
        {
            try
            {
                int id = hideAndPunish.Id;
                int type = hideAndPunish.Type;
                int check = 0;
                RecordPenaltyPoint? recordPenaltyPoint = new();
                switch (type)
                {
                    case 0:
                        //item
                        check = await _reportRepository.PunishViolatorsItems(hideAndPunish, recordPenaltyPoint, _emailService);
                        if (check > 0)
                        {
                            return true;
                        }
                        break;
                    case 1:
                        //order
                        check = await _reportRepository.PunishViolatorsOrders(hideAndPunish, recordPenaltyPoint, _emailService);
                        if (check > 0)
                        {
                            return true;
                        }
                        break;
                    case 2:
                        //comment
                        check = await _reportRepository.PunishViolatorsComments(hideAndPunish, recordPenaltyPoint, _emailService);
                        if (check > 0)
                        {
                            return true;
                        }
                        break;
                    case 3:
                        //reply
                        check = await _reportRepository.PunishViolatorsReplies(hideAndPunish, recordPenaltyPoint, _emailService);
                        if (check > 0)
                        {
                            return true;
                        }
                        break;
                    case 4:
                        //bg
                        check = await _reportRepository.PunishViolatorsBlogs(hideAndPunish, recordPenaltyPoint, _emailService);
                        if (check > 0)
                        {
                            return true;
                        }
                        break;
                }
                throw new ArgumentOutOfRangeException();
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                ErrorMessage = ex.Message;
                return false;
            }
        }
        public async Task<StatusAndReasonHidenViewModel?> GetStatusAndReasonHiden(int id, int type)
        {
            StatusAndReasonHidenViewModel? statusAndReasonHidenViewModel = null;
            try
            {
                statusAndReasonHidenViewModel = type switch
                {
                    0 => await _reportRepository.GetStatusAndReasonHidenItems(id, statusAndReasonHidenViewModel),//order
                    1 => await _reportRepository.GetStatusAndReasonHidenOrders(id, statusAndReasonHidenViewModel),//order
                    2 => await _reportRepository.GetStatusAndReasonHidenComments(id, statusAndReasonHidenViewModel),//comment
                    3 => await _reportRepository.GetStatusAndReasonHidenReplies(id, statusAndReasonHidenViewModel),//reply
                    4 => await _reportRepository.GetStatusAndReasonHidenBlogs(id, statusAndReasonHidenViewModel),//blog
                    _ => throw new ArgumentOutOfRangeException(),
                };
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                ErrorMessage = ex.Message;
            }
            return statusAndReasonHidenViewModel;
        }
        public async Task<UserAccount?> GetUserByObjID(GetUserObj getUserObj)
        {
            try
            {
                int id = getUserObj.Id;
                int type = getUserObj.Type;
                UserAccount? user = null;
                RecordPenaltyPoint? recordPenaltyPoint = new();
                switch (type)
                {
                    case 0:
                        //item
                        user = await _reportRepository.GetUserByItemID(id);
                        if (user != null)
                        {
                            return user;
                        }
                        break;
                    case 1:
                        //order
                        user = await _reportRepository.GetUserByOrderID(id);
                        if (user != null)
                        {
                            return user;
                        }
                        break;
                    case 2:
                        //comment
                        user = await _reportRepository.GetUserByCommentID(id);
                        if (user != null)
                        {
                            return user;
                        }
                        break;
                    case 3:
                        //reply
                        user = await _reportRepository.GetUserByReplyID(id);
                        if (user != null)
                        {
                            return user;
                        }
                        break;
                    case 4:
                        //bg
                        user = await _reportRepository.GetUserByBlogID(id);
                        if (user != null)
                        {
                            return user;
                        }
                        break;
                }
                throw new ArgumentOutOfRangeException();
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                ErrorMessage = ex.Message;
                return null;
            }
        }
        public async Task<bool> StatusProcessingReportOrder(int reportOrderID)
        {
            try
            {
                int check = await _reportRepository.StatusProcessingReportOrder(reportOrderID, _emailService);
                if (check <= 0)
                {
                    return false;
                }
                return true;
            }
            catch(Exception ex)
            {
                _logger4Net.Loggers(ex);
                ErrorMessage = ex.Message;
                return false;
            }
        }
    }
}
