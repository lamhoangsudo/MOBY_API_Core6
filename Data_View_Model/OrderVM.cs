﻿using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Data_View_Model
{
    public class OrderVM
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string Address { get; set; } = null!;
        public string? Note { get; set; }
        public int Status { get; set; }
        public string? ReasonCancel { get; set; }
        public bool? AllowReport { get; set; } = false;
        public int? daysLeftForReport { get; set; }
        public int? daysLeftForCancel { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DatePackage { get; set; }
        public DateTime? DateReceived { get; set; }
        public string? TransactionNo { get; set; }
        public string? CardType { get; set; }
        public string? BankCode { get; set; }

        public UserVM? UserSharerVM { get; set; }
        public UserVM? UserRecieverVM { get; set; }
        public ItemVM? itemVM { get; set; }


        public static OrderVM OrderToViewModel(Order order)
        {
            var orderVM = new OrderVM
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                Address = order.Address,
                Status = order.Status,
                ReasonCancel = order.ReasonCancel,
                Note = order.Note,
                DateCreate = order.DateCreate,
                DatePackage = order.DatePackage,
                DateReceived = order.DateReceived,
                TransactionNo = order.TransactionNo,
                CardType = order.CardType,
                BankCode = order.BankCode,
            };
            if (order.Status == 1 && order.DatePackage != null)
            {
                TimeSpan totalDays = DateTime.Now - order.DatePackage.Value;
                int totalDaysint = Convert.ToInt32(totalDays.TotalDays);
                if (totalDaysint < 14)
                {
                    orderVM.daysLeftForReport = 14 - totalDaysint;
                }
                if (totalDaysint >= 14)
                {
                    orderVM.AllowReport = true;
                }
            }
            if (order.Status == 0)
            {
                TimeSpan totalDays = DateTime.Now - order.DateCreate;
                int totalDaysint = Convert.ToInt32(totalDays.TotalDays);
                orderVM.daysLeftForCancel = 7 - totalDaysint;
            }

            var userSharer = order.Item.User;
            orderVM.UserSharerVM = UserVM.UserAccountToVewModel(userSharer);
            var userReciever = order.User;
            orderVM.UserRecieverVM = UserVM.UserAccountToVewModel(userReciever);
            var item = order.Item;
            orderVM.itemVM = ItemVM.ItemForOrderToViewModel(item);

            return orderVM;
        }
    }
}
