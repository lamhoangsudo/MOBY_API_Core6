using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;

namespace MOBY_API_Core6.Repository
{
    public class RecordPenaltyRepository : IRecordPenaltyRepository
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
