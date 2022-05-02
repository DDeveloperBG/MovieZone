namespace MovieZone.Common
{
    public static class ValidationConstants
    {
        public static class Movie
        {
            public const int NameMinLength = 1;
            public const int NameMaxLength = 80;

            public const int DescriptionMinLength = 40;
            public const int DescriptionMaxLength = 250;

            public const int YearOfPublishingMinValue = 1888;
            public const int YearOfPublishingAfterCurrentYearRange = 10;

            public static readonly IReadOnlySet<string> MovieFileValidExtensions
                            = new HashSet<string>() { "mp4", "mov", "wmv", "avi", "avchd" };

            public static readonly string MovieFileExtensionExceptionMessage = $"Movie file format is not supported. Please use one of the following: {string.Join(", ", MovieFileValidExtensions)}";

            public static string GetYearOfPublishingExceptionMessage(int maxYear)
            {
                return $"Year of publishing can be between {YearOfPublishingMinValue} and {maxYear}!";
            }
        }
    }
}
