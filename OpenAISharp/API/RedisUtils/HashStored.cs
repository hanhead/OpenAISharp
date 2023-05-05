using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAISharp.API.RedisUtils
{
    public class HashStored
    {
        public enum COMMAND
        {
            HDEL,
            HEXISTS,
            HGET,
            HGETALL,
            HINCRBY,
            HINCRBYFLOAT,
            HKEYS,
            HLEN,
            HMGET,
            HMSET,
            HRANDFIELD,
            HSCAN,
            HSET,
            HSETNX,
            HSTRLEN,
            HVALS
        }
        public static void Set(IDatabase db, string key, List<KeyValuePair<string, object>> keyValuePairs)
        {
            List<object> _args = new List<object>
            {
                key
            };
            foreach(var keyValue in keyValuePairs) { 
                _args.Add(keyValue.Key);
                _args.Add(keyValue.Value);
            }
            db.Execute(COMMAND.HSET.GetDescription(), _args);
        }
        public static List<object> Get(IDatabase db, string key, string field)
        {
            List<object> _args = new List<object>
            {
                key,
                field
            };
            return Utils.GetRedisResult(db.Execute(COMMAND.HGET.GetDescription(), _args));
        }
    }

}
