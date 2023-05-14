using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using NodaTime.Extensions;
using NodaTime;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Service
{
    public class BabyService : IBabyService
    {
        public readonly MOBYContext _context;
        public static string ErrorMessage { get; set; } = "";
        public BabyService(MOBYContext context)
        {
            _context = context;
        }
        //done
        public async Task<bool> InputInformationBaby(CreateBabyVM babyVM)
        {
            try
            {
                LocalDateTime now = DateTime.Now.ToLocalDateTime();
                LocalDateTime babyBirth = babyVM.DateOfBirth.ToLocalDateTime();
                Period period = Period.Between(babyBirth, now, PeriodUnits.AllDateUnits);
                double monthsAge = period.Months;
                if (monthsAge < 0)
                {
                    return false;
                }
                if (monthsAge == 0)
                {
                    double dayAge = period.Days;
                    if (dayAge <= 0)
                    {
                        return false;
                    }
                }
                Baby baby = new()
                {
                    UserId = babyVM.UserId,
                    Sex = babyVM.Sex,
                    DateOfBirth = babyVM.DateOfBirth,
                    Weight = babyVM.Weight,
                    Height = babyVM.Height
                };
                await _context.AddAsync(baby);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }
        //done
        public async Task<bool> UpdateInformationBaby(UpdateBabyVM babyVM)
        {
            try
            {
                Baby? baby = await _context.Babies.Where(bb => bb.Idbaby == babyVM.Idbaby && bb.UserId == babyVM.UserID).FirstOrDefaultAsync();
                LocalDateTime now = DateTime.Now.ToLocalDateTime();
                LocalDateTime babyBirth = babyVM.DateOfBirth.ToLocalDateTime();
                Period period = Period.Between(babyBirth, now, PeriodUnits.AllDateUnits);
                double monthsAge = period.Months;
                if (monthsAge < 0)
                {
                    return false;
                }
                if (monthsAge == 0)
                {
                    double dayAge = period.Days;
                    if (dayAge <= 0)
                    {
                        return false;
                    }
                }
                if (baby != null)
                {
                    baby.Sex = babyVM.Sex;
                    baby.DateOfBirth = babyVM.DateOfBirth;
                    baby.Weight = babyVM.Weight;
                    baby.Height = babyVM.Height;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }
        //done
        public async Task<List<Baby>?> GetBabyByUserID(int id)
        {
            try
            {
                List<Baby> babies = new();
                babies = await _context.Babies.Where(bb => bb.UserId == id).ToListAsync();
                return babies;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        //done
        public async Task<bool> DeleteBaby(int id, int us)
        {
            try
            {
                Baby? baby = await _context.Babies.Where(bb => bb.Idbaby == id && bb.UserId == us).FirstOrDefaultAsync();
                if (baby != null)
                {
                    _context.Babies.Remove(baby);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }
    }
}
