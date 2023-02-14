using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class DetailItemRequest
    {
        public int ItemId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string ItemTitle { get; set; } = null!;
        public string ItemDetailedDescription { get; set; } = null!;
        public int ItemShareAmount { get; set; }
        public bool ItemSponsoredOrderShippingFee { get; set; }
        public DateTime? ItemExpiredTime { get; set; }
        public string ItemShippingAddress { get; set; } = null!;
        public DateTime ItemDateCreated { get; set; }
        public bool ItemStatus { get; set; }
        public bool Share { get; set; }
        public string Image { get; set; } = null!;
        public string ItemCode { get; set; } = null!;
    }
}
