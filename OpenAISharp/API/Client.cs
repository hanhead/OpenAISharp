using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAISharp.API
{
    public class Client
    {
        public static async Task<T> Request<T>(string command, object request = null)
        {
            T resultResponse;
            string result;
            string organzationID = OpenAISettings.OrganizationID;
            string apiKey = OpenAISettings.ApiKey;
            var apiUrl = $"{OpenAISettings.UrlPrefix}{command}";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                client.DefaultRequestHeaders.Add("OpenAI-Organization", organzationID);
                if (request != null)
                {
                    string requestJson = JsonConvert.SerializeObject(request, Formatting.Indented, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });
                    StringContent requestJsonContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
                    using (HttpResponseMessage response = await client.PostAsync(apiUrl, requestJsonContent))
                    {
                        result = await response.Content.ReadAsStringAsync();
                        resultResponse = JsonConvert.DeserializeObject<T>(result);
                    }
                }
                else
                {
                    using (HttpResponseMessage response = await client.GetAsync(apiUrl))
                    {
                        result = await response.Content.ReadAsStringAsync();
                        resultResponse = JsonConvert.DeserializeObject<T>(result);
                    }
                }
            }
            return resultResponse;
        }
    }
}
