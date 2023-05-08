using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OpenAISharp.API
{
    public class Completions
    {

        private static string command = "/completions";
        public enum AvailableModel
        {
            [Description("text-davinci-003")]
            text_davinci_003,
            [Description("text-davinci-002")]
            text_davinci_002,
            [Description("text-curie-001")]
            text_curie_001,
            [Description("text-babbage-001")]
            text_babbage_001,
            [Description("text-ada-001")]
            text_ada_001,
            [Description("davinci")]
            davinci,
            [Description("curie")]
            curie,
            [Description("babbage")]
            babbage,
            [Description("ada")]
            ada
        }
        public Completions(AvailableModel Model = AvailableModel.text_davinci_003)
        {
            SelectedModel = Model;
        }
        [JsonIgnore]
        public AvailableModel SelectedModel { get; set; }

        #region properties for json
        private string _model;
        public string model {
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
        public string[] prompt { get; set; }
        public string? suffix { get; set; }
        public int? max_tokens { get; set; }
        [Range(0, 2)]
        public decimal? temperature { get; set; }
        [Range (0, 1)]
        public decimal? top_p { get; set;}
        public int? n { get; set; }
        public bool? stream { get; set; }
        public int? logprobs { get; set; }
        public bool? echo { get; set; }
        public string[]? stop { get; set; }
        public decimal? presence_penalty { get; set; }
        public decimal? frequency_penalty { get; set; }
        public int? best_of { get; set; }
        public object? logit_bias { get; set; }
        public string? user { get; set; }
        #endregion

        #region Request Methods
        public static async Task<CompletionsReponse> Request(string prompt, AvailableModel model = AvailableModel.text_davinci_003, string? suffix = null, int? max_tokens = null, decimal? temperature = null, decimal? top_p = null, int? n = null, bool? stream = null, int? logprobs = null, bool? echo = null, string[]? stop = null, decimal? presence_penalty = null, decimal? frequency_penalty = null, int? best_of = null, object? logit_bias = null, string? user = null)
        {
            return await Request(new Completions()
            {
                SelectedModel = model,
                prompt = new string[] { prompt },
                suffix = suffix,
                max_tokens = max_tokens,
                temperature = temperature,
                top_p = top_p,
                n = n,
                stream = stream,
                logprobs = logprobs,
                echo = echo,
                stop = stop,
                presence_penalty = presence_penalty,
                frequency_penalty = frequency_penalty,
                best_of = best_of,
                logit_bias = logit_bias,
                user = user
            });
        }
        public static async Task<CompletionsReponse> Request(string[] prompt, AvailableModel model = AvailableModel.text_davinci_003, string? suffix = null, int? max_tokens = null, decimal? temperature = null, decimal? top_p = null, int? n = null, bool? stream = null, int? logprobs = null, bool? echo = null, string[]? stop = null, decimal? presence_penalty = null, decimal? frequency_penalty = null, int? best_of = null, object? logit_bias = null, string? user = null)
        {
            return await Request(new Completions()
            {
                SelectedModel = model,
                prompt = prompt,
                suffix = suffix,
                max_tokens = max_tokens,
                temperature = temperature,
                top_p = top_p,
                n = n,
                stream = stream,
                logprobs = logprobs,
                echo = echo,
                stop = stop,
                presence_penalty = presence_penalty,
                frequency_penalty = frequency_penalty,
                best_of = best_of,
                logit_bias = logit_bias,
                user = user
            });
        }
        public static async Task<CompletionsReponse> Request(string[] prompt, string model, string? suffix = null, int? max_tokens = null, decimal? temperature = null, decimal? top_p = null, int? n = null, bool? stream = null, int? logprobs = null, bool? echo = null, string[]? stop = null, decimal? presence_penalty = null, decimal? frequency_penalty = null, int? best_of = null, object? logit_bias = null, string? user = null)
        {
            return await Request(new Completions()
            {
                model = model,
                prompt = prompt,
                suffix = suffix,
                max_tokens = max_tokens,
                temperature = temperature,
                top_p = top_p,
                n = n,
                stream = stream,
                logprobs = logprobs,
                echo = echo,
                stop = stop,
                presence_penalty = presence_penalty,
                frequency_penalty = frequency_penalty,
                best_of = best_of,
                logit_bias = logit_bias,
                user = user
            });
        }
        public static async Task<CompletionsReponse> Request(Completions completions)
        {
            return await Client.Request<CompletionsReponse>(command, completions);
        }
        #endregion


    }
}
