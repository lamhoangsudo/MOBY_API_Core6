using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository.IRepository
{
    public interface IBabyRepository
    {
        Task<bool> InputInformationBaby(BabyVM babyVM);
        Task<bool> UpdateInformationBaby(UpdateBabyVM babyVM);
        Task<List<Baby>?> GetBabyByUserID(int id);
    }
}
