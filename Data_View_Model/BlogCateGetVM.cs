using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class BlogCateGetVM
    {
        [Required]
        public int BlogCategoryId { get; set; }
    }
}
