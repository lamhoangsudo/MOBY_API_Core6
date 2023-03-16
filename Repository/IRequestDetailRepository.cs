﻿using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public interface IRequestDetailRepository
    {
        //public Task<List<RequestDetailVM>> GetAllRequestDetail(int requestDetailID);
        public Task<bool> CreateRequestDetail(CreateRequestDetailVM createdRequestDetail);
        public Task<bool> UpdateRequestDetail(RequestDetail requestDetail, UpdateRequestDetailVM updatedRequestDetail);
        public Task<RequestDetail?> GetRequestDetailByRequestDetailID(int requestDetailID);
        public Task<bool> DeleteRequestDetail(RequestDetail requestDetail);
        public Task<List<RequestDetailVM>> getRequestDetailByItemID(int itemid);
        public Task<bool> ConfirmRequestDetail(ListRequestDetailID requestDetailIDList);
        public Task<bool> AcceptRequestDetail(RequestDetailIdVM requestDetailid);
        public Task<bool> DenyRequestDetail(int requestDetailid);
        //public Task<List<CartDetailVM>> GetCartDetailByItemID(int itemID);

    }
}
