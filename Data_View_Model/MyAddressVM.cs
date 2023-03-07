using System.ComponentModel;

namespace MOBY_API_Core6.Data_View_Model
{
    public class MyAddressVM
    {
        [DefaultValue(null)]
        public string? address { get; set; }
        public int userID { get; set; }

        public MyAddressVM(string? address, int userID)
        {
            this.address = address;
            this.userID = userID;
        }
    }
}
