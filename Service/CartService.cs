using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Service
{
    public class CartService : ICartService
    {
        private readonly ICartRepository cartRepository;
        public CartService(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }
        public async Task<bool> CreateCart(int userID)
        {
            int check = await cartRepository.CreateCart(userID);
            if (check == 0)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> UpdateCart(Cart cart, UpdateCartVM updatedCart)
        {
            int check = await cartRepository.UpdateCart(cart, updatedCart);
            if (check == 0)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> CheackExistedCartByUid(int userID)
        {
            Cart? existedRequest = await cartRepository.CheackExistedCartByUid(userID);
            if (existedRequest != null)
            {
                return true;
            }
            return false;
        }
        public async Task<CartVM?> GetCartVMByUid(int userID)
        {
            CartVM? cart = await cartRepository.GetCartVMByUid(userID);
            return cart;
        }
        public async Task<Cart?> GetCartByUid(int userID)
        {
            Cart? cart = await cartRepository.GetCartByUid(userID);
            return cart;
        }

    }
}
