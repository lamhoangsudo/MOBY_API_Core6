using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class CreateSubCategoryVM
    {
        public int categoryID { get; set; }
        [Required]
        public string? subCategoryName { get; set; }
    }
}
