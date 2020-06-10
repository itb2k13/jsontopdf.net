using HandlebarsDotNet;

namespace JsonToPdf.net.Lib
{
    public class HandlebarsHtmlCompiler : IHtmlCompiler
    {

        public string Compile(object data, string source)
        {
            var hbar = Handlebars.Compile(source);
            var result = hbar(data ?? new { });
            return result;
        }
    }
}
