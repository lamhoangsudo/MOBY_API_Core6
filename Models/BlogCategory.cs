using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class BlogCategory
    {
        public BlogCategory()
        {
            Blogs = new HashSet<Blog>();
        }

        public int BlogCategoryId { get; set; }
        public string BlogCategoryName { get; set; } = null!;
        public string? Status { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; }
    }
}
