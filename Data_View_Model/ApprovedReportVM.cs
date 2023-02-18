using System.ComponentModel;

namespace MOBY_API_Core6.Data_View_Model
{
    public class ApprovedReportVM
    {
        public int reportID { get; set; }
        [DefaultValue (true)]
        public bool isApproved { get; set; }
    }
}
