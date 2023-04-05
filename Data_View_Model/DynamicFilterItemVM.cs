using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class DynamicFilterItemVM
    {
        [DefaultValue(null)]
        public int? CategoryID { get; set; }
        [DefaultValue(null)]
        public int? SubCategoryID { get; set; }
        [DefaultValue(null)]
        public string? TitleName { get; set; }
        [DefaultValue(null)]
        public string? Location { get; set; }
        [DefaultValue(0)]
        public float MinPrice { get; set; }
        [DefaultValue(0)]
        public float MaxPrice { get; set; }
        [DefaultValue(100)]
        public double MaxUsable { get; set; }
        [DefaultValue(40)]
        public double MinUsable { get; set; }
        [DefaultValue(null)]
        public bool? Share { get; set; }
        [DefaultValue(true)]
        public bool? Status { get; set;}
        [Required]
        public int PageNumber { get; set; }
        [Required]
        public int PageSize { get; set; }
        [DefaultValue(true)]
        public bool OrderByDateCreate { get; set; }
        [DefaultValue(false)]
        public bool OrderByDateUpdate { get; set; }
        [DefaultValue(false)]
        public bool? OrderByEstimateValue { get; set; }
        [DefaultValue(false)]
        public bool? OrderByPrice { get; set; }
        [DefaultValue(null)]
        public DateTime? MinDateCreate { get; set; }
        [DefaultValue(null)]
        public DateTime? MaxDateCreate { get; set; }
        [DefaultValue(null)]
        public DateTime? MinDateUpdate { get; set; }
        [DefaultValue(null)]
        public DateTime? MaxDateUpdate { get; set; }
        [DefaultValue(true)]
        public bool AscendingOrDescending { get; set; }

    }
}
