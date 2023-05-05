using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class Order
    {
        public Order()
        {
            Reports = new HashSet<Report>();
        }

        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int ItemId { get; set; }
        public string Address { get; set; } = null!;
        public string? Note { get; set; }
        public int Status { get; set; }
        public string? ReasonCancel { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DatePackage { get; set; }
        public DateTime? DateReceived { get; set; }
        public DateTime? DateCancel { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string? TransactionNo { get; set; }
        public string? CardType { get; set; }
        public string? BankCode { get; set; }
        public string? TransactionDate { get; set; }

        public virtual Item Item { get; set; } = null!;
        public virtual UserAccount User { get; set; } = null!;
        public virtual ICollection<Report> Reports { get; set; }
    }
}
