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
                .Include(r => r.User)
                .Include(r => r.Item)
                .ThenInclude(i => i.User)
                .Select(r => RequestVM.RequestToVewModel(r))
                .ToListAsync();
            return requests;
        }

        public async Task<List<RequestVM>> getRequestByUserID(int userid)
        {
            List<RequestVM> requests = await context.Requests
                .Where(r => r.UserId == userid)
                .Include(r => r.User)
                .Include(r => r.Item)
                .ThenInclude(i => i.User)
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
        public async Task<RequestVM?> getRequestVMByRequestID(int requestID)
        {
            RequestVM? requests = await context.Requests
                .Where(r => r.RequestId == requestID)
                .Include(r => r.User)
                .Include(r => r.Item)
                .ThenInclude(i => i.User)
                .Select(r => RequestVM.RequestToVewModel(r))
                .FirstOrDefaultAsync();

            return requests;
        }

        public async Task<String> AcceptRequest(Request request)
        {
            Models.Item? item = request.Item;
            //Models.Item? item = await context.Items.Where(i => i.ItemId == request.ItemId).FirstOrDefaultAsync();
            if (item != null)
            {
                if (item.ItemShareAmount < request.ItemQuantity)
                {
                    return "Item ammount not available";
                }
                if (item.ItemShareAmount - request.ItemQuantity == 0)
                {
                    item.ItemStatus = false;
                }

                item.ItemShareAmount -= request.ItemQuantity;
                request.Status = 1;
                request.DateChangeStatus = DateTime.Now;
            }

            if (await context.SaveChangesAsync() != 0)
            {
                Request? curremtRequest = await context.Requests
                    .Where(r => r.RequestId == request.RequestId)
                    .Include(r => r.Item).Include(r => r.User).FirstOrDefaultAsync();
                if (curremtRequest != null)
                {
                    String result = await orderDAO.CreateOrder(curremtRequest);
                    if (result != null)
                    {
                        if (!(result.Equals("")))
                        {
                            return result;
                        }
                    }
                    else
                    {
                        return "error at createOrder";
                    }
                }
            }

            return "error at AcceptRequest";
        }

        public async Task<bool> DenyOtherRequestWhichPassItemQuantity(Request request)
        {
            Models.Item? currentItem = request.Item;
            //Models.Item? currentItem = await context.Items.FindAsync(itemID);
            if (currentItem == null)
            {
                return false;
            }
            List<Request> requests = await context.Requests
                .Where(r => r.ItemId == currentItem.ItemId)
                .ToListAsync();
            if (requests == null || requests.Count == 0)
            {
                return true;
            }
            foreach (Request request1 in requests)
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

        public async Task<bool> DenyRequest(Request request)
        {
            request.Status = 2;
            request.DateChangeStatus = DateTime.Now;
            if (await context.SaveChangesAsync() != 0)
            {
                return true;
            }
            return false;
        }
    }
}
