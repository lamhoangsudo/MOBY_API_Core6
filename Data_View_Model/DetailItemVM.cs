namespace MOBY_API_Core6.Data_View_Model
{
    public class DetailItemVM
    {
        public int ItemId { get; set; }
        public string ItemCode { get; set; } = null!;
        public string ItemTitle { get; set; } = null!;
        public string ItemDetailedDescription { get; set; } = null!;
        public double ItemMass { get; set; }
        public bool ItemSize { get; set; }
        public double? ItemEstimateValue { get; set; }
        public double? ItemSalePrice { get; set; }
        public int ItemShareAmount { get; set; }
        public DateTime? ItemExpiredTime { get; set; }
        public string ItemShippingAddress { get; set; } = null!;
        public DateTime ItemDateCreated { get; set; }
        public bool? ItemStatus { get; set; }
        public bool Share { get; set; }
        public string Image { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public int UserId { get; set; }
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; } = null!;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public double? MaxAge { get; set; }
        public double? MinAge { get; set; }
        public double? MaxWeight { get; set; }
        public double? MinWeight { get; set; }
        public double? MinHeight { get; set; }
        public double? MaxHeight { get; set; }
    }
}
