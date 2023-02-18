using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public class ReportRepository : IReportRepository
    {
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
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
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
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
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
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
