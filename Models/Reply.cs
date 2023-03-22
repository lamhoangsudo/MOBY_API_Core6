using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class Reply
    {
        public Reply()
        {
            Reports = new HashSet<Report>();
        }

        public int ReplyId { get; set; }
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public string ReplyContent { get; set; } = null!;

        public virtual Comment Comment { get; set; } = null!;
        public virtual UserAccount User { get; set; } = null!;
        public virtual ICollection<Report> Reports { get; set; }
    }
}
