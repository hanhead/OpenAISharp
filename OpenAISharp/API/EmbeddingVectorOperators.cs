using Microsoft.ML;
using Microsoft.ML.Data;

namespace OpenAISharp.API
{
    public static class CosineSimilarity
    {
        public static double Calculate(double[] vector1, double[] vector2)
        {
            if (vector1.Length != vector2.Length)
            {
                throw new ArgumentException("Vectors must have the same dimensionality.");
            }

            double dotProduct = 0;
            double normA = 0;
            double normB = 0;

            for (int i = 0; i < vector1.Length; i++)
            {
                dotProduct += vector1[i] * vector2[i];
                normA += vector1[i] * vector1[i];
                normB += vector2[i] * vector2[i];
            }

            normA = (double)Math.Sqrt(normA);
            normB = (double)Math.Sqrt(normB);

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
        public static double Calculate(double[] a, double[] b)
        {
            if (a.Length != b.Length)
                throw new ArgumentException("Vectors must have the same length.");

            double sum = 0.0;
            for (int i = 0; i < a.Length; i++)
            {
                double diff = a[i] - b[i];
                sum += diff * diff;
            }
            return Math.Sqrt(sum);
        }
    }
}
