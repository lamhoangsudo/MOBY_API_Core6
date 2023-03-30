using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class ViewReportItem
    {
        public int ReportId { get; set; }
        public string UserName { get; set; } = null!;
        public string ItemTitle { get; set; } = null!;
        public int Expr1 { get; set; }
        public int UserId { get; set; }
        public int Expr2 { get; set; }
        public string Expr3 { get; set; } = null!;
    }
}
