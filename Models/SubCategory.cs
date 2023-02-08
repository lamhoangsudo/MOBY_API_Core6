using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class SubCategory
    {
        public SubCategory()
        {
            Items = new HashSet<Item>();
            Requests = new HashSet<Request>();
        }

        public int SubCategoryId { get; set; }
        public int CategoryId { get; set; }
        public string SubCategoryName { get; set; } = null!;
        public bool SubCategoryStatus { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}
