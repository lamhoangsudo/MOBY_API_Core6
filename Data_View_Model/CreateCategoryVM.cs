using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class CreateCategoryVM
    {
        [Required]
        public string? categoryName { get; set; }
        [Required]
        public string? categoryImage { get; set; }
    }
}
