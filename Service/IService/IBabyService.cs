using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Service.IService
{
    public interface IBabyService
    {
        Task<bool> InputInformationBaby(CreateBabyVM babyVM);
        Task<bool> UpdateInformationBaby(UpdateBabyVM babyVM);
        Task<List<Baby>?> GetBabyByUserID(int id);
        Task<bool> DeleteBaby(int id, int us);
    }
}
