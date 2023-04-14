using System.ComponentModel;

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
