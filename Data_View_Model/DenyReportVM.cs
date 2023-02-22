using System.ComponentModel;

namespace MOBY_API_Core6.Data_View_Model
{
    public class DenyReportVM
    {
        public int reportID { get; set; }
        [DefaultValue(0)]
        public int isDeny { get; set; }
    }
}
