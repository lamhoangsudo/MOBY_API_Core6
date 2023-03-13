using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class RequestDetailIdVM
    {
        [Required]
        public int CartDetailId { get; set; }
    }
}
