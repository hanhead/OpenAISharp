using static OpenAISharp.API.RedisUtils.FT;

namespace OpenAISharp.API.RedisUtils
{
    public class FlatVectorDocumentField
    {
        public string FieldID { get; set; }
        public string FieldIDAlias { get; set; }
        public VectorDataType VectorDataType { get; set; }
        public int? Dimension { get; set; }
        public Vector_Distance_Metric Vector_Distance_Metric { get; set; }
        public int? Initial_Capacity { get; set; }
        public int? Block_Size { get; set; }
        public DocumentField ToDocumentField()
        {
            List<string> AlgorithmAttributes = new List<string>() {
                "TYPE",
                VectorDataType.ToString(),
                "DIM",
                Dimension.ToString(),
                "DISTANCE_METRIC",
                Vector_Distance_Metric.ToString()
            };
            if (Initial_Capacity != null)
            {
                AlgorithmAttributes.Add("INITIAL_CAP");
                AlgorithmAttributes.Add(Initial_Capacity.Value.ToString());
            }
            if (Block_Size != null)
            {
                AlgorithmAttributes.Add("BLOCK_SIZE");
                AlgorithmAttributes.Add(Block_Size.Value.ToString());
            }
            AlgorithmAttributes.Insert(0, AlgorithmAttributes.Count.ToString());
            AlgorithmAttributes.Insert(0, VectorAlgorithm.FLAT.ToString());
            return new DocumentField { FieldID = FieldID, FieldIDAlias = FieldIDAlias, FieldType = FieldType.VECTOR, FieldOptions = AlgorithmAttributes };
        }
    }
}
