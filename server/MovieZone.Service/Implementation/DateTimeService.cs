namespace MovieZone.Service.Implementation
{
    using System;

    using MovieZone.Service.Contract;

    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}
