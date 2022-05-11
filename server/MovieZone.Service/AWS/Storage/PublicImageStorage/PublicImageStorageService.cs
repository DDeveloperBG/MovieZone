namespace MovieZone.Service.AWS.Storage.PublicImageStorage
{
    using MovieZone.Common;
    using MovieZone.Service.DTOs.AWS.Storage;

    public class PublicImageStorageService : StorageService, IPublicImageStorageService
    {
        private const string BucketName = Globals.AWS.Storage.S3ImageBucketName;

        public PublicImageStorageService(AWSStorageConfigKeys configKeys)
            : base(configKeys, BucketName)
        {
        }
    }
}
