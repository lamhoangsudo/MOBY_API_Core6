using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class ImageVerifyVM
    {
        [Required]
        public String imageURL { get; set; } = null!;
    }
}
