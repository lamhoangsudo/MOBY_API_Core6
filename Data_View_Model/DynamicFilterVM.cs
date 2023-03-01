using System.ComponentModel;

namespace MOBY_API_Core6.Data_View_Model
{
    public class DynamicFilterVM
    {
        [DefaultValue(null)]
        public int categoryID { get; set; }
        [DefaultValue(null)]
        public string titleName { get; set; }
        [DefaultValue(null)]
        public string location { get; set; }
        [DefaultValue(0)]
        public float minPrice { get; set; }
        [DefaultValue(null)]
        public float maxPrice { get; set; }
        [DefaultValue(100)]
        public double maxUsable { get; set; }
        [DefaultValue(40)]
        public double minUsable { get; set; }

        public DynamicFilterVM(int categoryID, string titleName, string location, float minPrice, float maxPrice, double maxUsable, double minUsable)
        {
            this.categoryID = categoryID;
            this.titleName = titleName;
            this.location = location;
            this.minPrice = minPrice;
            this.maxPrice = maxPrice;
            this.maxUsable = maxUsable;
            this.minUsable = minUsable;
        }
    }
}
