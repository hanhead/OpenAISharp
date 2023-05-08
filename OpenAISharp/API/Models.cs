using Newtonsoft.Json;

namespace OpenAISharp.API
{
    public class Models
    {
        public static async Task<Models> List()
        {
            string command = "/models";
            return await Client.Request<Models>(command);
        }
        public static async Task<Model> Get(string ModelName)
        {
            string command = string.Format("/models/{0}", ModelName);
            return await Client.Request<Model>(command);
        }
        public string @object { get; set; }
        public List<Model> data { get; set; }
    }
    public class Model
    {
        public string id { get; set; }
        public string @object { get; set; }
        public int created { get; set; }
        public string owned_by { get; set; }
        public List<Permission> permission { get; set; }
        public string root { get; set; }
        public object parent { get; set; }
    }

    public class Permission
    {
        public string id { get; set; }
        public string @object { get; set; }
        public int created { get; set; }
        public bool allow_create_engine { get; set; }
        public bool allow_sampling { get; set; }
        public bool allow_logprobs { get; set; }
        public bool allow_search_indices { get; set; }
        public bool allow_view { get; set; }
        public bool allow_fine_tuning { get; set; }
        public string organization { get; set; }
        public object group { get; set; }
        public bool is_blocking { get; set; }
    }
}
