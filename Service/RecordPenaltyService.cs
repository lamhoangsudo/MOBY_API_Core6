using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Service
{
    public class RecordPenaltyService : IRecordPenaltyService
    {
        private readonly IRecordPenaltyRepository _recordPenaltyRepository;
        public static string ErrorMessage { get; set; } = string.Empty;

        public RecordPenaltyService(IRecordPenaltyRepository recordPenaltyRepository)
        {
            _recordPenaltyRepository = recordPenaltyRepository;
        }
        public async Task<List<RecordPenaltyPoint>?> GetRecordPenaltyPointsByUserID(int userID)
        {
            try
            {
                return await _recordPenaltyRepository.GetRecordPenaltyPointsByUserID(userID);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        public async Task<bool> CreateRecord(RecordPenaltyPoint recordPenaltyPoint)
        {
            try
            {
                int check = await _recordPenaltyRepository.CreateRecord(recordPenaltyPoint);
                if (check <= 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }
    }
}
