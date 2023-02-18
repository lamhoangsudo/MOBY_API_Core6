using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public interface IReportRepository
    {
        public Task<bool> CreateReport(CreateReportVM reportVM);
        public Task<bool> UpdateReport(UpdateReportVM reportVM);
        public Task<bool> ApprovedReport(ApprovedReportVM reportVM);
    }
}
