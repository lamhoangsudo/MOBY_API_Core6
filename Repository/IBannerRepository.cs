﻿using MOBY_API_Core6.Data_View_Model;

namespace MOBY_API_Core6.Repository
{
    public interface IBannerRepository
    {
        Task<bool> CreateBanner(CreateBannerVM createBanner);
        Task<bool> UpdateBanner(UpdateBannerVM updateBanner);
        Task<bool> DeleteBanner(int id);
        Task<List<BannerVM>?> GetAllBanner();

    }
}
