using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class UpdateBlogCategoryVM
    {
        [Required]
        public int BlogCategoryId { get; set; }
        [Required]
        public string BlogCategoryName { get; set; } = null!;
    }
}
