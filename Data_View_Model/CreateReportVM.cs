using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MOBY_API_Core6.Data_View_Model
{
    public class CreateReportVM
    {
        [DefaultValue(null)]
        public int? ItemID { get; set; }
        [DefaultValue(null)]
        public int? OrderID { get; set; }
        [DefaultValue(null)]
        public int? CommentID { get; set; }
        [DefaultValue(null)]
        public int? ReplyID { get; set; }
        [DefaultValue(null)]
        public int? BlogID { get; set; }
        //[ReadOnly(true)]
        public int UserID { get; set; }
        [Required]
        public string? Title { get; set; }
        [DefaultValue(0)]
        [ReadOnly(true)]
        public int Status { get; set; }
        [Required]
        public string? Content { get; set; }
        [Required]
        public string? Image { get; set; }
        [Required]
        public int Type { get; set; }
    }
}
