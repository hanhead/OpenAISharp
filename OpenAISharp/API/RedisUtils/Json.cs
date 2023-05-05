using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAISharp.API.RedisUtils
{
    public class Json
    {
        public enum COMMAND
        {
            [Description("JSON.ARRAPPEND")]
            ARRAPPEND,
            [Description("JSON.ARRINDEX")]
            ARRINDEX,
            [Description("JSON.ARRINSERT")]
            ARRINSERT,
            [Description("JSON.ARRLEN")]
            ARRLEN,
            [Description("JSON.ARRPOP")]
            ARRPOP,
            [Description("JSON.ARRTRIM")]
            ARRTRIM,
            [Description("JSON.CLEAR")]
            CLEAR,
            [Description("JSON.DEBUG")]
            DEBUG,
            [Description("JSON.DEBUG MEMORY")]
            DEBUG_MEMORY,
            [Description("JSON.DEL")]
            DEL,
            [Description("JSON.FORGET")]
            FORGET,
            [Description("JSON.GET")]
            GET,
            [Description("JSON.MGET")]
            MGET,
            [Description("JSON.NUMINCRBY")]
            NUMINCRBY,
            [Description("JSON.NUMMULTBY ")]
            NUMMULTBY,
            [Description("JSON.OBJKEYS")]
            OBJKEYS,
            [Description("JSON.OBJLEN")]
            OBJLEN,
            [Description("JSON.RESP")]
            RESP,
            [Description("JSON.SET")]
            SET,
            [Description("JSON.STRAPPEND")]
            STRAPPEND,
            [Description("JSON.STRLEN")]
            STRLEN,
            [Description("JSON.TOGGLE")]
            TOGGLE,
            [Description("JSON.TYPE")]
            TYPE,
        }
        public enum NXorXX
        {
            NX,
            XX
        }
        public static void Set(IDatabase db, string key, string Json, NXorXX? NXorXX = null)
        {
            Set(db, key, new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("$", Json) }, NXorXX);
        }
        public static void Set(IDatabase db, string key, List<KeyValuePair<string, string>> PathValuePairs, NXorXX? NXorXX = null)
        {
            List<object> _args = new List<object>
            {
                key
            };
            foreach (var PathValuePair in PathValuePairs)
            {
                _args.Add(PathValuePair.Key);
                _args.Add(PathValuePair.Value);
            }
            if (NXorXX != null)
            {
                _args.Add(NXorXX.ToString());
            }
            db.Execute(COMMAND.SET.GetDescription(), _args);
        }
    }
}
