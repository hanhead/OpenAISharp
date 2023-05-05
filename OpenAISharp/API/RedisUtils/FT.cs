using NRedisStack.RedisStackCommands;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static OpenAISharp.API.RedisAIUtils;
using NRedisStack.RedisStackCommands;
using NRedisStack;
using static NRedisStack.Search.Schema;
using System.IO;
using System.Data.Common;

namespace OpenAISharp.API.RedisUtils
{
    public class FT
    {
        public enum COMMAND
        {
            [Description("FT._LIST")]
            _LIST,
            [Description("FT.AGGREGATE")]
            AGGREGATE,
            [Description("FT.ALIASADD")]
            ALIASADD,
            [Description("FT.ALIASDEL")]
            ALIASDEL,
            [Description("FT.ALIASUPDATE")]
            ALIASUPDATE,
            [Description("FT.ALTER")]
            ALTER,
            [Description("FT.CONFIG GET")]
            CONFIG_GET,
            [Description("FT.CONFIG SET")]
            CONFIG_SET,
            [Description("FT.CREATE")]
            CREATE,
            [Description("FT.CURSOR DEL")]
            CURSOR_DEL,
            [Description("FT.CURSOR READ")]
            CURSOR_READ,
            [Description("FT.DICTADD")]
            DICTADD,
            [Description("FT.DICTDEL")]
            DICTDEL,
            [Description("FT.DICTDUMP")]
            DICTDUMP,
            [Description("FT.DROPINDEX")]
            DROPINDEX,
            [Description("FT.EXPLAIN")]
            EXPLAIN,
            [Description("FT.EXPLAINCLI")]
            EXPLAINCLI,
            [Description("FT.INFO")]
            INFO,
            [Description("FT.PROFILE")]
            PROFILE,
            [Description("FT.SEARCH")]
            SEARCH,
            [Description("FT.SPELLCHECK")]
            SPELLCHECK,
            [Description("FT.SYNDUMP")]
            SYNDUMP,
            [Description("FT.SYNUPDATE")]
            SYNUPDATE,
            [Description("FT.TAGVALS")]
            TAGVALS
        }
        public enum IndexDataType
        {
            HASH,
            JSON
        }
        public enum FieldType
        {
            TEXT,
            TAG,
            NUMERIC,
            GEO,
            VECTOR
        }
        public enum VectorAlgorithm
        {
            FLAT,
            HNSW
        }
        public enum VectorDataType
        {
            FLOAT32,
            FLOAT64
        }
        public enum Vector_Distance_Metric
        {
            L2,
            IP,
            COSINE
        }
        public static void CreateFLATVector(IDatabase db, string IndexName, List<FlatVectorDocumentField> FlatVectorDocumentFields, IndexDataType? IndexDataType = null, List<object> OtherOptions = null)
        {
            Create(db, IndexName, FlatVectorDocumentFields.Select(f => f.ToDocumentField()).ToList(), IndexDataType, OtherOptions);
        }
        public static void CreateHNSWVector(IDatabase db, string IndexName, List<HNSWVectorDocumentField> HNSWVectorDocumentFields, IndexDataType? IndexDataType = null, List<object> OtherOptions = null)
        {
            Create(db, IndexName, HNSWVectorDocumentFields.Select(f => f.ToDocumentField()).ToList(), IndexDataType, OtherOptions);
        }
        public static void Create(IDatabase db, string IndexName , List<DocumentField> DocumentFields, IndexDataType? IndexDataType = null, List<object> OtherOptions = null)
        {
            List<object> _args = new List<object>
            {
                IndexName
            };
            if (IndexDataType != null)
            {
                _args.Add("ON");
                _args.Add(IndexDataType.ToString());
            }
            if (OtherOptions != null && OtherOptions.Count > 0)
            {
                _args.AddRange(OtherOptions);
            }
            _args.Add("SCHEMA");
            foreach(var field in DocumentFields)
            {
                _args.Add(field.FieldID);
                if (field.FieldIDAlias != null)
                {
                    _args.Add("AS");
                    _args.Add(field.FieldIDAlias);
                }
                _args.Add(field.FieldType.ToString());

                if (field.FieldOptions != null)
                {
                    _args.AddRange(field.FieldOptions);
                }
            }
            SerializedCommand serializedCommand = new SerializedCommand(COMMAND.CREATE.GetDescription(), _args);
            db.Execute(serializedCommand);
        }
        public static void DropIndex(IDatabase db, string IndexName, bool? deleteDocuments=false)
        {
            List<object> _args = new List<object>() { IndexName };
            if (deleteDocuments == true)
            {
                _args.Add("DD");
            }
            db.Execute(COMMAND.DROPINDEX.GetDescription(), _args);
        }
        /// <summary>
        ///     Search with query in Redis index.
        /// </summary>
        /// <param name="db">Redis server</param>
        /// <param name="indexName">Name of index</param>
        /// <param name="query">Query</param>
        /// <param name="options">
        ///   [NOCONTENT] 
        ///   [VERBATIM]
        ///   [NOSTOPWORDS]
        ///   [WITHSCORES]
        ///   [WITHPAYLOADS]
        ///   [WITHSORTKEYS]
        ///   [FILTER numeric_field min max [FILTER numeric_field min max...]] 
        ///   [GEOFILTER geo_field lon lat radius m | km | mi | ft [GEOFILTER geo_field lon lat radius m | km | mi | ft...]] 
        ///   [INKEYS count key [key...]] [INFIELDS count field [field...]] 
        ///   [RETURN count identifier [AS property][identifier [AS property] ...]] 
        ///   [SUMMARIZE [FIELDS count field [field...]] [FRAGS num][LEN fragsize][SEPARATOR separator]] 
        ///   [HIGHLIGHT [FIELDS count field [field...]] [TAGS open close]] 
        ///   [SLOP slop]
        ///   [TIMEOUT timeout]
        ///   [INORDER]
        ///   [LANGUAGE language]
        ///   [EXPANDER expander]
        ///   [SCORER scorer]
        ///   [EXPLAINSCORE]
        ///   [PAYLOAD payload]
        ///   [SORTBY sortby [ASC | DESC]] 
        ///   [LIMIT offset num]
        ///   [PARAMS nargs name value [name value...]] 
        ///   [DIALECT dialect]
        /// </param>
        public static List<object> Search(IDatabase db, string IndexName, string Query, List<object> options = null)
        {
            List<object> _args = new List<object>() {
                IndexName,
                Query
            };
            if (options != null)
            {
                _args.AddRange(options);
            }
            options = new List<object>() {
                "LIMIT",
                0,
                10
            };
            return Utils.GetRedisResult(db.Execute(COMMAND.SEARCH.GetDescription(), _args.ToArray()));
        }
        public static List<object> SearchKNN(IDatabase db, string IndexName, string Query, List<KeyValuePair<string, float[]>> KNNParmeters)
        {
            List<object> options = new List<object>() { "PARAMS", KNNParmeters.Count*2, };
            foreach(var KNNParameter in  KNNParmeters)
            {
                options.Add(KNNParameter.Key);
                options.Add(Utils.ConvertFloatArrayToByteArray(KNNParameter.Value));
            }
            options.Add("DIALECT");
            options.Add(2);
            return Search(db, IndexName, Query, options);
        }
    }

}
