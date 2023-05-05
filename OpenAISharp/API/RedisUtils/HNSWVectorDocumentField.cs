using static OpenAISharp.API.RedisUtils.FT;

namespace OpenAISharp.API.RedisUtils
{
    public class HNSWVectorDocumentField
    {
        public string FieldID { get; set; }
        public string FieldIDAlias { get; set; }
        public VectorDataType VectorDataType { get; set; }
        public int? Dimension { get; set; }
        public Vector_Distance_Metric Vector_Distance_Metric { get; set; }
        public int? Initial_Capacity { get; set; }
        public int? M { get; set; }
        public int? EF_Construction { get; set; }
        public int? EF_Runtime { get; set; }
        public float? EPSILON { get; set; }
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
            if (M != null)
            {
                AlgorithmAttributes.Add("M");
                AlgorithmAttributes.Add(M.Value.ToString());
            }
            if (EF_Construction != null)
            {
                AlgorithmAttributes.Add("EF_CONSTRUCTION");
                AlgorithmAttributes.Add(EF_Construction.Value.ToString());
            }
            if (EF_Runtime != null)
            {
                AlgorithmAttributes.Add("EF_RUNTIME");
                AlgorithmAttributes.Add(EF_Runtime.Value.ToString());
            }
            if (EPSILON != null)
            {
                AlgorithmAttributes.Add("EPSILON");
                AlgorithmAttributes.Add(EPSILON.Value.ToString());
            }
            AlgorithmAttributes.Insert(0, AlgorithmAttributes.Count.ToString());
            AlgorithmAttributes.Insert(0, VectorAlgorithm.HNSW.ToString());
            return new DocumentField { FieldID = FieldID, FieldIDAlias = FieldIDAlias, FieldType = FieldType.VECTOR, FieldOptions = AlgorithmAttributes };
        }
    }
}
