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
        public async Task<bool> CreateCart(int userID)
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

        public async Task<bool> CheackExistedCartByUid(int userID)
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

        public async Task<RequestVM?> GetCartByUid(int userID)
        {
            //Cart cart = context.Carts.Where(c => c.UserId == userID).FirstOrDefault();
            RequestVM? cart = await context.Requests
                .Where(c => c.UserId == userID)
                .Include(c => c.RequestDetails)
                .Select(c => RequestVM.CartToVewModel(c))
                .FirstOrDefaultAsync();

            return cart;
        }

    }
}
