namespace MovieZone.Service.AWS.Storage
{
    using System.IO;
    using System.Threading.Tasks;

    using Amazon;
    using Amazon.S3;
    using Amazon.S3.Model;

    using Microsoft.Extensions.Configuration;
    using MovieZone.Common;
    using MovieZone.Service.DTOs.AWS.Storage;

    public abstract class StorageService : IStorageService
    {
        private readonly IAmazonS3 s3Client;
        private readonly string bucketName;

        public StorageService(IConfiguration configuration, string bucketName)
        {
            this.s3Client = new AmazonS3Client(
                configuration[Globals.AWS.Storage.AccessKeyIdConfigKey],
                configuration[Globals.AWS.Storage.SecretAccessKeyConfigKey],
                RegionEndpoint.EUWest2);

            this.bucketName = bucketName;
        }

        public virtual Task UploadFileAsync(
            Stream fileStream,
            string fileName,
            string contentType)
        {
            return this.s3Client
                .UploadObjectFromStreamAsync(this.bucketName, fileName, fileStream, null);
        }

        public virtual async Task<GetFileByKeyDTO> GetFileByKeyAsync(
            string key)
        {
            var s3Object = await this.s3Client.GetObjectAsync(this.bucketName, key);

            return new GetFileByKeyDTO
            {
                FileStream = s3Object.ResponseStream,
                FileType = s3Object.Headers.ContentType,
            };
        }

        public virtual Task DeleteFileAsync(string key)
        {
            return this.s3Client.DeleteObjectAsync(this.bucketName, key);
        }
    }
}
