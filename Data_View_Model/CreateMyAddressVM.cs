﻿using System.ComponentModel.DataAnnotations;

namespace MOBY_API_Core6.Data_View_Model
{
    public class CreateMyAddressVM
    {
        [Required]
        public string? Address { get; set; }
    }
}
