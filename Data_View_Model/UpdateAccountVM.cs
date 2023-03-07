using System.ComponentModel;

namespace MOBY_API_Core6.Data_View_Model
{
    public class UpdateAccountVM
    {
        [DefaultValue("String UserName")]
        public string? UserName { get; set; }
        [DefaultValue("String UserImage")]
        public string? UserImage { get; set; }
        [DefaultValue("String UserAddress")]
        public string? UserAddress { get; set; }
        [DefaultValue("String UserPhone")]
        public string? UserPhone { get; set; }
        [DefaultValue(true)]
        public bool? UserSex { get; set; }
        [DefaultValue("2023-02-17")]
        public String? UserDateOfBirth { get; set; }
        [DefaultValue("String UserMoreInformation")]
        public string? UserMoreInformation { get; set; }
    }
}
