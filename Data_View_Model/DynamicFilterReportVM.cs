using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class DynamicFilterReportVM
    {
        [Required]
        public int PageNumber { get; set; }
        [Required]
        public int PageSize { get; set; }
        [DefaultValue(null)]
        [ReadOnly(true)]
        public int? UserID { get; set; }
        [DefaultValue(false)]
        public bool IsItem { get; set; }
        [DefaultValue(null)]
        public int? ItemID { get; set; }
        [DefaultValue(false)]
        public bool IsOrder { get; set; }
        [DefaultValue(null)]
        public int? OrderID { get; set; }
        [DefaultValue(false)]
        public bool IsComment { get; set; }
        [DefaultValue(null)]
        public int? CommentId { get; set; }
        [DefaultValue(false)]
        public bool IsReply { get; set; }
        [DefaultValue(null)]
        public int? ReplyId { get; set; }
        [DefaultValue(false)]
        public bool IsBlog { get; set; }
        [DefaultValue(null)]
        public int? BlogId { get; set; }
        [DefaultValue(null)]
        public int? Status { get; set;}
        [DefaultValue(null)]
        public string? Title { get; set; }
        [DefaultValue(true)]
        public bool? OrderByDateCreate { get; set; }
        [DefaultValue(false)]
        public bool? OrderByDateResolve { get; set; }
        [DefaultValue(null)]
        public DateTime? MinDateCreate { get; set; }
        [DefaultValue(null)]
        public DateTime? MaxDateCreate { get; set; }
        [DefaultValue(null)]
        public DateTime? MinDateResolve { get; set; }
        [DefaultValue(null)]
        public DateTime? MaxDateResolve { get; set; }
        [DefaultValue(true)]
        public bool AscendingOrDescending { get; set; }
    }
}
