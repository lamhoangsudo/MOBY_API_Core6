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
            var user = await context.UserAccounts.FindAsync(userID);
            if (user == null)
            {
                return false;
            }
            Models.Cart newCart = new Models.Cart();
            newCart.UserId = userID;
            newCart.Address = user.UserAddress;
            await context.Carts.AddAsync(newCart);
            if (await context.SaveChangesAsync() != 0)
            {
                return true;
            }
            return false;

        }

        public async Task<bool> UpdateCart(Models.Cart cart, UpdateCartVM updatedCart)
        {
            if (updatedCart.address == null || updatedCart.address == "")
            {
                return false;
            }
            cart.Address = updatedCart.address;

            if (await context.SaveChangesAsync() != 0)
            {
                return true;
            }
            return false;

        }

        public async Task<bool> CheackExistedCartByUid(int userID)
        {

            Models.Cart? existedRequest = await context.Carts
                .Where(c => c.UserId == userID)
                .FirstOrDefaultAsync();
            if (existedRequest != null)
            {
                return true;
            }
            return false;
        }

        public async Task<CartVM?> GetCartVMByUid(int userID)
        {
            //Cart cart = context.Carts.Where(c => c.UserId == userID).FirstOrDefault();
            CartVM? cart = await context.Carts
                .Where(c => c.UserId == userID)
                .Include(c => c.CartDetails)
                .ThenInclude(c => c.Item)
                .ThenInclude(i => i.User)
                .Include(c => c.CartDetails)
                .ThenInclude(c => c.Item)
                .ThenInclude(i => i.SubCategory)
                .Select(c => CartVM.CartToVewModel(c))
                .FirstOrDefaultAsync();

            return cart;
        }
        public async Task<Cart?> GetCartByUid(int userID)
        {
            //Cart cart = context.Carts.Where(c => c.UserId == userID).FirstOrDefault();
            Cart? cart = await context.Carts
                .Where(c => c.UserId == userID)
                .FirstOrDefaultAsync();

            return cart;
        }

    }
}
