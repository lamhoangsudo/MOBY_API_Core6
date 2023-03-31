using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public interface IReportRepository
    {
        Task<bool> CreateItemReport(CreateReportVM reportVM);

        Task<bool> CreateOrderReport(CreateReportVM reportVM);

        Task<bool> CreateCommentReport(CreateReportVM reportVM);

        Task<bool> CreateReplyReport(CreateReportVM reportVM);

        Task<bool> CreateBlogReport(CreateReportVM reportVM);

        Task<bool> UpdateReport(UpdateReportVM reportVM);

        Task<bool> ApprovedReport(ApprovedReportVM reportVM);

        Task<bool> DenyReport(DenyReportVM reportVM);

        Task<bool> DeleteReport(DeleteReportVM reportVM);

        Task<List<ViewReport>?> GetAllReport(int pageNumber, int pageSize);

        Task<List<ViewReport>?> GetAllReportByStatus(int status, int pageNumber, int pageSize);

        Task<List<ViewReport>?> GetAllReportByUserAndStatus(int status, int userid, int pageNumber, int pageSize);

        Task<List<ViewReport>?> GetAllReportByUser(int userid, int pageNumber, int pageSize);
    }
}
