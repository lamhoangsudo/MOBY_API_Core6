using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MOBY_API_Core6.Data_View_Model
{
    public class BannerVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string? Link { get; set; }
        [Required]
        public string? Imange { get; set; }
        [ReadOnly(true)]
        public DateTime? DateCreate { get; set; }
        [ReadOnly(true)]
        public DateTime? DateUpdate { get; set; }
    }
}
