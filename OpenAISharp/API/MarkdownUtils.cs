using Markdig;
using System.Text.RegularExpressions;

namespace OpenAISharp.API
{
    public static class MarkdownUtils
    {

        public static string MarkdownToHtml(this string markdown)
        {
            MarkdownPipeline pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            return Markdown.ToHtml(markdown, pipeline);
        }
        public static string MarkdownToText(this string markdown)
        {
            MarkdownPipeline pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            return Markdown.ToPlainText(markdown, pipeline);
        }
        public static string StripHTML(this string source)
        {
            try
            {
                string text = source.Replace("\r", " ");
                text = text.Replace("\n", " ");
                text = text.Replace("\t", string.Empty);
                text = Regex.Replace(text, "( )+", " ");
                text = Regex.Replace(text, "<( )*head([^>])*>", "<head>", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "(<( )*(/)( )*head( )*>)", "</head>", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "(<head>).*(</head>)", string.Empty, RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "<( )*script([^>])*>", "<script>", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "(<( )*(/)( )*script( )*>)", "</script>", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "(<script>).*(</script>)", string.Empty, RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "<( )*style([^>])*>", "<style>", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "(<( )*(/)( )*style( )*>)", "</style>", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "(<style>).*(</style>)", string.Empty, RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "<( )*td([^>])*>", "\t", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "<( )*br( )*>", "\r", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "<( )*li( )*>", "\r", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "<( )*div([^>])*>", "\r\r", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "<( )*tr([^>])*>", "\r\r", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "<( )*p([^>])*>", "\r\r", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "<[^>]*>", string.Empty, RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "&nbsp;", " ", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "&bull;", " * ", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "&lsaquo;", "<", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "&rsaquo;", ">", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "&trade;", "(tm)", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "&frasl;", "/", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "<", "<", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, ">", ">", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "&copy;", "(c)", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "&reg;", "(r)", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "&(.{2,6});", string.Empty, RegexOptions.IgnoreCase);
                text = text.Replace("\n", "\r");
                text = Regex.Replace(text, "(\r)( )+(\r)", "\r\r", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "(\t)( )+(\t)", "\t\t", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "(\t)( )+(\r)", "\t\r", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "(\r)( )+(\t)", "\r\t", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "(\r)(\t)+(\r)", "\r\r", RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "(\r)(\t)+", "\r\t", RegexOptions.IgnoreCase);
                string text2 = "\r\r\r";
                string text3 = "\t\t\t\t\t";
                for (int i = 0; i < text.Length; i++)
                {
                    text = text.Replace(text2, "\r\r");
                    text = text.Replace(text3, "\t\t\t\t");
                    text2 += "\r";
                    text3 += "\t";
                }

                return text;
            }
            catch
            {
            }

            return source;
        }

    }
}
