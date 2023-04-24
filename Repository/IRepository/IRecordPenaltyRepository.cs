using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository.IRepository
{
    public interface IRecordPenaltyRepository
    {
        Task<List<RecordPenaltyPoint>?> GetRecordPenaltyPointsByUserID(int userID);
        Task<bool> CreateRecord(RecordPenaltyPoint recordPenaltyPoint);
    }
}
