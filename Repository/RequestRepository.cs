using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public class RequestRepository : IRequestRepository
    {
        private readonly MOBYContext context;
        private readonly IOrderRepository orderDAO;


        public RequestRepository(MOBYContext context, IOrderRepository orderDAO)
        {
            this.context = context;
            this.orderDAO = orderDAO;

        }

        public async Task<List<RequestVM>> getRequestByItemID(int itemid)
        {
            List<RequestVM> requests = await context.Requests
                .Where(r => r.ItemId == itemid)
                .Include(r => r.Item)
                .Include(r => r.User)
                .Select(r => RequestVM.RequestToVewModel(r))
                .ToListAsync();
            return requests;
        }

        public async Task<Request?> getRequestByRequestID(int requestID)
        {
            Request? requests = await context.Requests
                .Where(r => r.RequestId == requestID)
                .Include(r => r.Item)
                .FirstOrDefaultAsync();

            return requests;
        }

        public async Task<bool> AcceptRequestDetail(Request request)
        {

            Models.Item? item = await context.Items.Where(i => i.ItemId == request.ItemId).FirstOrDefaultAsync();
            if (item != null)
            {
                if (item.ItemShareAmount < request.ItemQuantity)
                {
                    return false;
                }
                if (item.ItemShareAmount - request.ItemQuantity == 0)
                {
                    item.ItemStatus = false;
                }

                item.ItemShareAmount -= request.ItemQuantity;
                request.Status = 1;
            }

            if (await context.SaveChangesAsync() != 0)
            {
                Request? curremtRequest = await context.Requests
                    .Where(r => r.RequestId == request.RequestId)
                    .Include(r => r.Item).Include(r => r.User).FirstOrDefaultAsync();
                if (curremtRequest != null)
                {
                    await orderDAO.CreateOrder(curremtRequest);

                }
                return true;
            }

            return false;
        }

        public async Task<bool> DenyOtherRequestWhichPassItemQuantity(int itemID)
        {

            Models.Item? currentItem = await context.Items.FindAsync(itemID);
            if (currentItem == null)
            {
                return false;
            }
            List<Request> requests = await context.Requests
                .Where(r => r.ItemId == itemID)
                .ToListAsync();
            if (requests == null || requests.Count == 0)
            {
                return true;
            }
            foreach (Request request in requests)
            {
                if (!currentItem.ItemStatus || currentItem.ItemShareAmount == 0)
                {
                    request.Status = 2;
                }
                if (request.ItemQuantity > currentItem.ItemShareAmount)
                {
                    request.Status = 2;
                }
            }

            if (await context.SaveChangesAsync() != 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DenyRequestDetail(Request request)
        {
            request.Status = 2;
            if (await context.SaveChangesAsync() != 0)
            {
                return true;
            }
            return false;
        }
    }
}
