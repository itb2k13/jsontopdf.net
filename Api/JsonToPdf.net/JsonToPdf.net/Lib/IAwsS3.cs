using JsonToPdf.net.Models;
using System.Threading.Tasks;

namespace JsonToPdf.net.Lib
{
    public interface IAwsS3
    {
        Task<Result> UploadFileAsync(byte[] file, string keyName);
    }
}