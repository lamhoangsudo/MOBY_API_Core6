using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public interface IUserAddressRepository
    {
        Task<bool> createNewAddress(MyAddressVM myAddressVM);
        Task<List<string>?> getMylistAddress(int userID);
        Task<bool> deleteMyAddress(MyAddressVM myAddressVM);
    }
}
