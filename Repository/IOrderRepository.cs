using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public interface IOrderRepository
    {
        public Task<List<Order>> GetOrderByRecieverID(int uid, PaggingVM pagging);
        public Task<List<Order>> GetOrderBySharerID(int uid, PaggingVM pagging);
        public Task<bool> CreateOrder(Request request);
        public Task<Order?> GetOrderByOrderID(int orderID);
        public Task<bool> UpdateStatusOrder(Order order, int status);

    }
}
