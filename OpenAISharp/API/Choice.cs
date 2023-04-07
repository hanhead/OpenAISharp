using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAISharp.API
{
    public class Choice
    {
        public int index { get; set; }
        public string text { get; set; }
        public chatformat message { get; set; }
        public object logprobs { get; set; }
        public string finish_reason { get; set; }
    }
}
