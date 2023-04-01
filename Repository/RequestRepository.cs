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

        public async Task<List<RequestVM>> getRequestBySharerID(int uid, int status)
        {
            List<RequestVM> requests = await context.Requests
                .Include(r => r.User)
                    .Include(r => r.RequestDetails)
                    .ThenInclude(rd => rd.Item)
                    .ThenInclude(i => i.User)
                    .Where(r => r.Status == status && r.RequestDetails.Any(rd => rd.Item.UserId == uid))
                    .Select(r => RequestVM.RequestToVewModel(r))
                    .ToListAsync();
            /*List<RequestVM> requests = await context.Requests
                .Include(r => r.RequestDetails.Join(context.Items, rd => rd.ItemId, it => it.ItemId, (rd, it) => new { rd, it })
                .Join(context.UserAccounts, rdit => rdit.it.UserId, us => us.UserId, (rdit, us) => new { rdit, us })
                .Where(rditus => rditus.rdit.it.UserId == uid))

                .Select(r => RequestVM.RequestToVewModel(r))
                .ToListAsync();*/



            return requests;
        }

        public async Task<List<RequestVM>> getRequestByRecieverID(int uid, int status)
        {
            List<RequestVM> requests = await context.Requests
                .Where(r => r.UserId == uid && r.Status == status)
                .Include(r => r.User)
                .Include(r => r.RequestDetails)
                .ThenInclude(rd => rd.Item)
                .ThenInclude(i => i.User)
                .Select(r => RequestVM.RequestToVewModel(r))
                .ToListAsync();
            return requests;
        }

        public async Task<Request?> getRequestByRequestID(int requestID)
        {
            Request? requests = await context.Requests
                .Where(r => r.RequestId == requestID)
                .Include(r => r.RequestDetails)
                .ThenInclude(rd => rd.Item)
                .FirstOrDefaultAsync();

            return requests;
        }
        public async Task<RequestVM?> getRequestVMByRequestID(int requestID)
        {
            RequestVM? requests = await context.Requests
                .Where(r => r.RequestId == requestID)
                .Include(r => r.User)
                .Include(r => r.RequestDetails)
                .ThenInclude(rd => rd.Item)
                .ThenInclude(i => i.User)
                .Select(r => RequestVM.RequestToVewModel(r))
                .FirstOrDefaultAsync();

            return requests;
        }
        public async Task<bool> DenyRequest(Request request)
        {
            request.Status = 2;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SaveRequest()
        {
            await context.SaveChangesAsync();
            return true;
        }


    }
}
