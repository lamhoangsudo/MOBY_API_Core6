using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class @bool
    {
        public @bool()
        {
            Items = new HashSet<Item>();
            Requests = new HashSet<Request>();
        }

        public int ImageId { get; set; }
        public string ImageLink1 { get; set; } = null!;
        public string ImageLink2 { get; set; } = null!;
        public string ImageLink3 { get; set; } = null!;
        public string? ImageLink4 { get; set; }
        public string? ImageLink5 { get; set; }

        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}
