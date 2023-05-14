using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Service
{
    public class TransationService : ITransationService
    {
        private readonly ITransationRepository transationRepository;
        public TransationService(ITransationRepository transationRepository)
        {
            this.transationRepository = transationRepository;
        }

        public async Task<bool> CreateTransationLog(UserAccount user, double amount)
        {
            return await transationRepository.CreateTransationLog(user, amount);
        }

        public async Task<bool> UpdateTransationLog(int transactionID)
        {
            return await transationRepository.UpdateTransationLog(transactionID);
        }

        public async Task<bool> DeleteTransationLog(int transactionID)
        {
            return await transationRepository.DeleteTransationLog(transactionID);
        }

        public async Task<List<TransationLogVM>> GetTransationLog(bool status, PaggingVM pagging)
        {
            List<TransationLogVM>? transationLogList = await transationRepository.GetTransationLog(status, pagging);
            return transationLogList;
        }
        public async Task<int> GetTransationLogCount(bool status)
        {
            int transationLogList = await transationRepository.GetTransationLogCount(status);
            return transationLogList;
        }

        public async Task<List<TransationLogVM>> GetTransationLogByUserID(int uid, PaggingVM pagging)
        {
            int itemsToSkip = (pagging.PageNumber - 1) * pagging.PageSize;
            List<TransationLogVM>? transationLogList = await transationRepository.GetTransationLogByUserID(uid, pagging);
            return transationLogList;
        }

        public async Task<int> GetTransationLogByUserIDCount(int uid)
        {
            int transationLogList = await transationRepository.GetTransationLogByUserIDCount(uid);
            return transationLogList;
        }

        public async Task<TransationLogVM?> GetTransationLogByID(int transactionID)
        {
            TransationLogVM? transationLogList = await transationRepository.GetTransationLogByID(transactionID);
            return transationLogList;
        }

    }
}
