using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NRedisStack;
using NRedisStack.RedisStackCommands;
using OpenAISharp.API.RedisUtils;
using StackExchange.Redis;

namespace OpenAISharp.API
{
    public class RedisAIUtils
    {
        public enum AI
        {
            [Description("AI.TENSORSET")]
            TENSORSET,
            [Description("AI.TENSORGET")]
            TENSORGET,
            [Description("AI.MODELSTORE")]
            MODELSTORE,
            [Description("AI.MODELSET")]
            MODELSET,
            [Description("AI.MODELGET")]
            MODELGET,
            [Description("AI.MODELDEL")]
            MODELDEL,
            [Description("AI.MODELEXECUTE")]
            MODELEXECUTE,
            [Description("AI.MODELRUN")]
            MODELRUN,
            [Description("AI._MODELSCAN")]
            _MODELSCAN,
            [Description("AI.SCRIPTSTORE")]
            SCRIPTSTORE,
            [Description("AI.SCRIPTSET")]
            SCRIPTSET,
            [Description("AI.SCRIPTGET")]
            SCRIPTGET,
            [Description("AI.SCRIPTDEL")]
            SCRIPTDEL,
            [Description("AI.SCRIPTEXECUTE")]
            SCRIPTEXECUTE,
            [Description("AI.SCRIPTRUN")]
            SCRIPTRUN,
            [Description("AI._SCRIPTSCAN")]
            _SCRIPTSCAN,
            [Description("AI.DAGEXECUTE")]
            DAGEXECUTE,
            [Description("AI.DAGEXECUTE_RO")]
            DAGEXECUTE_RO,
            [Description("AI.DAGRUN")]
            DAGRUN,
            [Description("AI.DAGRUN_RO")]
            DAGRUN_RO,
            [Description("AI.INFO")]
            INFO,
            [Description("AI.CONFIG")]
            CONFIG
        }
        public enum TensorOutputType
        {
            META,
            BLOB,
            VALUES
        }
        public enum TensorInputDataType
        {
            FLOAT,
            DOUBLE,
            INT8,
            INT16,
            INT36,
            INT64,
            UNIT8,
            UNIT16,
            BOOL,
            STRING
        }
        public enum TensorInputType
        {
            BLOB,
            VALUES
        }
        public static void TensorSet(IDatabase db, string key, List<object> inputValues, TensorInputDataType tensorInputDataType, TensorInputType tensorInputType)
        {
            List<object> _args = new List<object>
            {
                key,
                tensorInputDataType.ToString(),
                inputValues.Count,
                tensorInputType.ToString()
            };
            _args.AddRange(inputValues);
            SerializedCommand serializedCommand = new SerializedCommand(AI.TENSORSET.GetDescription(), _args);
            db.Execute(serializedCommand);
        }

        public static List<object> TensorGet<T>(IDatabase db, string key, TensorOutputType returnType)
        {
            RedisResult tensorResult = db.Execute(AI.TENSORGET.GetDescription(), key, returnType.ToString());
            return Utils.GetRedisResult(tensorResult);
        }
    }
}
