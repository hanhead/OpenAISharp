﻿using Newtonsoft.Json;
using System.ComponentModel;
using System.Text;

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
        [JsonIgnore]
        public AvailableModel SelectedModel { get; set; }
        private string _model;
        public string model
        {
            get
            {
                if (SelectedModel != null)
                {
                    return SelectedModel.GetDescription();
                }
                else
                {
                    return _model;
                }
            }
            set
            {
                _model = value;
            }
        }
        public string[] input { get; set; }
        public string user { get; set; }
        public static async Task<EmbeddingsResponse> Request(Embeddings embeddings)
        {
            return await Client.Request<EmbeddingsResponse>(command, embeddings);
        }
        public static async Task<List<float[]>> Request(string[] input, AvailableModel model = AvailableModel.text_embedding_ada_002)
        {
            Embeddings embeddings = new Embeddings() { input = input, model = model.GetDescription() };
            EmbeddingsResponse response = await Request(embeddings);
            if (response != null)
            {
                if (response.error == null)
                {
                    return response.data.Select(d=>d.embedding).ToList();
                }
                else
                {
                    throw new Exception(response.error.message);
                }
            }
            return null;
        }
        public static async Task<float[]> Request(string input, AvailableModel model = AvailableModel.text_embedding_ada_002)
        {
            List<float[]> result = await Request(new string[] { input }, model);
            return result.FirstOrDefault();
        }
    }
}
