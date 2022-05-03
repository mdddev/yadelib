namespace Yadelib.Tests;

public class NonWorkingDayTests
{
    [Fact]
    public void Should_use_saturday_and_sunday_as_default_weekend_definition()
    {
        var saturday = new DateOnly(2022, 4, 23);
        var sunday = saturday.AddDays(1);
        var monday = sunday.AddDays(1);
        var tuesday = monday.AddDays(1);
        var wednesday = tuesday.AddDays(1);
        var thursday = wednesday.AddDays(1);
        var friday = thursday.AddDays(1);

        var defaultWeekendIsSatAndSun = saturday.isWeekend() &&
            sunday.isWeekend() &&
            !monday.isWeekend() &&
            !tuesday.isWeekend() &&
            !wednesday.isWeekend() &&
            !thursday.isWeekend() &&
            !friday.isWeekend();

        Assert.True(defaultWeekendIsSatAndSun);
    }

    [Fact]
    public void Should_respect_deviating_weekend_definition()
    {
        var saturday = new DateOnly(2022, 4, 23);
        var sunday = saturday.AddDays(1);
        var monday = sunday.AddDays(1);
        var tuesday = monday.AddDays(1);
        var wednesday = tuesday.AddDays(1);
        var thursday = wednesday.AddDays(1);
        var friday = thursday.AddDays(1);

        //weekend is WED + FRI
        var nwd = new NonWorkingDays { Weekend = new List<DayOfWeek> { DayOfWeek.Wednesday, DayOfWeek.Friday } };
        var customWeekendIsWedAndFri = !saturday.isWeekend(nwd) &&
            !sunday.isWeekend(nwd) &&
            !monday.isWeekend(nwd) &&
            !tuesday.isWeekend(nwd) &&
            wednesday.isWeekend(nwd) &&
            !thursday.isWeekend(nwd) &&
            friday.isWeekend(nwd);

        //there is no weekend
        var nwd2 = new NonWorkingDays { Weekend = new List<DayOfWeek>() };
        var thereIsNoWeekend = !saturday.isWeekend(nwd2) &&
            !sunday.isWeekend(nwd2) &&
            !monday.isWeekend(nwd2) &&
            !tuesday.isWeekend(nwd2) &&
            !wednesday.isWeekend(nwd2) &&
            !thursday.isWeekend(nwd2) &&
            !friday.isWeekend(nwd2);

        Assert.True(customWeekendIsWedAndFri && thereIsNoWeekend);
    }

    [Fact]
    public void Should_respect_custom_non_working_day_definition()
    {
        var someDate = new DateOnly(2022, 3, 17); // THU
        var nwd = new NonWorkingDays { CustomNonWorkingsDays = new List<DateOnly> { someDate } };
        var isWorkingDayBefore = someDate.IsWorkingDay();
        var isNoWorkingDayAfter = !someDate.IsWorkingDay(nwd);
        var respectsCustomNonWorkingDayDefinition = isWorkingDayBefore && isNoWorkingDayAfter;

        Assert.True(respectsCustomNonWorkingDayDefinition);
    }
}