namespace Yadelib.Helper;

public static partial class DateTimeExtensions
{
    public static DateOnly ToDateOnly(this DateTime thisDate) => System.DateOnly.FromDateTime(thisDate);
    public static DateOnly ToDateOnly(this DateTimeOffset thisDate) => ToDateOnly(thisDate.Date);
    public static DateOnly Today => ToDateOnly(System.DateTime.Now);
}
