using System.ComponentModel;

namespace MOBY_API_Core6.Data_View_Model
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class ListCartDetailidToConfirm
    {
        [DefaultValue("")]
        public string? Address { get; set; }
        [DefaultValue("")]
        public string? Note { get; set; }
        [DefaultValue("")]
        public string? Vnp_TransactionNo { get; set; }
        [DefaultValue("")]
        public string? Vnp_CardType { get; set; }
        [DefaultValue("")]
        public string? Vnp_BankCode { get; set; }
        [DefaultValue("")]
        public string? TransactionDate { get; set; }
        public List<int>? ListCartDetailID { get; set; }
    }
}
