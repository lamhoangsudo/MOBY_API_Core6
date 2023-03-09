using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class RequestDetail
    {
        public int RequestDetailId { get; set; }
        public int RequestId { get; set; }
        public int ItemId { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public int ItemQuantity { get; set; }
        public int Status { get; set; }

        public virtual Item Item { get; set; } = null!;
        public virtual Request Request { get; set; } = null!;
    }
}
