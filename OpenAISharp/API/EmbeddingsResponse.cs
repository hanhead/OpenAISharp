using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAISharp.API
{
    public class EmbeddingsResponse
    {
        public string @object { get; set; }
        public List<Datum> data { get; set; }
        public string model { get; set; }
        public Usage usage { get; set; }
        public class Datum
        {
            public string @object { get; set; }
            public double[] embedding { get; set; }
            public int index { get; set; }
        }
        public Error error { get; set; }
    }
}
