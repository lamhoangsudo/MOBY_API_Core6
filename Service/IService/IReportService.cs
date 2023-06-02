using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Service.IService
{
    public interface IReportService
    {
        Task<bool> CreateReport(CreateReportVM reportVM);

        Task<bool> UpdateReport(UpdateReportVM reportVM);

        Task<bool> ApprovedReport(ApprovedReportVM reportVM);

        Task<bool> DenyReport(DenyReportVM reportVM);

        Task<bool> DeleteReport(DeleteReportVM reportVM);

        Task<ListVM<ViewReport>?> GetReports(DynamicFilterReportVM dynamicFilterReportVM);

        Task<ViewReportItem?> ItemReportDetail(int reportID);

        Task<ViewReportBlog?> BlogReportDetail(int reportID);

        Task<ViewReportComment?> CommentReportDetail(int reportID);

        Task<ViewReportReply?> ReplyReportDetail(int reportID);

        Task<ViewReportOrder?> OrderReportDetail(int reportID);

        Task<bool> HiddenOject(HiddenAndPunish hideAndPunish);

        Task<bool> PunishViolators(HiddenAndPunish hideAndPunish);

        Task<StatusAndReasonHidenViewModel?> GetStatusAndReasonHiden(int id, int type);

        Task<UserAccount?> GetUserByObjID(GetUserObj getUserObj);

        Task<bool> StatusProcessingReportOrder(int reportOrderID);
    }
}
