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

        public async Task<List<Order>> GetOrderByRecieverID(int uid, PaggingVM pagging)
        {
            int itemsToSkip = (pagging.pageNumber - 1) * pagging.pageSize;
            List<Order> listOrder = await context.Orders.Where(o => o.UserId == uid)
                .Include(o => o.User)
                .Include(o => o.Item)
                .Skip(itemsToSkip)
                .Take(pagging.pageSize)
                .ToListAsync();
            return listOrder;
        }

        public async Task<List<Order>> GetOrderBySharerID(int uid, PaggingVM pagging)
        {
            int itemsToSkip = (pagging.pageNumber - 1) * pagging.pageSize;
            List<Order> listOrder = await context.Orders
                .Where(o => o.Item.UserId == uid)
                .Include(o => o.User)
                .Include(o => o.Item)
                .Skip(itemsToSkip)
                .Take(pagging.pageSize)
                .ToListAsync();
            return listOrder;
        }

        public async Task<bool> CreateOrder(Request request)
        {
            Order newOrder = new Order();

            newOrder.UserId = request.UserId;
            newOrder.ItemId = request.ItemId;
            newOrder.Quanlity = request.ItemQuantity;
            newOrder.Address = request.Address;
            newOrder.Note = request.Note;
            newOrder.Status = 0;
            newOrder.DateCreate = DateTime.Now;

            await context.Orders.AddAsync(newOrder);
            if (await context.SaveChangesAsync() != 0)
            {
                return true;
            }

            return false;
        }

        public async Task<Order?> GetOrderByOrderID(int orderID)
        {
            Order? order = await context.Orders.FindAsync(orderID);

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
