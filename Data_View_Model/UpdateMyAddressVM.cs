using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class UpdateMyAddressVM
    {
        [Required]
        public int UserAddressID { get; set; }
        public string Address { get; set; } = null!;
    }
}
