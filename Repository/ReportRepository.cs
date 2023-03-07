using Microsoft.EntityFrameworkCore;
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
                bool checkItem = await _context.Items.Where(it => it.ItemId == reportVM.itemID && it.ItemStatus == true).AnyAsync();
                bool checkUser = await _context.UserAccounts.Where(us => us.UserId == reportVM.userID && us.UserStatus == true).AnyAsync();
                if (checkItem == true && checkUser == true)
                {
                    DateTime dateTimeCreate = DateTime.Now;
                    Report report = new Report();
                    report.ReportDateCreate = dateTimeCreate;
                    report.ItemId = reportVM.itemID;
                    report.UserId = reportVM.userID;
                    report.ReportStatus = reportVM.status;
                    report.ReportContent= reportVM.content;
                    report.Image = reportVM.image;
                    await _context.Reports.AddAsync(report);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    errorMessage = "sản phẩm này không có tồn tại";
                    return false;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public async Task<bool> ApprovedReport(ApprovedReportVM reportVM)
        {
            try
            {
                Report? report =  await _context.Reports.Where(rp => rp.ReportId == reportVM.reportID && rp.ReportStatus == 0).FirstOrDefaultAsync();
                if (report != null)
                {
                    report.ReportDateUpdate = DateTime.Now;
                    report.ReportStatus = reportVM.isApproved;
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    errorMessage = "report này không tồn tại";
                    return false;
                }
            }
            catch (Exception ex) 
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public async Task<bool> DenyReport(DenyReportVM reportVM)
        {
            try
            {
                Report? report = await _context.Reports.Where(rp => rp.ReportId == reportVM.reportID && rp.ReportStatus == 0).FirstOrDefaultAsync();
                if (report != null)
                {
                    report.ReportDateUpdate = DateTime.Now;
                    report.ReportStatus = reportVM.isDeny;
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    errorMessage = "report này không tồn tại";
                    return false;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public async Task<bool> UpdateReport(UpdateReportVM reportVM)
        {
            try
            {
                Report? report = await _context.Reports.Where(rp => rp.ReportId == reportVM.reportID && rp.ReportStatus == 0).FirstOrDefaultAsync();
                if (report != null)
                {
                    report.ReportDateUpdate = DateTime.Now;
                    report.ReportContent = reportVM.content;
                    report.Image = reportVM.image;
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    errorMessage = "report này không có tồn tại";
                    return false;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public async Task<bool> DeleteReport(DeleteReport reportVM)
        {
            try
            {
                Report? report = await _context.Reports.Where(rp => rp.ReportId == reportVM.reportID && rp.ReportStatus == 0).FirstOrDefaultAsync();
                if (report != null)
                {
                    report.ReportDateUpdate = DateTime.Now;
                    report.ReportStatus = reportVM.isDelete;
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    errorMessage = "report này không tồn tại";
                    return false;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public async Task<List<ViewReport>?> GetAllReportByStatus(int status)
        {
            try
            {
                List<ViewReport> reports = new List<ViewReport>();
                reports = await _context.ViewReports.Where(rp => rp.ReportStatus == status).ToListAsync();
                return reports;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<ViewReport>?> GetAllReportByUserAndStatus(int status, int userid)
        {
            try
            {
                List<ViewReport> reports = new List<ViewReport>();
                reports = await _context.ViewReports.Where(rp => rp.ReportStatus == status && rp.UserId == userid).ToListAsync();
                return reports;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<ViewReport>?> GetAllReportByUser(int userid)
        {
            try
            {
                List<ViewReport> reports = new List<ViewReport>();
                reports = await _context.ViewReports.Where(rp => rp.UserId == userid).ToListAsync();
                return reports;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }
    }
}
