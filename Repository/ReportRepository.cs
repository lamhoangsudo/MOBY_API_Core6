using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly MOBYContext _context;

        public static string? ErrorMessage { get; set; }

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
                    Report report = new()
                    {
                        ReportDateCreate = dateTimeCreate,
                        ItemId = reportVM.itemID,
                        UserId = reportVM.userID,
#pragma warning disable CS8601 // Possible null reference assignment.
                        Title = reportVM.title,
#pragma warning restore CS8601 // Possible null reference assignment.
                        ReportStatus = reportVM.status,
#pragma warning disable CS8601 // Possible null reference assignment.
                        ReportContent = reportVM.content,
                        Evident = reportVM.image
                    };
#pragma warning restore CS8601 // Possible null reference assignment.
                    await _context.Reports.AddAsync(report);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    ErrorMessage = "sản phẩm này không có tồn tại";
                    return false;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
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
                    Report report = new()
                    {
                        ReportDateCreate = dateTimeCreate,
                        ItemId = reportVM.itemID,
                        UserId = reportVM.userID,
#pragma warning disable CS8601 // Possible null reference assignment.
                        Title = reportVM.title,
#pragma warning restore CS8601 // Possible null reference assignment.
                        ReportStatus = reportVM.status,
#pragma warning disable CS8601 // Possible null reference assignment.
                        ReportContent = reportVM.content,
                        Evident = reportVM.image
                    };
#pragma warning restore CS8601 // Possible null reference assignment.
                    await _context.Reports.AddAsync(report);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    ErrorMessage = "sản phẩm này không có tồn tại";
                    return false;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
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
                    Report report = new()
                    {
                        ReportDateCreate = dateTimeCreate,
                        CommentId = reportVM.commentID,
                        UserId = reportVM.userID,
#pragma warning disable CS8601 // Possible null reference assignment.
                        Title = reportVM.title,
#pragma warning restore CS8601 // Possible null reference assignment.
                        ReportStatus = reportVM.status,
#pragma warning disable CS8601 // Possible null reference assignment.
                        ReportContent = reportVM.content,
                        Evident = reportVM.image
                    };
#pragma warning restore CS8601 // Possible null reference assignment.
                    await _context.Reports.AddAsync(report);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    ErrorMessage = "sản phẩm này không có tồn tại";
                    return false;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
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
                    Report report = new()
                    {
                        ReportDateCreate = dateTimeCreate,
                        ReplyId = reportVM.replyID,
                        UserId = reportVM.userID,
#pragma warning disable CS8601 // Possible null reference assignment.
                        Title = reportVM.title,
#pragma warning restore CS8601 // Possible null reference assignment.
                        ReportStatus = reportVM.status,
#pragma warning disable CS8601 // Possible null reference assignment.
                        ReportContent = reportVM.content,
                        Evident = reportVM.image
                    };
#pragma warning restore CS8601 // Possible null reference assignment.
                    await _context.Reports.AddAsync(report);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    ErrorMessage = "sản phẩm này không có tồn tại";
                    return false;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
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
                    Report report = new()
                    {
                        ReportDateCreate = dateTimeCreate,
                        BlogId = reportVM.blogID,
                        UserId = reportVM.userID,
#pragma warning disable CS8601 // Possible null reference assignment.
                        Title = reportVM.title,
#pragma warning restore CS8601 // Possible null reference assignment.
                        ReportStatus = reportVM.status,
#pragma warning disable CS8601 // Possible null reference assignment.
                        ReportContent = reportVM.content,
                        Evident = reportVM.image
                    };
#pragma warning restore CS8601 // Possible null reference assignment.
                    await _context.Reports.AddAsync(report);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    ErrorMessage = "sản phẩm này không có tồn tại";
                    return false;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public async Task<bool> ApprovedReport(ApprovedReportVM reportVM)
        {
            try
            {
                Report? report = await _context.Reports
                    .Join(_context.UserAccounts, rp => rp.UserId, us => us.UserId, (rp, us) => new { rp , us })
                    .Where(rpus => rpus.rp.ReportId == reportVM.ReportID 
                    && rpus.rp.ReportStatus == 0
                    && rpus.us.UserStatus == true)
                    .Select(rpus => rpus.rp)
                    .FirstOrDefaultAsync();
                if (report != null)
                {
                    report.ReportDateResolve = DateTime.Now;
                    report.ReportStatus = reportVM.IsApproved;
                    var query = _context.UserAccounts;
                    if (report.ItemId != null)
                    {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                        UserAccount userAccount = await query
                            .Join(_context.Items, ua => ua.UserId,
                            it => it.UserId,
                            (ua, it) => new { ua, it })
                            .Where(uait => uait.it.ItemId == report.ItemId
                            && uait.ua.UserStatus == true
                            && uait.ua.Reputation != 0)
                            .Select(uait => uait.ua)
                            .FirstOrDefaultAsync();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable ty
                        if (userAccount != null)
                        {
                            userAccount.Reputation -= 10;
                            if (userAccount.Reputation < 0)
                            {
                                userAccount.Reputation = 0;
                                AutoDeleteAllBanUser();
                            }
                        }
                        Models.Item? item = await _context.Items.Where(it => it.ItemId == report.ItemId).FirstOrDefaultAsync();
                        if (item != null && item.ItemStatus == true)
                        {
                            item.ItemStatus = false;
                        }
                        else
                        {
                            ErrorMessage = "sản phẩm này đã bị xóa";
                        }
                    }
                    else if (report.OrderId != null)
                    {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                        UserAccount userAccount = await query
                            .Join(_context.Items, ua => ua.UserId,
                            it => it.UserId,
                            (ua, it) => new { ua, it })
                            .Join(_context.OrderDetails,
                            uait => uait.it.ItemId,
                            ord => ord.ItemId,
                            (uait, ord) => new { uait, ord })
                            .Join(_context.Orders,
                            uaitord => uaitord.ord.OrderId,
                            or => or.OrderId,
                            (uaitord, or) => new { uaitord, or })
                            .Where(uaitordor => uaitordor.or.OrderId == report.OrderId
                            && uaitordor.or.Status != 0
                            && uaitordor.uaitord.uait.ua.UserStatus == true
                            && uaitordor.uaitord.uait.ua.Reputation != 0)
                            .Select(uaitordor => uaitordor.uaitord.uait.ua)
                            .FirstOrDefaultAsync();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                        if (userAccount != null)
                        {
                            userAccount.Reputation -= 10;
                            if (userAccount.Reputation < 0)
                            {
                                userAccount.Reputation = 0;
                                AutoDeleteAllBanUser();
                            }
                        }
                        Order? order = await _context.Orders.Where(or => or.OrderId == report.OrderId).FirstOrDefaultAsync();
                        if (order != null && order.Status != 3)
                        {
                            order.Status = 3;
                        }
                        else
                        {
                            ErrorMessage = "Đơn hàng này đã bị hủy";
                        }
                    }
                    else if (report.BlogId != null)
                    {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                        UserAccount userAccount = await query
                            .Join(_context.Blogs, ua => ua.UserId, bg => bg.UserId, (ua, bg) => new { ua, bg })
                            .Where(uabg => uabg.bg.BlogId == report.BlogId
                            && uabg.ua.UserStatus == true
                            && uabg.ua.Reputation != 0)
                            .Select(uabg => uabg.ua)
                            .FirstOrDefaultAsync();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                        if (userAccount != null)
                        {
                            userAccount.Reputation -= 10;
                            if (userAccount.Reputation < 0)
                            {
                                userAccount.Reputation = 0;
                                AutoDeleteAllBanUser();
                            }
                        }
                        Blog? blog = await _context.Blogs.Where(bg => bg.BlogId == report.BlogId).FirstOrDefaultAsync();
                        if (blog != null && blog.BlogStatus != 2)
                        {
                            blog.BlogStatus = 2;
                        }
                        else
                        {
                            ErrorMessage = "bài blog này đã bị xóa";
                        }
                    }
                    else if (report.CommentId != null)
                    {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                        UserAccount userAccount = await query
                            .Join(_context.Comments,
                            ua => ua.UserId,
                            cm => cm.UserId,
                            (ua, cm) => new { ua, cm })
                            .Where(uacm => uacm.cm.CommentId == report.CommentId
                            && uacm.ua.UserStatus == true
                            && uacm.ua.Reputation != 0)
                            .Select(uacm => uacm.ua)
                            .FirstOrDefaultAsync();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                        if (userAccount != null)
                        {
                            userAccount.Reputation -= 10;
                            if (userAccount.Reputation < 0)
                            {
                                userAccount.Reputation = 0;
                                AutoDeleteAllBanUser();
                            }
                        }
                        Comment? comment = await _context.Comments.Where(cm => cm.CommentId == report.CommentId).FirstOrDefaultAsync();
                        if (comment != null && comment.Status == true)
                        {
                            comment.Status = false;
                        }
                        else
                        {
                            ErrorMessage = "bình luận này đã bị xóa";
                        }
                    }
                    else if (report.ReplyId != null)
                    {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                        UserAccount userAccount = await query
                            .Join(_context.Replies,
                            ua => ua.UserId,
                            rp => rp.UserId,
                            (ua, rp) => new { ua, rp })
                            .Where(uarp => uarp.rp.ReplyId == report.ReportId
                            && uarp.ua.UserStatus == true
                            && uarp.ua.Reputation != 0)
                            .Select(uarp => uarp.ua)
                            .FirstOrDefaultAsync();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                        if (userAccount != null)
                        {
                            userAccount.Reputation -= 10;
                            if (userAccount.Reputation < 0)
                            {
                                userAccount.Reputation = 0;
                                AutoDeleteAllBanUser();
                            }
                        }
                        Reply? reply = await _context.Replies.Where(rp => rp.ReplyId == report.ReportId).FirstOrDefaultAsync();
                        if (reply != null && reply.Status == true)
                        {
                            reply.Status = false;
                        }
                        else
                        {
                            ErrorMessage = "bình luận này đã bị xóa";
                        }
                    }
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    ErrorMessage = "report này không tồn tại";
                    return false;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public async Task<bool> DenyReport(DenyReportVM reportVM)
        {
            try
            {
                Report? report = await _context.Reports
                    .Join(_context.UserAccounts, rp => rp.UserId, us => us.UserId, (rp, us) => new { rp, us })
                    .Where(rpus => rpus.rp.ReportId == reportVM.reportID 
                    && rpus.rp.ReportStatus == 0
                    && rpus.us.UserStatus == true)
                    .Select(rpus => rpus.rp)
                    .FirstOrDefaultAsync();
                if (report != null)
                {
                    report.ReportDateResolve = DateTime.Now;
                    report.ReportStatus = reportVM.isDeny;
                    report.ReasonDeny = reportVM.reason;
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    ErrorMessage = "report này không tồn tại";
                    return false;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
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
#pragma warning disable CS8601 // Possible null reference assignment.
                    report.ReportContent = reportVM.content;
                    report.Title = reportVM.title;
                    report.Evident = reportVM.image;
#pragma warning restore CS8601 // Possible null reference assignment.
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    ErrorMessage = "report này không có tồn tại";
                    return false;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public async Task<bool> DeleteReport(DeleteReportVM reportVM)
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
                    ErrorMessage = "report này không tồn tại";
                    return false;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public async Task<ListVM<ViewReport>?> GetReports(DynamicFilterReportVM dynamicFilterReportVM)
        {
            try
            {
                AutoDeleteAllBanUser();
                int itemsToSkip = (dynamicFilterReportVM.PageNumber - 1) * dynamicFilterReportVM.PageSize;
                List<ViewReport> reports = new();
                var query = _context.ViewReports
                    .Join(_context.UserAccounts, vr => vr.UserId, us => us.UserId, (vr, us) => new { vr, us });
                if (dynamicFilterReportVM.UserID != null)
                {
                    query = query.Where(query => query.vr.UserId == dynamicFilterReportVM.UserID && query.us.UserStatus == true);
                }
                if (dynamicFilterReportVM.IsItem == true)
                {
                    if (dynamicFilterReportVM.ItemID != null)
                    {
                        query = query.Where(query => query.vr.ItemId == dynamicFilterReportVM.ItemID);
                    }
                    else
                    {
                        query = query.Where(query => query.vr.ItemId != null);
                    }
                }
                if (dynamicFilterReportVM.IsOrder == true)
                {
                    if (dynamicFilterReportVM.OrderID != null)
                    {
                        query = query.Where(query => query.vr.OrderId == dynamicFilterReportVM.OrderID);
                    }
                    else
                    {
                        query = query.Where(query => query.vr.OrderId != null);
                    }
                }
                if (dynamicFilterReportVM.IsComment == true)
                {
                    if (dynamicFilterReportVM.CommentId != null)
                    {
                        query = query.Where(query => query.vr.CommentId == dynamicFilterReportVM.CommentId);
                    }
                    else
                    {
                        query = query.Where(query => query.vr.CommentId != null);
                    }
                }
                if (dynamicFilterReportVM.IsReply == true)
                {
                    if (dynamicFilterReportVM.ReplyId != null)
                    {
                        query = query.Where(query => query.vr.ReplyId == dynamicFilterReportVM.ReplyId);
                    }
                    else
                    {
                        query = query.Where(query => query.vr.ReplyId != null);
                    }
                }
                if (dynamicFilterReportVM.IsBlog == true)
                {
                    if (dynamicFilterReportVM.BlogId != null)
                    {
                        query = query.Where(query => query.vr.BlogId == dynamicFilterReportVM.BlogId);
                    }
                    else
                    {
                        query = query.Where(query => query.vr.BlogId != null);
                    }
                }
                if (dynamicFilterReportVM.Status != null)
                {
                    query = query.Where(query => query.vr.ReportStatus == dynamicFilterReportVM.Status);
                }
                if (dynamicFilterReportVM.Title != null)
                {
                    query = query.Where(query => query.vr.Title.Equals(dynamicFilterReportVM.Title));
                }
                if (dynamicFilterReportVM.OrderByDateCreate == true && dynamicFilterReportVM.OrderByDateResolve == false)
                {
                    if (dynamicFilterReportVM.AscendingOrDescending == true)
                    {
                        query = query.OrderBy(query => query.vr.ReportDateCreate);
                    }
                    else
                    {
                        query = query.OrderByDescending(query => query.vr.ReportDateCreate);
                    }
                }
                if (dynamicFilterReportVM.OrderByDateCreate == false && dynamicFilterReportVM.OrderByDateResolve == true)
                {
                    if (dynamicFilterReportVM.AscendingOrDescending == true)
                    {
                        query = query.OrderBy(query => query.vr.ReportDateResolve);
                    }
                    else
                    {
                        query = query.OrderByDescending(query => query.vr.ReportDateResolve);
                    }
                }
                if (dynamicFilterReportVM.MinDateCreate <= dynamicFilterReportVM.MaxDateCreate)
                {
                    query = query.Where(query => query.vr.ReportDateCreate >= dynamicFilterReportVM.MinDateCreate && query.vr.ReportDateCreate <= dynamicFilterReportVM.MaxDateCreate);
                }
                else if (dynamicFilterReportVM.MinDateResolve <= dynamicFilterReportVM.MaxDateResolve)
                {
                    query = query.Where(query => query.vr.ReportDateResolve >= dynamicFilterReportVM.MinDateResolve && query.vr.ReportDateResolve <= dynamicFilterReportVM.MaxDateResolve);
                }
                int total = query.Count();
                int totalPage = total / dynamicFilterReportVM.PageSize;
                if (total % dynamicFilterReportVM.PageSize != 0)
                {
                    ++totalPage;
                }
                reports = await query
                    .Select(vrus => vrus.vr)
                    .Skip(itemsToSkip)
                    .Take(dynamicFilterReportVM.PageSize)
                    .ToListAsync();
                ListVM<ViewReport> ViewReport = new(total, totalPage, reports);
                return ViewReport;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        public async Task<ViewReportItem?> ItemReportDetail(int reportID)
        {
            try
            {
                ViewReportItem? itemReport = await _context.ViewReportItems.Where(vri => vri.ReportId == reportID).FirstOrDefaultAsync();
                if (itemReport == null)
                {
                    ErrorMessage = "report này không còn tồn tại trong dữ liệu";
                }
                return itemReport;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        public async Task<ViewReportBlog?> BlogReportDetail(int reportID)
        {
            try
            {
                ViewReportBlog? itemReport = await _context.ViewReportBlogs.Where(vri => vri.ReportId == reportID).FirstOrDefaultAsync();
                if (itemReport == null)
                {
                    ErrorMessage = "report này không còn tồn tại trong dữ liệu";
                }
                return itemReport;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        public async Task<ViewReportComment?> CommentReportDetail(int reportID)
        {
            try
            {
                ViewReportComment? itemReport = await _context.ViewReportComments.Where(vri => vri.ReportId == reportID).FirstOrDefaultAsync();
                if (itemReport == null)
                {
                    ErrorMessage = "report này không còn tồn tại trong dữ liệu";
                }
                return itemReport;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        public async Task<ViewReportReply?> ReplyReportDetail(int reportID)
        {
            try
            {
                ViewReportReply? itemReport = await _context.ViewReportReplies.Where(vri => vri.ReportId == reportID).FirstOrDefaultAsync();
                if (itemReport == null)
                {
                    ErrorMessage = "report này không còn tồn tại trong dữ liệu";
                }
                return itemReport;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        public async Task<ViewReportOrder?> OrderReportDetail(int reportID)
        {
            try
            {
                ViewReportOrder? itemReport = await _context.ViewReportOrders.Where(vri => vri.ReportId == reportID).FirstOrDefaultAsync();
                if (itemReport == null)
                {
                    ErrorMessage = "report này không còn tồn tại trong dữ liệu";
                }
                return itemReport;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        public void AutoDeleteAllBanUser()
        {
            try
            {
                List<Report>? reports = _context.Reports
                    .Join(_context.UserAccounts, rp => rp.UserId, us => us.UserId, ( rp,us ) => new { rp,us })
                    .Where(rpus => rpus.us.UserStatus == false && rpus.rp.ReportStatus == 0)
                    .Select(rpus => rpus.rp)
                    .ToList();
                if (reports.Any() && reports != null)
                {
                    foreach (Report report in reports)
                    {
                        report.ReportStatus = 3;
                    }
                    _context.SaveChanges();
                }
                else 
                {
                    ErrorMessage = "không có dữ liệu";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public async Task<bool> HideOject(int id, int tyle)
        {
            try
            {
                return false;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }
    }
}
