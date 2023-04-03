using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Data_View_Model
{
    public class ListVM
    {
        public int total { get; set; }
        public int totalPage { get; set; }
        public List<BriefItem>? briefItems { get; set; }
    }
}
