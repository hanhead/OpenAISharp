using System.ComponentModel;

namespace OpenAISharp.API
{
    public class Embeddings
    {
        private static string command = "/embeddings";
        public enum AvailableModel
        {
            [Description("text-embedding-ada-002")]
            text_embedding_ada_002,
            [Description("text-search-ada-doc-001")]
            text_search_ada_doc_001
        }

    }
}
