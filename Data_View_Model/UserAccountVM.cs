using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Data_View_Model
{
    public class UserAccountVM
    {
        public int UserId { get; set; }
        public string UserCode { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string UserGmail { get; set; } = null!;
        public string UserAddress { get; set; } = null!;
        public string? UserPhone { get; set; }
        public bool? UserSex { get; set; }
        public DateTime UserDateOfBirth { get; set; }
        public string? UserMoreInformation { get; set; }
        public string UserImage { get; set; } = null!;
        public int Reputation { get; set; }
        public bool UserStatus { get; set; }
        public DateTime UserDateCreate { get; set; }
        public DateTime? UserDateUpdate { get; set; }
        public int CartID { get; set; }

        public static UserAccountVM UserAccountToVewModel(UserAccount user, int cartid)
        {
            return new UserAccountVM
            {
                UserId = user.UserId,
                UserCode = user.UserCode,
                UserName = user.UserName,
                UserGmail = user.UserGmail,
                UserAddress = user.UserAddress,
                UserPhone = user.UserPhone,
                UserSex = user.UserSex,
                UserDateOfBirth = user.UserDateOfBirth,
                UserMoreInformation = user.UserMoreInformation,
                UserImage = user.UserImage,
                Reputation = user.Reputation,
                UserStatus = user.UserStatus,
                UserDateCreate = user.UserDateCreate,
                UserDateUpdate = user.UserDateUpdate,
                CartID = cartid,
            };
        }

        public static UserAccountVM UserAccountToVewModel(UserAccount user)
        {
            return new UserAccountVM
            {
                UserId = user.UserId,
                UserCode = user.UserCode,
                UserName = user.UserName,
                UserGmail = user.UserGmail,
                UserAddress = user.UserAddress,
                UserPhone = user.UserPhone,
                UserSex = user.UserSex,
                UserDateOfBirth = user.UserDateOfBirth,
                UserMoreInformation = user.UserMoreInformation,
                UserImage = user.UserImage,
                Reputation = user.Reputation,
                UserStatus = user.UserStatus,
                UserDateCreate = user.UserDateCreate,
                UserDateUpdate = user.UserDateUpdate,

            };
        }
    }
}
