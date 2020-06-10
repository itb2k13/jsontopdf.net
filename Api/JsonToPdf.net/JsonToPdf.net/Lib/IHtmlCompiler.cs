namespace JsonToPdf.net.Lib
{
    public interface IHtmlCompiler
    {
        string Compile(object data, string source);
    }
}