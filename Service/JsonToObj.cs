using MOBY_API_Core6.Data_View_Model;
using MOBY_API_Core6.Models;
using Newtonsoft.Json;

namespace MOBY_API_Core6.Service
{
    public class JsonToObj
    {
        public string? TransformLocation(string json)
        {
            Location locationObject = JsonConvert.DeserializeObject<Location>(json);
            string? location = null;
            if (locationObject != null)
            {
                if (locationObject.AddressProvince != null)
                {
                    location = "addressProvince:" + locationObject.AddressProvince;
                    if (locationObject.AddressDistrict != null)
                    {
                        location = location + ";" + "addressDistrict:" + locationObject.AddressDistrict;
                        if (locationObject.AddressWard != null)
                        {
                            location = location + ";" + "addressWard:" + locationObject.AddressWard;
                            if (locationObject.AddressDetail != null)
                            {
                                location = location + ";" + "addressDetail:" + locationObject.AddressDetail;
                            }
                        }
                    }
                }
            }
            return location;
        }

        public string? TransformJsonLocation(string stringLocation)
        {
            string? stringData = stringLocation
                .Replace("addressProvince:", "")
                .Replace("addressDistrict:", "")
                .Replace("addressWard:", "")
                .Replace("addressDetail:", "");
            string[] data = stringData.Split(";");
            Location location = new Location
            {
                AddressProvince = data[0],
                AddressDistrict = data[1],
                AddressWard = data[2],
                AddressDetail = data[3]
            };
            string jsonLocation = JsonConvert.SerializeObject(location);
            return jsonLocation;
        }
    }
}
