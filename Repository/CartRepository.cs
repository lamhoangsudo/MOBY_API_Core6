using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;

namespace MOBY_API_Core6.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly MOBYContext context;

        public CartRepository(MOBYContext context)
        {
            this.context = context;
        }
        public async Task<int> CreateCart(int userID)
        {
            var user = await context.UserAccounts.FindAsync(userID);
            if (user == null)
            {
                return 0;
            }
            Cart newCart = new()
            {
                UserId = userID,
                Address = user.UserAddress
            };
            await context.Carts.AddAsync(newCart);
            return await context.SaveChangesAsync();
        }
        public async Task<int> UpdateCart(Cart cart, UpdateCartVM updatedCart)
        {
            if (updatedCart.Address == null || updatedCart.Address == "")
            {
                return 0;
            }
            cart.Address = updatedCart.Address;
            return await context.SaveChangesAsync();
        }
        public async Task<Cart?> CheackExistedCartByUid(int userID)
        {
            return await context.Carts
                .Where(c => c.UserId == userID)
                .FirstOrDefaultAsync();
        }
        public async Task<CartVM?> GetCartVMByUid(int userID)
        {
            return await context.Carts
                .Where(c => c.UserId == userID)
                .Include(c => c.User)
                .Include(c => c.CartDetails)
                .ThenInclude(c => c.Item)
                .ThenInclude(i => i.User)
                .Include(c => c.CartDetails)
                .ThenInclude(c => c.Item)
                .ThenInclude(i => i.SubCategory)
                .Select(c => CartVM.CartToVewModel(c))
                .FirstOrDefaultAsync();
        }
        public async Task<Cart?> GetCartByUid(int userID)
        {
            return await context.Carts
                .Where(c => c.UserId == userID)
                .FirstOrDefaultAsync();
        }
    }
}
