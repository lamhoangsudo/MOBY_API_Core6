﻿using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MOBYContext context;
        private readonly IEmailRepository emailDAO;
        public OrderRepository(MOBYContext context, IEmailRepository emailDAO)
        {
            this.context = context;
            this.emailDAO = emailDAO;
        }

        public async Task<bool> CreateOrder(int uid, String Address, String? note, List<RequestDetail> accteptedRequestDetail)
        {

            Order newOrder = new Order();
            newOrder.UserId = uid;
            newOrder.Address = Address;
            newOrder.Note = note;
            newOrder.DateCreate = DateTime.Now;

            List<OrderDetail> ListOrderDetail = accteptedRequestDetail.Select(rd => new OrderDetail
            {
                ItemId = rd.ItemId,
                Quantity = rd.Quantity,
                Price = rd.Price,

            }).ToList();
            newOrder.OrderDetails = ListOrderDetail;

            context.Orders.Add(newOrder);
            String? recieverGmail = await context.UserAccounts.Where(U => U.UserId == uid).Select(u => u.UserGmail).FirstOrDefaultAsync();
            if (await context.SaveChangesAsync() != 0)
            {
                if (recieverGmail != null)
                {
                    Email newEmail = new Email();
                    newEmail.To = recieverGmail;
                    newEmail.Subject = "your order has been created";
                    newEmail.Body = "your order has been created";
                    emailDAO.SendEmai(newEmail);
                }
                return true;
            }

            return false;
        }

        public async Task<List<OrderBriefVM>> GetOrderByRecieverID(int uid, PaggingVM pagging, OrderStatusVM orderStatusVM)
        {
            int itemsToSkip = (pagging.pageNumber - 1) * pagging.pageSize;
            List<OrderBriefVM> listOrder = new List<OrderBriefVM>();
            if (pagging.orderBy)
            {
                if (orderStatusVM.OrderStatus == null)
                {
                    listOrder = await context.Orders.Where(o => o.UserId == uid)
                    .Include(o => o.User)
                    .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Item)
                    .ThenInclude(i => i.User)
                    .Skip(itemsToSkip)
                    .Take(pagging.pageSize)
                    .OrderByDescending(o => o.OrderId)
                    .Select(o => OrderBriefVM.OrderToBriefVewModel(o))
                    .ToListAsync();
                }
                else
                {
                    listOrder = await context.Orders.Where(o => o.UserId == uid && o.Status == orderStatusVM.OrderStatus)
                    .Include(o => o.User)
                    .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Item)
                    .ThenInclude(i => i.User)
                    .Skip(itemsToSkip)
                    .Take(pagging.pageSize)
                    .OrderByDescending(o => o.OrderId)
                    .Select(o => OrderBriefVM.OrderToBriefVewModel(o))
                    .ToListAsync();
                }
            }
            else
            {
                if (orderStatusVM.OrderStatus == null)
                {
                    listOrder = await context.Orders.Where(o => o.UserId == uid)
                    .Include(o => o.User)
                    .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Item)
                    .ThenInclude(i => i.User)
                    .Skip(itemsToSkip)
                    .Take(pagging.pageSize)
                    .Select(o => OrderBriefVM.OrderToBriefVewModel(o))
                    .ToListAsync();
                }
                else
                {
                    listOrder = await context.Orders.Where(o => o.UserId == uid && o.Status == orderStatusVM.OrderStatus)
                    .Include(o => o.User)
                    .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Item)
                    .ThenInclude(i => i.User)
                    .Skip(itemsToSkip)
                    .Take(pagging.pageSize)
                    .Select(o => OrderBriefVM.OrderToBriefVewModel(o))
                    .ToListAsync();
                }
            }
            return listOrder;
        }

        public async Task<int> GetOrderByRecieverIDCount(int uid, OrderStatusVM orderStatusVM)
        {

            int listOrderCount;
            if (orderStatusVM.OrderStatus == null || orderStatusVM.OrderStatus.Equals(""))
            {
                listOrderCount = await context.Orders.Where(o => o.UserId == uid).CountAsync();
            }
            else
            {
                listOrderCount = await context.Orders.Where(o => o.UserId == uid && o.Status == orderStatusVM.OrderStatus).CountAsync();
            }
            return listOrderCount;
        }

        public async Task<List<OrderBriefVM>> GetOrderBySharerID(int uid, PaggingVM pagging, OrderStatusVM orderStatusVM)
        {
            int itemsToSkip = (pagging.pageNumber - 1) * pagging.pageSize;
            List<OrderBriefVM> listOrder = new List<OrderBriefVM>();
            if (pagging.orderBy)
            {
                if (orderStatusVM.OrderStatus == null)
                {
                    listOrder = await context.Orders
                    .Include(o => o.User)
                    .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Item)
                    .ThenInclude(i => i.User)
                    .Where(r => r.OrderDetails.Any(od => od.Item.UserId == uid))
                    .Skip(itemsToSkip)
                    .Take(pagging.pageSize)
                    .OrderByDescending(o => o.OrderId)
                    .Select(o => OrderBriefVM.OrderToBriefVewModel(o))
                    .ToListAsync();
                }
                else
                {
                    listOrder = await context.Orders
                    .Include(o => o.User)
                    .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Item)
                    .ThenInclude(i => i.User)
                    .Where(o => o.OrderDetails.Any(od => od.Item.UserId == uid) && o.Status == orderStatusVM.OrderStatus)
                    .Skip(itemsToSkip)
                    .Take(pagging.pageSize)
                    .OrderByDescending(o => o.OrderId)
                    .Select(o => OrderBriefVM.OrderToBriefVewModel(o))
                    .ToListAsync();
                }
            }
            else
            {
                if (orderStatusVM.OrderStatus == null)
                {
                    listOrder = await context.Orders
                    .Include(o => o.User)
                    .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Item)
                    .ThenInclude(i => i.User)
                    .Where(r => r.OrderDetails.Any(od => od.Item.UserId == uid))
                    .Skip(itemsToSkip)
                    .Take(pagging.pageSize)
                    .Select(o => OrderBriefVM.OrderToBriefVewModel(o))
                    .ToListAsync();
                }
                else
                {
                    listOrder = await context.Orders
                    .Include(o => o.User)
                    .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Item)
                    .ThenInclude(i => i.User)
                    .Where(o => o.OrderDetails.Any(od => od.Item.UserId == uid) && o.Status == orderStatusVM.OrderStatus)
                    .Skip(itemsToSkip)
                    .Take(pagging.pageSize)
                    .Select(o => OrderBriefVM.OrderToBriefVewModel(o))
                    .ToListAsync();
                }
            }
            return listOrder;
        }

        public async Task<int> GetOrderBySharerIDCount(int uid, OrderStatusVM orderStatusVM)
        {

            int listOrderCount;
            if (orderStatusVM.OrderStatus == null || orderStatusVM.OrderStatus.Equals(""))
            {
                listOrderCount = await context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Item)
                .ThenInclude(i => i.User)
                .Where(o => o.OrderDetails.Any(od => od.Item.UserId == uid))
                .CountAsync();
            }
            else
            {
                listOrderCount = await context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Item)
                .ThenInclude(i => i.User)
                .Where(o => o.OrderDetails.Any(od => od.Item.UserId == uid) && o.Status == orderStatusVM.OrderStatus)
                .CountAsync();
            }
            return listOrderCount;
        }


        public async Task<bool> checkOrderSharer(int uid)
        {
            List<Order> listOrder = await context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Item)
                .ThenInclude(i => i.User)
                .Where(o => o.OrderDetails.Any(od => (od.Item.UserId == uid) && (od.Item.User.UserStatus == true)))
                //Where(o => o.Item.UserId == uid && o.Status != 2 && o.Item.User.UserStatus == true)
                .ToListAsync();

            if (listOrder.Count == 0)
            {
                return true;
            }

            foreach (Order order in listOrder)
            {
                if (order.Status == 0)
                {
                    TimeSpan lateTimePackageTime = (DateTime.Now - order.DateCreate);
                    if (Convert.ToInt32(lateTimePackageTime.TotalDays) >= 7)
                    {
                        order.Status = 3;
                        if (order.OrderDetails.First().Item.User.Reputation <= 10)
                        {
                            order.OrderDetails.First().Item.User.Reputation = 0;
                            order.OrderDetails.First().Item.User.UserStatus = false;
                        }
                        else
                        {
                            order.OrderDetails.First().Item.User.Reputation -= 10;
                        }

                    }

                }

            }
            if (await context.SaveChangesAsync() != 0)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> checkOrderReciever(int uid)
        {
            List<Order> listOrder = await context.Orders
                .Include(o => o.User)
                .Where(o => o.UserId == uid && o.Status == 1 && o.User.UserStatus == true)
                .ToListAsync();

            if (listOrder.Count == 0)
            {
                return true;
            }

            foreach (Order order in listOrder)
            {
                if (order.Status == 1 && order.DatePackage != null)
                {
                    TimeSpan daysAfterPackage = (TimeSpan)(DateTime.Now - order.DatePackage);
                    if (Convert.ToInt32(daysAfterPackage.TotalDays) == 20)
                    {
                        order.Status = 2;
                    }
                }

            }
            if (await context.SaveChangesAsync() != 0)
            {
                return true;
            }

            return false;
        }



        public async Task<Order?> GetOrderByOrderID(int orderID)
        {
            Order? order = await context.Orders.Where(o => o.OrderId == orderID)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Item)
                .ThenInclude(i => i.User)
                .FirstOrDefaultAsync();

            return order;
        }

        public async Task<OrderVM?> GetOrderVMByOrderID(int orderID)
        {
            OrderVM? order = await context.Orders.Where(o => o.OrderId == orderID)
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Item)
                .ThenInclude(i => i.User)
                .Select(o => OrderVM.OrderToViewModel(o))
                .FirstOrDefaultAsync();

            return order;
        }

        public async Task<bool> UpdateStatusOrder(Order order, int status)
        {
            if (status == 1)
            {
                order.Status = status;
                order.DatePackage = DateTime.Now;
            }
            if (status == 2)
            {
                order.Status = status;
                order.DateReceived = DateTime.Now;
            }

            if (await context.SaveChangesAsync() != 0)
            {
                return true;
            }

            return false;
        }
    }
}
