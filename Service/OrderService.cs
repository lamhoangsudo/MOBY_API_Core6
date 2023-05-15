﻿using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Service
{
    public class OrderService : IOrderService
    {
        private readonly MOBYContext context;
        private readonly IOrderRepository orderRepository;
        public OrderService(MOBYContext context, IOrderRepository orderRepository)
        {
            this.context = context;
            this.orderRepository = orderRepository;
        }

        public async Task<List<OrderBriefVM>> GetOrderByRecieverID(int uid, PaggingVM pagging, OrderStatusVM orderStatusVM)
        {
            List<OrderBriefVM> listOrder = await orderRepository.GetOrderByRecieverID(uid, pagging, orderStatusVM);
            return listOrder;
        }

        public async Task<int> GetOrderByRecieverIDCount(int uid, OrderStatusVM orderStatusVM)
        {
            return await orderRepository.GetOrderByRecieverIDCount(uid, orderStatusVM);
        }

        public async Task<List<OrderBriefVM>> GetOrderBySharerID(int uid, PaggingVM pagging, OrderStatusVM orderStatusVM)
        {
            List<OrderBriefVM> listOrder = await orderRepository.GetOrderBySharerID(uid, pagging, orderStatusVM);
            return listOrder;
        }

        public async Task<int> GetOrderBySharerIDCount(int uid, OrderStatusVM orderStatusVM)
        {
            return await orderRepository.GetOrderBySharerIDCount(uid, orderStatusVM);
        }

        public async Task<Order?> GetOrderByOrderID(int orderID)
        {
            Order? order = await orderRepository.GetOrderByOrderID(orderID);
            return order;
        }

        public async Task<OrderVM?> GetOrderVMByOrderID(int orderID)
        {
            OrderVM? order = await orderRepository.GetOrderVMByOrderID(orderID);
            return order;
        }

        public async Task<bool> UpdateStatusOrder(Order order, int status)
        {
            return await orderRepository.UpdateStatusOrder(order, status);
        }

        public async Task<bool> CancelOrder(Order order, string reasonCancel, int uid, bool pernament)
        {
            return await orderRepository.CancelOrder(order, reasonCancel, uid, pernament);
        }
    }
}
