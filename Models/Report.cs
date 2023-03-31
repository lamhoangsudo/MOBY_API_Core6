using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class Report
    {
        public int ReportId { get; set; }
        public int UserId { get; set; }
        public int? ItemId { get; set; }
        public int? OrderId { get; set; }
        public int? CommentId { get; set; }
        public int? ReplyId { get; set; }
        public int? BlogId { get; set; }
        public string Title { get; set; } = null!;
        public string Evident { get; set; } = null!;
        public string ReportContent { get; set; } = null!;
        public DateTime ReportDateCreate { get; set; }
        public DateTime? ReportDateResolve { get; set; }
        public int ReportStatus { get; set; }
        public string? ReasonDeny { get; set; }

        public virtual Blog? Blog { get; set; }
        public virtual Comment? Comment { get; set; }
        public virtual Item? Item { get; set; }
        public virtual Order? Order { get; set; }
        public virtual Reply? Reply { get; set; }
        public virtual UserAccount User { get; set; } = null!;
    }
}
