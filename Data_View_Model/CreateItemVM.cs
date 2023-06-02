using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class CreateItemVM
    {
        [ReadOnly(true)]
        public int UserId { get; set; }
        [Required]
        public int SubCategoryId { get; set; }
        [Required]
        public string ItemTitle { get; set; }
        [Required]
        public string ItemDetailedDescription { get; set; }
        [Range(0, 100)]
        public double ItemMass { get; set; }
        [DefaultValue(false)]
        public bool ItemSize { get; set; }
        [Range (40, 100)]
        [DefaultValue(40)]
        public double ItemEstimateValue { get; set; }
        [DefaultValue(0)]
        public double ItemSalePrice { get; set; }
        [DefaultValue(1)]
        public int ItemShareAmount { get; set; }
        [Required]
        public string ItemShippingAddress { get; set; }
        [Required]
        public string Image { get; set; }
        [DefaultValue(null)]
        public string? StringDateTimeExpired { get; set; }
        [DefaultValue(true)]
        public bool Share { get; set; }
        [DefaultValue(0)]
        public double MaxAge { get; set; } = 0;
        [DefaultValue(0)]
        public double MinAge { get; set; } = 0;
        [DefaultValue(0)]
        public double MaxWeight { get; set; } = 0;
        [DefaultValue(0)]
        public double MinWeight { get; set; } = 0;
        [DefaultValue(0)]
        public double MaxHeight { get; set; } = 0;
        [DefaultValue(0)]
        public double MinHeight { get; set; } = 0;

        public CreateItemVM(int userId, int subCategoryId, string itemTitle, string itemDetailedDescription, double itemMass, bool itemSize, double itemEstimateValue, double itemSalePrice, int itemShareAmount, string itemShippingAddress, string image, string? stringDateTimeExpired, bool share, double maxAge, double minAge, double maxWeight, double minWeight, double maxHeight, double minHeight)
        {
            this.UserId = userId;
            this.SubCategoryId = subCategoryId;
            this.ItemTitle = itemTitle;
            this.ItemDetailedDescription = itemDetailedDescription;
            this.ItemMass = itemMass;
            this.ItemSize = itemSize;
            this.ItemEstimateValue = itemEstimateValue;
            this.ItemSalePrice = itemSalePrice;
            this.ItemShareAmount = itemShareAmount;
            this.ItemShippingAddress = itemShippingAddress;
            this.Image = image;
            this.StringDateTimeExpired = stringDateTimeExpired;
            this.Share = share;
            MaxAge = maxAge;
            MinAge = minAge;
            MaxWeight = maxWeight;
            MinWeight = minWeight;
            MaxHeight = maxHeight;
            MinHeight = minHeight;
        }
    }
}
