using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class ViewReportComment
    {
        public int ReportId { get; set; }
        public int CommentId { get; set; }
        public string? ReasonDeny { get; set; }
        public int ReportStatus { get; set; }
        public DateTime? ReportDateResolve { get; set; }
        public DateTime ReportDateCreate { get; set; }
        public string ReportContent { get; set; } = null!;
        public string Evident { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string CommentContent { get; set; } = null!;
        public DateTime DateCreate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public int UserId { get; set; }
        public string UserCode { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string UserGmail { get; set; } = null!;
        public string UserImage { get; set; } = null!;
        public bool UserStatus { get; set; }
        public DateTime UserDateCreate { get; set; }
        public int Reputation { get; set; }
        public int Expr1 { get; set; }
        public string Expr2 { get; set; } = null!;
        public string Expr3 { get; set; } = null!;
        public string Expr4 { get; set; } = null!;
        public string Expr5 { get; set; } = null!;
        public bool Expr6 { get; set; }
        public DateTime Expr7 { get; set; }
        public int Expr8 { get; set; }
        public int BlogId { get; set; }
        public string BlogTitle { get; set; } = null!;
        public int ItemId { get; set; }
        public string ItemTitle { get; set; } = null!;
    }
}
