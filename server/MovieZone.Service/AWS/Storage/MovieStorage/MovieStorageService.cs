namespace MovieZone.Service.AWS.Storage.MovieStorage
{
    using MovieZone.Common;
    using MovieZone.Service.DTOs.AWS.Storage;

    public class MovieStorageService : StorageService, IMovieStorageService
    {
        private const string BucketName = Globals.AWS.Storage.S3MovieBucketName;

        public MovieStorageService(AWSStorageConfigKeys configKeys)
            : base(configKeys, BucketName)
        {
        }
    }
}
