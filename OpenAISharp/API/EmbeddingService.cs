using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAISharp.API
{
    public class EmbeddingService
    {
        public static float CosineSimilarity(float[] a, float[] b)
        {
            if (a.Length != b.Length)
            {
                throw new ArgumentException("Vectors must have the same dimensionality.");
            }

            float dotProduct = 0f;
            float normA = 0f;
            float normB = 0f;

            for (int i = 0; i < a.Length; i++)
            {
                dotProduct += a[i] * b[i];
                normA += a[i] * a[i];
                normB += b[i] * b[i];
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
}
