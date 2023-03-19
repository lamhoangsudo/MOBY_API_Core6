using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class UpdateOrderVM
    {
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int Status { get; set; }
    }
}
