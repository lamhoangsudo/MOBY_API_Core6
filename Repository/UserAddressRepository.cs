using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;

namespace MOBY_API_Core6.Repository
{
    public class UserAddressRepository : IUserAddressRepository
    {
        private readonly MOBYContext _context;
        public static string? ErrorMessage { get; set; }

        public UserAddressRepository(MOBYContext context)
        {
            _context = context;
        }

        public async Task<int> CreateNewAddress(CreateMyAddressVM createMyAddressVM, int uid)
        {
            UserAddress userAddress = new()
            {
                Address = createMyAddressVM.Address,
                UserId = uid
            };
            await _context.UserAddresses.AddAsync(userAddress);
            return await _context.SaveChangesAsync();
        }

        public async Task<UserAddress?> CheckExitedAddress(CreateMyAddressVM createMyAddressVM, int uid)
        {

            return await _context.UserAddresses
                .Where(ua => ua.UserId == uid && ua.Address.Equals(createMyAddressVM.Address))
                .FirstOrDefaultAsync();
        }

        public async Task<List<MyAddressVM>?> GetMylistAddress(int userID)
        {
            return await _context.UserAddresses
                    .Where(ua => ua.UserId == userID)
                    .Select(ma => MyAddressVM.MyAddressToViewModel(ma))
                    .ToListAsync();
        }
        public async Task<UserAddress?> FindUserAddressByUserAddressID(int userAddressID, int userID)
        {
            return await _context.UserAddresses
                .Where(ud => ud.Id == userAddressID && ud.UserId == userID)
                .FirstOrDefaultAsync();
        }

        public async Task<int> UpdateUserAddress(UpdateMyAddressVM updateMyAddressVM, UserAddress userAddress)
        {
            userAddress.Address = updateMyAddressVM.Address;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteMyAddress(UserAddress userAddress)
        {
            _context.UserAddresses.Remove(userAddress);
            return await _context.SaveChangesAsync();
        }
    }
}
