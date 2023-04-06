using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAISharp.API
{
    public class Audio
    {
        private static string transcriptionscommand = "/audio/transcriptions";
        private static string translationscommand = "/audio/translations";
        public enum AvailableModel
        {
            [Description("whisper-1")]
            whisper_1
        }

    }
}
