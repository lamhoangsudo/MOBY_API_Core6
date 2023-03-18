using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Data_View_Model
{
    public class UserVM
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string UserGmail { get; set; } = null!;
        public string UserImage { get; set; } = null!;
        public int Reputation { get; set; }
        public bool UserStatus { get; set; }
        public DateTime UserDateCreate { get; set; }

        public static UserVM UserAccountToVewModel(UserAccount user)
        {

            return new UserVM
            {
                UserId = user.UserId,
                UserName = user.UserName,
                UserGmail = user.UserGmail,
                UserImage = user.UserImage,
                Reputation = user.Reputation,
                UserStatus = user.UserStatus,
                UserDateCreate = user.UserDateCreate,

            };
        }
    }
}
