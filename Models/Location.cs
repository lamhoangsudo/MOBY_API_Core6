using System.ComponentModel;

namespace MOBY_API_Core6.Models
{
    public class Location
    {
        [DefaultValue(null)]
        public string? AddressProvince { get; set; }
        [DefaultValue(null)]
        public string? AddressDistrict { get; set; }
        [DefaultValue(null)]
        public string? AddressWard { get; set; }
        [DefaultValue(null)]
        public string? AddressDetail { get; set; }
    }
}
