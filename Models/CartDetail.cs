using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class CartDetail
    {
        public int CartDetailId { get; set; }
        public int CartId { get; set; }
        public int ItemId { get; set; }
        public int ItemQuantity { get; set; }

        public virtual Cart Cart { get; set; } = null!;
        public virtual Item Item { get; set; } = null!;
    }
}
