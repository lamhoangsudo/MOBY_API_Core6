using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Repository.IRepository
{
    public interface IReportRepository
    {
        Task<int> CreateItemReport(CreateReportVM reportVM);
        Task<int> CreateOrderReport(CreateReportVM reportVM);
        Task<int> CreateCommentReport(CreateReportVM reportVM);
        Task<int> CreateReplyReport(CreateReportVM reportVM);
        Task<int> CreateBlogReport(CreateReportVM reportVM);
        Task<int> ApprovedReport(ApprovedReportVM reportVM, IEmailService emailRepository);
        Task<int> DenyReport(DenyReportVM reportVM, IEmailService emailRepository);
        Task<int> UpdateReport(UpdateReportVM reportVM);
        Task<int> DeleteReport(DeleteReportVM reportVM);
        Task<ListVM<ViewReport>?> GetReports(DynamicFilterReportVM dynamicFilterReportVM);
        Task<ViewReportItem?> ItemReportDetail(int reportID);
        Task<ViewReportBlog?> BlogReportDetail(int reportID);
        Task<ViewReportComment?> CommentReportDetail(int reportID);
        Task<ViewReportReply?> ReplyReportDetail(int reportID);
        Task<ViewReportOrder?> OrderReportDetail(int reportID);
        Task<bool> HiddenItems(HiddenAndPunish hideAndPunish, IEmailService emailRepository);
        Task<bool> HiddenOrders(HiddenAndPunish hideAndPunish, IEmailService emailRepository);
        Task<bool> HiddenComments(HiddenAndPunish hideAndPunish, IEmailService emailRepository);
        Task<bool> HiddenReplies(HiddenAndPunish hideAndPunish, IEmailService emailRepository);
        Task<bool> HiddenBlogs(HiddenAndPunish hideAndPunish, IEmailService emailRepository);
        Task<int> PunishViolatorsItems(HiddenAndPunish hideAndPunish, RecordPenaltyPoint recordPenaltyPoint, IEmailService emailRepository);
        Task<int> PunishViolatorsOrders(HiddenAndPunish hideAndPunish, RecordPenaltyPoint recordPenaltyPoint, IEmailService emailRepository);
        Task<int> PunishViolatorsComments(HiddenAndPunish hideAndPunish, RecordPenaltyPoint recordPenaltyPoint, IEmailService emailRepository);
        Task<int> PunishViolatorsReplies(HiddenAndPunish hideAndPunish, RecordPenaltyPoint recordPenaltyPoint, IEmailService emailRepository);
        Task<int> PunishViolatorsBlogs(HiddenAndPunish hideAndPunish, RecordPenaltyPoint recordPenaltyPoint, IEmailService emailRepository);
        Task<StatusAndReasonHidenViewModel?> GetStatusAndReasonHidenItems(int id, StatusAndReasonHidenViewModel? statusAndReasonHidenViewModel);
        Task<StatusAndReasonHidenViewModel?> GetStatusAndReasonHidenOrders(int id, StatusAndReasonHidenViewModel? statusAndReasonHidenViewModel);
        Task<StatusAndReasonHidenViewModel?> GetStatusAndReasonHidenComments(int id, StatusAndReasonHidenViewModel? statusAndReasonHidenViewModel);
        Task<StatusAndReasonHidenViewModel?> GetStatusAndReasonHidenReplies(int id, StatusAndReasonHidenViewModel? statusAndReasonHidenViewModel);
        Task<StatusAndReasonHidenViewModel?> GetStatusAndReasonHidenBlogs(int id, StatusAndReasonHidenViewModel? statusAndReasonHidenViewModel);
        Task<UserAccount?> GetUserByItemID(int id);
        Task<UserAccount?> GetUserByOrderID(int id);
        Task<UserAccount?> GetUserByCommentID(int id);
        Task<UserAccount?> GetUserByReplyID(int id);
        Task<UserAccount?> GetUserByBlogID(int id);
    }
}
