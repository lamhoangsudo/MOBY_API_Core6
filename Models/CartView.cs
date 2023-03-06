using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class CartView
    {
        public int CartId { get; set; }
        public int ItemId { get; set; }
        public string ItemTitle { get; set; } = null!;
        public double? ItemSalePrice { get; set; }
        public int ItemShareAmount { get; set; }
        public bool? ItemSponsoredOrderShippingFee { get; set; }
        public int CartDetailId { get; set; }
        public DateTime CartDetailDateCreate { get; set; }
        public DateTime? CartDetailDateUpdate { get; set; }
        public int CartDetailItemQuantity { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
    }
}
