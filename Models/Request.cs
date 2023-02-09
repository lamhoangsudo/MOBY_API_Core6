using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class Request
    {
        public int RequestId { get; set; }
        public int UserId { get; set; }
        public int SubCategoryId { get; set; }
        public string RequestTitle { get; set; } = null!;
        public string? RequestDetailedDescription { get; set; }
        public int RequestAmountRequired { get; set; }
        public DateTime? RequestExpiredTime { get; set; }
        public string RequestDeliveryAddress { get; set; } = null!;
        public int ImageId { get; set; }
        public DateTime RequestDateCreate { get; set; }
        public DateTime? RequestDateUpdate { get; set; }
        public virtual @bool Image { get; set; } = null!;
        public virtual SubCategory SubCategory { get; set; } = null!;
        public virtual UserAccount User { get; set; } = null!;
    }
}
