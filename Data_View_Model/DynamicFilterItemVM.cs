﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class DynamicFilterItemVM
    {
        [DefaultValue(null)]
        public int categoryID { get; set; }
        [DefaultValue(null)]
        public string? titleName { get; set; }
        [DefaultValue(null)]
        public string? location { get; set; }
        [DefaultValue(0)]
        public float minPrice { get; set; }
        [DefaultValue(0)]
        public float maxPrice { get; set; }
        [DefaultValue(100)]
        public double maxUsable { get; set; }
        [DefaultValue(40)]
        public double minUsable { get; set; }
        [DefaultValue(null)]
        public bool? share { get; set; }
        [Required]
        public int pageNumber { get; set; }
        [Required]
        public int pageSize { get; set; }
    }
}