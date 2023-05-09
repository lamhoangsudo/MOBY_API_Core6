using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class CreateSubCategoryVM
    {
        public int CategoryID { get; set; }
        [Required]
        public string? SubCategoryName { get; set; }
    }
}
