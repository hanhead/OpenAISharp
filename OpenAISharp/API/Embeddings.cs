using Newtonsoft.Json;
using System.ComponentModel;
using System.Text;

namespace OpenAISharp.API
{
    public class Embeddings
    {
        private static string command = "/embeddings";
        public enum AvailableModel
        {
            [Description("text-embedding-ada-002")]
            text_embedding_ada_002,
            [Description("text-search-ada-doc-001")]
            text_search_ada_doc_001
        }
        [JsonIgnore]
        public AvailableModel SelectedModel { get; set; }
        private string _model;
        public string model
        {
            get
            {
                if (SelectedModel != null)
                {
                    return SelectedModel.GetDescription();
                }
                else
                {
                    return _model;
                }
            }
            set
            {
                _model = value;
            }
        }
        public string[] input { get; set; }
        public string user { get; set; }
        public static async Task<EmbeddingsResponse> Request(Embeddings embeddings)
        {
            EmbeddingsResponse embeddingsResponse = null;
            string result;
            string organzationID = OpenAISettings.OrganizationID;
            string apiKey = OpenAISettings.ApiKey;
            var apiUrl = $"{OpenAISettings.UrlPrefix}{command}";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                client.DefaultRequestHeaders.Add("OpenAI-Organization", organzationID);
                string requestJson = JsonConvert.SerializeObject(embeddings, Formatting.Indented, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
                StringContent requestJsonContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
                using (HttpResponseMessage response = await client.PostAsync(apiUrl, requestJsonContent))
                {
                    result = await response.Content.ReadAsStringAsync();
                    embeddingsResponse = JsonConvert.DeserializeObject<EmbeddingsResponse>(result);
                }
            }
            return embeddingsResponse;
        }
    }
}
