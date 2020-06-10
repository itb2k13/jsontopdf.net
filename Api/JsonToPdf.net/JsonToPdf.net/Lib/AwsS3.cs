using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using JsonToPdf.net.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace JsonToPdf.net.Lib
{

    public class AwsS3 : IAwsS3
    {
        private readonly IAmazonS3 s3Client;
        private readonly string _bucketName;

        public AwsS3(IConfiguration configuration)
        {
            _bucketName = configuration.GetValue<string>("defaultBucketName");
            var awsConfig = new AmazonS3Config { Timeout = TimeSpan.FromSeconds(10), RegionEndpoint = RegionEndpoint.EUWest1 };

            if (configuration.GetValue<string>("ProxyHost") != string.Empty)
            {
                awsConfig.ProxyHost = configuration.GetValue<string>("ProxyHost");
                awsConfig.ProxyPort = configuration.GetValue<int>("ProxyPort");
            }

            s3Client = new AmazonS3Client(
                configuration.GetValue<string>("awsAccessKeyId"),
                configuration.GetValue<string>("awsSecretAccessKey"),
                awsConfig
                );

        }

        public async Task<Result> UploadFileAsync(byte[] file, string keyName)
        {
            try
            {
                using (var fileToUpload = new MemoryStream(file))
                {
                    await new TransferUtility(s3Client).UploadAsync(fileToUpload, _bucketName, keyName);
                    return new Result { Success = true, Message = $"https://{_bucketName}.s3-eu-west-1.amazonaws.com/{keyName}" };
                }
            }
            catch (Exception e)
            {
                return new Result { Message = e.Message };
            }

        }
    }
}
