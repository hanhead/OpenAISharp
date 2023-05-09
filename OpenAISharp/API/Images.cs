using Newtonsoft.Json;
using System.ComponentModel;
using System.Text;

namespace OpenAISharp.API
{
    public class Images
    {
        private static string command = "/images/generations";
        public enum ImageSize
        {
            [Description("256x256")]
            x256,
            [Description("512x512")]
            x512,
            [Description("1024x1024")]
            x1024
        }
        public enum Response_Format
        {
            url,
            b64_json
        }
        public string prompt { get; set; }
        public int? n { get; set; }
        public string size { get; set; }
        public string response_format { get; set; }
        public string user { get; set; }

        public static async Task<ImagesResponse> Generate(Images Images)
        {
            return await Client.Request<ImagesResponse>(command, Images);
        }
        public static async Task<List<byte[]>> Generate(string prompt, int? n=null, ImageSize? size =null, string user = null)
        {
            ImagesResponse _imagesResponse = await Generate(new Images() {
                prompt = prompt,
                n = n,
                size = size.Value.GetDescription(),
                response_format = Response_Format.url.ToString(),
                user = user
            });
            List<byte[]> _imageByteList = new List<byte[]>();
            string[] _urls = _imagesResponse.data.Select(d => d.url).ToArray();
            using (HttpClient httpClient = new HttpClient())
            {
                foreach (string url in _urls)
                {
                    byte[] imageData = await httpClient.GetByteArrayAsync(url);
                    _imageByteList.Add(imageData);
                }
            }
            return _imageByteList;
        }
    }
}
