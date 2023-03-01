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
            context.Carts.Add(cart);
            context.SaveChanges();
            return true;

        }

        public async Task<bool> CheackExistedCartByUid(int userID)
        {
            Cart cart = new Cart();
            cart = context.Carts.Where(c => c.UserId == userID).FirstOrDefault();
            if (cart != null)
            {
                return true;
            }
            return false;
        }

        public async Task<CartVM> GetCartByUid(int userID)
        {
            //Cart cart = context.Carts.Where(c => c.UserId == userID).FirstOrDefault();
            var user = context.UserAccounts.Where(u => u.UserId == userID)
                .Include(user => user.Carts)
                .FirstOrDefault();
            var cart = user.Carts.FirstOrDefault();
            return CartVM.CartToVewModel(cart);
        }

    }
}
