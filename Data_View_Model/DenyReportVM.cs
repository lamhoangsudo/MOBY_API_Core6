﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class DenyReportVM
    {
        [Required]
        public int ReportID { get; set; }
        [Required]
        public string? Reason { get; set; }
    }
}
