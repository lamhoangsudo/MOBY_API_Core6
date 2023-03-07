using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class CreateReportVM
    {
        public int itemID { get; set; }
        public int userID { get; set; }
        [DefaultValue(1)]
        public int status { get; set; }
        [Required]
        public string? content { get; set; }
        public string? image { get; set; }
    }
}
