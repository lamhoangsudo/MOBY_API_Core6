using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MOBY_API_Core6.Data_View_Model
{
    public class CreateReportVM
    {
        [DefaultValue(null)]
        public int? itemID { get; set; }
        [DefaultValue(null)]
        public int? orderID { get; set; }
        [DefaultValue(null)]
        public int? commentID { get; set; }
        [DefaultValue(null)]
        public int? replyID { get; set; }
        [DefaultValue(null)]
        public int? blogID { get; set; }
        [ReadOnly(true)]
        public int userID { get; set; }
        [Required]
        public string? title { get; set; }
        [DefaultValue(0)]
        public int status { get; set; }
        [Required]
        public string? content { get; set; }
        [Required]
        public string? image { get; set; }
    }
}
