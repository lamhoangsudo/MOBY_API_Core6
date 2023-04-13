using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class RecordPenaltyPoint
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ObjReport { get; set; }
        public int Type { get; set; }
        public int PenaltyPoint { get; set; }
        public string ReasonDeductionOfPoints { get; set; } = null!;

        public virtual UserAccount User { get; set; } = null!;
    }
}
