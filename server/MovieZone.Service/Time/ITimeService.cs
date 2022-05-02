namespace MovieZone.Service.Time
{
    using System;

    public interface ITimeService
    {
        public DateTime GetUtcNow();
    }
}
