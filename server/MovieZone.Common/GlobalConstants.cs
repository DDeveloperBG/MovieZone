namespace MovieZone.Common
{
    public static class GlobalConstants
    {
        public static class AppSettings
        {
            public const string DatabaseConnectionKey = "DefaultConnection";
        }

        public static class CategoryMovies
        {
            public const int PageSize = 8;

            public const string DefaultMoviesCategoryName = "New";

            public const int MovieDescriptionMaxLength = 65;
        }
    }
}
