using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class Comment
    {
        public Comment()
        {
            Replies = new HashSet<Reply>();
            Reports = new HashSet<Report>();
        }

        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int? BlogId { get; set; }
        public int? ItemId { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public string CommentContent { get; set; } = null!;

        public virtual Blog? Blog { get; set; }
        public virtual Item? Item { get; set; }
        public virtual UserAccount User { get; set; } = null!;
        public virtual ICollection<Reply> Replies { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
