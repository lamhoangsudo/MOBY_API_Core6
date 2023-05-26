using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MOBY_API_Core6.Data_View_Model
{
    public class UpdateBannerVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string? Link { get; set; }
        [Required]
        public string? Image { get; set; }
    }
}
