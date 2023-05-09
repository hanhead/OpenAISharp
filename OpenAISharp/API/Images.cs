using Newtonsoft.Json;
using System.ComponentModel;
using System.Text;
using static OpenAISharp.API.Images;

namespace OpenAISharp.API
{
    public class Images
    {
        private static string command = "/images/{0}";
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
        public static async Task<ImagesResponse> edits(string imagepath, string prompt, string maskimagepath = null, int? n = null, ImageSize? size = null, Response_Format? response_Format = null, string user = null)
        {
            StreamContent image = new StreamContent(System.IO.File.OpenRead(imagepath));
            string imageName = Path.GetFileName(imagepath);
            StreamContent maskImage = maskimagepath == null ? null : new StreamContent(System.IO.File.OpenRead(maskimagepath));
            string maskImageName = maskImage == null ? null : Path.GetFileName(maskimagepath);
            ImagesResponse imagesResponse = await edits(prompt, image, maskImage, imageName, maskImageName, n, size, response_Format, user);
            return imagesResponse;
        }
        public static async Task<List<byte[]>> editsImage(string imagepath, string prompt, string maskimagepath = null, int? n = null, ImageSize? size = null, string user = null)
        {
            ImagesResponse imagesResponse = await edits(imagepath, prompt, maskimagepath, n, size, Response_Format.url, user);
            string[] _urls = imagesResponse.data.Select(d => d.url).ToArray();
            List<byte[]> _imageByteList = await GetBytesFromUrls(_urls);
            return _imageByteList;
        }
        public static async Task<ImagesResponse> edits(string prompt, StreamContent image, StreamContent maskImage = null, string imageName = "image.png", string maskImageName = "mask.png", int? n=null, ImageSize? size = null, Response_Format? response_Format = null, string user = null)
        {
            ImagesResponse imagesResponse = null;
            using (MultipartFormDataContent multipartFormData = new MultipartFormDataContent())
            {
                multipartFormData.Add(image, "image", imageName);
                if (maskImage != null)
                {
                    multipartFormData.Add(maskImage, "mask", maskImageName);
                }
                multipartFormData.Add(new StringContent(prompt), "prompt");
                if (n.HasValue)
                {
                    multipartFormData.Add(new StringContent(n.Value.ToString()), "n");
                }
                if (size.HasValue)
                {
                    multipartFormData.Add(new StringContent(size.Value.GetDescription()), "size");
                }
                if (response_Format.HasValue)
                {
                    multipartFormData.Add(new StringContent(response_Format.Value.ToString()), "response_format");
                }
                if (user != null)
                {
                    multipartFormData.Add(new StringContent(user), "user");
                }
                imagesResponse = await Client.Request<ImagesResponse>(string.Format(command, "edits"), null, multipartFormData);
            }

            return imagesResponse;
        }
        public static async Task<List<byte[]>> editsImage(string prompt, StreamContent image, StreamContent maskImage = null, string imageName = "image.png", string maskImageName = "mask.png", int? n = null, ImageSize? size=null, string user = null)
        {
            ImagesResponse imagesResponse = await edits(prompt, image, maskImage, imageName, maskImageName, n, size, Response_Format.url, user);
            string[] _urls = imagesResponse.data.Select(d => d.url).ToArray();
            List<byte[]> _imageByteList = await GetBytesFromUrls(_urls);
            return _imageByteList;
        }
        public static async Task<ImagesResponse> Generate(Images Images)
        {
            return await Client.Request<ImagesResponse>(string.Format(command, "generations"), Images);
        }
        public static async Task<List<byte[]>> Generate(string prompt, int? n=null, ImageSize? size =null, string user = null)
        {
            string[] _urls = await GenerateReturnURLs(prompt, n, size, user);
            List<byte[]> _imageByteList = await GetBytesFromUrls(_urls);
            return _imageByteList;
        }
        private static async Task<string[]> GenerateReturnURLs(string prompt, int? n=null, ImageSize? size=null, string user=null)
        {
            ImagesResponse _imagesResponse = await Generate(new Images()
            {
                prompt = prompt,
                n = n,
                size = size.Value.GetDescription(),
                response_format = Response_Format.url.ToString(),
                user = user
            });
            string[] _urls = _imagesResponse.data.Select(d => d.url).ToArray();
            return _urls;
        }
        private static async Task<List<byte[]>> GetBytesFromUrls(string[] _urls)
        {
            List<byte[]> _imageByteList = new List<byte[]>();
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
