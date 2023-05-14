using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Service
{
    public class CartService : ICartService
    {
        private readonly MOBYContext context;

        public CartService(MOBYContext context)
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
            Cart newCart = new Cart();
            newCart.UserId = userID;
            newCart.Address = user.UserAddress;
            await context.Carts.AddAsync(newCart);
            if (await context.SaveChangesAsync() != 0)
            {
                return true;
            }
            return false;

        }

        public async Task<bool> UpdateCart(Cart cart, UpdateCartVM updatedCart)
        {
            if (updatedCart.Address == null || updatedCart.Address == "")
            {
                return false;
            }
            cart.Address = updatedCart.Address;

            if (await context.SaveChangesAsync() != 0)
            {
                return true;
            }
            return false;

        }

        public async Task<bool> CheackExistedCartByUid(int userID)
        {

            Cart? existedRequest = await context.Carts
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
            //Cart cart = context.Carts.Where(c => c.UserId == UserID).FirstOrDefault();
            CartVM? cart = await context.Carts
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

            return cart;
        }
        public async Task<Cart?> GetCartByUid(int userID)
        {
            //Cart cart = context.Carts.Where(c => c.UserId == UserID).FirstOrDefault();
            Cart? cart = await context.Carts
                .Where(c => c.UserId == userID)
                .FirstOrDefaultAsync();

            return cart;
        }

    }
}
