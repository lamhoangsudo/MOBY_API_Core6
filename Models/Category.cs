using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class Category
    {
        public Category()
        {
            SubCategories = new HashSet<SubCategory>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? CategoryImage { get; set; }
        public bool CategoryStatus { get; set; }

        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
