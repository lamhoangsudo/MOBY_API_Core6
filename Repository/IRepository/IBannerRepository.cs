using MOBY_API_Core6.Data_View_Model;

namespace MOBY_API_Core6.Repository.IRepository
{
    public interface IBannerRepository
    {
        Task<int> CreateBanner(CreateBannerVM createBanner);
        Task<int> UpdateBanner(UpdateBannerVM updateBanner);
        Task<int> DeleteBanner(int id);
        Task<List<BannerVM>?> GetAllBanner();
    }
}
