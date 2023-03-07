using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class UpdateBlogVM
    {

        [Required]
        public int BlogId { get; set; }
        [Required]
        public int BlogCategoryId { get; set; }

        [DefaultValue("String blogTittle")]
        public string BlogTitle { get; set; } = null!;
        [DefaultValue("String BlogDescription")]
        public string? BlogDescription { get; set; }
        [DefaultValue("String BlogContent")]
        public string BlogContent { get; set; } = null!;
    }
}
