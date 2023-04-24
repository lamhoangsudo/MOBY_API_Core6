using System.ComponentModel;

namespace MOBY_API_Core6.Data_View_Model
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public class UpdateAccountVM
    {
        [DefaultValue("String UserName")]
        public string UserName { get; set; }
        [DefaultValue("String UserImage")]
        public string UserImage { get; set; }
        [DefaultValue("String UserAddress")]
        public string UserAddress { get; set; }
        [DefaultValue("String UserPhone")]
        public string UserPhone { get; set; }
        [DefaultValue(true)]
        public bool UserSex { get; set; }
        [DefaultValue("2023-02-17")]
        public string UserDateOfBirth { get; set; }
        [DefaultValue("String UserMoreInformation")]
        public string? UserMoreInformation { get; set; }
    }
}
