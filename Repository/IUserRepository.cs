﻿using MOBY_API_Core6.Models;
using System.Security.Claims;

namespace MOBY_API_Core6.Repository
{
    public interface IUserRepository
    {
        public Task<bool> CheckExistedUser(IEnumerable<Claim> claims);
        public Task<UserAccount?> FindUserByCode(String userCode);
        public Task<int> getUserIDByUserCode(String userCode);
        public Task<int?> GetRoleByToken(IEnumerable<Claim> claims);
        public Task<bool> CreateUser(IEnumerable<Claim> claims, String address, String phone, bool sex, String dateOfBirth);
        public Task<bool> EditUser(UserAccount currentUser, String userName, String picture, String address, String phone, bool sex, String dateOfBirth, String User_More_Information);
        public Task<List<UserAccount>> GetAllUser();
        public Task<bool> BanUser(String uid);
        public Task<bool> UnbanUser(String uid);
    }
}
