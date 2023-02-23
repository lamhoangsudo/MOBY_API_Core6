using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class DenyReportVM
    {
        [Required]
        public int reportID { get; set; }
        [DefaultValue(1)]
        [ReadOnly(true)]
        public int isDeny { get; set; }
    }
}
