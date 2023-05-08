using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OpenAISharp.API
{
    public class Chat
    {
        private static string command = "/chat/completions";
        public enum AvailableModel
        {
            [Description("gpt-4")]
            gpt_4,
            [Description("gpt-4-0314")]
            gpt_4_0314,
            [Description("gpt-4-32k")]
            gpt_4_32k,
            [Description("gpt-4-32k-0314")]
            gpt_4_32k_0314,
            [Description("gpt-3.5-turbo")]
            gpt_3_5_turbo,
            [Description("gpt-3.5-turbo-0301")]
            gpt_3_5_turbo_0301
        }
        public Chat(AvailableModel Model = AvailableModel.gpt_3_5_turbo)
        {
            SelectedModel = Model;
        }
        [JsonIgnore]
        public AvailableModel SelectedModel { get; set; }

        #region properties for json
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
        public chatformat[] messages { get; set; }
        public decimal? temperature { get; set; }
        [Range(0, 1)]
        public decimal? top_p { get; set; }
        public int? n { get; set; }
        public bool? stream { get; set; }
        public string[]? stop { get; set; }
        public int? max_tokens { get; set; }
        [Range(0, 2)]
        public decimal? presence_penalty { get; set; }
        public decimal? frequency_penalty { get; set; }
        public object? logit_bias { get; set; }
        public string? user { get; set; }
        #endregion
        #region Request Methods
        public static async Task<ChatResponse> Request(Chat chat)
        {
            return await Client.Request<ChatResponse>(command, chat);
        }
        public static async Task<string> Request(string chat)
        {
            ChatResponse chatResponse = await Request(new Chat() {
                messages = new chatformat[]
                {
                    new chatformat() { role = chatformat.roles.user, content = chat }
                }
            });
            return chatResponse.error != null ? chatResponse.error.message : chatResponse.choices.FirstOrDefault().message.content;
        }
        #endregion

    }
}
