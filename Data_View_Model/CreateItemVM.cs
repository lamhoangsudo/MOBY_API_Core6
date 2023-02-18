using System.ComponentModel;

namespace MOBY_API_Core6.Data_View_Model
{
    public class CreateItemVM
    {
        public int userId { get; set; }
        public int subCategoryId { get; set; }
        public string itemTitle { get; set; }
        public string itemDetailedDescription { get; set; }
        public double itemMass { get; set; }
        public bool itemSize { get; set; }
        public string itemQuanlity { get; set; }
        [DefaultValue(0)]
        public double itemEstimateValue { get; set; }
        [DefaultValue(0)]
        public double itemSalePrice { get; set; }
        [DefaultValue(1)]
        public int itemShareAmount { get; set; }
        public bool itemSponsoredOrderShippingFee { get; set; }
        public string itemShippingAddress { get; set; }
        public string image { get; set; }
        [DefaultValue(null)]
        public string? stringDateTimeExpired { get; set; }
        public bool share { get; set; }
    }
}
