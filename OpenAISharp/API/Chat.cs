using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAISharp.API
{
    public class Chat
    {
        private static string command = "/chat/completions";
        public enum AvailableModel
        {
            [Description("gpt-4")]
            gpt_4,
            [Description("gpt-4-0314")]
            gpt_4_0314,
            [Description("gpt-4-32k")]
            gpt_4_32k,
            [Description("gpt-4-32k-0314")]
            gpt_4_32k_0314,
            [Description("gpt-3.5-turbo")]
            gpt_3_5_turbo,
            [Description("gpt-3.5-turbo-0301")]
            gpt_3_5_turbo_0301
        }

    }
}
