using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public class RequestRepository : IRequestRepository
    {
        private readonly MOBYContext context;

        public RequestRepository(MOBYContext context)
        {
            this.context = context;
        }
        public async Task<bool> CreateRequest(int userID)
        {
            Request newRequest = new Request();
            newRequest.UserId = userID;
            await context.Requests.AddAsync(newRequest);
            if (await context.SaveChangesAsync() != 0)
            {
                return true;
            }
            return false;

        }

        public async Task<bool> CheackExistedRequestByUid(int userID)
        {

            Request? existedRequest = await context.Requests
                .Where(c => c.UserId == userID)
                .FirstOrDefaultAsync();
            if (existedRequest != null)
            {
                return true;
            }
            return false;
        }

        public async Task<RequestVM?> GetRequestByUid(int userID)
        {
            //Cart cart = context.Carts.Where(c => c.UserId == userID).FirstOrDefault();
            RequestVM? cart = await context.Requests
                .Where(c => c.UserId == userID)
                .Include(c => c.RequestDetails)
                .ThenInclude(c => c.Item)
                .ThenInclude(i => i.User)
                .Select(c => RequestVM.RequestToVewModel(c))
                .FirstOrDefaultAsync();

            return cart;
        }

    }
}
