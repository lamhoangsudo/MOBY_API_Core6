using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Log4Net;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;

namespace MOBY_API_Core6.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MOBYContext context;
        private readonly Logger4Net _logger4Net;
        public OrderRepository(MOBYContext context)
        {
            this.context = context;
            _logger4Net = new Logger4Net();
        }
        public async Task<List<OrderBriefVM>> GetOrderByRecieverID(int uid, PaggingVM pagging, OrderStatusVM orderStatusVM)
        {
            int itemsToSkip = (pagging.PageNumber - 1) * pagging.PageSize;
            List<OrderBriefVM> listOrder = new();
            if (pagging.OrderBy)
            {
                if (orderStatusVM.OrderStatus == null)
                {
                    listOrder = await context.Orders.Where(o => o.UserId == uid)
                    .Include(o => o.User)
                    .Include(o => o.Item)
                    .ThenInclude(i => i.User)
                    .Skip(itemsToSkip)
                    .Take(pagging.PageSize)
                    .OrderByDescending(o => o.OrderId)
                    .Select(o => OrderBriefVM.OrderToBriefVewModel(o))
                    .ToListAsync();
                }
                else
                {
                    listOrder = await context.Orders.Where(o => o.UserId == uid && o.Status == orderStatusVM.OrderStatus)
                    .Include(o => o.User)
                    .Include(o => o.Item)
                    .ThenInclude(i => i.User)
                    .Skip(itemsToSkip)
                    .Take(pagging.PageSize)
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
                    .Include(o => o.Item)
                    .ThenInclude(i => i.User)
                    .Skip(itemsToSkip)
                    .Take(pagging.PageSize)
                    .Select(o => OrderBriefVM.OrderToBriefVewModel(o))
                    .ToListAsync();
                }
                else
                {
                    listOrder = await context.Orders.Where(o => o.UserId == uid && o.Status == orderStatusVM.OrderStatus)
                    .Include(o => o.User)
                    .Include(o => o.Item)
                    .ThenInclude(i => i.User)
                    .Skip(itemsToSkip)
                    .Take(pagging.PageSize)
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

        public async Task<List<Order>> GetShippingOrder(int uid)
        {
            List<Order> listShippingOrder = await context.Orders
                    .Include(o => o.Reports)
                    .ThenInclude(r => r.User)
                    .Include(o => o.Item)
                    .ThenInclude(i => i.User)
                    .Where(o => o.Item.UserId == uid && o.Status == 1)
                    .ToListAsync();
            return listShippingOrder;
        }

        public async Task<List<OrderBriefVM>> GetOrderBySharerID(int uid, PaggingVM pagging, OrderStatusVM orderStatusVM)
        {
            int itemsToSkip = (pagging.PageNumber - 1) * pagging.PageSize;
            List<OrderBriefVM> listOrder = new();
            if (pagging.OrderBy)
            {
                if (orderStatusVM.OrderStatus == null)
                {
                    listOrder = await context.Orders
                    .Include(o => o.User)
                    .Include(o => o.Item)
                    .ThenInclude(i => i.User)
                    .Where(r => r.Item.UserId == uid)
                    .Skip(itemsToSkip)
                    .Take(pagging.PageSize)
                    .OrderByDescending(o => o.OrderId)
                    .Select(o => OrderBriefVM.OrderToBriefVewModel(o))
                    .ToListAsync();
                }
                else
                {
                    listOrder = await context.Orders
                    .Include(o => o.User)
                    .Include(o => o.Item)
                    .ThenInclude(i => i.User)
                    .Where(o => o.Item.UserId == uid && o.Status == orderStatusVM.OrderStatus)
                    .Skip(itemsToSkip)
                    .Take(pagging.PageSize)
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
                    .Include(o => o.Item)
                    .ThenInclude(i => i.User)
                    .Where(r => r.Item.UserId == uid)
                    .Skip(itemsToSkip)
                    .Take(pagging.PageSize)
                    .Select(o => OrderBriefVM.OrderToBriefVewModel(o))
                    .ToListAsync();
                }
                else
                {
                    listOrder = await context.Orders
                    .Include(o => o.User)
                    .Include(o => o.Item)
                    .ThenInclude(i => i.User)
                    .Where(o => o.Item.UserId == uid && o.Status == orderStatusVM.OrderStatus)
                    .Skip(itemsToSkip)
                    .Take(pagging.PageSize)
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
                .Include(o => o.Item)
                .ThenInclude(i => i.User)
                .Where(o => o.Item.UserId == uid)
                .CountAsync();
            }
            else
            {
                listOrderCount = await context.Orders
                .Include(o => o.Item)

                .ThenInclude(i => i.User)
                .Where(o => o.Item.UserId == uid && o.Status == orderStatusVM.OrderStatus)
                .CountAsync();
            }
            return listOrderCount;
        }

        public async Task<Order?> GetOrderByOrderID(int orderID)
        {
            Order? order = await context.Orders.Where(o => o.OrderId == orderID)
                .Include(o => o.User)
                .Include(od => od.Item)
                .ThenInclude(i => i.User)
                .FirstOrDefaultAsync();
            return order;
        }

        public async Task<OrderVM?> GetOrderVMByOrderID(int orderID)
        {
            OrderVM? order = await context.Orders.Where(o => o.OrderId == orderID)
                .Include(o => o.User)
                .Include(o => o.Item)
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
                if (order.Item.User.Reputation > 90)
                {
                    order.Item.User.Reputation = 100;
                }
                else
                {
                    order.Item.User.Reputation += 10;
                }
                if (order.Item.User.Balance == null)
                {
                    order.Item.User.Balance = order.Quantity * order.Price;
                }
                else
                {
                    order.Item.User.Balance += order.Quantity * order.Price;
                }
            }
            if (await context.SaveChangesAsync() != 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> CancelOrder(Order order, string reasonCancel, int uid, bool pernament)
        {
            try
            {
                if (order.UserId != uid)
                {
                    return false;
                }
                order.Status = 3;
                order.DateCancel = DateTime.Now;
                order.ReasonCancel = reasonCancel;
                if (pernament)
                {
                    if (order.User.Reputation <= 5)
                    {
                        order.User.Reputation = 0;
                        order.User.UserStatus = false;
                    }
                    else
                    {
                        order.User.Reputation -= 5;
                    }

                }
                order.User.Balance += (order.Price * order.Quantity);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return false;
            }
        }
    }
}
