namespace Yadelib.Helper;

public static class DateOnlyConversionExtensions
{
    ///<summary>Uses Zero-timespan and unspecified datetime-kind</summary>
    public static DateTime ToDateTime(this DateOnly thisDate)
    {
        TimeOnly time = TimeOnly.FromTimeSpan(TimeSpan.Zero);
        return thisDate.ToDateTime(time, DateTimeKind.Unspecified);
    }

    public static DateTimeOffset ToDateTimeOffset(this DateOnly thisDate) => new(thisDate.ToDateTime(), TimeSpan.Zero);
}