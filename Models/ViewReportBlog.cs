using System;
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
        public int Expr1 { get; set; }
        public string Expr2 { get; set; } = null!;
    }
}
