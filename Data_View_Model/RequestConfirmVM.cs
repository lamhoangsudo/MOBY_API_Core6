using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class RequestConfirmVM
    {
        [Required]
        public int RequestID { get; set; }

        public List<int> ListRequestDetailID { get; set; } = new List<int>();
    }
}
