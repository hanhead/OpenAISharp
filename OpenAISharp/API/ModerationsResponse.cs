using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAISharp.API
{
    public class ModerationsResponse
    {
        public string id { get; set; }
        public string model { get; set; }
        public Error error { get; set; }
        public List<Result> results { get; set; }

        public class Categories
        {
            public bool hate { get; set; }

            [JsonProperty("hate/threatening")]
            public bool hatethreatening { get; set; }

            [JsonProperty("self-harm")]
            public bool selfharm { get; set; }
            public bool sexual { get; set; }

            [JsonProperty("sexual/minors")]
            public bool sexualminors { get; set; }
            public bool violence { get; set; }

            [JsonProperty("violence/graphic")]
            public bool violencegraphic { get; set; }
        }

        public class CategoryScores
        {
            public double hate { get; set; }

            [JsonProperty("hate/threatening")]
            public double hatethreatening { get; set; }

            [JsonProperty("self-harm")]
            public double selfharm { get; set; }
            public double sexual { get; set; }

            [JsonProperty("sexual/minors")]
            public double sexualminors { get; set; }
            public double violence { get; set; }

            [JsonProperty("violence/graphic")]
            public double violencegraphic { get; set; }
        }

        public class Result
        {
            public Categories categories { get; set; }
            public CategoryScores category_scores { get; set; }
            public bool flagged { get; set; }
        }
    }
}
