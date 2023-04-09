using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class ViewReportReply
    {
        public int ReportId { get; set; }
        public int ReplyId { get; set; }
        public string Title { get; set; } = null!;
        public string Evident { get; set; } = null!;
        public string ReportContent { get; set; } = null!;
        public DateTime ReportDateCreate { get; set; }
        public DateTime? ReportDateResolve { get; set; }
        public int ReportStatus { get; set; }
        public string? ReasonDeny { get; set; }
        public bool? Status { get; set; }
        public string ReplyContent { get; set; } = null!;
        public DateTime DateCreate { get; set; }
        public int UserId { get; set; }
        public string UserCode { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string UserGmail { get; set; } = null!;
        public string UserImage { get; set; } = null!;
        public bool UserStatus { get; set; }
        public DateTime UserDateCreate { get; set; }
        public int Reputation { get; set; }
        public int Idureport { get; set; }
        public string CodeUreport { get; set; } = null!;
        public string NameUreport { get; set; } = null!;
        public string GmailUreport { get; set; } = null!;
        public string ImageUreport { get; set; } = null!;
        public bool StatusUreport { get; set; }
        public DateTime DateCreateUreport { get; set; }
        public int ReputationUreport { get; set; }
    }
}
