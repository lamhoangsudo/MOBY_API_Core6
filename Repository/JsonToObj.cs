using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using Newtonsoft.Json;

namespace MOBY_API_Core6.Repository
{
    public class JsonToObj
    {
        public string? TransformLocation(string json)
        {
            Location locationObject = JsonConvert.DeserializeObject<Location>(json);
            string? location = null;
            if (locationObject != null)
            {
                if(locationObject.AddressProvince != null)
                {
                    location = "addressProvince:" + locationObject.AddressProvince;
                    if(locationObject.AddressDistrict != null)
                    {
                        location = location + "," + "addressDistrict:" + locationObject.AddressDistrict;
                        if(locationObject.AddressWard != null)
                        {
                            location = location + "," + "addressWard:" + locationObject.AddressWard;
                            if(locationObject.AddressDetail != null)
                            {
                                location = location + "," + "addressDetail:" + locationObject.AddressDetail;
                            }
                        }
                    }
                }
            }
            return location;
        }
    }
}
