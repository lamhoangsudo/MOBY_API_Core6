using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Log4Net;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly Logger4Net _logger4Net;
        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
            _logger4Net = new Logger4Net();
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
        public async Task<bool> CheckOrderReceivedDate(int uid)
        {
            List<Order> listShippingOrder = await orderRepository.GetShippingOrder(uid);
            List<Order> listShippingOrder1 = await orderRepository.GetShippingOrder(uid);
            if (listShippingOrder == null)
            {
                return false;
            }
            foreach (Order o in listShippingOrder)
            {
                if (o.Reports.Any())
                {
                    foreach (Report r in o.Reports)
                    {
                        if (r.ReportStatus == 0 && r.User.UserStatus == true)
                        {
                            listShippingOrder1.Remove(o);
                            break;
                        }
                    }
                }
            }
            foreach (Order o in listShippingOrder1)
            {
                TimeSpan totalDays = DateTime.Now - o.DatePackage!.Value;
                if (totalDays.TotalDays >= 16)
                {
                    await orderRepository.UpdateStatusOrder(o, 2);
                }
            }
            return true;
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
            try
            {
                return await orderRepository.CancelOrder(order, reasonCancel, uid, pernament);
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                return false;
            }
        }
    }
}
