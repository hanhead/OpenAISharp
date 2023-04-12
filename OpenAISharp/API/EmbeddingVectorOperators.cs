using Microsoft.ML;
using Microsoft.ML.Data;

namespace OpenAISharp.API
{
    public static class CosineSimilarity
    {
        public static float CalculateMLNET(float[] vector1, float[] vector2)
        {
            var context = new MLContext();
            var data = vector1.Zip(vector2, (v1, v2) => new { v1, v2 });
            var dataView = context.Data.LoadFromEnumerable(data);
            var similarityEstimator = context.Transforms.Conversion.ConvertType("v1", outputKind: DataKind.Single)
                .Append(context.Transforms.Conversion.ConvertType("v2", outputKind: DataKind.Single))
                .Append(context.Transforms.Concatenate("Features", "v1", "v2"))
                .Append(context.Transforms.NormalizeLpNorm("Features", "Features"));
            var transformedData = similarityEstimator.Fit(dataView).Transform(dataView);
            var similarityValues = transformedData.GetColumn<float[]>("Features").ToArray();
            return similarityValues[0][0];
        }
        public static float Calculate(float[] vector1, float[] vector2)
        {
            if (vector1.Length != vector2.Length)
            {
                throw new ArgumentException("Vectors must have the same dimensionality.");
            }

            float dotProduct = 0f;
            float normA = 0f;
            float normB = 0f;

            for (int i = 0; i < vector1.Length; i++)
            {
                dotProduct += vector1[i] * vector2[i];
                normA += vector1[i] * vector1[i];
                normB += vector2[i] * vector2[i];
            }

            normA = (float)Math.Sqrt(normA);
            normB = (float)Math.Sqrt(normB);

            if (normA == 0 || normB == 0)
            {
                return 0f;
            }
            else
            {
                return dotProduct / (normA * normB);
            }
        }
    }
    public static class EuclideanDistance
    {
        public static float Calculate(float[] vector1, float[] vector2)
        {
            var context = new MLContext();
            var data = vector1.Zip(vector2, (v1, v2) => new { v1, v2 });
            var dataView = context.Data.LoadFromEnumerable(data);
            var distanceEstimator = context.Transforms.Conversion.ConvertType("v1", outputKind: DataKind.Single)
                .Append(context.Transforms.Conversion.ConvertType("v2", outputKind: DataKind.Single))
                .Append(context.Transforms.Concatenate("Features", "v1", "v2"))
                .Append(context.Transforms.NormalizeLpNorm("Features", "Features"))
                .Append(context.Transforms.NormalizeMinMax("Features"));
            var transformedData = distanceEstimator.Fit(dataView).Transform(dataView);
            var distanceValues = transformedData.GetColumn<float[]>("Features").ToArray();
            return distanceValues[0][0];
        }
    }
}
