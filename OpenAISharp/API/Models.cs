using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OpenAISharp.API
{
    public class Models
    {
        public static async Task<Models> List(bool cachedData = true)
        {
            string command = "/models";
            string result;
            Models models = new Models();
            if (!cachedData)
            {
                string organzationID = OpenAISettings.OrganizationID;
                string apiKey = OpenAISettings.ApiKey;
                var apiUrl = $"{OpenAISettings.UrlPrefix}{command}";

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                    client.DefaultRequestHeaders.Add("OpenAI-Organization", organzationID);
                    using (HttpResponseMessage response = await client.GetAsync(apiUrl))
                    {
                        result = await response.Content.ReadAsStringAsync();
                        System.IO.File.WriteAllText("cachedData\\models.json", result);
                        models = JsonConvert.DeserializeObject<Models>(result);
                    }
                }
            }
            else
            {
                models = JsonConvert.DeserializeObject<Models>(System.IO.File.ReadAllText("cachedData\\models.json"));
            }

            return models;
        }
        public static async Task<Model> Get(string ModelName, bool cachedData = true)
        {
            string command = "/models/{0}";
            string result;
            Model model = new Model();
            if (!cachedData)
            {
                string organzationID = OpenAISettings.OrganizationID;
                string apiKey = OpenAISettings.ApiKey;
                var apiUrl = $"{OpenAISettings.UrlPrefix}{string.Format(command, ModelName)}";

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                    client.DefaultRequestHeaders.Add("OpenAI-Organization", organzationID);
                    using (HttpResponseMessage response = await client.GetAsync(apiUrl))
                    {
                        result = await response.Content.ReadAsStringAsync();
                        System.IO.File.WriteAllText($"cachedData\\models.{ModelName}.json", result);
                        model = JsonConvert.DeserializeObject<Model>(result);
                    }
                }
            }
            else
            {
                model = JsonConvert.DeserializeObject<Model>(System.IO.File.ReadAllText($"cachedData\\models.{ModelName}.json"));
            }

            return model;
        }
        public string @object { get; set; }
        public List<Model> data { get; set; }
    }
    public class Model
    {
        public string id { get; set; }
        public string @object { get; set; }
        public int created { get; set; }
        public string owned_by { get; set; }
        public List<Permission> permission { get; set; }
        public string root { get; set; }
        public object parent { get; set; }
    }

    public class Permission
    {
        public string id { get; set; }
        public string @object { get; set; }
        public int created { get; set; }
        public bool allow_create_engine { get; set; }
        public bool allow_sampling { get; set; }
        public bool allow_logprobs { get; set; }
        public bool allow_search_indices { get; set; }
        public bool allow_view { get; set; }
        public bool allow_fine_tuning { get; set; }
        public string organization { get; set; }
        public object group { get; set; }
        public bool is_blocking { get; set; }
    }
}
