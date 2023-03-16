using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class CreateRequestDetailVM
    {
        [Required]
        public int RequestId { get; set; }
        [Required]
        public int ItemId { get; set; }
        [Required]
        public string? Address { get; set; }

    }
}
