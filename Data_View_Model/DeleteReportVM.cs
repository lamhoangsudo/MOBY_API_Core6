using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class DeleteReportVM
    {
        [Required]
        public int ReportID { get; set; }
    }
}
