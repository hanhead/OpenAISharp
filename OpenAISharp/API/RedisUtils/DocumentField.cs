using static OpenAISharp.API.RedisUtils.FT;

namespace OpenAISharp.API.RedisUtils
{
    public class DocumentField
    {
        public string FieldID { get; set; }
        public string FieldIDAlias { get; set; }
        public FieldType FieldType { get; set; }
        public List<string> FieldOptions { get; set; }
    }
}
