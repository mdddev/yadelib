namespace Yadelib.Tests;
internal class EndOfMonth
{
    public DateOnly EOM { get; set; }
}
public class FirstAndLastDayTests
{
    [Theory]
    [InlineData(2020, 1)]
    [InlineData(2020, 2)]
    [InlineData(2020, 3)]
    [InlineData(2020, 4)]
    [InlineData(2020, 5)]
    [InlineData(2020, 6)]
    [InlineData(2020, 7)]
    [InlineData(2020, 8)]
    [InlineData(2020, 9)]
    [InlineData(2020, 10)]
    [InlineData(2020, 11)]
    [InlineData(2020, 12)]
    public void Should_correctly_calculate_the_first_day_of_the_month(int year, int month)
    {
        var expected = new DateOnly (year, month, 1);
        var actual = new DateOnly(year, month, 15).GetFirstDay();
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(2020, 1, "{\"EOM\":\"2020-01-31\"}")]
    [InlineData(2020, 2, "{\"EOM\":\"2020-02-29\"}")] //leap-year
    [InlineData(2020, 3, "{\"EOM\":\"2020-03-31\"}")]
    [InlineData(2020, 4, "{\"EOM\":\"2020-04-30\"}")]
    [InlineData(2020, 5, "{\"EOM\":\"2020-05-31\"}")]
    [InlineData(2020, 6, "{\"EOM\":\"2020-06-30\"}")]
    [InlineData(2020, 7, "{\"EOM\":\"2020-07-31\"}")]
    [InlineData(2020, 8, "{\"EOM\":\"2020-08-31\"}")]
    [InlineData(2020, 9, "{\"EOM\":\"2020-09-30\"}")]
    [InlineData(2020, 10, "{\"EOM\":\"2020-10-31\"}")]
    [InlineData(2020, 11, "{\"EOM\":\"2020-11-30\"}")]
    [InlineData(2020, 12, "{\"EOM\":\"2020-12-31\"}")]
    [InlineData(2021, 2, "{\"EOM\":\"2021-02-28\"}")]
    [InlineData(2022, 2, "{\"EOM\":\"2022-02-28\"}")]
    [InlineData(2023, 2, "{\"EOM\":\"2023-02-28\"}")]
    [InlineData(2024, 2, "{\"EOM\":\"2024-02-29\"}")] //leap-year
    [InlineData(2025, 2, "{\"EOM\":\"2025-02-28\"}")]
    [InlineData(2026, 2, "{\"EOM\":\"2026-02-28\"}")]
    [InlineData(2027, 2, "{\"EOM\":\"2027-02-28\"}")]
    [InlineData(2028, 2, "{\"EOM\":\"2028-02-29\"}")] //leap-year
    [InlineData(2029, 2, "{\"EOM\":\"2029-02-28\"}")]
    [InlineData(2030, 2, "{\"EOM\":\"2030-02-28\"}")]
    public void Should_correctly_calculate_the_last_day_of_the_month(int year, int month, string lastDay)
    {
        var expected = JsonSerializer.Deserialize<EndOfMonth>(lastDay);
        ArgumentNullException.ThrowIfNull(expected, nameof(expected));
        var actual = new DateOnly(year, month, 15).GetLastDay();
        Assert.Equal(expected.EOM, actual);
    }
}