using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public class CartDetailRepository
    {
        private readonly MOBYContext context;

        public CartDetailRepository(MOBYContext context)
        {
            this.context = context;
        }
        public async Task<List<CartDetail>> GetAllCartDetail(int cartID)
        {

            List<CartDetail> listCartDetail = new List<CartDetail>();
            //listCartDetail = await context.CartDetails.Where(u => u.CartId == cartID).ToList();

            return listCartDetail;
        }

        public async Task<int> getQuantityCartDetail(int cartDetailID)
        {

            List<CartDetail> listCartDetail = new List<CartDetail>();
            //listCartDetail = await context.CartDetails.Where(cd => cd.CartDetailId = cartDetailID).ToList();

            return 0;
        }


    }

}



