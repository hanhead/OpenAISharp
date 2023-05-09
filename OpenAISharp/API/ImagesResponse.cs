using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAISharp.API
{
    public class ImagesResponse
    {
        public int created { get; set; }
        public Datum[] data { get; set; }
        public class Datum
        {
            public string url { get; set; }
        }
        public Error error { get; set; }

    }
}
