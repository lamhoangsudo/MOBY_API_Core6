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
        public int Expr3 { get; set; }
        public string Expr4 { get; set; } = null!;
        public int Expr5 { get; set; }
        public string Expr6 { get; set; } = null!;
        public int Expr7 { get; set; }
        public string Expr8 { get; set; } = null!;
        public int Expr9 { get; set; }
        public string Expr10 { get; set; } = null!;
        public int Expr1 { get; set; }
        public string Expr2 { get; set; } = null!;
    }
}
