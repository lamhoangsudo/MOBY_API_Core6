﻿using Microsoft.EntityFrameworkCore;
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
                bool checkOrder = await _context.Orders.Where(or => or.OrderId == reportVM.orderID && or.Status != 3).AnyAsync();
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
                bool checkBlog = await _context.Blogs.Where(bg => bg.BlogId == reportVM.blogID && bg.BlogStatus == 1).AnyAsync();
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
                    .Join(_context.UserAccounts, rp => rp.UserId, us => us.UserId, (rp, us) => new { rp, us })
                    .Where(rpus => rpus.rp.ReportId == reportVM.ReportID
                    && rpus.rp.ReportStatus == 0
                    && rpus.us.UserStatus == true)
                    .Select(rpus => rpus.rp)
                    .FirstOrDefaultAsync();
                if (report != null)
                {
                    report.ReportDateResolve = DateTime.Now;
                    report.ReportStatus = reportVM.IsApproved;
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
                    .Join(_context.UserAccounts, rp => rp.UserId, us => us.UserId, (rp, us) => new { rp, us })
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

        public async Task<bool> HiddenOject(HiddenAndPunish hideAndPunish)
        {
            try
            {
                int id = hideAndPunish.Id;
                int tyle = hideAndPunish.Type;
                switch (tyle)
                {
                    case 0:
                        //item
                        Models.Item? item = await _context.Items.Where(it => it.ItemId == id && it.ItemStatus == true).FirstOrDefaultAsync();
                        if (item != null)
                        {
                            item.ItemStatus = false;
                            await _context.SaveChangesAsync();
                            return true;
                        }
                        else
                        {
                            ErrorMessage = "sản phẩm này đã bị xóa";
                            return false;
                        }
                    case 1:
                        //blog
                        Order? order = await _context.Orders.Where(or => or.OrderId == id && or.Status != 3).FirstOrDefaultAsync();
                        if (order != null)
                        {
                            order.Status = 3;
                            await _context.SaveChangesAsync();
                            return true;
                        }
                        else
                        {
                            ErrorMessage = "Đơn hàng này đã bị hủy";
                            return false;
                        }
                    case 2:
                        //blog
                        Comment? comment = await _context.Comments.Where(cm => cm.CommentId == id && cm.Status == true).FirstOrDefaultAsync();
                        if (comment != null)
                        {
                            comment.Status = false;
                            await _context.SaveChangesAsync();
                            return true;

                        }
                        else
                        {
                            ErrorMessage = "bình luận này đã bị xóa";
                            return false;
                        }
                    case 3:
                        //blog
                        Reply? reply = await _context.Replies.Where(rp => rp.ReplyId == id && rp.Status == true).FirstOrDefaultAsync();
                        if (reply != null)
                        {
                            reply.Status = false;
                            await _context.SaveChangesAsync();
                            return true;
                        }
                        else
                        {
                            ErrorMessage = "bình luận này đã bị xóa";
                            return false;
                        }
                    case 4:
                        //blog
                        Blog? blog = await _context.Blogs.Where(bg => bg.BlogId == id && bg.BlogStatus != 2).FirstOrDefaultAsync();
                        if (blog != null)
                        {
                            blog.BlogStatus = 2;
                            await _context.SaveChangesAsync();
                            return true;
                        }
                        else
                        {
                            ErrorMessage = "bài blog này đã bị xóa";
                            return false;
                        }
                }
                ErrorMessage = "định dạng không tồn tại";
                return false;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public async Task<bool> PunishViolators(HiddenAndPunish hideAndPunish)
        {
            try
            {
                int id = hideAndPunish.Id;
                int tyle = hideAndPunish.Type;
                switch (tyle)
                {
                    case 0:
                        //item
                        var queryItem = _context.Items
                            .Join(_context.UserAccounts, it => it.UserId, us => us.UserId, (it, us) => new { it, us })
                            .Where(itus => itus.it.ItemId == id);
                        UserAccount? userAccountItem = await queryItem
                            .Where(query => query.us.UserStatus == true)
                            .Select(query => query.us)
                            .FirstOrDefaultAsync();
                        if (userAccountItem != null)
                        {
                            userAccountItem.Reputation -= 10;
                            if (userAccountItem.Reputation < 0)
                            {
                                userAccountItem.Reputation = 0;
                                AutoDeleteAllBanUser();
                            }
                            Models.Item? item = await queryItem
                                .Where(query => query.it.ItemStatus == true)
                                .Select(query => query.it)
                                .FirstOrDefaultAsync();
                            if (item != null)
                            {
                                item.ItemStatus = false;
                            }
                            await _context.SaveChangesAsync();
                            return true;
                        }
                        else
                        {
                            ErrorMessage = "người dùng này đã bị khóa tài khoản";
                            return false;
                        }
                    case 1:
                        //order
                        var queryOrder = _context.Orders
                            .Join(_context.OrderDetails, or => or.OrderId, ord => ord.OrderId, (or, ord) => new { or, ord })
                            .Join(_context.Items, orord => orord.ord.ItemId, it => it.ItemId, (orord, it) => new { orord, it })
                            .Join(_context.UserAccounts, orordit => orordit.it.UserId, us => us.UserId, (orordit, us) => new { orordit, us })
                            .Where(ororditus => ororditus.orordit.orord.or.OrderId == id);
                        UserAccount? userAccountOrder = await queryOrder
                            .Where(queryOrder => queryOrder.us.UserStatus == true)
                            .Select(queryOrder => queryOrder.us)
                            .FirstOrDefaultAsync();
                        if (userAccountOrder != null)
                        {
                            userAccountOrder.Reputation -= 10;
                            if (userAccountOrder.Reputation < 0)
                            {
                                userAccountOrder.Reputation = 0;
                                AutoDeleteAllBanUser();
                            }
                            Order? order = await queryOrder
                                .Where(queryOrder => queryOrder.orordit.orord.or.Status != 3)
                                .Select(queryOrder => queryOrder.orordit.orord.or)
                                .FirstOrDefaultAsync();
                            if (order != null)
                            {
                                order.Status = 3;
                            }
                            await _context.SaveChangesAsync();
                            return true;
                        }
                        else
                        {
                            ErrorMessage = "Đơn hàng này đã bị hủy";
                            return false;
                        }
                    case 2:
                        //comment
                        var queryComment = _context.Comments
                            .Join(_context.UserAccounts, cm => cm.UserId, us => us.UserId, (cm, us) => new { cm, us })
                            .Where(cmus => cmus.cm.CommentId == id);
                        UserAccount? userAccountComment = await queryComment
                            .Where(queryComment => queryComment.us.UserStatus == true)
                            .Select(queryComment => queryComment.us)
                            .FirstOrDefaultAsync();
                        if (userAccountComment != null)
                        {
                            userAccountComment.Reputation -= 10;
                            if (userAccountComment.Reputation < 0)
                            {
                                userAccountComment.Reputation = 0;
                                AutoDeleteAllBanUser();
                            }
                            Comment? comment = await queryComment
                                .Where(queryComment => queryComment.cm.Status == true)
                                .Select(queryComment => queryComment.cm)
                                .FirstOrDefaultAsync();
                            if (comment != null)
                            {
                                comment.Status = false;
                            }
                            await _context.SaveChangesAsync();
                            return true;
                        }
                        else
                        {
                            ErrorMessage = "bình luận này đã bị xóa";
                            return false;
                        }
                    case 3:
                        //reply
                        var queryReply = _context.Replies
                            .Join(_context.UserAccounts, rp => rp.UserId, us => us.UserId, (rp, us) => new { rp, us })
                            .Where(rpus => rpus.rp.ReplyId == id);
                        UserAccount? userAccountReply = await queryReply
                            .Where(queryReply => queryReply.us.UserStatus == true)
                            .Select(queryReply => queryReply.us)
                            .FirstOrDefaultAsync();
                        if (userAccountReply != null)
                        {
                            userAccountReply.Reputation -= 10;
                            if (userAccountReply.Reputation < 0)
                            {
                                userAccountReply.Reputation = 0;
                                AutoDeleteAllBanUser();
                            }
                            Reply? reply = await queryReply
                                .Where(queryReply => queryReply.rp.Status == true)
                                .Select(queryReply => queryReply.rp)
                                .FirstOrDefaultAsync();
                            if (reply != null)
                            {
                                reply.Status = false;
                            }
                            await _context.SaveChangesAsync();
                            return true;
                        }
                        else
                        {
                            ErrorMessage = "bình luận này đã bị xóa";
                            return false;
                        }
                    case 4:
                        //blog
                        var queryBlog = _context.Blogs
                            .Join(_context.UserAccounts, blg => blg.UserId, us => us.UserId, (blg, us) => new { blg, us })
                            .Where(blgus => blgus.blg.BlogId == id);
                        UserAccount? userAccountBlog = await queryBlog
                            .Where(queryBlog => queryBlog.us.UserStatus == true)
                            .Select(queryBlog => queryBlog.us)
                            .FirstOrDefaultAsync();
                        if (userAccountBlog != null)
                        {
                            userAccountBlog.Reputation -= 10;
                            if (userAccountBlog.Reputation < 0)
                            {
                                userAccountBlog.Reputation = 0;
                                AutoDeleteAllBanUser();
                            }
                            Blog? blog = await queryBlog
                                .Where(queryBlog => queryBlog.blg.BlogStatus != 2)
                                .Select(queryBlog => queryBlog.blg)
                                .FirstOrDefaultAsync();
                            if (blog != null)
                            {
                                blog.BlogStatus = 2;
                            }
                            await _context.SaveChangesAsync();
                            return true;
                        }
                        else
                        {
                            ErrorMessage = "bài blog này đã bị xóa";
                            return false;
                        }
                }
                ErrorMessage = "định dạng không tồn tại";
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
