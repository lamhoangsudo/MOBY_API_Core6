using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int Quanlity { get; set; }
        public string Address { get; set; } = null!;
        public int Status { get; set; }
        public bool SponsoredOrderShippingFee { get; set; }
        public string? ReasonDeny { get; set; }
        public string? Note { get; set; }

        public virtual Item Item { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}
