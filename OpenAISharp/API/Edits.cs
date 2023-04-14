using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace OpenAISharp.API
{
    public class Edits
    {
        private static string command = "/edits";
        public enum AvailableModel
        {
            [Description("text-davinci-edit-001")]
            text_davinci_edit_001,
            [Description("code-davinci-edit-001")]
            code_davinci_edit_001
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
        public string input { get; set; }
        public string instruction { get; set; }
        public int? n { get; set; }
        public decimal? temperature { get; set; }
        public decimal? top_p { get; set; }

        public static async Task<string> Request(string input, string instruction, AvailableModel selectedModel = AvailableModel.text_davinci_edit_001)
        {
            EditsResponse editsResponse = await Request(new Edits() { 
                SelectedModel = selectedModel,
                input = input,
                instruction = instruction
            });
            return editsResponse.error != null ? editsResponse.error.message : editsResponse.choices[0].text;
        }
        public static async Task<EditsResponse> Request(Edits edits)
        {
            EditsResponse editsResponse = null;
            string result;
            string organzationID = OpenAISettings.OrganizationID;
            string apiKey = OpenAISettings.ApiKey;
            var apiUrl = $"{OpenAISettings.UrlPrefix}{command}";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                client.DefaultRequestHeaders.Add("OpenAI-Organization", organzationID);
                string requestJson = JsonConvert.SerializeObject(edits, Formatting.Indented, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
                StringContent requestJsonContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
                using (HttpResponseMessage response = await client.PostAsync(apiUrl, requestJsonContent))
                {
                    result = await response.Content.ReadAsStringAsync();
                    editsResponse = JsonConvert.DeserializeObject<EditsResponse>(result);
                }
            }
            return editsResponse;
        }
    }
}
