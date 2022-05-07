namespace MovieZone.Common
{
    public static class ValidationConstants
    {
        public static readonly IReadOnlySet<string> ImageFileValidExtensions
                            = new HashSet<string>() { "png", "jpg", "jpeg", "gif" };

        public static readonly string ImageFileExtensionExceptionMessage = $"Image file format is not supported. Please use one of the following: {string.Join(", ", ImageFileValidExtensions)}";

        public static class Movie
        {
            public const int NameMinLength = 1;
            public const int NameMaxLength = 80;

            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 250;

            public const int YearOfPublishingMinValue = 1888;
            public const int YearOfPublishingAfterCurrentYearRange = 10;

            public const int AgeRestrictionMinValue = 1;
            public const int AgeRestrictionMaxValue = 21;

            public static readonly IReadOnlySet<string> MovieFileValidExtensions
                            = new HashSet<string>() { "mp4", "wmv", "webm" };

            public static readonly string MovieFileExtensionExceptionMessage = $"Movie file format is not supported. Please use one of the following: {string.Join(", ", MovieFileValidExtensions)}";

            public static string GetYearOfPublishingExceptionMessage(int maxYear)
            {
                return $"Year of publishing can be between {YearOfPublishingMinValue} and {maxYear}!";
            }
        }
    }
}
