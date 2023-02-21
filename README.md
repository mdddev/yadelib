# About

Yadelib is yet another date extensions library written in C#. Its core functionality is to provide a convenient way of determining if any given date is a working day or not. Built-in definitions of non-working days can easily be extended or overriden completely. Yadelib fully leverages .NET's 6  `DateOnly` data type.

# How to use

Extension methods on the DateOnly, DateTime, and DateTimeOffset types make it easy to access yadelib's functionality. If needed, convert a DateTime(Offset) into a DateOnly using the `.ToDateOnly()` extension. Afterwards, yadelib's feature set is available to you.

An optional input to all of the extension methods is an instance of the `NonWorkingDays` class. This assumes a set of non-working days by default, which yadelib then uses to determine the working day status of a date. Currently, Gregorian Easter-based holidays and some German public holidays are supported for the default.

# Examples
As default, Easter-based holidays are not workdays:
```csharp
using Yadelib;
using static System.Console;

// Easter sunday 2022 was on April 17th
var d = new DateOnly(2022, 4, 18);
WriteLine(d.IsWorkingDay()); //false
```

If not specified otherwise, weekends are on Saturdays and Sundays. This can be customised. Assume saturdays are workdays:

```csharp
using Yadelib;
using Yadelib.Models;
using static System.Console;

var d = new DateOnly(2022, 4, 30);
var nwd = new NonWorkingDays
{
    Weekend = new DayOfWeek[] { DayOfWeek.Sunday }
};
WriteLine(d.IsWorkingDay()); //false
WriteLine(d.IsWorkingDay(nwd)); //true
```

Get the 5th working day a month:
```csharp
using Yadelib;
using Yadelib.Models;
using static System.Console;

var d = new DateOnly(2023, 5, 1);
var nwd = new NonWorkingDays
{
    Weekend = new DayOfWeek[] { DayOfWeek.Sunday }
};
WriteLine(d.GetXthWorkingDay(5)); //2023-05-08
WriteLine(d.GetXthWorkingDay(5, startFromEnd: true)); //2023-05-24; note: 2023-05-29 is a holiday
WriteLine(d.GetXthWorkingDay(5, nonWorkingDays: nwd)); //2023-05-06
```
Get last day of the month:

```csharp
using Yadelib;
using static System.Console;

var d = new DateOnly(2022, 7, 1);
WriteLine(d.GetLastWorkingDay()); //2022-07-29
```

Get adjacent workday:
```csharp
using Yadelib;
using Yadelib.Models;
using static System.Console;

var d = new DateOnly(2023, 5, 2);
var nwd = new NonWorkingDays
{
    CustomNonWorkingsDays = new DateOnly[]
    {
        new (2023, 5, 3),
        new (2023, 5, 4)
    }
};
WriteLine(d.GetPreviousWorkingDay()); //2023-04-28
WriteLine(d.AddWorkingDays(-1)); //2023-04-28
WriteLine(d.GetNextWorkingDay(nwd)); //2023-05-05
```