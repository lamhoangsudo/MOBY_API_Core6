using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public class ReportRepository : IReportRepository
    {
        public static string? errorMessage;
        private readonly MOBYContext _context;
        public ReportRepository(MOBYContext context) 
        {
            _context = context;
        }
        public async Task<bool> CreateReport(CreateReportVM reportVM)
        {
            try
            {
                bool checkItem = _context.Items.Where(it => it.ItemId == reportVM.itemID && it.ItemStatus == true).Any();
                if (checkItem)
                {
                    DateTime dateTimeCreate = DateTime.Now;
                    Report report = new Report();
                    report.ReportDateCreate = dateTimeCreate;
                    report.ItemId = reportVM.itemID;
                    report.ReportStatus = reportVM.status;
                    report.ReportContent= reportVM.content;
                    report.Image = reportVM.image;
                    _context.Reports.Add(report);
                    _context.SaveChanges();
                    return await Task.FromResult(true);
                }
                else
                {
                    errorMessage = "sản phẩm này không có tồn tại";
                    return await Task.FromResult(false);
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> ApprovedReport(ApprovedReportVM reportVM)
        {
            try
            {
                Report report = _context.Reports.Where(rp => rp.ReportId == reportVM.reportID && rp.ReportStatus == 1).FirstOrDefault();
                if (report != null)
                {
                    report.ReportDateUpdate = DateTime.Now;
                    report.ReportStatus = reportVM.isApproved;
                    _context.SaveChanges();
                    return await Task.FromResult(true);
                }
                else
                {
                    errorMessage = "report này không tồn tại";
                    return await Task.FromResult(false);
                }
            }
            catch (Exception ex) 
            {
                errorMessage = ex.Message;
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> UpdateReport(UpdateReportVM reportVM)
        {
            try
            {
                Report report = _context.Reports.Where(rp => rp.ReportId == reportVM.reportID && rp.ReportStatus == 1).FirstOrDefault();
                if (report != null)
                {
                    report.ReportDateUpdate = DateTime.Now;
                    report.ReportContent = reportVM.content;
                    report.Image = reportVM.image;
                    _context.SaveChanges();
                    return await Task.FromResult(true);
                }
                else
                {
                    errorMessage = "report này không có tồn tại";
                    return await Task.FromResult(false);
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return await Task.FromResult(false);
            }
        }

        public async Task<List<Report>?> GetAllReportByStatus(int status)
        {
            try
            {
                List<Report> reports = new List<Report>();
                reports = _context.Reports.Where(rp => rp.ReportStatus == status).ToList();
                return await Task.FromResult(reports);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<Report>?> GetAllReportByUserAndStatus(int status, int userid)
        {
            try
            {
                List<Report> reports = new List<Report>();
                reports = _context.Reports.Where(rp => rp.ReportStatus == status).ToList();
                return await Task.FromResult(reports);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }
    }
}
