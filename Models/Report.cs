using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class Report
    {
        public int ReportId { get; set; }
        public int ItemId { get; set; }
        public DateTime ReportDateCreate { get; set; }
        public DateTime? ReportDateUpdate { get; set; }
        public bool ReportStatus { get; set; }
        public string? ReportContent { get; set; }

        public virtual Item Item { get; set; } = null!;
        public virtual UserAccount ItemNavigation { get; set; } = null!;
    }
}
