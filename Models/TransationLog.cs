using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class TransationLog
    {
        public int TransactionId { get; set; }
        public int UserId { get; set; }
        public double Value { get; set; }
        public bool TransactionStatus { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateUpdate { get; set; }

        public virtual UserAccount User { get; set; } = null!;
    }
}
