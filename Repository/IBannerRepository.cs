using MOBY_API_Core6.Data_View_Model;

namespace MOBY_API_Core6.Repository
{
    public interface IBannerRepository
    {
        Task<bool> CreateBanner(string link);
        Task<bool> UpdateBanner(BannerVM updateBanner);
        Task<bool> DeleteBanner(int id);
        Task<List<BannerVM>?> GetAllBanner();

    }
}
