using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OpenAISharp.API
{
    public class Completions
    {
        
        public enum Model
        {
            gpt_3_5_turbo,
            gpt_3_5_turbo_0301,
            text_davinci_003,
            text_davinci_002,
            code_davinci_002
        }
        public class ModelDetail
        {
            public Model key {  get; set; }
            public string ID { get; set; }
            public string Description { get; set; }
            public int MaxTokens { get; set; }
            public string TrainingData { get; set; }
        }
        public Completions(Model Model = Model.gpt_3_5_turbo)
        {
            SelectedModel = Model;
            Models = new List<ModelDetail>() { 
                new ModelDetail()
                {
                    key = Model.gpt_3_5_turbo,
                    ID = "gpt-3.5-turbo",
                    Description = "Most capable GPT-3.5 model and optimized for chat at 1/10th the cost of text-davinci-003. Will be updated with our latest model iteration.",
                    MaxTokens = 4096,
                    TrainingData = "Up to Sep 2021"
                },
                new ModelDetail()
                {
                    key = Model.gpt_3_5_turbo_0301,
                    ID = "gpt-3.5-turbo-0301",
                    Description = "Snapshot of gpt-3.5-turbo from March 1st 2023. Unlike gpt-3.5-turbo, this model will not receive updates, and will only be supported for a three month period ending on June 1st 2023.",
                    MaxTokens = 4096,
                    TrainingData = "Up to Sep 2021"
                },
                new ModelDetail()
                {
                    key = Model.text_davinci_003,
                    ID = "text-davinci-003",
                    Description = "Can do any language task with better quality, longer output, and consistent instruction-following than the curie, babbage, or ada models. Also supports inserting completions within text.",
                    MaxTokens = 4097,
                    TrainingData = "Up to Jun 2021"
                },
                new ModelDetail()
                {
                    key = Model.text_davinci_002,
                    ID = "text-davinci-002",
                    Description = "Similar capabilities to text-davinci-003 but trained with supervised fine-tuning instead of reinforcement learning",
                    MaxTokens = 4097,
                    TrainingData = "Up to Jun 2021"
                },
                new ModelDetail()
                {
                    key = Model.code_davinci_002,
                    ID = "code-davinci-002",
                    Description = "Optimized for code-completion tasks",
                    MaxTokens = 8001,
                    TrainingData = "Up to Jun 2021"
                }
            };
        }
        [JsonIgnore]
        public Model SelectedModel { get; set; }
        [JsonIgnore]
        public List<ModelDetail> Models { get; }

        #region properties for json
        private string _model;
        public string model {
            get 
            { 
                if (SelectedModel != null)
                {
                    ModelDetail? _selectedModel = Models.FirstOrDefault(m => m.key.Equals(SelectedModel));
                    return _selectedModel?.ID;
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
        public static async Task<string> Request(string prompt, Model model = Model.text_davinci_003, string? suffix = null, int? max_tokens = null, decimal? temperature = null, decimal? top_p = null, int? n = null, bool? stream = null, int? logprobs = null, bool? echo = null, string[]? stop = null, decimal? presence_penalty = null, decimal? frequency_penalty = null, int? best_of = null, object? logit_bias = null, string? user = null)
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
        public static async Task<string> Request(string[] prompt, Model model = Model.text_davinci_003, string? suffix = null, int? max_tokens = null, decimal? temperature = null, decimal? top_p = null, int? n = null, bool? stream = null, int? logprobs = null, bool? echo = null, string[]? stop = null, decimal? presence_penalty = null, decimal? frequency_penalty = null, int? best_of = null, object? logit_bias = null, string? user = null)
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
        public static async Task<string> Request(string[] prompt, string model, string? suffix = null, int? max_tokens = null, decimal? temperature = null, decimal? top_p = null, int? n = null, bool? stream = null, int? logprobs = null, bool? echo = null, string[]? stop = null, decimal? presence_penalty = null, decimal? frequency_penalty = null, int? best_of = null, object? logit_bias = null, string? user = null)
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
        public static async Task<string> Request(Completions completions)
        {
            string command = "/completions";
            string result;
            string organzationID = OpenAISettings.OrganizationID;
            string apiKey = OpenAISettings.ApiKey;
            var apiUrl = $"{OpenAISettings.UrlPrefix}{command}";
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                client.DefaultRequestHeaders.Add("OpenAI-Organization", organzationID);
                string requestJson = JsonConvert.SerializeObject(completions, Formatting.Indented, settings);
                StringContent requestJsonContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
                using (HttpResponseMessage response = await client.PostAsync(apiUrl, requestJsonContent))
                {
                    result = await response.Content.ReadAsStringAsync();
                    System.IO.File.WriteAllText("cachedData\\completions.json", result);
                    //models = JsonConvert.DeserializeObject<Models>(result);
                }
            }
            return result;
        }
    }
}
