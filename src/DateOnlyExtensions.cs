namespace Yadelib;

public static class DateOnlyExtensions
{
    public static bool IsWorkingDay(this DateOnly thisDate, NonWorkingDays? nonWorkingDays = null)
    {
        var isWeekend = thisDate.isWeekend(nonWorkingDays);
        var isHoliday = thisDate.isHoliday(nonWorkingDays);
        return !isWeekend && !isHoliday;
    }
    
    public static bool IsMonthEnd(this DateOnly thisDate, bool useLastWorkingDay = false, NonWorkingDays? nonWorkingDays = null)
    {
        var currentDate = new DateOnly(thisDate.Year, thisDate.Month, thisDate.Day);
        var monthEnd = thisDate.lastWorkingDay(useLastWorkingDay, nonWorkingDays);
        return monthEnd.Equals(currentDate);
    }

    public static DateOnly AddWorkingDays(this DateOnly thisDate, int x, NonWorkingDays? nonWorkingDays = null)
    {
        if (x == 0)
        {
            return thisDate;
        }
        else if (x > 0)
        {
            var i = 0;
            do
            {
                thisDate = thisDate.AddDays(1).GetNextWorkingDay(nonWorkingDays);
                i++;
            } while (i < x);

            return thisDate;
        }
        else
        {
            var i = 0;
            do
            {
                thisDate = thisDate.AddDays(-1).GetPreviousWorkingDay(nonWorkingDays);
                i++;
            } while (i < Math.Abs(x));

            return thisDate;
        }
    }

    public static DateOnly GetNextWorkingDay(this DateOnly thisDate, NonWorkingDays? nonWorkingDays = null)
    {
        var date = thisDate.AddDays(1);
        while (!date.IsWorkingDay(nonWorkingDays)) date = date.AddDays(1);
        return date;
    }

    public static DateOnly GetPreviousWorkingDay(this DateOnly thisDate, NonWorkingDays? nonWorkingDays = null)
    {
        var date = thisDate.AddDays(-1);
        while (!date.IsWorkingDay(nonWorkingDays)) date = date.AddDays(-1);
        return date;
    }

    public static DateOnly GetXthWorkingDay(this DateOnly thisDate, int x = 1, bool startFromEnd = false, NonWorkingDays? nonWorkingDays = null)
    {
        var runningTotalOfWorkingDays = 0;
        var DAY = 0;
        if (startFromEnd)
        {
            DateOnly testDate;
            var endOfMonth = thisDate.lastDay();
            DAY = endOfMonth.Day;
            while (runningTotalOfWorkingDays < x)
            {
                try
                {
                    testDate = new DateOnly(thisDate.Year, thisDate.Month, DAY);
                }
                catch (ArgumentOutOfRangeException)
                {
                    throw new ArgumentOutOfRangeException(nameof(DAY), $"There aren't {DAY} days in {thisDate.Year}/{thisDate.Month}");
                }
                var isWorkingDay = testDate.IsWorkingDay(nonWorkingDays);
                if (isWorkingDay) { runningTotalOfWorkingDays++; }
                if (runningTotalOfWorkingDays < x) { DAY--; }
            }
        }
        else
        {
            DateOnly testDate;
            do
            {
                DAY++;
                try
                {
                    testDate = new DateOnly(thisDate.Year, thisDate.Month, DAY);
                }
                catch (ArgumentOutOfRangeException)
                {
                    throw new ArgumentOutOfRangeException(nameof(DAY), $"There aren't {DAY} days in {thisDate.Year}/{thisDate.Month}");
                }

                var isWorkingDay = testDate.IsWorkingDay(nonWorkingDays);
                if (isWorkingDay) { runningTotalOfWorkingDays++; }
            } while (runningTotalOfWorkingDays != x);
        }
        return new DateOnly(thisDate.Year, thisDate.Month, DAY);
    }

    public static DateOnly GetFirstWorkingDay(this DateOnly thisDate, NonWorkingDays? nonWorkingDays = null)
    {
        return thisDate.GetFirstXWorkingDays(1, nonWorkingDays).Single();
    }

    public static List<DateOnly> GetFirstXWorkingDays(this DateOnly thisDate, int x = 1, NonWorkingDays? nonWorkingDays = null)
    {
        if (x < 1) throw new ArgumentOutOfRangeException(nameof(x), x, $"{nameof(x)} should be gte 1");

        var days = new List<DateOnly>();
        for (int i = 1; i <= x; i++)
        {
            days.Add(thisDate.GetXthWorkingDay(i, startFromEnd: false, nonWorkingDays));
        }

        return days;
    }

    public static List<DateOnly> GetLastXWorkingDays(this DateOnly thisDate, int x = 1, NonWorkingDays? nonWorkingDays = null)
    {
        if (x < 1) throw new ArgumentOutOfRangeException(nameof(x), x, $"{nameof(x)} should be gte 1");

        var days = new List<DateOnly>();
        for (int i = 1; i <= x; i++)
        {
            days.Add(thisDate.GetXthWorkingDay(i, startFromEnd: true, nonWorkingDays));
        }

        return days;
    }

    public static DateOnly GetLastWorkingDay(this DateOnly thisDate, NonWorkingDays? nonWorkingDays = null)
    {
        return thisDate.GetLastXWorkingDays(1, nonWorkingDays).Single();
    }

    public static DateOnly GetLastDay(this DateOnly thisDate)
    {
        return new DateOnly(thisDate.Year, thisDate.Month, 1).AddMonths(1).AddDays(-1);
    }

    #region INTERNAL

    internal static bool isWeekend(this DateOnly thisDate, NonWorkingDays? nonWorkingDays = null)
    {
        NonWorkingDays considerAsNonWorkingDays = nonWorkingDays ?? new();
        DayOfWeek[] weekendDays = considerAsNonWorkingDays.Weekend.ToArray();
        return weekendDays.Contains(thisDate.DayOfWeek);
    }

    internal static bool isHoliday(this DateOnly thisDate, NonWorkingDays? nonWorkingDays = null)
    {
        HashSet<DateOnly> holidays = new();
        NonWorkingDays considerAsHoliday = nonWorkingDays ?? new();

        if (considerAsHoliday.EuropeanTARGET)
        {
            holidays.UnionWith(Holidays.TargetBankHolidays(thisDate.Year));
        }

        var selectedState = considerAsHoliday.GermanPublicHolidays;
        if (selectedState != GermanState.NONE)
        {
            holidays.UnionWith(Holidays.GermanPublicHolidays(thisDate.Year, selectedState));
        }

        if (considerAsHoliday.CustomNonWorkingsDays.Any())
        {
            holidays.UnionWith(considerAsHoliday.CustomNonWorkingsDays);
        }

        return holidays.Contains(thisDate);
    }
    
    internal static DateOnly firstDayOfMonth(this DateOnly thisDate) => new (thisDate.Year, thisDate.Month, 1);

    internal static DateOnly lastDay(this DateOnly thisDate) => thisDate.firstDayOfMonth().AddMonths(1).AddDays(-1);

    internal static DateOnly lastWorkingDay(this DateOnly thisDate, bool useLastWorkingDay = false, NonWorkingDays? nonWorkingDays = null)
    {
        var lastCalendarDayOfTheMonth = thisDate.lastDay();
        if (!useLastWorkingDay) return lastCalendarDayOfTheMonth;

        var daysToSubtract = 0;
        var adjustedDate = lastCalendarDayOfTheMonth;
        var isWorkingDay = adjustedDate.IsWorkingDay(nonWorkingDays);
        while (!isWorkingDay)
        {
            daysToSubtract--;
            adjustedDate = lastCalendarDayOfTheMonth.AddDays(daysToSubtract);
            isWorkingDay = adjustedDate.IsWorkingDay(nonWorkingDays);
        }
        return adjustedDate;
    }

    #endregion
}