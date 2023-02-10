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
            context.Add(cart);
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

        public async Task<Cart> GetCartByUid(int userID)
        {
            Cart cart = context.Carts.Where(c => c.UserId == userID).FirstOrDefault();


            return cart;
        }

    }
}
