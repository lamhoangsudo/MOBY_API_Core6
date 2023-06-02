using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;
using MOBY_API_Core6.Service.IService;
using System.Security.Claims;

namespace MOBY_API_Core6.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<string> CheckExistedUser(string userCode)
        {
            string result;
            var existUser = await userRepository.CheckExistedUser(userCode);
            if (existUser == null)
            {
                result = "new user";
            }
            else if (existUser.Role.RoleName.Equals("Admin"))
            {
                result = "Admin";
            }
            else
            {
                result = "User existed";
            }
            return result;
        }

        public async Task<UserAccount?> FindUserByCode(string userCode)
        {
            UserAccount? foundAccount = await userRepository.FindUserByCode(userCode);
            return foundAccount;
        }

        public async Task<UserAccount?> FindUserByUid(int uid)
        {
            UserAccount? foundAccount = await userRepository.FindUserByUid(uid);
            return foundAccount;
        }

        public async Task<UserAccount?> FindUserByUidWithoutStatus(int uid)
        {
            UserAccount? foundAccount = await userRepository.FindUserByUidWithoutStatus(uid);
            return foundAccount;
        }

        public async Task<int> GetUserIDByUserCode(string userCode)// nay laf tren token
        {
            return await userRepository.GetUserIDByUserCode(userCode);
        }

        public async Task<bool> CreateUser(IEnumerable<Claim> claims, CreateAccountVM createUserVM)
        {
            return await userRepository.CreateUser(claims, createUserVM);
        }

        public async Task<bool> EditUser(UserAccount currentUser, UpdateAccountVM accountVM)
        {
            return await userRepository.EditUser(currentUser, accountVM);
        }

        public async Task<bool> EditBankAccount(UserAccount currentUser, UpdateBankAccount accountVM)
        {
            return await userRepository.EditBankAccount(currentUser, accountVM);
        }

        public async Task<List<UserVM>> GetAllUser(PaggingVM pagging, UserAccountFilterVM userAccountFilterVM)
        {
            List<UserVM> accountListVM = await userRepository.GetAllUser(pagging, userAccountFilterVM);
            return accountListVM;
        }

        public async Task<int> GetAllUserCount(UserAccountFilterVM userAccountFilterVM)
        {
            return await userRepository.GetAllUserCount(userAccountFilterVM);
        }

        public async Task<bool> BanUser(UserUidVM uid)
        {
            return await userRepository.BanUser(uid);
        }

        public async Task<bool> UnbanUser(UserUidVM uid)
        {
            return await userRepository.UnbanUser(uid);
        }
    }
}
