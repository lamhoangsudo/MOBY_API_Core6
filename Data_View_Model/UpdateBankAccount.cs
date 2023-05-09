using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class UpdateBankAccount
    {
        [Required]
        public string CardNumber { get; set; }

        [Required]
        public string BankName { get; set; }

    }
}
