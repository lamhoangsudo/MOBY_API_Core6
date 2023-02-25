using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class Reply
    {
        public int ReplyId { get; set; }
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public string ReplyContent { get; set; } = null!;

        public virtual Comment Comment { get; set; } = null!;
        public virtual UserAccount User { get; set; } = null!;
    }
}
