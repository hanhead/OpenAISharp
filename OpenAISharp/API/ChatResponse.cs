﻿namespace OpenAISharp.API
{
    public class ChatResponse
    {
        public string id { get; set; }
        public string @object { get; set; }
        public int created { get; set; }
        public List<Choice> choices { get; set; }
        public Usage usage { get; set; }
        public Error error { get; set; }

    }
}
