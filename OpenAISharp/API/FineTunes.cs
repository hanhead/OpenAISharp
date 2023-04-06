using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAISharp.API
{
    public class FineTunes
    {
        private static string command = "/fine-tunes";
        public enum AvailableModel
        {
            [Description("davinci")]
            davinci,
            [Description("curie")]
            curie,
            [Description("babbage")]
            babbage,
            [Description("ada")]
            ada
        }

    }
}
