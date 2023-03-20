using MOBY_API_Core6.Data_View_Model;

namespace MOBY_API_Core6.Repository
{
    public interface IUserAddressRepository
    {
        Task<bool> createNewAddress(CreateMyAddressVM createMyAddressVM, int uid);
        public Task<bool> CheckExitedAddress(CreateMyAddressVM createMyAddressVM, int uid);
        Task<List<MyAddressVM>?> getMylistAddress(int userID);
        Task<bool> deleteMyAddress(CreateMyAddressVM createMyAddressVM, int uid);
    }
}
