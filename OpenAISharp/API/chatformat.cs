using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OpenAISharp.API
{
    public class chatformat
    {
        public enum roles
        {
            system,
            user,
            assistant
        }
        [JsonConverter(typeof(StringEnumConverter))]
        public roles role { get; set; }
        public string content { get; set; }
    }
}
