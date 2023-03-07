using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class CreateItemVM
    {
        [Required]
        public int userId { get; set; }
        [Required]
        public int subCategoryId { get; set; }
        [Required]
        public string itemTitle { get; set; }
        [Required]
        public string itemDetailedDescription { get; set; }
        [Range(0, 100)]
        public double itemMass { get; set; }
        [DefaultValue(false)]
        public bool itemSize { get; set; }
        public string? itemQuanlity { get; set; }
        [Range (40, 100)]
        [DefaultValue(40)]
        public double itemEstimateValue { get; set; }
        [DefaultValue(0)]
        public double itemSalePrice { get; set; }
        [DefaultValue(1)]
        public int itemShareAmount { get; set; }
        [DefaultValue(null)]
        public bool? itemSponsoredOrderShippingFee { get; set; }
        [Required]
        public string itemShippingAddress { get; set; }
        [Required]
        public string image { get; set; }
        [DefaultValue(null)]
        public string? stringDateTimeExpired { get; set; }
        [DefaultValue(true)]
        public bool share { get; set; }

        public CreateItemVM(int userId, int subCategoryId, string itemTitle, string itemDetailedDescription, double itemMass, bool itemSize, string? itemQuanlity, double itemEstimateValue, double itemSalePrice, int itemShareAmount, bool? itemSponsoredOrderShippingFee, string itemShippingAddress, string image, string? stringDateTimeExpired, bool share)
        {
            this.userId = userId;
            this.subCategoryId = subCategoryId;
            this.itemTitle = itemTitle;
            this.itemDetailedDescription = itemDetailedDescription;
            this.itemMass = itemMass;
            this.itemSize = itemSize;
            this.itemQuanlity = itemQuanlity;
            this.itemEstimateValue = itemEstimateValue;
            this.itemSalePrice = itemSalePrice;
            this.itemShareAmount = itemShareAmount;
            this.itemSponsoredOrderShippingFee = itemSponsoredOrderShippingFee;
            this.itemShippingAddress = itemShippingAddress;
            this.image = image;
            this.stringDateTimeExpired = stringDateTimeExpired;
            this.share = share;
        }
    }
}
