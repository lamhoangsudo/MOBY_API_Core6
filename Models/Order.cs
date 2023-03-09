using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int ItemId { get; set; }
        public int Quanlity { get; set; }
        public string Address { get; set; } = null!;
        public int Status { get; set; }
        public bool SponsoredOrderShippingFee { get; set; }
        public string? ReasonDeny { get; set; }
        public string? Note { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DatePackage { get; set; }
        public DateTime? DateReceived { get; set; }
        public DateTime? DatePunishment { get; set; }

        public virtual Item Item { get; set; } = null!;
        public virtual UserAccount User { get; set; } = null!;
    }
}
