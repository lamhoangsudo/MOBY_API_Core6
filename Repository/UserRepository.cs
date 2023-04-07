using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using System.Security.Claims;

namespace MOBY_API_Core6.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MOBYContext context;
        private readonly IEmailRepository emailDAO;
        public UserRepository(MOBYContext context, IEmailRepository emailDAO)
        {
            this.context = context;
            this.emailDAO = emailDAO;
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
            newUser.Reputation = 80;
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
            Cart cart = new Cart
            {
                Address = newUser.UserAddress,

            };
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

            currentUser.UserMoreInformation = accountVM.UserMoreInformation;
            currentUser.UserDateUpdate = DateTime.Now;

            if (await context.SaveChangesAsync() != 0)
            {
                return true;
            }
            return false;

        }


        public async Task<List<UserVM>> GetAllUser(PaggingVM pagging, UserAccountFilterVM userAccountFilterVM)
        {
            int itemsToSkip = (pagging.pageNumber - 1) * pagging.pageSize;
            List<UserVM> accountListVM = new List<UserVM>();
            if (userAccountFilterVM.UserName == null || userAccountFilterVM.UserGmail == null)
            {
                return accountListVM;
            }
            if (userAccountFilterVM.UserStatus != null)
            {
                if (pagging.orderBy)
                {
                    accountListVM = await context.UserAccounts
                    .Where(uc => uc.UserStatus == userAccountFilterVM.UserStatus && uc.UserName.Contains(userAccountFilterVM.UserName) && uc.UserGmail.Contains(userAccountFilterVM.UserGmail))
                    .Skip(itemsToSkip)
                    .Take(pagging.pageSize)
                    .OrderByDescending(x => x.UserId)
                    .Select(u => UserVM.UserAccountToVewModel(u))
                    .ToListAsync();

                }
                else
                {
                    accountListVM = await context.UserAccounts
                    .Where(uc => uc.UserStatus == userAccountFilterVM.UserStatus && uc.UserName.Contains(userAccountFilterVM.UserName) && uc.UserGmail.Contains(userAccountFilterVM.UserGmail))
                    .Skip(itemsToSkip)
                    .Take(pagging.pageSize)
                    .Select(u => UserVM.UserAccountToVewModel(u))
                    .ToListAsync();

                }
            }
            else
            {
                if (pagging.orderBy)
                {
                    accountListVM = await context.UserAccounts
                    .Where(uc => uc.UserName.Contains(userAccountFilterVM.UserName) && uc.UserGmail.Contains(userAccountFilterVM.UserGmail))
                    .Skip(itemsToSkip)
                    .Take(pagging.pageSize)
                    .OrderByDescending(x => x.UserId)
                    .Select(u => UserVM.UserAccountToVewModel(u))
                    .ToListAsync();

                }
                else
                {
                    accountListVM = await context.UserAccounts
                    .Where(uc => uc.UserName.Contains(userAccountFilterVM.UserName) && uc.UserGmail.Contains(userAccountFilterVM.UserGmail))
                    .Skip(itemsToSkip)
                    .Take(pagging.pageSize)
                    .Select(u => UserVM.UserAccountToVewModel(u))
                    .ToListAsync();

                }
            }

            return accountListVM;
        }
        public async Task<int> GetAllUserCount()
        {

            int totalCount = await context.UserAccounts.CountAsync();

            return totalCount;
        }


        public async Task<bool> BanUser(UserUidVM uid)
        {


            UserAccount? foundAccount = await context.UserAccounts.Where(u => u.UserId == uid.UserId).FirstOrDefaultAsync();
            if (foundAccount != null)
            {
                foundAccount.UserStatus = false;
                await context.SaveChangesAsync();
                Email newEmail = new Email();
                newEmail.To = foundAccount.UserGmail;
                newEmail.Subject = "your has been ban";
                newEmail.Body = foundAccount.UserGmail + " has been banned by admintrator at " + DateTime.Now.ToString();
                emailDAO.SendEmai(newEmail);
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
                Email newEmail = new Email();
                newEmail.To = foundAccount.UserGmail;
                newEmail.Subject = "your has been unbanned";
                newEmail.Body = foundAccount.UserGmail + " has been unbanned by admintrator at " + DateTime.Now.ToString();
                emailDAO.SendEmai(newEmail);
                return true;

            }
            return false;

        }
    }
}
