using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class DetailItem
    {
        public int ItemId { get; set; }
        public string ItemCode { get; set; } = null!;
        public string ItemTitle { get; set; } = null!;
        public string ItemDetailedDescription { get; set; } = null!;
        public double ItemMass { get; set; }
        public bool ItemSize { get; set; }
        public string ItemQuanlity { get; set; } = null!;
        public double? ItemEstimateValue { get; set; }
        public double? ItemSalePrice { get; set; }
        public int ItemShareAmount { get; set; }
        public DateTime? ItemExpiredTime { get; set; }
        public string ItemShippingAddress { get; set; } = null!;
        public DateTime ItemDateCreated { get; set; }
        public bool ItemStatus { get; set; }
        public bool Share { get; set; }
        public string Image { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public int UserId { get; set; }
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; } = null!;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
    }
}
