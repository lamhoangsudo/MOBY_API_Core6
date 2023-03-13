using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class CreateRequestDetailVM
    {
        [Required]
        public int RequestId { get; set; }
        [Required]
        public int ItemId { get; set; }
        [DefaultValue(1)]
        public int ItemQuantity { get; set; }
    }
}
