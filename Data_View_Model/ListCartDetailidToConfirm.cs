using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class ListCartDetailidToConfirm
    {
        [DefaultValue("")]
        public String? address { get; set; }
        [DefaultValue("")]
        public String? note { get; set; }
        [Required]
        public string vnp_TransactionNo { get; set; }
        [Required]
        public string? vnp_CardType { get; set; }
        [Required]
        public string vnp_BankCode { get; set; }
        public List<int>? listCartDetailID { get; set; }
    }
}
