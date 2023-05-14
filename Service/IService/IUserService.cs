using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using System.Security.Claims;

namespace MOBY_API_Core6.Service.IService
{
    public interface IUserService
    {
        public Task<bool> CheckExistedUser(string userCode);
        public Task<UserAccount?> FindUserByCode(string userCode);
        public Task<UserAccount?> FindUserByUid(int uid);
        public Task<UserAccount?> FindUserByUidWithoutStatus(int uid);
        public Task<int> GetUserIDByUserCode(string userCode);
        public Task<bool> CreateUser(IEnumerable<Claim> claims, CreateAccountVM createUserVm);
        public Task<bool> EditUser(UserAccount currentUser, UpdateAccountVM accountVM);
        public Task<bool> EditBankAccount(UserAccount currentUser, UpdateBankAccount accountVM);
        public Task<List<UserVM>> GetAllUser(PaggingVM pagging, UserAccountFilterVM userAccountFilterVM);
        public Task<int> GetAllUserCount();
        public Task<bool> BanUser(UserUidVM uid);
        public Task<bool> UnbanUser(UserUidVM uid);
    }
}
