using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class Request
    {
        public int RequestId { get; set; }
        public int UserId { get; set; }
        public int ItemQuantity { get; set; }
        public int ItemId { get; set; }
        public string Address { get; set; } = null!;
        public string? Note { get; set; }
        public DateTime DateCreate { get; set; }
        public int Status { get; set; }
        public DateTime? DateChangeStatus { get; set; }
        public double Price { get; set; }

        public virtual Item Item { get; set; } = null!;
        public virtual UserAccount User { get; set; } = null!;
    }
}
