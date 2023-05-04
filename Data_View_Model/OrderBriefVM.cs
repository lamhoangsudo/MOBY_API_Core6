﻿using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Data_View_Model
{
    public class OrderBriefVM
    {

        public int OrderId { get; set; }
        public int Status { get; set; }
        public DateTime DateCreate { get; set; }

        public UserVM? UserRecieverVM { get; set; }
        public UserVM? UserSharerVM { get; set; }
        public ItemVMForOrderBriefVM? itemVM { get; set; }

        public static OrderBriefVM OrderToBriefVewModel(Order order)
        {
            var orderBriefVM = new OrderBriefVM
            {
                OrderId = order.OrderId,
                DateCreate = order.DateCreate,

                Status = order.Status,
            };


            var userOwner = order.Item.User;
            orderBriefVM.UserSharerVM = UserVM.UserAccountToVewModel(userOwner);

            var user = order.User;
            orderBriefVM.UserRecieverVM = UserVM.UserAccountToVewModel(user);

            var item = order.Item;
            orderBriefVM.itemVM = ItemVMForOrderBriefVM.ItemForOrderToViewModel(item);
            return orderBriefVM;
        }
    }
}