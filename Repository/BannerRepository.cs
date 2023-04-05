﻿using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public class BannerRepository : IBannerRepository
    {
        private readonly MOBYContext _context;
        public BannerRepository(MOBYContext context)
        {
            _context = context;
        }

        public static string? ErrorMessage { get; set; }

        public async Task<bool> CreateBanner(string link)
        {
            try
            {
                Banner banner = new Banner();
                banner.BannerLink = link;
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

        public async Task<bool> UpdateBanner(BannerVM updateBanner)
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

        public async Task<List<BannerVM>?> GetAllBanner()
        {
            try
            {
                List<BannerVM> bannerVMs = await _context.Banners.Select(
                    bn => new BannerVM
                    {
                        Id = bn.BannerId,
                        Link = bn.BannerLink
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
