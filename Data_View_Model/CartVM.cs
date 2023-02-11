using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Data_View_Model
{
    public class CartVM
    {
        public int CartId { get; set; }
        public DateTime? CartDateCreate { get; set; }
        public int? UserId { get; set; }

        public static CartVM CartToVewModel(Cart cart)
        {
            return new CartVM
            {
                CartId = cart.CartId,
                CartDateCreate = cart.CartDateCreate,
                UserId = cart.UserId
            };
        }

    }
}
