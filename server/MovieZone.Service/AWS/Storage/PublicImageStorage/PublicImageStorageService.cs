namespace MovieZone.Service.AWS.Storage.PublicImageStorage
{
    using Microsoft.Extensions.Configuration;

    using MovieZone.Common;

    public class PublicImageStorageService : StorageService, IPublicImageStorageService
    {
        private const string BucketName = Globals.AWS.S3ImageBucketName;

        public PublicImageStorageService(IConfiguration configuration)
            : base(configuration, BucketName)
        {
        }
    }
}
