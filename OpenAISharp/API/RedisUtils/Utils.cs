using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAISharp.API.RedisUtils
{
    public class Utils
    {
        public static List<object> GetRedisResult(RedisResult redisResult)
        {
            var result = new List<object>();
            if (redisResult.Type is ResultType.MultiBulk)
            {
                return ((RedisResult[]?)redisResult).Select(r => GetRedisSingleResult(r)).ToList();
            }
            else
            {
                return new List<object>() { GetRedisSingleResult(redisResult) };
            }
        }
        public static object GetRedisSingleResult(RedisResult redisResult)
        {
            return (redisResult.IsNull ? null : redisResult.Type is ResultType.MultiBulk?GetRedisResult(redisResult) : Convert.ChangeType(redisResult, typeof(object)));
        }
        public static byte[] ConvertFloatArrayToByteArray(float[] inputArray)
        {
            byte[] byteArray = new byte[inputArray.Length * 4];
            Buffer.BlockCopy(inputArray, 0, byteArray, 0, byteArray.Length);

            double[] doubleArray2 = new double[byteArray.Length / 4];
            Buffer.BlockCopy(byteArray, 0, doubleArray2, 0, byteArray.Length);
            return byteArray;
        }
    }
}
