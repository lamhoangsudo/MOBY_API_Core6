using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string OrderCode { get; set; } = null!;
        public int PaymentId { get; set; }
        public int OrderStatus { get; set; }
        public DateTime OrderDateCreate { get; set; }
        public DateTime? OrderDateUpdate { get; set; }
        public string OrderAddress { get; set; } = null!;

        public virtual Payment Payment { get; set; } = null!;
        public virtual UserAccount User { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
