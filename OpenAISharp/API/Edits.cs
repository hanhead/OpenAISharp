using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAISharp.API
{
    public class Edits
    {
        private static string command = "/edits";
        public enum AvailableModel
        {
            [Description("text-davinci-edit-001")]
            text_davinci_edit_001,
            [Description("code-davinci-edit-001")]
            code_davinci_edit_001
        }
    }
}
