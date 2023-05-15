using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Repository.IRepository
{
    public interface IUserAddressRepository
    {
        Task<int> CreateNewAddress(CreateMyAddressVM createMyAddressVM, int uid);
        Task<UserAddress?> CheckExitedAddress(CreateMyAddressVM createMyAddressVM, int uid);
        Task<List<MyAddressVM>?> GetMylistAddress(int userID);
        Task<UserAddress?> FindUserAddressByUserAddressID(int userAddressID, int userID);
        Task<int> UpdateUserAddress(UpdateMyAddressVM updateMyAddressVM, UserAddress userAddress);
        Task<int> DeleteMyAddress(UserAddress userAddress);
    }
}
