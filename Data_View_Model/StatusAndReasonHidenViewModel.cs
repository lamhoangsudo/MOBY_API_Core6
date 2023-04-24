using System.ComponentModel;

namespace MOBY_API_Core6.Data_View_Model
{
    public class StatusAndReasonHidenViewModel
    {
        [ReadOnly(true)]
        public int? Id { get; set; }
        [DefaultValue(null)]
        public bool? StatusBit { get; set; }
        [DefaultValue(null)]
        public int? StatusInt { get; set; }
        [ReadOnly(true)]
        public string ReasonHiden { get; set; } = string.Empty;
    }
}
