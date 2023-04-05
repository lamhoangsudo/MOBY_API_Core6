using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class ViewReport
    {
        public int ReportId { get; set; }
        public string Title { get; set; } = null!;
        public string ReportContent { get; set; } = null!;
        public DateTime ReportDateCreate { get; set; }
        public int ReportStatus { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public int Expr1 { get; set; }
        public int? ItemId { get; set; }
        public int? OrderId { get; set; }
        public int? CommentId { get; set; }
        public int? ReplyId { get; set; }
        public int? BlogId { get; set; }
        public DateTime? ReportDateResolve { get; set; }
    }
}
