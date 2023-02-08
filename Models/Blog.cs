﻿using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class Blog
    {
        public int BlogId { get; set; }
        public int BlogCategoryId { get; set; }
        public int UserId { get; set; }
        public string BlogTitle { get; set; } = null!;
        public string? BlogDescription { get; set; }
        public string BlogContent { get; set; } = null!;
        public DateTime BlogDateCreate { get; set; }
        public DateTime? BlogDateUpdate { get; set; }
        public bool? BlogStatus { get; set; }

        public virtual BlogCategory BlogCategory { get; set; } = null!;
        public virtual UserAccount User { get; set; } = null!;
    }
}
