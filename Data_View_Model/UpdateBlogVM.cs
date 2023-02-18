using System.ComponentModel;

namespace MOBY_API_Core6.Data_View_Model
{
    public class UpdateBlogVM
    {
        [DefaultValue(1)]
        public int BlogId { get; set; }
        [DefaultValue(1)]
        public int BlogCategoryId { get; set; }

        [DefaultValue("String blogTittle")]
        public string BlogTitle { get; set; } = null!;
        [DefaultValue("String blogTittle")]
        public string? BlogDescription { get; set; }
        [DefaultValue("String blogTittle")]
        public string BlogContent { get; set; } = null!;
    }
}
