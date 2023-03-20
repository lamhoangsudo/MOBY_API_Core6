using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;


namespace MOBY_API_Core6.Repository
{
    public class UserAddressRepository : IUserAddressRepository
    {
        private readonly MOBYContext _context;
        public static string? errorMessage;

        public UserAddressRepository(MOBYContext context)
        {
            _context = context;
        }

        public async Task<bool> createNewAddress(CreateMyAddressVM createMyAddressVM, int uid)
        {
            try
            {
                UserAddress userAddress = new UserAddress();

                userAddress.Address = createMyAddressVM.address;

                userAddress.UserId = uid;
                await _context.UserAddresses.AddAsync(userAddress);
                if (await _context.SaveChangesAsync() != 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public async Task<bool> CheckExitedAddress(CreateMyAddressVM createMyAddressVM, int uid)
        {

            UserAddress? addresses = await _context.UserAddresses
                .Where(ua => ua.UserId == uid && ua.Address.Equals(createMyAddressVM.address))
                .FirstOrDefaultAsync();

            if (addresses == null)
            {
                return false;
            }
            return true;

        }

        public async Task<List<MyAddressVM>?> getMylistAddress(int userID)
        {
            try
            {
                List<MyAddressVM> addresses = await _context.UserAddresses
                    .Where(ua => ua.UserId == userID)
                    .Select(ma => MyAddressVM.MyAddressToViewModel(ma))
                    .ToListAsync();

                return addresses;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }

        public async Task<bool> deleteMyAddress(CreateMyAddressVM createMyAddressVM, int uid)
        {
            try
            {
                UserAddress? addresse = await _context.UserAddresses
                    .Where(ua => ua.UserId == uid && ua.Address.Equals(createMyAddressVM.address))
                    .FirstOrDefaultAsync();
                if (addresse != null)
                {
                    _context.UserAddresses.Remove(addresse);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
    }
}
