﻿using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public interface IOrderRepository
    {
        public Task<List<OrderBriefVM>> GetOrderByRecieverID(int uid, PaggingVM pagging, OrderStatusVM orderStatusVM);
        public Task<List<OrderBriefVM>> GetOrderBySharerID(int uid, PaggingVM pagging, OrderStatusVM orderStatusVM);
        public Task<bool> checkOrderSharer(int uid);
        public Task<bool> checkOrderReciever(int uid);
        public Task<bool> CreateOrder(Request request);
        public Task<Order?> GetOrderByOrderID(int orderID);
        public Task<OrderVM?> GetOrderVMByOrderID(int orderID);
        public Task<bool> UpdateStatusOrder(Order order, int status);

    }
}
