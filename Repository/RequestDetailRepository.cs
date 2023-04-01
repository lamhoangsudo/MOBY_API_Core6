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


        public async Task<List<RequestDetailVM>> getRequestDetailBySharerID(int uid, int status)
        {
            List<RequestDetailVM>? listRequestDetail = await context.RequestDetails
                .Include(rd => rd.Item)
                .ThenInclude(i => i.User)
                .Where(rd => rd.Item.UserId == uid && rd.Status == status)
                .Select(rd => RequestDetailVM.RequestDetailToVewModel(rd))
                .ToListAsync();

            return listRequestDetail;
        }

        public async Task<List<RequestDetailVM>> getRequestDetailByRecieverID(int uid, int status)
        {
            List<RequestDetailVM>? listRequestDetail = await context.RequestDetails
                .Include(rd => rd.Request)
                .Include(rd => rd.Item)
                .ThenInclude(i => i.User)
                .Where(rd => rd.Request.UserId == uid && rd.Status == status)
                .Select(rd => RequestDetailVM.RequestDetailToVewModelIncludeIntemOwner(rd))
                .ToListAsync();

            return listRequestDetail;
        }

        public bool AcceptRequestDetail(RequestDetail requestDetail)
        {
            Models.Item? item = requestDetail.Item;
            //Models.Item? item = await context.Items.Where(i => i.ItemId == request.ItemId).FirstOrDefaultAsync();
            if (item != null)
            {
                if (item.ItemShareAmount < requestDetail.Quantity)
                {
                    return false;
                }
                if (item.ItemShareAmount - requestDetail.Quantity == 0)
                {
                    item.ItemStatus = false;
                }

                item.ItemShareAmount -= requestDetail.Quantity;
                requestDetail.Status = 1;

            }



            return true;
        }



        public async Task<bool> DenyOtherRequestWhichPassItemQuantity(RequestDetail RequestDetail)
        {
            Models.Item? currentItem = RequestDetail.Item;
            //Models.Item? currentItem = await context.Items.FindAsync(itemID);
            if (currentItem == null)
            {
                return false;
            }
            List<RequestDetail> requestDetails = await context.RequestDetails
                .Where(rd => rd.ItemId == currentItem.ItemId && rd.Status == 0).ToListAsync();

            if (requestDetails == null || requestDetails.Count == 0)
            {
                return true;
            }
            foreach (RequestDetail rd in requestDetails)
            {
                if (!currentItem.ItemStatus || currentItem.ItemShareAmount == 0 || rd.Quantity > currentItem.ItemShareAmount)
                {
                    rd.Status = 2;
                }

            }


            return true;
        }

        public bool DenyRequestDetail(RequestDetail requestDetail)
        {
            requestDetail.Status = 2;


            return true;
        }
    }
}
