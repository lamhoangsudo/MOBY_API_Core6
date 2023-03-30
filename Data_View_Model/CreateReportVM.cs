using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class CreateReportVM
    {
        [DefaultValue(null)]
        public int itemID { get; set; }
        [DefaultValue(null)]
        public int orderID { get; set; }
        [DefaultValue(null)]
        public int commentID { get; set; }
        [DefaultValue(null)]
        public int replyID { get; set; }
        [DefaultValue(null)]
        public int blogID { get; set; }
        [Required]
        public int userID { get; set; }
        [DefaultValue(1)]
        public int status { get; set; }
        [Required]
        public string? content { get; set; }
        [Required]
        public string? image { get; set; }
    }
}
