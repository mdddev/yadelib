namespace Yadelib.Helper;

public static class DateFormatExtensions
{
    public static string ToIsoString(this DateOnly thisDate) => thisDate.ToString("yyyy-MM-dd");
    public static string ToIsoString(this DateTime thisDate) => new DateTimeOffset(thisDate).ToIsoString();
    public static string ToIsoString(this DateTimeOffset thisDate) => thisDate.ToString("yyyy-MM-ddTHH:mm:ss.fffffZ");
    public static string yyyyMMdd(this DateTimeOffset thisDate) => thisDate.ToString("yyyyMMdd");
    public static string yyyyMMdd(this DateTime thisDate) => thisDate.ToString("yyyyMMdd");
    public static string yyyyMMdd(this DateOnly thisDate) => thisDate.ToString("yyyyMMdd");
}
