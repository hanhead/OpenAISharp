using Microsoft.ML;
using Microsoft.ML.Data;

namespace OpenAISharp.API
{
    public static class CosineSimilarity
    {
        public static float Calculate(float[] vector1, float[] vector2)
        {
            if (vector1.Length != vector2.Length)
            {
                throw new ArgumentException("Vectors must have the same dimensionality.");
            }

            float dotProduct = 0;
            float normA = 0;
            float normB = 0;

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
                return 0;
            }
            else
            {
                return dotProduct / (normA * normB);
            }
        }
    }
    public static class EuclideanDistance
    {
        public static float Calculate(float[] a, float[] b)
        {
            if (a.Length != b.Length)
                throw new ArgumentException("Vectors must have the same length.");

            float sum = 0.0f;
            for (int i = 0; i < a.Length; i++)
            {
                float diff = a[i] - b[i];
                sum += diff * diff;
            }
            return (float)Math.Sqrt((double)sum);
        }
    }
}
