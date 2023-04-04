﻿using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class Item
    {
        public Item()
        {
            CartDetails = new HashSet<CartDetail>();
            Comments = new HashSet<Comment>();
            OrderDetails = new HashSet<OrderDetail>();
            Reports = new HashSet<Report>();
            RequestDetails = new HashSet<RequestDetail>();
        }

        public int ItemId { get; set; }
        public string ItemCode { get; set; } = null!;
        public int UserId { get; set; }
        public int SubCategoryId { get; set; }
        public string ItemTitle { get; set; } = null!;
        public string ItemDetailedDescription { get; set; } = null!;
        public double ItemMass { get; set; }
        public bool ItemSize { get; set; }
        public string ItemQuanlity { get; set; } = null!;
        public double? ItemEstimateValue { get; set; }
        public double? ItemSalePrice { get; set; }
        public int ItemShareAmount { get; set; }
        public DateTime? ItemExpiredTime { get; set; }
        public string ItemShippingAddress { get; set; } = null!;
        public DateTime ItemDateCreated { get; set; }
        public DateTime? ItemDateUpdate { get; set; }
        public bool? ItemStatus { get; set; }
        public bool Share { get; set; }
        public string Image { get; set; } = null!;

        public virtual SubCategory SubCategory { get; set; } = null!;
        public virtual UserAccount User { get; set; } = null!;
        public virtual ICollection<CartDetail> CartDetails { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<RequestDetail> RequestDetails { get; set; }
    }
}
