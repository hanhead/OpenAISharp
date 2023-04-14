namespace OpenAISharp.API
{
    public class EditsResponse
    {
        public string @object { get; set; }
        public int created { get; set; }
        public List<Choice> choices { get; set; }
        public Usage usage { get; set; }
        public Error error { get; set; }
    }
}
