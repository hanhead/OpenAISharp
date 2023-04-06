using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAISharp.API
{
    public class Moderations
    {
        private static string command = "/moderations";
        public enum AvailableModel
        {
            [Description("text-moderation-stable")]
            text_moderation_stable,
            [Description("text-moderation-latest")]
            text_moderation_latest
        }

    }
}
