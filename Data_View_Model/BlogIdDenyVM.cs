using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class BlogIdDenyVM
    {
        [Required]
        public int BlogId { get; set; }

        [DefaultValue("other reason")]
        public String? reason { get; set; }
    }
}
