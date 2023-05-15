using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;
using MOBY_API_Core6.Service.IService;

namespace MOBY_API_Core6.Service
{
    public class UserAddressService : IUserAddressService
    {
        public static string ErrorMessage { get; set; } = string.Empty;
        private readonly IUserAddressRepository _userAddressRepository;

        public UserAddressService(IUserAddressRepository userAddressRepository)
        {
            _userAddressRepository = userAddressRepository;
        }

        public async Task<bool> CreateNewAddress(CreateMyAddressVM createMyAddressVM, int uid)
        {
            try
            {
                if (await _userAddressRepository.CheckExitedAddress(createMyAddressVM, uid) != null)
                {
                    throw new InvalidDataException();
                }
                int check = await _userAddressRepository.CreateNewAddress(createMyAddressVM, uid);
                if (check != 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }
        public async Task<List<MyAddressVM>?> GetMylistAddress(int userID)
        {
            try
            {
                List<MyAddressVM>? addresses = await _userAddressRepository.GetMylistAddress(userID);
                if (addresses == null)
                {
                    addresses = new List<MyAddressVM>();
                }
                return addresses;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        public async Task<UserAddress?> FindUserAddressByUserAddressID(int userAddressID, int userID)
        {
            try
            {
                UserAddress? currentUserAddress = await _userAddressRepository.FindUserAddressByUserAddressID(userAddressID, userID);
                return currentUserAddress;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        public async Task<bool> UpdateUserAddress(UpdateMyAddressVM updateMyAddressVM, UserAddress userAddress)
        {
            try
            {
                int check = await _userAddressRepository.UpdateUserAddress(updateMyAddressVM, userAddress);
                if (check != 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public async Task<bool> DeleteMyAddress(UserAddress userAddress)
        {
            try
            {
                int check = await _userAddressRepository.DeleteMyAddress(userAddress);
                if (check != 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }

        }
    }
}
