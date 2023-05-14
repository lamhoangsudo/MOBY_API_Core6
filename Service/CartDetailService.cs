using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Service
{
    public class CartDetailService : ICartDetailService
    {
        private readonly ICartDetailRepository cartDetailRepository;

        public CartDetailService(ICartDetailRepository cartDetailRepository)
        {
            this.cartDetailRepository = cartDetailRepository;
        }
        public async Task<CartDetail?> GetCartDetailByCartDetailID(int cartDetailID)
        {
            CartDetail? foundCartDetail = await cartDetailRepository.GetCartDetailByCartDetailID(cartDetailID);
            return foundCartDetail;
        }
        public async Task<string> CheclExistCartDetail(CreateCartDetailVM createdcartDetail)
        {
            return await cartDetailRepository.CheclExistCartDetail(createdcartDetail);
        }
        public async Task<string> CreateCartDetail(CreateCartDetailVM createdRequestDetail, int uid)
        {
            return await cartDetailRepository.CreateCartDetail(createdRequestDetail, uid);
        }
        public async Task<string> UpdateCartDetail(CartDetail cartDetail, UpdateCartDetailVM updatedcartDetail)
        {
            return await cartDetailRepository.UpdateCartDetail(cartDetail, updatedcartDetail);
        }
        public async Task<bool> DeleteCartDetail(CartDetail cartDetail)
        {
            int check = await cartDetailRepository.DeleteCartDetail(cartDetail);
            if (check != 0)
            {
                return true;
            }
            return false;
        }
        public async Task<List<CartDetailVM>> GetListCartDetailByListID(int[] listOfIds)
        {
            List<CartDetailVM> listCartDetail = new();
            if (listOfIds == null || listOfIds.Length == 0)
            {
                return listCartDetail;
            }
            foreach (int id in listOfIds)
            {
                CartDetailVM? currentRequestDetail = await cartDetailRepository.GetCartDetailByID(id);
                if (currentRequestDetail != null)
                {
                    listCartDetail.Add(currentRequestDetail);
                }
            }
            return listCartDetail;
        }
        public async Task<string> CheckCartDetail(int[] listCartDetailID, int uid)
        {
            int countLeft = 7;
            List<Order> listOrder = await cartDetailRepository.GetListOrder(uid);
            foreach (Order order in listOrder)
            {
                TimeSpan date = DateTime.Now - order.DateCreate;
                if (date.TotalDays <= 7)
                {
                    countLeft--;
                }
            }
            if (countLeft <= 0)
            {
                return "Bạn đã vượt quá giới hạn lượt nhận trong tuần, vui lòng thử lại khi khác";
            }
            if (countLeft == 7)
            {
                return "success";
            }
            int cartDetailFree = await cartDetailRepository.CartDetailFreeCount(listCartDetailID);
            if (cartDetailFree > countLeft)
            {
                return "Bạn đã nhận " + (7 - countLeft) + "/7 sản phẩm trong tuần, bạn không thể nhận thêm " + cartDetailFree + " sản phẩm, vui lòng thử lại khi khác";
            }
            return "success";
        }
        public async Task<bool> ConfirmCartDetail(ListCartDetailidToConfirm cartDetailIDList, int uid)
        {
            return await cartDetailRepository.ConfirmCartDetail(cartDetailIDList, uid);
        }
    }
}



