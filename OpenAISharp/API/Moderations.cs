using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

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
        public string[] input { get; set; }
        public string model { get; set; }
        
        public static async Task<bool> isViolated(string input, AvailableModel? model = null)
        {
            ModerationsResponse response = await Request(new string[] { input }, model);
            if (response.error == null)
            {
                return response.results.Count(r => r.flagged == true) > 0;
            }
            else
            {
                throw new Exception(response.error.message);
            }
        }
        public static async Task<ModerationsResponse> Request(string[] input, AvailableModel? model = null)
        {
            return await Client.Request<ModerationsResponse>(command, new Moderations
            {
                input = input,
                model = model == null ? null : model.Value.GetDescription()
            });
        }

    }
}
