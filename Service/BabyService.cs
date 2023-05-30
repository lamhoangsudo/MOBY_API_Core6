using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using NodaTime.Extensions;
using NodaTime;
using MOBY_API_Core6.Service.IService;
using MOBY_API_Core6.Repository.IRepository;
using MOBY_API_Core6.Log4Net;

namespace MOBY_API_Core6.Service
{
    public class BabyService : IBabyService
    {
        public readonly IBabyRepository _babyRepository;
        private readonly Logger4Net _logger4Net;
        public static string ErrorMessage { get; set; } = "";
        public BabyService(IBabyRepository babyRepository)
        {
            _babyRepository = babyRepository;
            _logger4Net = new Logger4Net();
        }
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
                int check = await _babyRepository.InputInformationBaby(babyVM);
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
        public async Task<bool> UpdateInformationBaby(UpdateBabyVM babyVM)
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
                int check = await _babyRepository.UpdateInformationBaby(babyVM);
                if (check <= 0) 
                {
                    return true; 
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
        public async Task<List<Baby>?> GetBabyByUserID(int id)
        {
            try
            {
                List<Baby>? babies = new();
                babies = await _babyRepository.GetBabyByUserID(id);
                return babies;
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                ErrorMessage = ex.Message;
                return null;
            }
        }
        public async Task<bool> DeleteBaby(int id, int us)
        {
            try
            {
                int check = await _babyRepository.DeleteBaby(id, us);
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
