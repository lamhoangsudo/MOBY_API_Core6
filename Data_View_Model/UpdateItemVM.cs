using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class UpdateItemVM
    {
        [Required]
        public int userId { get; set; }
        [Required]
        public int itemID { get; set; }
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
        public string itemQuanlity { get; set; }
        [DefaultValue(0)]
        public double itemEstimateValue { get; set; }
        [DefaultValue(0)]
        public double itemSalePrice { get; set; }
        [DefaultValue(1)]
        public int itemShareAmount { get; set; }
        [DefaultValue(true)]
        public bool itemSponsoredOrderShippingFee { get; set; }
        [Required]
        public string itemShippingAddress { get; set; }
        [Required]
        public string image { get; set; }
        [DefaultValue(null)]
        public string? stringDateTimeExpired { get; set; }
        [DefaultValue(true)]
        public bool share { get; set; }
    }
}
