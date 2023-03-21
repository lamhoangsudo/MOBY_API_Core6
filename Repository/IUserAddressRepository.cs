using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository
{
    public interface IUserAddressRepository
    {
        Task<bool> createNewAddress(CreateMyAddressVM createMyAddressVM, int uid);
        public Task<bool> CheckExitedAddress(CreateMyAddressVM createMyAddressVM, int uid);
        Task<List<MyAddressVM>?> getMylistAddress(int userID);
        public Task<UserAddress?> FindUserAddressByUserAddressID(int userAddressID, int userID);
        public Task<bool> UpdateUserAddress(UpdateMyAddressVM updateMyAddressVM, UserAddress userAddress);
        Task<bool> deleteMyAddress(UserAddress userAddress);
    }
}
