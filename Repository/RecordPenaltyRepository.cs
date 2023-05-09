using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;

namespace MOBY_API_Core6.Repository
{
    public class RecordPenaltyRepository : IRecordPenaltyRepository
    {
        private readonly MOBYContext _context;

        public static string? ErrorMessage { get; set; }

        public RecordPenaltyRepository(MOBYContext context)
        {
            _context = context;
        }
        //done
        public async Task<List<RecordPenaltyPoint>?> GetRecordPenaltyPointsByUserID(int userID)
        {
            try
            {
                List<RecordPenaltyPoint> recordPenaltyPoints = new();
                recordPenaltyPoints = await _context.RecordPenaltyPoints.Where(rc => rc.UserId == userID).ToListAsync();
                return recordPenaltyPoints;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        //done
        public async Task<bool> CreateRecord(RecordPenaltyPoint recordPenaltyPoint)
        {
            try
            {
                await _context.AddAsync(recordPenaltyPoint);
                await _context.SaveChangesAsync();
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
