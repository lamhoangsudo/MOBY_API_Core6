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


        public async Task<int?> GetRoleByToken(IEnumerable<Claim> claims)
        {
            var uid = claims.First(i => i.Type == "user_id").Value;

            UserAccount foundAccount = context.UserAccounts.Where(u => u.UserCode == uid).FirstOrDefault();
            return foundAccount.RoleId;

        }

        public async Task<int> getUserIDByUserCode(String userCode)
        {
            UserAccount Userfound = context.UserAccounts.Where(u => u.UserCode == userCode).FirstOrDefault();
            return Userfound.UserId;
        }



        public async Task<bool> CreateUser(IEnumerable<Claim> claims, String address, String phone, bool sex, String dateOfBirth)
        {

            try
            {
                UserAccount newUser = new UserAccount();
                newUser.UserCode = claims.First(i => i.Type == "user_id").Value;
                newUser.RoleId = 1;

                newUser.UserName = claims.First(i => i.Type.Contains("identity/claims/name")).Value;
                newUser.UserGmail = claims.First(i => i.Type.Contains("emailaddress")).Value;
                newUser.UserAddress = address;
                newUser.UserPhone = phone;
                newUser.UserSex = sex;


                newUser.UserDateOfBirth = DateTime.Parse(dateOfBirth);
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

        public async Task<bool> EditUser(UserAccount currentUser, String userName, String picture, String address, String phone, bool sex, String dateOfBirth, String User_More_Information)
        {

            try
            {
                //FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);

                //UserRecord user = await FirebaseAuth.DefaultInstance.GetUserAsync(decodedToken.Uid);

                currentUser.UserName = userName;

                currentUser.UserAddress = address;
                currentUser.UserPhone = phone;
                currentUser.UserSex = sex;
                currentUser.UserDateOfBirth = DateTime.Parse(dateOfBirth);
                currentUser.UserImage = picture;
                //currentUser.UserStatus = true;
                currentUser.UserMoreInformation = User_More_Information;
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


        public async Task<List<UserAccount>> GetAllUser()
        {
            List<UserAccount> accountList = new List<UserAccount>();
            accountList = context.UserAccounts.ToList();
            return accountList;
        }

        public async Task<String> BanUser(String uid)
        {

            try
            {
                UserAccount foundAccount = context.UserAccounts.Where(u => u.UserCode == uid).FirstOrDefault();
                foundAccount.UserStatus = false;
                return "success";
            }
            catch
            {
                return "false";
            }
            return "false";
        }

        public async Task<String> UnbanUser(String uid)
        {

            try
            {
                UserAccount foundAccount = context.UserAccounts.Where(u => u.UserCode == uid).FirstOrDefault();
                foundAccount.UserStatus = true;
                return "success";
            }
            catch
            {
                return "false";
            }
            return "false";
        }
    }
}
