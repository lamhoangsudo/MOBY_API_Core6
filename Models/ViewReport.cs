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
        public int UserIdcomments { get; set; }
        public string UserNameComments { get; set; } = null!;
        public int UserIdblogs { get; set; }
        public string UserNameBlogs { get; set; } = null!;
        public int UserIdreplies { get; set; }
        public string UserNameReplies { get; set; } = null!;
        public int UserIditem { get; set; }
        public string UserNameItem { get; set; } = null!;
        public int UserIdorder { get; set; }
        public string UserNameOrder { get; set; } = null!;
    }
}
