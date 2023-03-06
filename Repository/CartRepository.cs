using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly MOBYContext context;

        public CartRepository(MOBYContext context)
        {
            this.context = context;
        }
        public async Task<bool> CreateCart(int userID)
        {
            Cart cart = new Cart();
            cart.CartDateCreate = DateTime.Now;
            cart.UserId = userID;
            await context.Carts.AddAsync(cart);
            if (await context.SaveChangesAsync() != 0)
            {
                return true;
            }
            return false;

        }

        public async Task<bool> CheackExistedCartByUid(int userID)
        {
            Cart? cart = await context.Carts
                .Where(c => c.UserId == userID)
                .FirstOrDefaultAsync();
            if (cart != null)
            {
                return true;
            }
            return false;
        }

        public async Task<CartVM?> GetCartByUid(int userID)
        {
            //Cart cart = context.Carts.Where(c => c.UserId == userID).FirstOrDefault();
            CartVM? cart = await context.Carts
                .Where(c => c.UserId == userID)
                .Include(c => c.CartDetails)
                .Select(c => CartVM.CartToVewModel(c))
                .FirstOrDefaultAsync();

            return cart;
        }

    }
}
