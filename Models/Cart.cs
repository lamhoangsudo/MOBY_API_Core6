using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class Cart
    {
        public Cart()
        {
            CartDetails = new HashSet<CartDetail>();
        }

        public int CartId { get; set; }
        public int UserId { get; set; }
        public string Address { get; set; } = null!;

        public virtual UserAccount User { get; set; } = null!;
        public virtual ICollection<CartDetail> CartDetails { get; set; }
    }
}
