using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class Blog
    {
        public Blog()
        {
            Comments = new HashSet<Comment>();
            Reports = new HashSet<Report>();
        }

        public int BlogId { get; set; }
        public int BlogCategoryId { get; set; }
        public int UserId { get; set; }
        public string BlogTitle { get; set; } = null!;
        public string? BlogDescription { get; set; }
        public string BlogContent { get; set; } = null!;
        public DateTime BlogDateCreate { get; set; }
        public DateTime? BlogDateUpdate { get; set; }
        public int? BlogStatus { get; set; }
        public string? ReasonDeny { get; set; }
        public string? Image { get; set; }

        public virtual BlogCategory BlogCategory { get; set; } = null!;
        public virtual UserAccount User { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
