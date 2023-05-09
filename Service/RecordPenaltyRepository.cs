using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Service
{
    public class RecordPenaltyRepository
    {
        private readonly MOBYContext _context;

        public RecordPenaltyRepository(MOBYContext context)
        {
            _context = context;
        }
        public async Task<List<RecordPenaltyPoint>?> GetRecordPenaltyPointsByUserID(int userID)
        {
                List<RecordPenaltyPoint> recordPenaltyPoints = new();
                recordPenaltyPoints = await _context.RecordPenaltyPoints.Where(rc => rc.UserId == userID).ToListAsync();
                return recordPenaltyPoints;
        }

        public async Task<int> CreateRecord(RecordPenaltyPoint recordPenaltyPoint)
        {
                await _context.AddAsync(recordPenaltyPoint);
                return await _context.SaveChangesAsync();
        }
    }
}
