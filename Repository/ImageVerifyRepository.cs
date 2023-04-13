using MOBY_API_Core6.Repository.IRepository;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MOBY_API_Core6.Repository
{
    public class ImageVerifyRepository : IImageVerifyRepository
    {
        public async Task<bool> verify(String url)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://vision.googleapis.com");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var jsonRequestString = "{  'requests': [    {      'image': {        'source': {            'imageUri': '" + url + "'        }      },      'features': [        {          'type': 'SAFE_SEARCH_DETECTION'        }      ]    }  ]}";

            var encodeRequestData = new StringContent(jsonRequestString, System.Text.Encoding.UTF8, "application/json");


            var response = await client.PostAsync(
                "/v1/images:annotate?alt=json&key=AIzaSyCxji22udm5vkVgN72OXcYWnqIYbAUwtYk",
                encodeRequestData);

            response.EnsureSuccessStatusCode();

            Console.Write(response.ToString());

            String responseBody = await response.Content.ReadAsStringAsync();
            dynamic responseData = JsonConvert.DeserializeObject(responseBody)!;

            var adult = (String)responseData.responses[0].safeSearchAnnotation.adult;
            var medical = (String)responseData.responses[0].safeSearchAnnotation.medical;
            var violence = (String)responseData.responses[0].safeSearchAnnotation.violence;
            var racy = (String)responseData.responses[0].safeSearchAnnotation.racy;

            List<String> rates = new List<String> { "VERY_LIKELY", "POSSIBLE" };

            if (rates.Contains(adult) || rates.Contains(medical) || rates.Contains(violence) || rates.Contains(racy))
            {
                return false;
            }

            return true;
        }
    }
}
