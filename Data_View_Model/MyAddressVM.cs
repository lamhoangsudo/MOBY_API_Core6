using MOBY_API_Core6.Models;
using System.ComponentModel;

namespace MOBY_API_Core6.Data_View_Model
{
    public class MyAddressVM
    {
        [DefaultValue(null)]
        public string? address { get; set; }


        public static MyAddressVM MyAddressToViewModel(UserAddress userAddress)
        {
            return new MyAddressVM
            {
                address = userAddress.Address
            };
        }
    }
}




