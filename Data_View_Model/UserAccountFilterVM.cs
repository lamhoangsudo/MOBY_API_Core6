using System.ComponentModel;

namespace MOBY_API_Core6.Data_View_Model
{
    public class UserAccountFilterVM
    {
        [DefaultValue("")]
        public string? UserName { get; set; }

        [DefaultValue("")]
        public string? UserGmail { get; set; }

        [DefaultValue(null)]
        public bool? UserStatus { get; set; }
    }
}
