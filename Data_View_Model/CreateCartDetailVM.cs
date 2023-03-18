using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class CreateCartDetailVM
    {
        [Required]
        public int CartId { get; set; }
        [Required]
        public int ItemId { get; set; }


    }
}
