using System.ComponentModel;

namespace MOBY_API_Core6.Data_View_Model
{
    public class CreateAccountVM
    {
        [DefaultValue("String UserAddress")]
        public string UserAddress { get; set; }
        [DefaultValue("String UserPhone")]
        public string UserPhone { get; set; }
        [DefaultValue(true)]
        public bool UserSex { get; set; }
        [DefaultValue("2023-02-17")]
        public string UserDateOfBirth { get; set; }

        public CreateAccountVM(string userAddress, string userPhone, bool userSex, string userDateOfBirth)
        {
            UserAddress = userAddress;
            UserPhone = userPhone;
            UserSex = userSex;
            UserDateOfBirth = userDateOfBirth;
        }
    }
}
