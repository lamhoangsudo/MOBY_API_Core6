using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public class RequestDetailRepository : IRequestDetailRepository
    {
        private readonly MOBYContext context;

        public RequestDetailRepository(MOBYContext context)
        {
            this.context = context;
        }
        public async Task<List<RequestDetailVM>> GetAllRequestDetail(int requestID)
        {
            List<RequestDetailVM> listRequestDetailMV = await context.RequestDetails
                .Where(cd => cd.RequestId == requestID)
                .Include(cd => cd.Item)
                .ThenInclude(item => item.User)
                .Select(cd => RequestDetailVM.RequestDetailToVewModel(cd))
                .ToListAsync();
            /*List<CartDetail> listCartDetail = new List<CartDetail>();
            List<CartDetailVM> listCartDetailMV = new List<CartDetailVM>();
            var cart = context.Carts.Where(c => c.CartId == cartID)
                .Include(thisCart => thisCart.CartDetails).FirstOrDefault();
            //listCartDetail = context.CartDetails.Where(u => u.CartId == cartID).ToList();
            listCartDetail = cart.CartDetails.ToList();
            CartDetailVM crmv = new CartDetailVM();
            foreach (var item in listCartDetail)
            {
                crmv = CartDetailVM.CartDetailToVewModel(item);
                listCartDetailMV.Add(crmv);
            }*/
            return listRequestDetailMV;
        }

        public async Task<RequestDetail?> GetRequestDetailByRequestDetailID(int requestID)
        {

            RequestDetail? foundRequestDetail = await context.RequestDetails
                .Where(cd => cd.RequestDetailId == requestID)
                .FirstOrDefaultAsync();

            return foundRequestDetail;
        }
        /*public List<CartDetailVM> GetCartDetailByItemID(int itemID)
        {

            List<CartDetail> foundCartDetail = context.CartDetails.Where(cd => cd.ItemId == itemID).ToList();
            List<CartDetailVM> CartDetailToVM = new List<CartDetailVM>();
            CartDetailVM cdmv = new CartDetailVM();
            foreach (var item in foundCartDetail)
            {
                cdmv = CartDetailVM.CartDetailToVewModel(item);
                CartDetailToVM.Add(cdmv);
            }
            return CartDetailToVM;
        }*/

        public async Task<bool> CreateRequestDetail(CreateRequestDetailVM createdRequestDetail)
        {

            RequestDetail newRquestDetail = new RequestDetail();
            newRquestDetail.RequestId = createdRequestDetail.RequestId;
            newRquestDetail.ItemId = createdRequestDetail.ItemId;
            newRquestDetail.DateCreate = DateTime.Now;
            newRquestDetail.ItemQuantity = createdRequestDetail.ItemQuantity;
            newRquestDetail.Status = 0;


            await context.RequestDetails.AddAsync(newRquestDetail);
            if (await context.SaveChangesAsync() != 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateRequestDetail(RequestDetail requestDetail, int quantity)
        {

            requestDetail.ItemQuantity = quantity;
            requestDetail.DateUpdate = DateTime.Now;

            if (await context.SaveChangesAsync() != 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteRequestDetail(RequestDetail requestDetail)
        {

            context.RequestDetails.Remove(requestDetail);

            if (await context.SaveChangesAsync() != 0)
            {
                return true;
            }
            return false;
        }




    }

}



