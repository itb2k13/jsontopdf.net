using Microsoft.Extensions.Configuration;

namespace JsonToPdf.net.Lib
{
    public interface IConverter
    {
        byte[] Convert(string htmlCode, bool isPortrait);
    }
}