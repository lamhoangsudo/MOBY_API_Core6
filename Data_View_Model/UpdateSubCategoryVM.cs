using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class UpdateSubCategoryVM
    {
        [Required]
        public int SubCategoryID { get; set; }
        [Required]
        public string? SubCategoryName { get; set; }
        [Required]
        public int CategoryID { get; set; }
    }
}
