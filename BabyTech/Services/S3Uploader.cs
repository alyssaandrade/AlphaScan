using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BabyTech.Services
{
    class S3Uploader
    {
        private readonly RegionEndpoint bucketRegion = RegionEndpoint.USEast1;
        private IAmazonS3 s3Client;

        public async Task UploadFileAsync(string bucketName, string keyName, string filePath)
        {
            try
            {
                var fileTransferUtility =
                    new TransferUtility(s3Client);

                await fileTransferUtility.UploadAsync(filePath, bucketName, keyName);
                Console.WriteLine("Upload completed");
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
            }

        }

        public S3Uploader(AWSCredentials credentials)
        {
            this.s3Client = new AmazonS3Client(credentials, this.bucketRegion);
        }
    }
}
