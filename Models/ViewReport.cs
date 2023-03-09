using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class ViewReport
    {
        public int ReportId { get; set; }
        public DateTime ReportDateCreate { get; set; }
        public int ReportStatus { get; set; }
        public string? ReportContent { get; set; }
        public string? Image { get; set; }
        public DateTime? ReportDateUpdate { get; set; }
        public string UserName { get; set; } = null!;
        public string ItemTitle { get; set; } = null!;
        public int? ItemId { get; set; }
        public int UserId { get; set; }
    }
}
