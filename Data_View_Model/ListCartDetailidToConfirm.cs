using System.ComponentModel;

namespace MOBY_API_Core6.Data_View_Model
{
    public class ListCartDetailidToConfirm
    {
        [DefaultValue(null)]
        public String? address { get; set; }
        [DefaultValue(null)]
        public String? note { get; set; }
        public List<int>? listCartDetailID { get; set; }
    }
}
