using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class CartDetailView
    {
        public int CartDetailId { get; set; }
        public int CartId { get; set; }
        public DateTime CartDetailDateCreate { get; set; }
        public DateTime? CartDetailDateUpdate { get; set; }
        public int CartDetailItemQuantity { get; set; }
        public int ItemId { get; set; }
        public string ItemCode { get; set; } = null!;
        public string ItemQuanlity { get; set; } = null!;
        public double? ItemEstimateValue { get; set; }
        public double? ItemSalePrice { get; set; }
        public int ItemShareAmount { get; set; }
        public bool? ItemSponsoredOrderShippingFee { get; set; }
        public string ItemShippingAddress { get; set; } = null!;
        public DateTime? ItemExpiredTime { get; set; }
        public string Image { get; set; } = null!;
        public string ItemTitle { get; set; } = null!;
        public double ItemMass { get; set; }
        public bool ItemSize { get; set; }
        public string UserName { get; set; } = null!;
        public int UserId { get; set; }
        public string CategoryName { get; set; } = null!;
        public int CategoryId { get; set; }
        public string SubCategoryName { get; set; } = null!;
        public int SubCategoryId { get; set; }
    }
}
