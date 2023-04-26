using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MOBY_API_Core6.Models
{
    public partial class RecordSearch
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [DefaultValue(1)]
        public int Count { get; set; }
        public int? CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public string? TitleName { get; set; }

        public virtual UserAccount User { get; set; } = null!;
    }
}
