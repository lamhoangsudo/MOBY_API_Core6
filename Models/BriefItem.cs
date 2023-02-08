using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class BriefItem
    {
        public int ItemId { get; set; }
        public string ItemCode { get; set; } = null!;
        public string ItemTitle { get; set; } = null!;
        public DateTime ItemDateCreated { get; set; }
        public double? ItemSalePrice { get; set; }
        public string ImageLink1 { get; set; } = null!;
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public int CategoryId { get; set; }
        public string SubCategoryName { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public int SubCategoryId { get; set; }
    }
}
