using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class Request
    {
        public Request()
        {
            RequestDetails = new HashSet<RequestDetail>();
        }

        public int RequestId { get; set; }
        public int UserId { get; set; }
        public string Address { get; set; } = null!;
        public string? Note { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateChangeStatus { get; set; }
        public int Status { get; set; }

        public virtual UserAccount User { get; set; } = null!;
        public virtual ICollection<RequestDetail> RequestDetails { get; set; }
    }
}
