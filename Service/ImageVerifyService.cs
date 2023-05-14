using MOBY_API_Core6.Service.IService;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MOBY_API_Core6.Service
{
    public class ImageVerifyService : IImageVerifyService
    {
        public async Task<bool> Verify(string url)
        {
            HttpClient client = new()
            {
                BaseAddress = new Uri("https://vision.googleapis.com")
            };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var jsonRequestString = "{  'requests': [    {      'image': {        'source': {            'imageUri': '" + url + "'        }      },      'features': [        {          'type': 'SAFE_SEARCH_DETECTION'        }      ]    }  ]}";

            var encodeRequestData = new StringContent(jsonRequestString, System.Text.Encoding.UTF8, "application/json");


            var response = await client.PostAsync(
                "/v1/images:annotate?alt=json&key=AIzaSyCxji22udm5vkVgN72OXcYWnqIYbAUwtYk",
                encodeRequestData);

            response.EnsureSuccessStatusCode();

            Console.Write(response.ToString());

            string responseBody = await response.Content.ReadAsStringAsync();
            dynamic responseData = JsonConvert.DeserializeObject(responseBody)!;

            var adult = (string)responseData.responses[0].safeSearchAnnotation.adult;
            var medical = (string)responseData.responses[0].safeSearchAnnotation.medical;
            var violence = (string)responseData.responses[0].safeSearchAnnotation.violence;
            var racy = (string)responseData.responses[0].safeSearchAnnotation.racy;

            List<string> rates = new List<string> { "VERY_LIKELY", "POSSIBLE" };

            if (rates.Contains(adult) || rates.Contains(medical) || rates.Contains(violence) || rates.Contains(racy))
            {
                return false;
            }

            return true;
        }
    }
}
