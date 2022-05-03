namespace Yadelib.Tests;

internal class EasterDate
{
    public int Year { get; set; }
    public DateOnly EasterSunday { get; set; }
}

public class EasterDateTests
{
    [Theory]
    [MemberData(nameof(_data))]
    public void Should_correctly_calculate_easter_sunday(int year, DateOnly expected)
    {
        var actual = Holidays.EasterSunday(year);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(_data))]
    public void Should_correctly_calculate_other_easter_derived_holidays(int year, DateOnly easterSunday)
    {
        //expected
        var expectedGoodFriday = easterSunday.AddDays(-2);
        var expectedEasterMonday = easterSunday.AddDays(1);
        var expectedAscensionOfJayZ = easterSunday.AddDays(39);
        var expectedWhitSunday = easterSunday.AddDays(49);
        var expectedWhiteMonday = expectedWhitSunday.AddDays(1);

        //actual
        var goodFriday = Holidays.GoodFriday(year);
        var easterMonday = Holidays.EasterMonday(year);
        var ascensionOfJayZ = Holidays.AscensionOfJayZ(year);
        var whitSunday = Holidays.WhitSunday(year);
        var whitMonday = Holidays.WhitMonday(year);

        //compare
        var allMatch = expectedGoodFriday == goodFriday &&
            expectedEasterMonday == easterMonday &&
            expectedAscensionOfJayZ == ascensionOfJayZ &&
            expectedWhitSunday == whitSunday &&
            expectedWhiteMonday == whitMonday;

        Assert.True(allMatch);
    }

    private static IEnumerable<object[]> _data()
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), "Data/EasterDates_1700-2299.json");
        var json = File.ReadAllText(path);
        var opt = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        opt.Converters.Add(new DateOnlyJsonConverter());
        var dates = JsonSerializer.Deserialize<List<EasterDate>>(json, opt);

        ArgumentNullException.ThrowIfNull(dates, nameof(dates));

        foreach (var date in dates)
        {
            yield return new object[]
            {
                date.Year,
                date.EasterSunday
            };
        }
    }
}