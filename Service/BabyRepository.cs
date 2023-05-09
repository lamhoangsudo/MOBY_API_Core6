using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Service
{
    public class BabyRepository : IBabyRepository
    {
        public readonly MOBYContext _context;
        public BabyRepository(MOBYContext context)
        {
            _context = context;
        }
        public async Task<int> InputInformationBaby(CreateBabyVM babyVM)
        {
            Baby baby = new()
            {
                UserId = babyVM.UserId,
                Sex = babyVM.Sex,
                DateOfBirth = babyVM.DateOfBirth,
                Weight = babyVM.Weight,
                Height = babyVM.Height
            };
            await _context.AddAsync(baby);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> UpdateInformationBaby(UpdateBabyVM babyVM)
        {
            Baby? baby = await _context.Babies.Where(bb => bb.Idbaby == babyVM.Idbaby && bb.UserId == babyVM.UserID).FirstOrDefaultAsync();
            if (baby != null)
            {
                baby.Sex = babyVM.Sex;
                baby.DateOfBirth = babyVM.DateOfBirth;
                baby.Weight = babyVM.Weight;
                baby.Height = babyVM.Height;
                return await _context.SaveChangesAsync();
            }
            throw new KeyNotFoundException();
        }
        public async Task<List<Baby>?> GetBabyByUserID(int id)
        {
            List<Baby> babies = await _context.Babies.Where(bb => bb.UserId == id).ToListAsync();
            return babies;
        }
        public async Task<int> DeleteBaby(int id, int us)
        {
            Baby? baby = await _context.Babies.Where(bb => bb.Idbaby == id && bb.UserId == us).FirstOrDefaultAsync();
            if (baby != null)
            {
                _context.Babies.Remove(baby);
                return await _context.SaveChangesAsync();
            }
            throw new KeyNotFoundException();
        }
    }
}
