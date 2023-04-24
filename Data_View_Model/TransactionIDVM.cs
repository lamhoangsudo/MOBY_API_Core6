using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class TransactionIDVM
    {
        [Required]
        public int TransactionId { get; set; }
    }
}
