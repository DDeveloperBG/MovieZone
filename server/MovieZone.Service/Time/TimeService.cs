namespace MovieZone.Service.Time
{
    using System;

    public class TimeService : ITimeService
    {
        public DateTime GetUtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}
