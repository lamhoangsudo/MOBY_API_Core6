using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Service.IService
{
    public interface ITransationService
    {
        public Task<bool> CreateTransationLog(UserAccount user, double amount);
        public Task<bool> UpdateTransationLog(int transactionID);
        public Task<bool> DeleteTransationLog(int transactionID);
        public Task<List<TransationLogVM>> GetTransationLog(bool status, PaggingVM pagging);
        public Task<int> GetTransationLogCount(bool status);
        public Task<List<TransationLogVM>> GetTransationLogByUserID(int uid, PaggingVM pagging);
        public Task<int> GetTransationLogByUserIDCount(int uid);
        public Task<TransationLogVM?> GetTransationLogByID(int transactionID);
    }
}
