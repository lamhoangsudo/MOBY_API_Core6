﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class BannerVM
    {
        public int Id { get; set; }
        public string? Link { get; set; }
        public string? Image { get; set; }
        public DateTime? DateCreate { get; set; }
        public DateTime? DateUpdate { get; set; }
    }
}
