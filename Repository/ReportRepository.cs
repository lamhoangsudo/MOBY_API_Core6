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
        public async Task<bool> CreateItemReport(CreateReportVM reportVM)
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
                    report.ReportContent = reportVM.content;
                    report.Evident = reportVM.image;
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

        public async Task<bool> CreateOrderReport(CreateReportVM reportVM)
        {
            try
            {
                bool checkOrder = await _context.Orders.Where(or => or.OrderId == reportVM.orderID && or.Status != 0).AnyAsync();
                bool checkUser = await _context.UserAccounts.Where(us => us.UserId == reportVM.userID && us.UserStatus == true).AnyAsync();
                if (checkOrder == true && checkUser == true)
                {
                    DateTime dateTimeCreate = DateTime.Now;
                    Report report = new Report();
                    report.ReportDateCreate = dateTimeCreate;
                    report.ItemId = reportVM.itemID;
                    report.UserId = reportVM.userID;
                    report.ReportStatus = reportVM.status;
                    report.ReportContent = reportVM.content;
#pragma warning disable CS8601 // Possible null reference assignment.
                    report.Evident = reportVM.image;
#pragma warning restore CS8601 // Possible null reference assignment.
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

        public async Task<bool> CreateCommentReport(CreateReportVM reportVM)
        {
            try
            {
                bool checkComment = await _context.Comments.Where(cm => cm.CommentId == reportVM.commentID).AnyAsync();
                bool checkUser = await _context.UserAccounts.Where(us => us.UserId == reportVM.userID && us.UserStatus == true).AnyAsync();
                if (checkComment == true && checkUser == true)
                {
                    DateTime dateTimeCreate = DateTime.Now;
                    Report report = new Report();
                    report.ReportDateCreate = dateTimeCreate;
                    report.CommentId = reportVM.commentID;
                    report.UserId = reportVM.userID;
                    report.ReportStatus = reportVM.status;
                    report.ReportContent = reportVM.content;
#pragma warning disable CS8601 // Possible null reference assignment.
                    report.Evident = reportVM.image;
#pragma warning restore CS8601 // Possible null reference assignment.
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

        public async Task<bool> CreateReplyReport(CreateReportVM reportVM)
        {
            try
            {
                bool checkReply = await _context.Replies.Where(rp => rp.ReplyId == reportVM.replyID).AnyAsync();
                bool checkUser = await _context.UserAccounts.Where(us => us.UserId == reportVM.userID && us.UserStatus == true).AnyAsync();
                if (checkReply == true && checkUser == true)
                {
                    DateTime dateTimeCreate = DateTime.Now;
                    Report report = new Report();
                    report.ReportDateCreate = dateTimeCreate;
                    report.ReplyId = reportVM.replyID;
                    report.UserId = reportVM.userID;
                    report.ReportStatus = reportVM.status;
                    report.ReportContent = reportVM.content;
#pragma warning disable CS8601 // Possible null reference assignment.
                    report.Evident = reportVM.image;
#pragma warning restore CS8601 // Possible null reference assignment.
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

        public async Task<bool> CreateBlogReport(CreateReportVM reportVM)
        {
            try
            {
                bool checkBlog = await _context.Blogs.Where(bg => bg.BlogId == reportVM.blogID && bg.BlogStatus == 0).AnyAsync();
                bool checkUser = await _context.UserAccounts.Where(us => us.UserId == reportVM.userID && us.UserStatus == true).AnyAsync();
                if (checkBlog == true && checkUser == true)
                {
                    DateTime dateTimeCreate = DateTime.Now;
                    Report report = new Report();
                    report.ReportDateCreate = dateTimeCreate;
                    report.BlogId = reportVM.blogID;
                    report.UserId = reportVM.userID;
                    report.ReportStatus = reportVM.status;
                    report.ReportContent = reportVM.content;
#pragma warning disable CS8601 // Possible null reference assignment.
                    report.Evident = reportVM.image;
#pragma warning restore CS8601 // Possible null reference assignment.
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
                Report? report = await _context.Reports.Where(rp => rp.ReportId == reportVM.reportID && rp.ReportStatus == 0).FirstOrDefaultAsync();
                if (report != null)
                {
                    report.ReportDateResolve = DateTime.Now;
                    report.ReportStatus = reportVM.isApproved;
                    var query = _context.UserAccounts;
                    if (report.ItemId != null)
                    {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                        UserAccount userAccount = await query
                            .Join(_context.Items, ua => ua.UserId, it => it.UserId, (ua, it) => new { ua, it })
                            .Where(uait => uait.it.ItemId == report.ItemId && uait.ua.UserStatus == true && uait.ua.Reputation != 0)
                            .Select(uait => uait.ua)
                            .FirstOrDefaultAsync();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable ty
                        if (userAccount != null)
                        {
                            userAccount.Reputation = userAccount.Reputation - 10;
                            if (userAccount.Reputation < 0)
                            {
                                userAccount.Reputation = 0;
                            }
                        }
                    }
                    else if (report.OrderId != null)
                    {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                        UserAccount userAccount = await query
                            .Join(_context.Items, ua => ua.UserId, it => it.UserId, (ua, it) => new { ua, it })
                            .Join(_context.OrderDetails, uait => uait.it.ItemId, ord => ord.ItemId, (uait, ord) => new { uait, ord })
                            .Join(_context.Orders, uaitord => uaitord.ord.OrderId, or => or.OrderId, (uaitord, or) => new { uaitord, or })
                            .Where(uaitordor => uaitordor.or.OrderId == report.OrderId && uaitordor.or.Status != 0 && uaitordor.uaitord.uait.ua.UserStatus == true && uaitordor.uaitord.uait.ua.Reputation != 0)
                            .Select(uaitordor => uaitordor.uaitord.uait.ua)
                            .FirstOrDefaultAsync();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                        if (userAccount != null)
                        {
                            userAccount.Reputation = userAccount.Reputation - 10;
                            if (userAccount.Reputation < 0)
                            {
                                userAccount.Reputation = 0;
                            }
                        }
                    }
                    else if (report.BlogId != null)
                    {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                        UserAccount userAccount = await query
                            .Join(_context.Blogs, ua => ua.UserId, bg => bg.UserId, (ua, bg) => new { ua, bg })
                            .Where(uabg => uabg.bg.BlogId == report.BlogId && uabg.ua.UserStatus == true && uabg.ua.Reputation != 0)
                            .Select(uabg => uabg.ua)
                            .FirstOrDefaultAsync();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                        if (userAccount != null)
                        {
                            userAccount.Reputation = userAccount.Reputation - 10;
                            if (userAccount.Reputation < 0)
                            {
                                userAccount.Reputation = 0;
                            }
                        }
                    }
                    else if (report.CommentId != null)
                    {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                        UserAccount userAccount = await query
                            .Join(_context.Comments, ua => ua.UserId, cm => cm.UserId, (ua, cm) => new { ua, cm })
                            .Where(uacm => uacm.cm.CommentId == report.CommentId && uacm.ua.UserStatus == true && uacm.ua.Reputation != 0)
                            .Select(uacm => uacm.ua)
                            .FirstOrDefaultAsync();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                        if (userAccount != null)
                        {
                            userAccount.Reputation = userAccount.Reputation - 10;
                            if (userAccount.Reputation < 0)
                            {
                                userAccount.Reputation = 0;
                            }
                        }
                    }
                    else if (report.ReplyId != null)
                    {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                        UserAccount userAccount = await query
                            .Join(_context.Replies, ua => ua.UserId, rp => rp.UserId, (ua, rp) => new { ua, rp })
                            .Where(uarp => uarp.rp.ReplyId == report.ReportId && uarp.ua.UserStatus == true && uarp.ua.Reputation != 0)
                            .Select(uarp => uarp.ua)
                            .FirstOrDefaultAsync();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                        if (userAccount != null)
                        {
                            userAccount.Reputation = userAccount.Reputation - 10;
                            if (userAccount.Reputation < 0)
                            {
                                userAccount.Reputation = 0;
                            }
                        }
                    }
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
                    report.ReportDateResolve = DateTime.Now;
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
                    report.ReportDateResolve = DateTime.Now;
                    report.ReportContent = reportVM.content;
#pragma warning disable CS8601 // Possible null reference assignment.
                    report.Evident = reportVM.image;
#pragma warning restore CS8601 // Possible null reference assignment.
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
                    report.ReportDateResolve = DateTime.Now;
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



        /*public async Task<List<ViewReport>?> GetAllReportByStatus(int status)
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
        }*/
    }
}
