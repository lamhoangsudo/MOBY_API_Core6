using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public interface IReportRepository
    {
        Task<bool> CreateReport(CreateReportVM reportVM);

        Task<bool> UpdateReport(UpdateReportVM reportVM);

        Task<bool> ApprovedReport(ApprovedReportVM reportVM);

        Task<List<Report>?> GetAllReportByStatus(int status);

        Task<List<Report>?> GetAllReportByUserAndStatus(int status, int userid);
    }
}
