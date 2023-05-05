using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;

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
                    .Include(o => o.Item)
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
                    .Include(o => o.Item)
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
                    .Include(o => o.Item)
                    .ThenInclude(i => i.User)
                    .Where(r => r.Item.UserId == uid)
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
                    .Include(o => o.Item)
                    .ThenInclude(i => i.User)
                    .Where(o => o.Item.UserId == uid && o.Status == orderStatusVM.OrderStatus)
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
                    .Include(o => o.Item)
                    .ThenInclude(i => i.User)
                    .Where(r => r.Item.UserId == uid)
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


        /*public async Task<bool> checkOrderSharer(int uid)
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
        }*/

        /*public async Task<bool> checkOrderReciever(int uid)
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
        }*/



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

        public async Task<bool> cancelOrder(Order order, string reasonCancel, int uid, bool pernament)
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
                if (order.User.Reputation <= 3)
                {
                    order.User.Reputation = 0;
                    order.User.UserStatus = false;
                }
                else
                {
                    order.User.Reputation -= 3;
                }

            }
            await context.SaveChangesAsync();
            return true;
        }
    }
}
