using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class ViewBlog
    {
        public DateTime? BlogDateUpdate { get; set; }
        public DateTime BlogDateCreate { get; set; }
        public string BlogContent { get; set; } = null!;
        public string? BlogDescription { get; set; }
        public string BlogTitle { get; set; } = null!;
        public int BlogId { get; set; }
        public int BlogCategoryId { get; set; }
        public string BlogCategoryName { get; set; } = null!;
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public int? BlogStatus { get; set; }
    }
}
