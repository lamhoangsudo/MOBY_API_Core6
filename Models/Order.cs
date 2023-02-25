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
        public DateTime DateCreate { get; set; }

        public virtual UserAccount User { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
