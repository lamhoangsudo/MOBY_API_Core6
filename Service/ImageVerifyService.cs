using MOBY_API_Core6.Service.IService;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MOBY_API_Core6.Service
{
    public class ImageVerifyService : IImageVerifyService
    {
        public async Task<string> Verify(string url)
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

            List<string> rates = new List<string> { "VERY_LIKELY", "LIKELY", "POSSIBLE" };
            List<string> rates2 = new List<string> { "VERY_LIKELY", "LIKELY" };

            string result = "Hình ảnh hợp lệ";
            if (rates2.Contains(adult))
            {
                result = "Hình ảnh mang tính gợi dục. Vui lòng chọn ảnh khác";
            }

            if (medical.Contains("VERY_LIKELY"))
            {
                result = "Hình ảnh liên quan đến các vấn đề pháp y, bệnh lý. Vui lòng chọn ảnh khác";
            }

            if (rates.Contains(violence))
            {
                result = "Hình ảnh mang tính bạo lực. Vui lòng chọn ảnh khác";
            }

            /*if (rates.Contains(racy))
            {
                result = "Hình ảnh mang tính phân biệt chủng tộc. Vui lòng chọn ảnh khác";
            }*/

            return result;
        }
    }
}
