using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class DynamicFilterReportVM
    {
        [Required]
        public int pageNumber { get; set; }
        [Required]
        public int pageSize { get; set; }
        [DefaultValue(false)]
        public bool isItem { get; set; }
        [DefaultValue(null)]
        public int? itemID { get; set; }
        [DefaultValue(false)]
        public bool isOrder { get; set; }
        [DefaultValue(null)]
        public int? orderID { get; set; }
        [DefaultValue(false)]
        public bool isComment { get; set; }
        [DefaultValue(null)]
        public int? commentId { get; set; }
        [DefaultValue(false)]
        public bool isReply { get; set; }
        [DefaultValue(null)]
        public int? replyId { get; set; }
        [DefaultValue(false)]
        public bool isBlog { get; set; }
        [DefaultValue(null)]
        public int? blogId { get; set; }
        [DefaultValue(null)]
        public int? status { get; set;}
        [DefaultValue(null)]
        public string? title { get; set; }
        [DefaultValue(true)]
        public bool? orderByDateCreate { get; set; }
        [DefaultValue(false)]
        public bool? orderByDateResolve { get; set; }
        [DefaultValue(null)]
        public DateTime? minDateCreate { get; set; }
        [DefaultValue(null)]
        public DateTime? maxDateCreate { get; set; }
        [DefaultValue(null)]
        public DateTime? minDateResolve { get; set; }
        [DefaultValue(null)]
        public DateTime? maxDateResolve { get; set; }
    }
}
