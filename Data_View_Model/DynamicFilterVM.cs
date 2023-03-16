using System.ComponentModel;

namespace MOBY_API_Core6.Data_View_Model
{
    public class DynamicFilterVM
    {
        [DefaultValue(null)]
        public int categoryID { get; set; }
        [DefaultValue(null)]
        public string? titleName { get; set; }
        [DefaultValue(null)]
        public string? location { get; set; }
        [DefaultValue(null)]
        public float? minPrice { get; set; }
        [DefaultValue(null)]
        public float? maxPrice { get; set; }
        [DefaultValue(100)]
        public double maxUsable { get; set; }
        [DefaultValue(40)]
        public double minUsable { get; set; }
    }
}
