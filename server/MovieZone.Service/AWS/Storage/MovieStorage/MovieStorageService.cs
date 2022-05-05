namespace MovieZone.Service.AWS.Storage.MovieStorage
{
    using Microsoft.Extensions.Configuration;

    using MovieZone.Common;

    public class MovieStorageService : StorageService, IMovieStorageService
    {
        private const string BucketName = GlobalConstants.AWS.S3MovieBucketName;

        public MovieStorageService(IConfiguration configuration)
            : base(configuration, BucketName)
        {
        }
    }
}
