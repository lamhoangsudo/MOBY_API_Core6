using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class UserAddress
    {
        public int Id { get; set; }
        public string Address { get; set; } = null!;
        public int UserId { get; set; }

        public virtual UserAccount User { get; set; } = null!;
    }
}
