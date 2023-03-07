using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class UpdateReportVM
    {
        public int reportID { get; set; }
        [Required]
        public string? content { get; set; }
        public string? image { get; set; }
    }
}
