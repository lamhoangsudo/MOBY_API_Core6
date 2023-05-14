using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Service
{
    public class BannerService : IBannerService
    {
        private readonly MOBYContext _context;
        public BannerService(MOBYContext context)
        {
            _context = context;
        }

        public static string? ErrorMessage { get; set; }
        //done
        public async Task<bool> CreateBanner(CreateBannerVM bannerVM)
        {
            try
            {
                Banner banner = new()
                {
                    BannerLink = bannerVM.Link,
                    DateCreate = DateTime.Now,
                    DateUpdate = DateTime.Now,
                    Image = bannerVM.Image,
                };
                await _context.Banners.AddAsync(banner);
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
        public async Task<bool> UpdateBanner(UpdateBannerVM updateBanner)
        {
            try
            {
                Banner? banner = await _context.Banners.Where(bn => bn.BannerId == updateBanner.Id).FirstOrDefaultAsync();
                if (banner == null)
                {
                    ErrorMessage = "banner không tồn tại";
                    return false;
                }
                else
                {
#pragma warning disable CS8601 // Possible null reference assignment.
                    banner.BannerLink = updateBanner.Link;
                    banner.DateUpdate = DateTime.Now;
                    banner.Image = updateBanner.Imange;
#pragma warning restore CS8601 // Possible null reference assignment.
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }
        //done
        public async Task<bool> DeleteBanner(int id)
        {
            try
            {
                Banner? banner = await _context.Banners.Where(bn => bn.BannerId == id).FirstOrDefaultAsync();
                if (banner == null)
                {
                    ErrorMessage = "banner này đã bị xóa";
                    return false;
                }
                else
                {
                    _context.Banners.Remove(banner);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }
        //done
        public async Task<List<BannerVM>?> GetAllBanner()
        {
            try
            {
                List<BannerVM> bannerVMs = await _context.Banners
                    .Select(
                    bn => new BannerVM
                    {
                        Id = bn.BannerId,
                        Link = bn.BannerLink,
                        Image = bn.Image,
                        DateCreate = bn.DateCreate,
                        DateUpdate = bn.DateUpdate
                    }
                    ).ToListAsync();
                return bannerVMs;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
    }
}
