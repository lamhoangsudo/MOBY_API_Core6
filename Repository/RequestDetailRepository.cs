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
        /*public async Task<List<RequestDetailVM>> GetAllRequestDetail(int requestID)
        {

            //[1,2,3,4]
            //[1,2]
            //1: [re quest details]
            //2: [request details]
            var listRequestDetailMV = await context.Requests
                .Where(cd => cd.RequestId == requestID)
                .Include(cd => cd.RequestDetails)
                .ThenInclude(rd => rd.Item)
                .ThenInclude(item => item.User)
                .FirstOrDefaultAsync();





            //


            *//*List<CartDetail> listCartDetail = new List<CartDetail>();
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
            }*//*
            return listRequestDetailMV;
        }*/

        public async Task<List<RequestDetailVM>> getRequestDetailByItemID(int itemid)
        {
            List<Request> requests = await context.Requests.Include(r => r.User)
                .Include(r => r.RequestDetails.Where(rd => rd.ItemId == itemid && rd.Status == 1))
                .ThenInclude(rd => rd.Item)
                .ToListAsync();
            List<RequestDetailVM> result = new List<RequestDetailVM>();
            foreach (Request request in requests)
            {
                if (request.RequestDetails.Count > 0)
                {

                    RequestDetail? rd = request.RequestDetails.FirstOrDefault();

                    if (rd != null && request.User != null)
                    {
                        RequestDetailVM requestDetailVMrequest = RequestDetailVM.RequestDetailToVewModel(rd, rd.Item, request.User);

                        result.Add(requestDetailVMrequest);
                    }


                }

            }


            return result;
        }

        public async Task<RequestDetail?> GetRequestDetailByRequestDetailID(int requestID)
        {

            RequestDetail? foundRequestDetail = await context.RequestDetails
                .Where(cd => cd.RequestDetailId == requestID)
                .Include(cd => cd.Item)
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
            int quantityleft = await context.Items.Where(i => i.ItemId == requestDetail.ItemId)
                .Select(i => i.ItemShareAmount)
                .FirstOrDefaultAsync();
            if (quantityleft < quantity)
            {
                return false;
            }
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



        public async Task<bool> ConfirmRequestDetail(ListRequestDetailID requestDetailIDList)
        {
            if (requestDetailIDList.listRequestDetailID == null)
            {
                return false;
            }
            foreach (int id in requestDetailIDList.listRequestDetailID)
            {
                RequestDetail? requestDetail = await context.RequestDetails.Where(rd => rd.RequestDetailId == id).FirstOrDefaultAsync();

                if (requestDetail != null)
                {
                    requestDetail.DateUpdate = DateTime.Now;
                    requestDetail.Status = 1;
                }
            }

            if (await context.SaveChangesAsync() != 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> AcceptRequestDetail(RequestDetailIdVM requestDetailid)
        {
            if (requestDetailid == null)
            {
                return false;
            }


            RequestDetail? requestDetail = await context.RequestDetails
                .Where(rd => rd.RequestDetailId == requestDetailid.RequestDetailId)
                .FirstOrDefaultAsync();

            if (requestDetail != null)
            {

                requestDetail.Status = 2;
                Models.Item? item = await context.Items.Where(i => i.ItemId == requestDetail.ItemId).FirstOrDefaultAsync();
                if (item != null)
                {
                    if (item.ItemShareAmount < requestDetail.ItemQuantity)
                    {
                        return false;
                    }

                    item.ItemShareAmount -= requestDetail.ItemQuantity;
                }
            }
            if (await context.SaveChangesAsync() != 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DenyRequestDetail(int requestDetailid)
        {



            RequestDetail? requestDetail = await context.RequestDetails
                .Where(rd => rd.RequestDetailId == requestDetailid)
                .FirstOrDefaultAsync();

            if (requestDetail != null)
            {

                requestDetail.Status = 3;


            }
            if (await context.SaveChangesAsync() != 0)
            {
                return true;
            }
            return false;
        }


    }

}



