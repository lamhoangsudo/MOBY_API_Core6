﻿using System;
using System.Collections.Generic;

namespace MOBY_API_Core6.Models
{
    public partial class UserAccount
    {
        public UserAccount()
        {
            Babies = new HashSet<Baby>();
            Blogs = new HashSet<Blog>();
            Carts = new HashSet<Cart>();
            Comments = new HashSet<Comment>();
            Items = new HashSet<Item>();
            Orders = new HashSet<Order>();
            RecordPenaltyPoints = new HashSet<RecordPenaltyPoint>();
            RecordSearches = new HashSet<RecordSearch>();
            Replies = new HashSet<Reply>();
            Reports = new HashSet<Report>();
            TransationLogs = new HashSet<TransationLog>();
            UserAddresses = new HashSet<UserAddress>();
        }

        public int UserId { get; set; }
        public string UserCode { get; set; } = null!;
        public int RoleId { get; set; }
        public string UserName { get; set; } = null!;
        public string UserGmail { get; set; } = null!;
        public string UserAddress { get; set; } = null!;
        public string? UserPhone { get; set; }
        public bool? UserSex { get; set; }
        public DateTime UserDateOfBirth { get; set; }
        public string? UserMoreInformation { get; set; }
        public string UserImage { get; set; } = null!;
        public bool UserStatus { get; set; }
        public DateTime UserDateCreate { get; set; }
        public DateTime? UserDateUpdate { get; set; }
        public int Reputation { get; set; }
        public string? CardNumber { get; set; }
        public string? BankName { get; set; }
        public double? Balance { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Baby> Babies { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<RecordPenaltyPoint> RecordPenaltyPoints { get; set; }
        public virtual ICollection<RecordSearch> RecordSearches { get; set; }
        public virtual ICollection<Reply> Replies { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<TransationLog> TransationLogs { get; set; }
        public virtual ICollection<UserAddress> UserAddresses { get; set; }
    }
}
