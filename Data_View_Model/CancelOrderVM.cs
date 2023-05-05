using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class CancelOrderVM
    {
        [Required]
        public int OrderId { get; set; }
        [Required]
        public string? ReasonCancel { get; set; }
    }
}
