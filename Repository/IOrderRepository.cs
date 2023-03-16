using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public interface IOrderRepository
    {
        public Task<List<Order>> GetOrderByRecieverID(int uid);
        public Task<List<Order>> GetOrderBySharerID(int uid);
        public Task<bool> CreateOrder(RequestDetail requestDetail);
        public Task<Order?> GetOrderByOrderID(int orderID);
        public Task<bool> UpdateStatusOrder(Order order, int status);

    }
}
