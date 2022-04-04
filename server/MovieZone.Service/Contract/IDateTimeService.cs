namespace MovieZone.Service.Contract
{
    using System;

    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
    }
}
