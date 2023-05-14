using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Service.IService
{
    public interface IRecordPenaltyService
    {
        Task<List<RecordPenaltyPoint>?> GetRecordPenaltyPointsByUserID(int userID);
        Task<bool> CreateRecord(RecordPenaltyPoint recordPenaltyPoint);
    }
}
