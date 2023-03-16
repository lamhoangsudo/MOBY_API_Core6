using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class UpdateRequestDetailVM
    {
        [Required]
        public int RequestDetailId { get; set; }
        [DefaultValue(1)]
        public int ItemQuantity { get; set; }
        public bool? SponsoredOrderShippingFee { get; set; }
        public string? Address { get; set; }
        public string? Note { get; set; }
    }
}
