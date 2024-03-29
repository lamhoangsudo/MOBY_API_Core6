﻿using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class ViewReportBlog
    {
        public int ReportId { get; set; }
        public string BlogTitle { get; set; } = null!;
        public int BlogId { get; set; }
        public string UserName { get; set; } = null!;
        public int UserId { get; set; }
        public DateTime BlogDateCreate { get; set; }
        public DateTime? BlogDateUpdate { get; set; }
        public int? BlogStatus { get; set; }
        public string? Image { get; set; }
        public string UserGmail { get; set; } = null!;
        public string UserCode { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Evident { get; set; } = null!;
        public string ReportContent { get; set; } = null!;
        public DateTime ReportDateCreate { get; set; }
        public DateTime? ReportDateResolve { get; set; }
        public int ReportStatus { get; set; }
        public string? ReasonDeny { get; set; }
        public string UserImage { get; set; } = null!;
        public bool UserStatus { get; set; }
        public DateTime UserDateCreate { get; set; }
        public int Reputation { get; set; }
        public int UserIdreport { get; set; }
        public string UserCodeReport { get; set; } = null!;
        public string UserNameReport { get; set; } = null!;
        public string UserGmailReport { get; set; } = null!;
        public string UserImageReport { get; set; } = null!;
        public bool UserStatusReport { get; set; }
        public DateTime UserDateCreateReport { get; set; }
        public int ReputationReport { get; set; }
    }
}
