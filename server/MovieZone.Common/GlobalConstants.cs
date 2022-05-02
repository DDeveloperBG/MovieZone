namespace MovieZone.Common
{
    public static class GlobalConstants
    {
        public static class AppRoles
        {
            public const string AdminRoleName = "Admin";
        }

        public static class AppSettings
        {
            public const string DatabaseConnectionKey = "DefaultConnection";
        }

        public static class CategoryMovies
        {
            public const int PageSize = 8;

            public const string DefaultMoviesCategoryName = "New";
        }

        public static class AWS
        {
            public const string S3MovieBucketName = "movie-zone-movies";
        }
    }
}
