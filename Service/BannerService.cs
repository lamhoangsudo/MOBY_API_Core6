using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Log4Net;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Service
{
    public class BannerService : IBannerService
    {
        private readonly IBannerRepository _bannerRepository;
        private readonly Logger4Net _logger4Net;
        public BannerService(IBannerRepository bannerRepository)
        {
            _bannerRepository = bannerRepository;
            _logger4Net = new Logger4Net();
        }

        public static string? ErrorMessage { get; set; }
        public async Task<bool> CreateBanner(CreateBannerVM bannerVM)
        {
            try
            {
                int check = await _bannerRepository.CreateBanner(bannerVM);
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
        public async Task<bool> UpdateBanner(UpdateBannerVM updateBanner)
        {
            try
            {
                int check = await _bannerRepository.UpdateBanner(updateBanner);
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
        public async Task<bool> DeleteBanner(int id)
        {
            try
            {
                int check = await _bannerRepository.DeleteBanner(id);
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
        public async Task<List<BannerVM>?> GetAllBanner()
        {
            try
            {
                List<BannerVM>? bannerVMs = await _bannerRepository.GetAllBanner();
                return bannerVMs;
            }
            catch (Exception ex)
            {
                _logger4Net.Loggers(ex);
                ErrorMessage = ex.Message;
                return null;
            }
        }
    }
}
