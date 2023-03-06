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
        public int? UserId { get; set; }

        public virtual UserAccount? User { get; set; }
        public virtual ICollection<CartDetail> CartDetails { get; set; }
    }
}
