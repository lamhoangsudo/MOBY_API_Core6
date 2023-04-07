using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class CreateBannerVM
    {
        [Required]
        public string? Link { get; set; }
        [Required]
        public string? Image { get; set; }
    }
}
