using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class Banner
    {
        public int BannerId { get; set; }
        public string? BannerLink { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public string? Image { get; set; }
    }
}
