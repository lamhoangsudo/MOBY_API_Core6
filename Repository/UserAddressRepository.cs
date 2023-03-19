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

        public async Task<bool> createNewAddress(MyAddressVM myAddressVM)
        {
            try
            {
                UserAddress userAddress = new UserAddress();
#pragma warning disable CS8601 // Possible null reference assignment.
                userAddress.Address = myAddressVM.address;
#pragma warning restore CS8601 // Possible null reference assignment.
                userAddress.UserId = myAddressVM.userID;
                await _context.UserAddresses.AddAsync(userAddress);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public async Task<List<string>?> getMylistAddress(int userID)
        {
            try
            {
                List<string> addresses = await _context.UserAddresses.Where(ua => ua.UserId == userID).Select(ma => ma.Address).ToListAsync();

                return addresses;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }

        public async Task<bool> deleteMyAddress(MyAddressVM myAddressVM)
        {
            try
            {
                UserAddress? addresse = await _context.UserAddresses.Where(ua => ua.UserId == myAddressVM.userID && ua.Address.Equals(myAddressVM.address)).SingleOrDefaultAsync();
                if (addresse != null)
                {
                    _context.Remove(addresse);
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
