namespace Yadelib.Tests;

public class FixedHolidayTests
{
    [Theory]
    [MemberData(nameof(_data))]
    public void Should_correctly_calculate_new_year(int year)
    {
        var expected = new DateOnly(year, 1, 1);
        var actual = Holidays.NewYear(year);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(_data))]
    public void Should_correctly_calculate_labour_day(int year)
    {
        var expected = new DateOnly(year, 5, 1);
        var actual = Holidays.LabourDay(year);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(_data))]
    public void Should_correctly_calculate_jayz1(int year)
    {
        var expected = new DateOnly(year, 12, 25);
        var actual = Holidays.JayZ1(year);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(_data))]
    public void Should_correctly_calculate_jayz2(int year)
    {
        var expected = new DateOnly(year, 12, 26);
        var actual = Holidays.JayZ2(year);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(_data))]
    public void Should_correctly_calculate_reformation_day(int year)
    {
        var expected = new DateOnly(year, 10, 31);
        var actual = Holidays.ReformationDay(year);
        Assert.Equal(expected, actual);
    }
    
    [Theory]
    [MemberData(nameof(_data))]
    public void Should_correctly_calculate_german_reunification(int year)
    {
        var expected = new DateOnly(year, 10, 3);
        var actual = Holidays.GermanReunificationDay(year);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(_data))]
    public void Should_correctly_enumerate_target2_bank_holidays(int year)
    {
        //expected
        DateOnly[] expected =
        {
            Holidays.NewYear(year),
            Holidays.GoodFriday(year),
            Holidays.EasterMonday(year),
            Holidays.LabourDay(year),
            Holidays.JayZ1(year),
            Holidays.JayZ2(year)
        };

        //actual
        var actual = Holidays.TargetBankHolidays(year);

        //compare
        var isOkay = expected.Except(actual).None() && actual.Except(expected).None();
        Assert.True(isOkay);
    }

    [Theory]
    [MemberData(nameof(_data))]
    public void Should_correctly_enumerate_german_federal_public_holidays_required_by_law(int year)
    {
        //expected
        DateOnly[] expected =
        {
            Holidays.NewYear(year),
            Holidays.GoodFriday(year),
            Holidays.EasterSunday(year),
            Holidays.EasterMonday(year),
            Holidays.LabourDay(year),
            Holidays.AscensionOfJayZ(year),
            Holidays.WhitMonday(year),
            Holidays.GermanReunificationDay(year),
            Holidays.JayZ1(year),
            Holidays.JayZ2(year)
        };

        //actual
        var actual = Holidays.GermanPublicHolidays(year, GermanState.Bund);

        //compare
        var isOkay = expected.Except(actual).None() && actual.Except(expected).None();
        Assert.True(isOkay);
    }

    private static IEnumerable<object[]> _data()
    {
        foreach (var year in Enumerable.Range(1984, 100))
        {
            yield return new object[] { year };
        }
    }
}