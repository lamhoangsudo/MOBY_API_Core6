using MOBY_API_Core6.Data_View_Model;

namespace MOBY_API_Core6.Repository
{
    public interface IReportRepository
    {
        Task<bool> CreateReport(CreateReportVM reportVM);

        Task<bool> UpdateReport(UpdateReportVM reportVM);

        Task<bool> ApprovedReport(ApprovedReportVM reportVM);

        Task<bool> DenyReport(DenyReportVM reportVM);

        //Task<List<ViewReport>?> GetAllReportByStatus(int status);

        //Task<List<ViewReport>?> GetAllReportByUserAndStatus(int status, int userid);
    }
}
