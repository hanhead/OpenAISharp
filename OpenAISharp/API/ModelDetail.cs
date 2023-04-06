using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAISharp.API
{
    public class ModelDetail
    {
        public Completions.AvailableModel key { get; set; }
        public string ID { get; set; }
        public string Description { get; set; }
        public int MaxTokens { get; set; }
        public string TrainingData { get; set; }
    }
}
