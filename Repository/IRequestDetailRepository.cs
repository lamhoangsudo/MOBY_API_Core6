using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public interface IRequestDetailRepository
    {
        public bool AcceptRequestDetail(RequestDetail requestDetail);
        public Task<bool> DenyOtherRequestWhichPassItemQuantity(RequestDetail RequestDetail);
        public bool DenyRequestDetail(RequestDetail requestDetail);
    }
}
