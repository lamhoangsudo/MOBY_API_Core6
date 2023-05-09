using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Service.IService
{
    public interface IBabyRepository
    {
        Task<int> InputInformationBaby(CreateBabyVM babyVM);
        Task<int> UpdateInformationBaby(UpdateBabyVM babyVM);
        Task<List<Baby>?> GetBabyByUserID(int id);
        Task<int> DeleteBaby(int id, int us);
    }
}
