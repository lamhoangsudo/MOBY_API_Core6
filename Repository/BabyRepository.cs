﻿using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;
using NodaTime.Extensions;
using NodaTime;

namespace MOBY_API_Core6.Repository
{
    public class BabyRepository : IBabyRepository
    {
        public readonly MOBYContext _context;
        public BabyRepository(MOBYContext context)
        {
            _context = context;
        }
        public async Task<bool> InputInformationBaby(CreateBabyVM babyVM)
        {
            try
            {
                LocalDateTime now = DateTime.Now.ToLocalDateTime();
                LocalDateTime babyBirth = babyVM.DateOfBirth.ToLocalDateTime();
                Period period = Period.Between(babyBirth, now, PeriodUnits.Months);
                int monthsAge = period.Months;
                if (monthsAge <= 0) {
                    return false;
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
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateInformationBaby(UpdateBabyVM babyVM)
        {
            try
            {
                Baby? baby = await _context.Babies.Where(bb => bb.Idbaby == babyVM.Idbaby).FirstOrDefaultAsync();
                LocalDateTime now = DateTime.Now.ToLocalDateTime();
                LocalDateTime babyBirth = babyVM.DateOfBirth.ToLocalDateTime();
                Period period = Period.Between(babyBirth, now, PeriodUnits.Months);
                int monthsAge = period.Months;
                if (monthsAge <= 0)
                {
                    return false;
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
            catch
            {
                return false;
            }
        }

        public async Task<List<Baby>?> GetBabyByUserID(int id)
        {
            try
            {
                List<Baby> babies = new();
                babies = await _context.Babies.Where(bb => bb.UserId == id).ToListAsync();
                return babies;
            }
            catch
            {
                return null;
            }
        }
    }
}
