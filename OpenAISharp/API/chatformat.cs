using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

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
