using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class CreateCategoryVM
    {
        [Required]
        public string? CategoryName { get; set; }
        [Required]
        public string? CategoryImage { get; set; }
    }
}
