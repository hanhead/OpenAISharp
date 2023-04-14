using System.ComponentModel;

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
