using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository.IRepository
{
    public interface IRequestDetailRepository
    {
        public bool AcceptRequestDetail(RequestDetail requestDetail);
        public Task<bool> DenyOtherRequestWhichPassItemQuantity(RequestDetail RequestDetail);
        public bool DenyRequestDetail(RequestDetail requestDetail);
        public Task<List<RequestDetailVM>> getRequestDetailBySharerID(int uid, int status);
        public Task<List<RequestDetailVM>> getRequestDetailByRecieverID(int uid, int status);
    }
}
