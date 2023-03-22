using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MOBYContext context;

        public OrderRepository(MOBYContext context)
        {
            this.context = context;
        }

        public async Task<List<OrderBriefVM>> GetOrderByRecieverID(int uid, PaggingVM pagging, OrderStatusVM orderStatusVM)
        {
            int itemsToSkip = (pagging.pageNumber - 1) * pagging.pageSize;
            List<OrderBriefVM> listOrder = new List<OrderBriefVM>();
            if (orderStatusVM.OrderStatus == null)
            {
                listOrder = await context.Orders.Where(o => o.UserId == uid)
                .Include(o => o.User)
                .Include(o => o.Item)
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
                .Include(o => o.Item)
                .ThenInclude(i => i.User)
                .Skip(itemsToSkip)
                .Take(pagging.pageSize)
                .Select(o => OrderBriefVM.OrderToBriefVewModel(o))
                .ToListAsync();
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
            if (orderStatusVM.OrderStatus == null)
            {
                listOrder = await context.Orders
                .Include(o => o.User)
                .Include(o => o.Item)
                .ThenInclude(i => i.User)
                .Where(o => o.Item.UserId == uid)
                .Skip(itemsToSkip)
                .Take(pagging.pageSize)
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
                .Take(pagging.pageSize)
                .Select(o => OrderBriefVM.OrderToBriefVewModel(o))
                .ToListAsync();
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
                .Where(o => o.Item.UserId == uid)
                .CountAsync();
            }
            else
            {
                listOrderCount = await context.Orders
                .Include(o => o.Item)
                .Where(o => o.Item.UserId == uid && o.Status == orderStatusVM.OrderStatus)
                .CountAsync();
            }
            return listOrderCount;
        }


        public async Task<bool> checkOrderSharer(int uid)
        {
            List<Order> listOrder = await context.Orders
                .Include(o => o.User)
                .Include(o => o.Item)
                .ThenInclude(i => i.User)
                .Where(o => o.Item.UserId == uid && o.Status != 2 && o.Item.User.UserStatus == true)
                .ToListAsync();

            if (listOrder.Count == 0)
            {
                return true;
            }

            foreach (Order order in listOrder)
            {
                if (order.Status == 0 && order.DatePunishment == null)
                {
                    TimeSpan lateTimePackageFirstTime = (DateTime.Now - order.DateCreate);
                    if (Convert.ToInt32(lateTimePackageFirstTime.TotalDays) == 2)
                    {
                        if (order.Item.User.Reputation >= 1)
                        {
                            order.Item.User.Reputation -= 1;
                            order.DatePunishment = DateTime.Now;
                        }
                        if (order.Item.User.Reputation < 1)
                        {
                            order.Item.User.Reputation = 0;
                            order.Item.User.UserStatus = false;
                        }
                    }
                    else if (Convert.ToInt32(lateTimePackageFirstTime.TotalDays) > 2)
                    {
                        int latePackageMoreThan2Days = Convert.ToInt32(lateTimePackageFirstTime.TotalDays) - 2;
                        int totalMinus = +1 + 2 * (latePackageMoreThan2Days);
                        if (totalMinus >= 100 || totalMinus >= order.Item.User.Reputation)
                        {
                            order.Item.User.Reputation = 0;
                            order.Item.User.UserStatus = false;
                        }
                        order.Item.User.Reputation -= totalMinus;
                        order.DatePunishment = DateTime.Now;
                    }
                }
                else if (order.Status == 0 && order.DatePunishment != null)
                {
                    TimeSpan lateTimePackage = (TimeSpan)(DateTime.Now - order.DatePunishment);
                    if (Convert.ToInt32(lateTimePackage.TotalDays) >= 1)
                    {
                        int latePackageMoreThan1Day = Convert.ToInt32(lateTimePackage.TotalDays) - 1;
                        int totalMinus = +2 + 2 * (latePackageMoreThan1Day);

                        if (totalMinus >= 100 || totalMinus >= order.Item.User.Reputation)
                        {
                            order.Item.User.Reputation = 0;
                            order.Item.User.UserStatus = false;
                        }
                        order.Item.User.Reputation -= totalMinus;
                        order.DatePunishment = DateTime.Now;

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
                .Where(o => o.UserId == uid && o.Status != 2 && o.User.UserStatus == true)
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
                    if (Convert.ToInt32(daysAfterPackage.TotalDays) == 21)
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

        public async Task<String> CreateOrder(Request request)
        {
            Order newOrder = new Order();

            newOrder.UserId = request.UserId;
            newOrder.ItemId = request.ItemId;
            newOrder.Quanlity = request.ItemQuantity;
            newOrder.Price = request.Price;
            newOrder.Address = request.Address;
            newOrder.Note = request.Note;
            newOrder.Status = 0;
            newOrder.DateCreate = DateTime.Now;

            await context.Orders.AddAsync(newOrder);
            if (await context.SaveChangesAsync() != 0)
            {
                return "OrderID: " + newOrder.OrderId + "";
            }

            return "";
        }

        public async Task<Order?> GetOrderByOrderID(int orderID)
        {
            Order? order = await context.Orders.Where(o => o.OrderId == orderID)
                .Include(o => o.Item)
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
            }

            if (await context.SaveChangesAsync() != 0)
            {
                return true;
            }

            return false;
        }
    }
}
