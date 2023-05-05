using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAISharp.API.RedisUtils
{
    public class Client
    {
        public enum COMMAND
        {
            [Description("AUTH")]
            AUTH,
            [Description("CLIENT CACHING")]
            CACHING,
            [Description("CLIENT GETNAME")]
            GETNAME,
            [Description("CLIENT GETREDIR")]
            GETREDIR,
            [Description("CLIENT ID")]
            ID,
            [Description("CLIENT INFO")]
            INFO,
            [Description("CLIENT KILL")]
            KILL,
            [Description("CLIENT LIST")]
            LIST,
            [Description("CLIENT NO-EVICT")]
            NO_EVICT,
            [Description("CLIENT NO-TOUCH")]
            NO_TOUCH,
            [Description("CLIENT PAUSE")]
            PAUSE,
            [Description("CLIENT REPLY")]
            REPLY,
            [Description("CLIENT SETINFO")]
            SETINFO,
            [Description("CLIENT SETNAME")]
            SETNAME,
            [Description("CLIENT TRACKING")]
            TRACKING,
            [Description("CLIENT TRACKINGINFO")]
            TRACKINGINFO,
            [Description("CLIENT UNBLOCK")]
            UNBLOCK,
            [Description("CLIENT UNPAUSE")]
            UNPAUSE,
            [Description("ECHO")]
            ECHO,
            [Description("HELLO")]
            HELLO,
            [Description("PING")]
            PING,
        }
    }
}
