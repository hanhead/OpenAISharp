using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAISharp.API
{
    public class Error
    {
        public string message { get; set; }
        public string type { get; set; }
        public string param { get; set; }
        public object code { get; set; }
    }
}
