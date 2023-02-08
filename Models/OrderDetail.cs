using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int OrderDetailStatus { get; set; }
        public int OrderDetailQuantity { get; set; }
        public decimal OrderDetailPrice { get; set; }
        public DateTime? OrderDetailDateUpdate { get; set; }
        public string OrderDetailShareAddress { get; set; } = null!;

        public virtual Item Item { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}
