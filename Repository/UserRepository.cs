using Microsoft.EntityFrameworkCore;
using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using MOBY_API_Core6.Repository.IRepository;
using MOBY_API_Core6.Service.IService;
using System.Security.Claims;

namespace MOBY_API_Core6.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MOBYContext context;
        private readonly IEmailService emailDAO;

        public UserRepository(MOBYContext context, IEmailService emailDAO)
        {
            this.context = context;
            this.emailDAO = emailDAO;
        }

        public async Task<UserAccount?> CheckExistedUser(string userCode)
        {
            return await context.UserAccounts.Where(u => u.UserCode == userCode).FirstOrDefaultAsync();
        }

        public async Task<UserAccount?> FindUserByCode(string userCode)
        {
            return await context.UserAccounts
                .Where(u => u.UserCode == userCode && u.UserStatus == true)
                .Include(u => u.Carts)
                .FirstOrDefaultAsync();
        }

        public async Task<UserAccount?> FindUserByUid(int uid)
        {
            return await context.UserAccounts.Where(u => u.UserId == uid && u.UserStatus == true)
                .Include(u => u.Carts)
                .FirstOrDefaultAsync();
        }

        public async Task<UserAccount?> FindUserByUidWithoutStatus(int uid)
        {
            return await context.UserAccounts.Where(u => u.UserId == uid)
                .Include(u => u.Carts)
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetUserIDByUserCode(string userCode)// nay laf tren token
        {
            UserAccount? Userfound = await context.UserAccounts.Where(u => u.UserCode == userCode).FirstOrDefaultAsync();
            if (Userfound != null)
            {
                if (Userfound.UserStatus == false)
                {
                    return 0;
                }
                return Userfound.UserId;
            }
            return -1;
        }

        public async Task<bool> CreateUser(IEnumerable<Claim> claims, CreateAccountVM createUserVM)
        {
            UserAccount newUser = new()
            {
                UserCode = claims.First(i => i.Type == "user_id").Value,
                RoleId = 1,
                Reputation = 80,
                Balance = 0,
                UserName = claims.First(i => i.Type.Contains("identity/claims/name")).Value,
                UserGmail = claims.First(i => i.Type.Contains("emailaddress")).Value,
                UserAddress = createUserVM.UserAddress,
                UserPhone = createUserVM.UserPhone,
                UserSex = createUserVM.UserSex,
                UserDateOfBirth = DateTime.Parse(createUserVM.UserDateOfBirth),
                UserImage = claims.First(i => i.Type == "picture").Value,
                UserStatus = true,
                UserDateCreate = DateTime.Now
            };
            //new cart: 
            Cart cart = new()
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

        public async Task<bool> EditBankAccount(UserAccount currentUser, UpdateBankAccount accountVM)
        {
            currentUser.CardNumber = accountVM.CardNumber;
            currentUser.BankName = accountVM.BankName;
            if (await context.SaveChangesAsync() != 0)
            {
                return true;
            }
            return false;
        }

        public async Task<List<UserVM>> GetAllUser(PaggingVM pagging, UserAccountFilterVM userAccountFilterVM)
        {
            int itemsToSkip = (pagging.PageNumber - 1) * pagging.PageSize;
            List<UserVM> accountListVM = new();
            if (userAccountFilterVM.UserName == null || userAccountFilterVM.UserGmail == null)
            {
                return accountListVM;
            }
            if (userAccountFilterVM.UserStatus != null)
            {
                if (pagging.OrderBy)
                {
                    return await context.UserAccounts
                    .Where(uc => uc.UserStatus == userAccountFilterVM.UserStatus && uc.UserName.Contains(userAccountFilterVM.UserName) && uc.UserGmail.Contains(userAccountFilterVM.UserGmail))
                    .Skip(itemsToSkip)
                    .Take(pagging.PageSize)
                    .OrderByDescending(x => x.UserId)
                    .Select(u => UserVM.UserAccountToVewModel(u))
                    .ToListAsync();
                }
                else
                {
                    return await context.UserAccounts
                    .Where(uc => uc.UserStatus == userAccountFilterVM.UserStatus && uc.UserName.Contains(userAccountFilterVM.UserName) && uc.UserGmail.Contains(userAccountFilterVM.UserGmail))
                    .Skip(itemsToSkip)
                    .Take(pagging.PageSize)
                    .Select(u => UserVM.UserAccountToVewModel(u))
                    .ToListAsync();
                }
            }
            else
            {
                if (pagging.OrderBy)
                {
                    return await context.UserAccounts
                    .Where(uc => uc.UserName.Contains(userAccountFilterVM.UserName) && uc.UserGmail.Contains(userAccountFilterVM.UserGmail))
                    .Skip(itemsToSkip)
                    .Take(pagging.PageSize)
                    .OrderByDescending(x => x.UserId)
                    .Select(u => UserVM.UserAccountToVewModel(u))
                    .ToListAsync();
                }
                else
                {
                    return await context.UserAccounts
                    .Where(uc => uc.UserName.Contains(userAccountFilterVM.UserName) && uc.UserGmail.Contains(userAccountFilterVM.UserGmail))
                    .Skip(itemsToSkip)
                    .Take(pagging.PageSize)
                    .Select(u => UserVM.UserAccountToVewModel(u))
                    .ToListAsync();
                }
            }
            throw new InvalidDataException();
        }
        public async Task<int> GetAllUserCount()
        {
            return await context.UserAccounts.CountAsync();
        }

        public async Task<bool> BanUser(UserUidVM uid)
        {
            UserAccount? foundAccount = await context.UserAccounts.Where(u => u.UserId == uid.UserId).FirstOrDefaultAsync();
            if (foundAccount != null)
            {
                foundAccount.UserStatus = false;
                await context.SaveChangesAsync();
                Email newEmail = new()
                {
                    To = foundAccount.UserGmail,
                    Subject = "your has been ban",
                    Obj = "Account",
                    Link = foundAccount.UserGmail + " has been banned by admintrator at " + DateTime.Now.ToString()
                };
                await emailDAO.SendEmai(newEmail);
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
                Email newEmail = new()
                {
                    To = foundAccount.UserGmail,
                    Subject = "your has been unbanned",
                    Link = foundAccount.UserGmail + " has been unbanned by admintrator at " + DateTime.Now.ToString()
                };
                await emailDAO.SendEmai(newEmail);
                return true;
            }
            return false;
        }
    }
}
