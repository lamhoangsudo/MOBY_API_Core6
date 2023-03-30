using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class ViewReportReply
    {
        public int ReportId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public int Expr1 { get; set; }
        public string Expr2 { get; set; } = null!;
        public int ReplyId { get; set; }
    }
}
