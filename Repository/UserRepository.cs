using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> CheckExistedUser(String userCode)
        {
            //FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(token);

            //UserRecord user = await FirebaseAuth.DefaultInstance.GetUserAsync(decodedToken.Uid);


            var existUser = await context.UserAccounts.Where(u => u.UserCode == userCode).FirstOrDefaultAsync();

            if (existUser != null)
            {
                return true;
            }
            return false;
        }

        public async Task<UserAccount?> FindUserByCode(String userCode)
        {
            UserAccount? foundAccount = await context.UserAccounts
                .Where(u => u.UserCode == userCode)
                .Include(u => u.Carts)
                .FirstOrDefaultAsync();
            return foundAccount;
        }

        public async Task<UserAccount?> FindUserByUid(int uid)
        {
            UserAccount? foundAccount = await context.UserAccounts.Where(u => u.UserId == uid)
                .Include(u => u.Carts)
                .FirstOrDefaultAsync();
            return foundAccount;
        }

        /*public async Task<int?> GetRoleByToken(IEnumerable<Claim> claims)
        {
            var uid = claims.First(i => i.Type == "user_id").Value;

            UserAccount foundAccount = context.UserAccounts.Where(u => u.UserCode == uid).FirstOrDefault();
            return foundAccount.RoleId;

        }*/

        public async Task<int> getUserIDByUserCode(String userCode)// nay laf tren token
        {
            UserAccount? Userfound = await context.UserAccounts.Where(u => u.UserCode == userCode).FirstOrDefaultAsync();
            if (Userfound != null)
            {
                return Userfound.UserId;
            }
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return Userfound.UserId;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }



        public async Task<bool> CreateUser(IEnumerable<Claim> claims, CreateAccountVM createUserVM)
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

            //new cart: 
            var cart = new Cart();
            newUser.Carts.Add(cart);


            var addUser = context.UserAccounts.AddAsync(newUser);
            if (await context.SaveChangesAsync() != 0)
            {
                return true;
            }
            return false;


        }

        public async Task<bool> EditUser(UserAccount currentUser, UpdateAccountVM accountVM)
        {
            currentUser.UserName = accountVM.UserName;

            currentUser.UserAddress = accountVM.UserAddress;
            currentUser.UserPhone = accountVM.UserPhone;
            currentUser.UserSex = accountVM.UserSex;
            currentUser.UserDateOfBirth = DateTime.Parse(accountVM.UserDateOfBirth);
            currentUser.UserImage = accountVM.UserImage;
            //currentUser.UserStatus = true;
            currentUser.UserMoreInformation = accountVM.UserMoreInformation;
            currentUser.UserDateUpdate = DateTime.Now;

            if (await context.SaveChangesAsync() != 0)
            {
                return true;
            }
            return false;

        }


        public async Task<List<UserVM>> GetAllUser(PaggingVM pagging)
        {
            int itemsToSkip = (pagging.pageNumber - 1) * pagging.pageSize;
            List<UserVM> accountListVM = await context.UserAccounts
                .Skip(itemsToSkip)
                .Take(pagging.pageSize)
                .Select(u => UserVM.UserAccountToVewModel(u))
                .ToListAsync();

            return accountListVM;
        }


        public async Task<bool> BanUser(UserUidVM uid)
        {


            UserAccount? foundAccount = await context.UserAccounts.Where(u => u.UserId == uid.UserId).FirstOrDefaultAsync();
            if (foundAccount != null)
            {
                foundAccount.UserStatus = false;
                await context.SaveChangesAsync();
                return true;
            }


            return false;
        }

        public async Task<bool> UnbanUser(UserUidVM uid)
        {


            UserAccount? foundAccount = await context.UserAccounts.Where(u => u.UserId == uid.UserId).FirstOrDefaultAsync();
            if (foundAccount != null)
            {
                foundAccount.UserStatus = true;
                await context.SaveChangesAsync();
                return true;

            }
            return false;

        }
    }
}
