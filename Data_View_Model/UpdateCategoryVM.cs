using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class UpdateCategoryVM
    {
        public int categoryID { get; set; }
        [Required]
        public string? categoryName { get; set; }
        [Required]
        public string? categoryImage { get; set; }
    }
}
