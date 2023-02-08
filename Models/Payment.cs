using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class Payment
    {
        public Payment()
        {
            Orders = new HashSet<Order>();
        }

        public int PaymentId { get; set; }
        public string PaymentName { get; set; } = null!;
        public string? PaymentDescription { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
