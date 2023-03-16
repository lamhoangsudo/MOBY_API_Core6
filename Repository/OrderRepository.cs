using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Order>> GetOrderByRecieverID(int uid)
        {
            List<Order> listOrder = await context.Orders.Where(o => o.UserId == uid).ToListAsync();
            return listOrder;
        }

        public async Task<List<Order>> GetOrderBySharerID(int uid)
        {
            List<Order> listOrder = await context.Orders.Include(o => o.Item).Where(o => o.Item.UserId == uid).ToListAsync();
            return listOrder;
        }

        public async Task<bool> CreateOrder(RequestDetail requestDetail)
        {
            Order newOrder = new Order();

            newOrder.UserId = requestDetail.Request.UserId;
            newOrder.ItemId = requestDetail.ItemId;
            newOrder.Quanlity = requestDetail.ItemQuantity;
#pragma warning disable CS8601 // Possible null reference assignment.
            newOrder.Address = requestDetail.Address;
#pragma warning restore CS8601 // Possible null reference assignment.
#pragma warning disable CS8629 // Nullable value type may be null.
            newOrder.SponsoredOrderShippingFee = requestDetail.SponsoredOrderShippingFee.Value;
#pragma warning restore CS8629 // Nullable value type may be null.
            newOrder.Note = requestDetail.Note;
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
