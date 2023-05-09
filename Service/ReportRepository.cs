using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;
using Org.BouncyCastle.Bcpg.Sig;

namespace MOBY_API_Core6.Service
{
    public class ReportRepository
    {
        private readonly MOBYContext _context;
        private readonly IEmailRepository _emailRepository;
        private readonly IRecordPenaltyRepository _recordPenaltyRepository;

        public ReportRepository(MOBYContext context, IEmailRepository emailRepository, IRecordPenaltyRepository recordPenaltyRepository)
        {
            _context = context;
            _emailRepository = emailRepository;
            _recordPenaltyRepository = recordPenaltyRepository;
        }
        public async void AutoDeleteAllBanUser()
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
                await _context.SaveChangesAsync();
            }
            throw new KeyNotFoundException();
        }
        public async Task<int> CreateItemReport(CreateReportVM reportVM)
        {
            bool checkItem = await _context.Items.Where(it => it.ItemId == reportVM.ItemID && it.ItemStatus == true).AnyAsync();
            bool checkUser = await _context.UserAccounts.Where(us => us.UserId == reportVM.UserID && us.UserStatus == true).AnyAsync();
            if (checkItem == true && checkUser == true)
            {
                DateTime dateTimeCreate = DateTime.Now;
                Report report = new()
                {
                    ReportDateCreate = dateTimeCreate,
                    ItemId = reportVM.ItemID,
                    UserId = reportVM.UserID,
                    Title = reportVM.Title,
                    ReportStatus = reportVM.Status,
                    ReportContent = reportVM.Content,
                    Evident = reportVM.Image
                };
                await _context.Reports.AddAsync(report);
                return await _context.SaveChangesAsync();
            }
            throw new KeyNotFoundException();
        }
        public async Task<int> CreateOrderReport(CreateReportVM reportVM)
        {
            bool checkOrder = await _context.Orders.Where(or => or.OrderId == reportVM.OrderID && or.Status != 3).AnyAsync();
            bool checkUser = await _context.UserAccounts.Where(us => us.UserId == reportVM.UserID && us.UserStatus == true).AnyAsync();
            if (checkOrder == true && checkUser == true)
            {
                DateTime dateTimeCreate = DateTime.Now;
                Report report = new()
                {
                    ReportDateCreate = dateTimeCreate,
                    ItemId = reportVM.ItemID,
                    UserId = reportVM.UserID,
                    Title = reportVM.Title,

                    ReportStatus = reportVM.Status,
                    ReportContent = reportVM.Content,
                    Evident = reportVM.Image
                };
                await _context.Reports.AddAsync(report);
                return await _context.SaveChangesAsync();
            }
            throw new KeyNotFoundException();
        }
        public async Task<int> CreateCommentReport(CreateReportVM reportVM)
        {
            bool checkComment = await _context.Comments.Where(cm => cm.CommentId == reportVM.CommentID).AnyAsync();
            bool checkUser = await _context.UserAccounts.Where(us => us.UserId == reportVM.UserID && us.UserStatus == true).AnyAsync();
            if (checkComment == true && checkUser == true)
            {
                DateTime dateTimeCreate = DateTime.Now;
                Report report = new()
                {
                    ReportDateCreate = dateTimeCreate,
                    CommentId = reportVM.CommentID,
                    UserId = reportVM.UserID,
                    Title = reportVM.Title,
                    ReportStatus = reportVM.Status,
                    ReportContent = reportVM.Content,
                    Evident = reportVM.Image
                };
                await _context.Reports.AddAsync(report);
                return await _context.SaveChangesAsync();
            }
            throw new KeyNotFoundException();
        }
        public async Task<int> CreateReplyReport(CreateReportVM reportVM)
        {
            bool checkReply = await _context.Replies.Where(rp => rp.ReplyId == reportVM.ReplyID).AnyAsync();
            bool checkUser = await _context.UserAccounts.Where(us => us.UserId == reportVM.UserID && us.UserStatus == true).AnyAsync();
            if (checkReply == true && checkUser == true)
            {
                DateTime dateTimeCreate = DateTime.Now;
                Report report = new()
                {
                    ReportDateCreate = dateTimeCreate,
                    ReplyId = reportVM.ReplyID,
                    UserId = reportVM.UserID,
                    Title = reportVM.Title,
                    ReportStatus = reportVM.Status,
                    ReportContent = reportVM.Content,
                    Evident = reportVM.Image
                };
                await _context.Reports.AddAsync(report);
                return await _context.SaveChangesAsync();
            }
            throw new KeyNotFoundException();
        }
        public async Task<int> CreateBlogReport(CreateReportVM reportVM)
        {
            bool checkBlog = await _context.Blogs.Where(bg => bg.BlogId == reportVM.BlogID && bg.BlogStatus == 1).AnyAsync();
            bool checkUser = await _context.UserAccounts.Where(us => us.UserId == reportVM.UserID && us.UserStatus == true).AnyAsync();
            if (checkBlog == true && checkUser == true)
            {
                DateTime dateTimeCreate = DateTime.Now;
                Report report = new()
                {
                    ReportDateCreate = dateTimeCreate,
                    BlogId = reportVM.BlogID,
                    UserId = reportVM.UserID,
                    Title = reportVM.Title,
                    ReportStatus = reportVM.Status,
                    ReportContent = reportVM.Content,
                    Evident = reportVM.Image
                };
                await _context.Reports.AddAsync(report);
                return await _context.SaveChangesAsync();
            }
            throw new KeyNotFoundException();
        }
        public async Task<int> ApprovedReport(ApprovedReportVM reportVM)
        {

            var query = _context.Reports
                .Join(_context.UserAccounts, rp => rp.UserId, us => us.UserId, (rp, us) => new { rp, us })
                .Where(rpus => rpus.rp.ReportId == reportVM.ReportID
                && rpus.rp.ReportStatus == 0
                && rpus.us.UserStatus == true);
            Report? report = await query
                .Select(rpus => rpus.rp)
                .FirstOrDefaultAsync();
            if (report != null)
            {
                int? type = null;
                if (report.ItemId != null)
                {
                    type = 0;
                }
                if (report.OrderId != null)
                {
                    type = 1;
                }
                if (report.CommentId != null)
                {
                    type = 2;
                }
                if (report.ReplyId != null)
                {
                    type = 3;
                }
                if (report.BlogId != null)
                {
                    type = 4;
                }
                string? username = await query
                    .Select(rpus => rpus.us.UserName)
                    .FirstOrDefaultAsync();
                string? gmail = await query
                .Select(rpus => rpus.us.UserGmail)
                .FirstOrDefaultAsync();
                if (type != null)
                {
                    Email email = new()
                    {
                        To = gmail,
                        Subject = "your report has been accepted",
                        Link = "Dear " + username + "/n" + "your report: https://moby-customer.vercel.app/Report/GetDetailReport?report=" + report.ReportId + "&type=" + type + "has been accepted by admintrator"
                    };
                    await _emailRepository.SendEmai(email);
                }
                report.ReportDateResolve = DateTime.Now;
                report.ReportStatus = 1;
                return await _context.SaveChangesAsync();
            }
            throw new KeyNotFoundException();
        }
        public async Task<int> DenyReport(DenyReportVM reportVM)
        {
            var query = _context.Reports
                .Join(_context.UserAccounts, rp => rp.UserId, us => us.UserId, (rp, us) => new { rp, us })
                .Where(rpus => rpus.rp.ReportId == reportVM.ReportID
                && rpus.rp.ReportStatus == 0
                && rpus.us.UserStatus == true);
            Report? report = await query
                .Select(rpus => rpus.rp)
                .FirstOrDefaultAsync();
            if (report != null)
            {
                int? type = null;
                if (report.Item != null)
                {
                    type = 0;
                }
                if (report.OrderId != null)
                {
                    type = 1;
                }
                if (report.CommentId != null)
                {
                    type = 2;
                }
                if (report.ReplyId != null)
                {
                    type = 3;
                }
                if (report.BlogId != null)
                {
                    type = 4;
                }
                string? username = await query
                    .Select(rpus => rpus.us.UserName)
                    .FirstOrDefaultAsync();
                string? gmail = await query
                .Select(rpus => rpus.us.UserGmail)
                .FirstOrDefaultAsync();
                if (type != null)
                {
                    Email email = new()
                    {
                        To = gmail,
                        Subject = "your report has been deny",
                        Link = "Dear " + username + "/n" + "your report: https://moby-customer.vercel.app/Report/GetDetailReport?report=" + report.ReportId + "&type=" + type + "has been rejected by admintrator"
                    };
                    await _emailRepository.SendEmai(email);
                }
                report.ReportDateResolve = DateTime.Now;
                report.ReportStatus = 2;
                report.ReasonDeny = reportVM.Reason;
                return await _context.SaveChangesAsync();
            }
            throw new KeyNotFoundException();
        }
        public async Task<int> UpdateReport(UpdateReportVM reportVM)
        {
            Report? report = await _context.Reports.Where(rp => rp.ReportId == reportVM.ReportID && rp.ReportStatus == 0).FirstOrDefaultAsync();
            if (report != null)
            {
                report.ReportDateResolve = DateTime.Now;
                report.ReportContent = reportVM.Content;
                report.Title = reportVM.Title;
                report.Evident = reportVM.Image;
                return await _context.SaveChangesAsync();
            }
            throw new KeyNotFoundException();
        }
        public async Task<bool> DeleteReport(DeleteReportVM reportVM)
        {
            Report? report = await _context.Reports.Where(rp => rp.ReportId == reportVM.ReportID && rp.ReportStatus == 0).FirstOrDefaultAsync();
            if (report != null)
            {
                report.ReportDateResolve = DateTime.Now;
                report.ReportStatus = reportVM.IsDelete;
                await _context.SaveChangesAsync();
                return true;
            }
            throw new KeyNotFoundException();
        }
        public async Task<List<ViewReport>?> GetReports(DynamicFilterReportVM dynamicFilterReportVM)
        {
            AutoDeleteAllBanUser();
            int itemsToSkip = (dynamicFilterReportVM.PageNumber - 1) * dynamicFilterReportVM.PageSize;
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
            return await query
                .Select(vrus => vrus.vr)
                .Skip(itemsToSkip)
                .Take(dynamicFilterReportVM.PageSize)
                .ToListAsync();
        }
        public async Task<ViewReportItem?> ItemReportDetail(int reportID)
        {
            return await _context.ViewReportItems.Where(vri => vri.ReportId == reportID).FirstOrDefaultAsync();
        }
        public async Task<ViewReportBlog?> BlogReportDetail(int reportID)
        {
            return await _context.ViewReportBlogs.Where(vri => vri.ReportId == reportID).FirstOrDefaultAsync();
        }
        public async Task<ViewReportComment?> CommentReportDetail(int reportID)
        {
            return await _context.ViewReportComments.Where(vri => vri.ReportId == reportID).FirstOrDefaultAsync();
        }
        public async Task<ViewReportReply?> ReplyReportDetail(int reportID)
        {
            return await _context.ViewReportReplies.Where(vri => vri.ReportId == reportID).FirstOrDefaultAsync();
        }
        public async Task<ViewReportOrder?> OrderReportDetail(int reportID)
        {
            return await _context.ViewReportOrders.Where(vri => vri.ReportId == reportID).FirstOrDefaultAsync();
        }
        public async Task<bool> HiddenItems(HiddenAndPunish hideAndPunish)
        {
            var queryItem = _context.Items
                            .Where(it => it.ItemId == hideAndPunish.Id && it.ItemStatus == true)
                            .Join(_context.UserAccounts, it => it.UserId, us => us.UserId, (it, us) => new { it, us });
            Models.Item? item = await queryItem
                .Select(itus => itus.it)
                .FirstOrDefaultAsync();
            if (item != null)
            {
                UserAccount? userAccount = await queryItem
                    .Select(itus => itus.us)
                    .FirstOrDefaultAsync();
                item.ItemStatus = false;
                item.ReasonHiden = hideAndPunish.Reason;
                await _context.SaveChangesAsync();
                Email email = new()
                {
                    To = userAccount.UserGmail,
                    Subject = "your order has been hidden",
                    Link = "Dear "
                    + userAccount.UserName
                    + "/n"
                    + "your order: https://moby-customer.vercel.app/product/"
                    + item.ItemId
                    + "has been hidden by admintrator. \n"
                    + "Reason: "
                    + item.ReasonHiden
                };
                await _emailRepository.SendEmai(email);
                return true;
            }
            throw new KeyNotFoundException();
        }
        public async Task<bool> HiddenOrders(HiddenAndPunish hideAndPunish)
        {
            var queryOrder = _context.Orders
                            .Where(or => or.OrderId == hideAndPunish.Id && or.Status != 3)
                            .Join(_context.UserAccounts, or => or.UserId, us => us.UserId, (or, us) => new { or, us });
            Order? order = await _context.Orders
                .Where(or => or.OrderId == hideAndPunish.Id && or.Status != 3)
                .FirstOrDefaultAsync();
            if (order != null)
            {
                UserAccount? userAccount = await queryOrder
                    .Select(orus => orus.us)
                    .FirstOrDefaultAsync();
                order.Status = 3;
                order.ReasonCancel = hideAndPunish.Reason;
                await _context.SaveChangesAsync();
                Email email = new()
                {
                    To = userAccount.UserGmail,
                    Subject = "your order has been cancel",
                    Link = "Dear "
                    + userAccount.UserName
                    + "/n"
                    + "your order: https://moby-customer.vercel.app/order/"
                    + order.OrderId
                    + "has been cancel by admintrator \n"
                    + "Reason: "
                    + order.ReasonCancel
                };
                await _emailRepository.SendEmai(email);
                return true;
            }
            throw new KeyNotFoundException();
        }
        public async Task<bool> HiddenComments(HiddenAndPunish hideAndPunish)
        {
            var queryComment = _context.Comments
                            .Where(cm => cm.CommentId == hideAndPunish.Id && cm.Status == true)
                            .Join(_context.UserAccounts, cm => cm.UserId, us => us.UserId, (cm, us) => new { cm, us });
            Comment? comment = await queryComment
                .Select(cmus => cmus.cm)
                .FirstOrDefaultAsync();
            if (comment != null)
            {
                UserAccount? userAccount = await queryComment
                    .Select(cmus => cmus.us)
                    .FirstOrDefaultAsync();
                comment.Status = false;
                comment.ReasonHiden = hideAndPunish.Reason;
                await _context.SaveChangesAsync();
                Email email = new()
                {
                    To = userAccount.UserGmail,
                    Subject = "your comment has been hidden",
                    Link = "Dear "
                    + userAccount.UserName
                    + "/n"
                    + "your comment: https://moby-customer.vercel.app/order/"
                    + comment.CommentId
                    + "has been hidden by admintrator \n"
                    + "Reason: "
                    + comment.ReasonHiden
                };
                await _emailRepository.SendEmai(email);
                return true;
            }
            throw new KeyNotFoundException();
        }
        public async Task<bool> HiddenReplies(HiddenAndPunish hideAndPunish)
        {
            var queryReply = _context.Replies
                            .Where(rl => rl.ReplyId == hideAndPunish.Id && rl.Status == true)
                            .Join(_context.UserAccounts, rl => rl.UserId, us => us.UserId, (rl, us) => new { rl, us });
            Reply? reply = await queryReply
                .Select(rlus => rlus.rl)
                .FirstOrDefaultAsync();
            if (reply != null)
            {
                UserAccount? userAccount = await queryReply
                    .Select(rlus => rlus.us)
                    .FirstOrDefaultAsync();
                reply.Status = false;
                reply.ReasonHiden = hideAndPunish.Reason;
                await _context.SaveChangesAsync();
                Email email = new()
                {
                    To = userAccount.UserGmail,
                    Subject = "your reply has been hidden",
                    Link = "Dear "
                    + userAccount.UserName
                    + "/n"
                    + "your comment: https://moby-customer.vercel.app/order/"
                    + reply.ReplyId
                    + "has been hidden by admintrator \n"
                    + "Reason: "
                    + reply.ReasonHiden
                };
                await _emailRepository.SendEmai(email);
                return true;
            }
            throw new KeyNotFoundException();
        }
        public async Task<bool> HiddenBlogs(HiddenAndPunish hideAndPunish)
        {

            var queryBlog = _context.Blogs
                .Where(bg => bg.BlogId == hideAndPunish.Id && bg.BlogStatus != 2)
                .Join(_context.UserAccounts, bg => bg.UserId, us => us.UserId, (bg, us) => new { bg, us });
            Blog? blog = await queryBlog
                .Select(bgus => bgus.bg)
                .FirstOrDefaultAsync();
            if (blog != null)
            {
                UserAccount? userAccount = await queryBlog
                    .Select(bgus => bgus.us)
                    .FirstOrDefaultAsync();
                blog.BlogStatus = 2;
                blog.ReasonDeny = hideAndPunish.Reason;
                await _context.SaveChangesAsync();
                Email email = new()
                {
                    To = userAccount.UserGmail,
                    Subject = "your blog has been hidden",
                    Link = "Dear "
                    + userAccount.UserName
                    + "/n"
                    + "your blog: https://moby-customer.vercel.app/order/"
                    + blog.BlogId
                    + "has been hidden by admintrator \n"
                    + "Reason: "
                    + blog.ReasonDeny
                };
                await _emailRepository.SendEmai(email);
                return true;
            }
            throw new KeyNotFoundException();
        }
        public async Task<bool> PunishViolatorsItems(HiddenAndPunish hideAndPunish, RecordPenaltyPoint recordPenaltyPoint)
        {
            var queryItem = _context.Items
                            .Join(_context.UserAccounts, it => it.UserId, us => us.UserId, (it, us) => new { it, us })
                            .Where(itus => itus.it.ItemId == hideAndPunish.Id);
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
                    recordPenaltyPoint = new()
                    {
                        UserId = userAccountItem.UserId,
                        ObjReport = item.ItemId,
                        Type = hideAndPunish.Type,
                        PenaltyPoint = 10,
                        ReasonDeductionOfPoints = hideAndPunish.Reason
                    };
                    if (await _recordPenaltyRepository.CreateRecord(recordPenaltyPoint) == true)
                    {
                        Email email = new()
                        {
                            To = userAccountItem.UserGmail,
                            Subject = "your blog has been hidden",
                            Link = "Dear " + userAccountItem.UserName + "/n" + "your order: https://moby-customer.vercel.app/order/" + item.ItemId + " has seriously violated our policy \n" + "Reason: " + hideAndPunish.Reason
                        };
                        await _emailRepository.SendEmai(email);
                    }
                }
                await _context.SaveChangesAsync();
                return true;
            }
            throw new KeyNotFoundException();
        }
        public async Task<bool> PunishViolatorsOrders(HiddenAndPunish hideAndPunish, RecordPenaltyPoint recordPenaltyPoint)
        {
            var queryOrder = _context.Orders
                        .Join(_context.Items, or => or.ItemId, it => it.ItemId, (or, it) => new { or, it })
                        .Join(_context.UserAccounts, orit => orit.it.UserId, us => us.UserId, (orit, us) => new { orit, us })
                        .Where(oritus => oritus.orit.or.OrderId == hideAndPunish.Id);
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
                    .Where(queryOrder => queryOrder.orit.or.Status != 3)
                    .Select(queryOrder => queryOrder.orit.or)
                    .FirstOrDefaultAsync();
                if (order != null)
                {
                    order.Status = 3;
                    recordPenaltyPoint = new()
                    {
                        UserId = userAccountOrder.UserId,
                        ObjReport = order.OrderId,
                        Type = hideAndPunish.Type,
                        PenaltyPoint = 10,
                        ReasonDeductionOfPoints = hideAndPunish.Reason
                    };
                    if (await _recordPenaltyRepository.CreateRecord(recordPenaltyPoint) == true)
                    {
                        Email email = new()
                        {
                            To = userAccountOrder.UserGmail,
                            Subject = "your blog has been hidden",
                            Link = "Dear " + userAccountOrder.UserName + "/n" + "your order: https://moby-customer.vercel.app/order/" + order.OrderId + " has seriously violated our policy \n" + "Reason: " + hideAndPunish.Reason
                        };
                        await _emailRepository.SendEmai(email);
                    }
                }
                await _context.SaveChangesAsync();
                return true;
            }
            throw new KeyNotFoundException();
        }
        public async Task<bool> PunishViolatorsComments(HiddenAndPunish hideAndPunish, RecordPenaltyPoint recordPenaltyPoint)
        {
            var queryComment = _context.Comments
                            .Join(_context.UserAccounts, cm => cm.UserId, us => us.UserId, (cm, us) => new { cm, us })
                            .Where(cmus => cmus.cm.CommentId == hideAndPunish.Id);
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
                    recordPenaltyPoint = new()
                    {
                        UserId = userAccountComment.UserId,
                        ObjReport = comment.CommentId,
                        Type = hideAndPunish.Type,
                        PenaltyPoint = 10,
                        ReasonDeductionOfPoints = hideAndPunish.Reason
                    };
                    if (await _recordPenaltyRepository.CreateRecord(recordPenaltyPoint) == true)
                    {
                        Email email = new()
                        {
                            To = userAccountComment.UserGmail,
                            Subject = "your blog has been hidden",
                            Link = "Dear " + userAccountComment.UserName + "/n" + "your commnet: https://moby-customer.vercel.app/order/" + comment.CommentId + " has seriously violated our policy \n" + "Reason: " + hideAndPunish.Reason
                        };
                        await _emailRepository.SendEmai(email);
                    }
                }
                await _context.SaveChangesAsync();
                return true;
            }
            throw new KeyNotFoundException();
        }
        public async Task<bool> PunishViolatorsReplies(HiddenAndPunish hideAndPunish, RecordPenaltyPoint recordPenaltyPoint)
        {
            var queryReply = _context.Replies
                            .Join(_context.UserAccounts, rp => rp.UserId, us => us.UserId, (rp, us) => new { rp, us })
                            .Where(rpus => rpus.rp.ReplyId == hideAndPunish.Id);
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
                    recordPenaltyPoint = new()
                    {
                        UserId = userAccountReply.UserId,
                        ObjReport = reply.ReplyId,
                        Type = hideAndPunish.Type,
                        PenaltyPoint = 10,
                        ReasonDeductionOfPoints = hideAndPunish.Reason
                    };
                    if (await _recordPenaltyRepository.CreateRecord(recordPenaltyPoint) == true)
                    {
                        Email email = new()
                        {
                            To = userAccountReply.UserGmail,
                            Subject = "your blog has been hidden",
                            Link = "Dear " + userAccountReply.UserName + "/n" + "your comment: https://moby-customer.vercel.app/order/" + reply.ReplyId + " has seriously violated our policy \n" + "Reason: " + hideAndPunish.Reason
                        };

                        await _emailRepository.SendEmai(email);
                    }
                }
                await _context.SaveChangesAsync();
                return true;
            }
            throw new KeyNotFoundException();
        }
        public async Task<bool> PunishViolatorsBlogs(HiddenAndPunish hideAndPunish, RecordPenaltyPoint recordPenaltyPoint)
        {
            var queryBlog = _context.Blogs
                            .Join(_context.UserAccounts, blg => blg.UserId, us => us.UserId, (blg, us) => new { blg, us })
                            .Where(blgus => blgus.blg.BlogId == hideAndPunish.Id);
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
                    recordPenaltyPoint = new()
                    {
                        UserId = userAccountBlog.UserId,
                        ObjReport = blog.BlogId,
                        Type = hideAndPunish.Type,
                        PenaltyPoint = 10,
                        ReasonDeductionOfPoints = hideAndPunish.Reason
                    };
                    if (await _recordPenaltyRepository.CreateRecord(recordPenaltyPoint) == true)
                    {
                        Email email = new()
                        {
                            To = userAccountBlog.UserGmail,
                            Subject = "your blog has been hidden",
                            Link = "Dear " + userAccountBlog.UserName + "/n" + "your blog: https://moby-customer.vercel.app/order/" + blog.BlogId + " has seriously violated our policy \n" + "Reason: " + hideAndPunish.Reason
                        };

                        await _emailRepository.SendEmai(email);
                    }
                }
                await _context.SaveChangesAsync();
                return true;
            }
            throw new KeyNotFoundException();
        }
        public async Task<StatusAndReasonHidenViewModel?> GetStatusAndReasonHidenItems(int id, StatusAndReasonHidenViewModel statusAndReasonHidenViewModel)
        {
            Models.Item? item = await _context.Items.Where(it => it.ItemId == id).FirstOrDefaultAsync();
            if (item != null)
            {
                return statusAndReasonHidenViewModel = new()
                {
                    Id = id,
                    StatusBit = item.ItemStatus,
                    ReasonHiden = item.ReasonHiden
                };
            }
            throw new KeyNotFoundException();
        }
        public async Task<StatusAndReasonHidenViewModel?> GetStatusAndReasonHidenOrders(int id, StatusAndReasonHidenViewModel statusAndReasonHidenViewModel)
        {
            Order? order = await _context.Orders.Where(or => or.OrderId == id).FirstOrDefaultAsync();
            if (order != null)
            {
                return statusAndReasonHidenViewModel = new()
                {
                    Id = id,
                    StatusInt = order.Status,
                    ReasonHiden = order.ReasonCancel
                };
            }
            throw new KeyNotFoundException();
        }
        public async Task<StatusAndReasonHidenViewModel?> GetStatusAndReasonHidenComments(int id, StatusAndReasonHidenViewModel statusAndReasonHidenViewModel)
        {
            Comment? comment = await _context.Comments.Where(cm => cm.CommentId == id).FirstOrDefaultAsync();
            if (comment != null)
            {
                return statusAndReasonHidenViewModel = new()
                {
                    Id = id,
                    StatusBit = comment.Status,
                    ReasonHiden = comment.ReasonHiden
                };
            }
            throw new KeyNotFoundException();
        }
        public async Task<StatusAndReasonHidenViewModel?> GetStatusAndReasonHidenReplies(int id, StatusAndReasonHidenViewModel statusAndReasonHidenViewModel)
        {
            Reply? reply = await _context.Replies.Where(rpl => rpl.ReplyId == id).FirstOrDefaultAsync();
            if (reply != null)
            {
                return statusAndReasonHidenViewModel = new()
                {
                    Id = id,
                    StatusBit = reply.Status,
                    ReasonHiden = reply.ReasonHiden
                };
            }
            throw new KeyNotFoundException();
        }
        public async Task<StatusAndReasonHidenViewModel?> GetStatusAndReasonHidenBlogs(int id, StatusAndReasonHidenViewModel statusAndReasonHidenViewModel)
        {
            Blog? blog = await _context.Blogs.Where(blg => blg.BlogId == id).FirstOrDefaultAsync();
            if (blog != null)
            {
                return statusAndReasonHidenViewModel = new()
                {
                    Id = id,
                    StatusInt = blog.BlogStatus,
                    ReasonHiden = blog.ReasonDeny
                };
            }
            throw new KeyNotFoundException();
        }
    }
}
