﻿using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Data_View_Model
{
    public class OrderVM
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int ItemId { get; set; }
        public int Quanlity { get; set; }
        public string Address { get; set; } = null!;
        public int Status { get; set; }
        public string? ReasonDeny { get; set; }
        public string? Note { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DatePackage { get; set; }
        public DateTime? DateReceived { get; set; }
        public DateTime? DatePunishment { get; set; }
        public ItemVM? ItemVM { get; set; }
        public UserVM? UserVM { get; set; }

        public static OrderVM OrderToViewModel(Order order)
        {
            var orderVM = new OrderVM
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                ItemId = order.ItemId,
                Quanlity = order.Quanlity,
                Address = order.Address,
                Status = order.Status,
                ReasonDeny = order.ReasonDeny,
                Note = order.Note,
                DateCreate = order.DateCreate,
                DatePackage = order.DatePackage,
                DateReceived = order.DateReceived,
                DatePunishment = order.DatePunishment,
            };
            var item = order.Item;
            orderVM.ItemVM = ItemVM.ItemToViewModel(item);

            var user = order.User;
            orderVM.UserVM = UserVM.UserAccountToVewModel(user);


            return orderVM;
        }
    }
}
