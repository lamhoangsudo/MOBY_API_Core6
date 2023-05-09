using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Service
{
    public class BannerRepository : IBannerRepository
    {
        private readonly MOBYContext _context;
        public BannerRepository(MOBYContext context)
        {
            _context = context;
        }

        public async Task<int> CreateBanner(CreateBannerVM bannerVM)
        {
            Banner banner = new()
            {
                BannerLink = bannerVM.Link,
                DateCreate = DateTime.Now,
                DateUpdate = DateTime.Now,
                Image = bannerVM.Image,
            };
            await _context.Banners.AddAsync(banner);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> UpdateBanner(UpdateBannerVM updateBanner)
        {
            Banner? banner = await _context.Banners.Where(bn => bn.BannerId == updateBanner.Id).FirstOrDefaultAsync();
            if (banner != null)
            {
                banner.BannerLink = updateBanner.Link;
                banner.DateUpdate = DateTime.Now;
                banner.Image = updateBanner.Imange;
                return await _context.SaveChangesAsync();
            }
            throw new KeyNotFoundException();
        }
        public async Task<int> DeleteBanner(int id)
        {
            Banner? banner = await _context.Banners.Where(bn => bn.BannerId == id).FirstOrDefaultAsync();
            if (banner != null)
            {
                _context.Banners.Remove(banner);
                return await _context.SaveChangesAsync();
            }
            throw new KeyNotFoundException();
        }
        public async Task<List<BannerVM>?> GetAllBanner()
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
    }
}
