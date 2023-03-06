using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class OrderDetailView
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int Quanlity { get; set; }
        public string Address { get; set; } = null!;
        public int Status { get; set; }
        public bool SponsoredOrderShippingFee { get; set; }
        public string? ReasonDeny { get; set; }
        public int ItemId { get; set; }
        public string SubCategoryName { get; set; } = null!;
        public int SubCategoryId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string ItemTitle { get; set; } = null!;
        public double ItemMass { get; set; }
        public bool ItemSize { get; set; }
        public string ItemQuanlity { get; set; } = null!;
        public double? ItemEstimateValue { get; set; }
        public double? ItemSalePrice { get; set; }
        public int ItemShareAmount { get; set; }
        public bool? ItemSponsoredOrderShippingFee { get; set; }
        public DateTime? ItemExpiredTime { get; set; }
        public string ItemShippingAddress { get; set; } = null!;
        public DateTime ItemDateCreated { get; set; }
        public DateTime? ItemDateUpdate { get; set; }
        public bool ItemStatus { get; set; }
        public string Image { get; set; } = null!;
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string UserGmail { get; set; } = null!;
        public string UserAddress { get; set; } = null!;
        public string? UserPhone { get; set; }
    }
}
