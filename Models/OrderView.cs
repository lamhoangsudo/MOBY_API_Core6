using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class OrderView
    {
        public int OrderId { get; set; }
        public DateTime DateCreate { get; set; }
        public int OrderDetailId { get; set; }
        public int Quanlity { get; set; }
        public string Address { get; set; } = null!;
        public int Status { get; set; }
        public bool SponsoredOrderShippingFee { get; set; }
        public string ItemTitle { get; set; } = null!;
        public double? ItemSalePrice { get; set; }
        public string ItemQuanlity { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string? UserPhone { get; set; }
        public string UserGmail { get; set; } = null!;
        public string UserAddress { get; set; } = null!;
        public string? Note { get; set; }
        public int ItemId { get; set; }
        public int UserId { get; set; }
        public bool? ItemSponsoredOrderShippingFee { get; set; }
        public string Image { get; set; } = null!;
    }
}
