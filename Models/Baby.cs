using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class Baby
    {
        public int Idbaby { get; set; }
        public int UserId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool Sex { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }

        public virtual UserAccount User { get; set; } = null!;
    }
}
