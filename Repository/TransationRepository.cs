using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;

namespace MOBY_API_Core6.Repository
{
    public class TransationRepository : ITransationRepository
    {
        private readonly MOBYContext context;

        public TransationRepository(MOBYContext context)
        {
            this.context = context;
        }

        public async Task<bool> CreateTransationLog(UserAccount user, double amount)
        {
            TransationLog newTransaction = new()
            {
                UserId = user.UserId,
                Value = amount,
                TransactionStatus = false,
                DateCreate = DateTime.Now
            };
            context.TransationLogs.Add(newTransaction);
            if (user.Balance - amount < 0)
            {
                return false;
            }
            user.Balance -= amount;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateTransationLog(int transactionID)
        {
            TransationLog? transationLog = await context.TransationLogs.Where(t => t.TransactionId == transactionID).FirstOrDefaultAsync();
            if (transationLog == null)
            {
                return false;
            }
            transationLog.TransactionStatus = true;
            transationLog.DateUpdate = DateTime.Now;

            if (await context.SaveChangesAsync() != 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteTransationLog(int transactionID)
        {
            TransationLog? transationLog = await context.TransationLogs.Where(t => t.TransactionId == transactionID).FirstOrDefaultAsync();
            if (transationLog == null)
            {
                return false;
            }
            context.TransationLogs.Remove(transationLog);
            if (await context.SaveChangesAsync() != 0)
            {
                return true;
            }
            return false;
        }

        public async Task<List<TransationLogVM>> GetTransationLog(bool status, PaggingVM pagging)
        {
            int itemsToSkip = (pagging.PageNumber - 1) * pagging.PageSize;
            return await context.TransationLogs
                .Where(t => t.TransactionStatus == status)
                .Include(t => t.User)
                .OrderByDescending(t => t.TransactionId)
                .Skip(itemsToSkip)
                .Take(pagging.PageSize)
                .Select(t => TransationLogVM.TransactionToVewModel(t))
                .ToListAsync();
        }
        public async Task<int> GetTransationLogCount(bool status)
        {
            return await context.TransationLogs
                .Where(t => t.TransactionStatus == status)
                .CountAsync();
        }

        public async Task<List<TransationLogVM>> GetTransationLogByUserID(int uid, PaggingVM pagging)
        {
            int itemsToSkip = (pagging.PageNumber - 1) * pagging.PageSize;
            return await context.TransationLogs
                .Where(t => t.UserId == uid)
                .Include(t => t.User)
                .OrderByDescending(t => t.TransactionId)
                .Skip(itemsToSkip)
                .Take(pagging.PageSize)
                .Select(t => TransationLogVM.TransactionToVewModel(t))
                .ToListAsync();
        }

        public async Task<int> GetTransationLogByUserIDCount(int uid)
        {
            return await context.TransationLogs
                .Where(t => t.UserId == uid)
                .CountAsync();
        }

        public async Task<TransationLogVM?> GetTransationLogByID(int transactionID)
        {
            return await context.TransationLogs
                .Where(t => t.TransactionId == transactionID)
                .Include(t => t.User)
                .Select(t => TransationLogVM.TransactionToVewModel(t))
                .FirstOrDefaultAsync();
        }
    }
}
