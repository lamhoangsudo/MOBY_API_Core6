using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Service.IService
{
    public interface IUserAddressService
    {
        Task<bool> CreateNewAddress(CreateMyAddressVM createMyAddressVM, int uid);
        public Task<bool> CheckExitedAddress(CreateMyAddressVM createMyAddressVM, int uid);
        Task<List<MyAddressVM>?> GetMylistAddress(int userID);
        public Task<UserAddress?> FindUserAddressByUserAddressID(int userAddressID, int userID);
        public Task<bool> UpdateUserAddress(UpdateMyAddressVM updateMyAddressVM, UserAddress userAddress);
        Task<bool> DeleteMyAddress(UserAddress userAddress);
    }
}
