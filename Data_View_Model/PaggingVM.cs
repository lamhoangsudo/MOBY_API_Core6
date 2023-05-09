using System.ComponentModel;

namespace MOBY_API_Core6.Data_View_Model
{
    public class PaggingVM
    {
        [DefaultValue(1)]
        public int PageNumber { get; set; }
        [DefaultValue(5)]
        public int PageSize { get; set; }
        [DefaultValue(true)]
        public bool OrderBy { get; set; }
    }
}
