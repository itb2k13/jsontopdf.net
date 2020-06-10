using HiQPdf;
using Microsoft.Extensions.Configuration;
using System;

namespace JsonToPdf.net.Lib
{
    public class HiQHtmlToPdfConverter : IConverter
    {
        IConfiguration _configuration;

        public HiQHtmlToPdfConverter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public byte[] Convert(string htmlCode, bool isPortrait)
        {

            var htmlToPdfConverter = new HtmlToPdf();            

            if (_configuration.GetValue<string>("ProxyHost") != string.Empty)
            {
                htmlToPdfConverter.Proxy.Host = _configuration.GetValue<string>("ProxyHost");
                htmlToPdfConverter.Proxy.Port = _configuration.GetValue<int>("ProxyPort");
                htmlToPdfConverter.Proxy.Protocol = ProxyProtocol.HTTP;
            }

            htmlToPdfConverter.Document.PageSize = PdfPageSize.Letter;

            if (isPortrait)
            {
                htmlToPdfConverter.BrowserWidth = 816;
                htmlToPdfConverter.Document.PageOrientation = PdfPageOrientation.Portrait;
            }
            else
            {
                htmlToPdfConverter.BrowserWidth = 1056;
                htmlToPdfConverter.Document.PageOrientation = PdfPageOrientation.Landscape;
            }

            return htmlToPdfConverter.ConvertHtmlToMemory(htmlCode, string.Empty);
        }
    }
}
