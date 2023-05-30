using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Log4Net;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Service
{
    public class RecordPenaltyService : IRecordPenaltyService
    {
        private readonly IRecordPenaltyRepository _recordPenaltyRepository;
        private readonly Logger4Net _logger4Net;
        public static string ErrorMessage { get; set; } = string.Empty;

        public RecordPenaltyService(IRecordPenaltyRepository recordPenaltyRepository)
        {
            _recordPenaltyRepository = recordPenaltyRepository;
            _logger4Net = new Logger4Net();
        }
        public async Task<List<RecordPenaltyPoint>?> GetRecordPenaltyPointsByUserID(int userID)
        {
            try
            {
                return await _recordPenaltyRepository.GetRecordPenaltyPointsByUserID(userID);
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
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
                _logger4Net.Loggers(ex);
                ErrorMessage = ex.Message;
                return false;
            }
        }
    }
}
