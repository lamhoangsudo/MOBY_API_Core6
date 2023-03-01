using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using System.Security.Claims;

namespace MOBY_API_Core6.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MOBYContext context;

        public UserRepository(MOBYContext context)
        {
            this.context = context;
        }

        public async Task<bool> CheckExistedUser(IEnumerable<Claim> claims)
        {
            //FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);

            //UserRecord user = await FirebaseAuth.DefaultInstance.GetUserAsync(decodedToken.Uid);

            var userId = claims.FirstOrDefault(i => i.Type == "user_id").Value;
            var existUser = context.UserAccounts.Where(u => u.UserCode == userId).FirstOrDefault();

            if (existUser != null)
            {
                return true;
            }
            return false;
        }

        public async Task<UserAccount?> FindUserByCode(String userCode)
        {
            UserAccount foundAccount;
            foundAccount = context.UserAccounts.Where(u => u.UserCode == userCode).FirstOrDefault();
            return foundAccount;
        }

        public async Task<UserAccount?> FindUserByUid(int uid)
        {
            UserAccount foundAccount;
            foundAccount = context.UserAccounts.Where(u => u.UserId == uid).FirstOrDefault();
            return foundAccount;
        }

        public async Task<int?> GetRoleByToken(IEnumerable<Claim> claims)
        {
            var uid = claims.First(i => i.Type == "user_id").Value;

            UserAccount foundAccount = context.UserAccounts.Where(u => u.UserCode == uid).FirstOrDefault();
            return foundAccount.RoleId;

        }

        public async Task<int> getUserIDByUserCode(String userCode)// nay laf tren token
        {
            UserAccount Userfound = context.UserAccounts.Where(u => u.UserCode == userCode).FirstOrDefault();
            return Userfound.UserId;
        }



        public async Task<bool> CreateUser(IEnumerable<Claim> claims, CreateAccountVM createUserVM)
        {

            try
            {
                UserAccount newUser = new UserAccount();
                newUser.UserCode = claims.First(i => i.Type == "user_id").Value;
                newUser.RoleId = 1;

                newUser.UserName = claims.First(i => i.Type.Contains("identity/claims/name")).Value;
                newUser.UserGmail = claims.First(i => i.Type.Contains("emailaddress")).Value;
                newUser.UserAddress = createUserVM.UserAddress;
                newUser.UserPhone = createUserVM.UserPhone;
                newUser.UserSex = createUserVM.UserSex;


                newUser.UserDateOfBirth = DateTime.Parse(createUserVM.UserDateOfBirth);
                newUser.UserImage = claims.First(i => i.Type == "picture").Value;
                newUser.UserStatus = true;
                newUser.UserDateCreate = DateTime.Now;



                var addUser = context.UserAccounts.Add(newUser);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            return false;
        }

        public async Task<bool> EditUser(UserAccount currentUser, UpdateAccountVM accountVM)
        {

            try
            {
                //FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);

                //UserRecord user = await FirebaseAuth.DefaultInstance.GetUserAsync(decodedToken.Uid);

                currentUser.UserName = accountVM.UserName;

                currentUser.UserAddress = accountVM.UserAddress;
                currentUser.UserPhone = accountVM.UserPhone;
                currentUser.UserSex = accountVM.UserSex;
                currentUser.UserDateOfBirth = DateTime.Parse(accountVM.UserDateOfBirth);
                currentUser.UserImage = accountVM.UserImage;
                //currentUser.UserStatus = true;
                currentUser.UserMoreInformation = accountVM.UserMoreInformation;
                currentUser.UserDateUpdate = DateTime.Now;



                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            return false;
        }


        public async Task<List<UserAccountVM>> GetAllUser()
        {
            List<UserAccountVM> accountListVM = context.UserAccounts.Select(u => UserAccountVM.UserAccountToVewModel(u)).ToList();

            return accountListVM;
        }


        public async Task<bool> BanUser(UserUidVM uid)
        {

            try
            {
                UserAccount foundAccount = context.UserAccounts.Where(u => u.UserId == uid.UserId).FirstOrDefault();
                foundAccount.UserStatus = false;
                return true;
            }
            catch
            {

            }
            return false;
        }

        public async Task<bool> UnbanUser(UserUidVM uid)
        {

            try
            {
                UserAccount foundAccount = context.UserAccounts.Where(u => u.UserId == uid.UserId).FirstOrDefault();
                foundAccount.UserStatus = true;
                return true;
            }
            catch
            {

            }
            return false;
        }
    }
}
