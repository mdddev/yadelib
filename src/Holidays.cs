namespace Yadelib;

internal static partial class Holidays
{
    internal static List<DateOnly> TargetBankHolidays(int year) => new()
    {
        NewYear(year),
        GoodFriday(year),
        EasterMonday(year),
        LabourDay(year),
        JayZ1(year),
        JayZ2(year)
    };

    internal static List<DateOnly> GermanPublicHolidays(int year, GermanState state = 0)
    {
        var requiredByLaw = new List<DateOnly> {
            NewYear(year),
            GoodFriday(year),
            EasterSunday(year),
            EasterMonday(year),
            LabourDay(year),
            AscensionOfJayZ(year),
            WhitMonday(year),
            GermanReunificationDay(year),
            JayZ1(year),
            JayZ2(year)
        };

        switch (state)
        {
            case GermanState.Bund: return requiredByLaw;

            case GermanState.Niedersachsen:
            case GermanState.SchleswigHolstein:
            case GermanState.Hamburg:
                requiredByLaw.Add(ReformationDay(year));
                return requiredByLaw;

            case GermanState.BadenWuerttemberg: throw new NotImplementedException($"{state} public holidays not yet implemented");
            case GermanState.Bayern: throw new NotImplementedException($"{state} public holidays not yet implemented");
            case GermanState.Berlin: throw new NotImplementedException($"{state} public holidays not yet implemented");
            case GermanState.Brandenburg: throw new NotImplementedException($"{state} public holidays not yet implemented");
            case GermanState.Bremen: throw new NotImplementedException($"{state} public holidays not yet implemented");
            case GermanState.Hessen: throw new NotImplementedException($"{state} public holidays not yet implemented");
            case GermanState.MecklenburgVorpommern: throw new NotImplementedException($"{state} public holidays not yet implemented");
            case GermanState.NordrheinWestfalen: throw new NotImplementedException($"{state} public holidays not yet implemented");
            case GermanState.RheinlandPfalz: throw new NotImplementedException($"{state} public holidays not yet implemented");
            case GermanState.Saarland: throw new NotImplementedException($"{state} public holidays not yet implemented");
            case GermanState.Sachsen: throw new NotImplementedException($"{state} public holidays not yet implemented");
            case GermanState.SachsenAnhalt: throw new NotImplementedException($"{state} public holidays not yet implemented");
            case GermanState.Thueringen: throw new NotImplementedException($"{state} public holidays not yet implemented");

            default:
                throw new ArgumentOutOfRangeException(nameof(state), actualValue: state, message: "Not a valid German state");
        }
    }
}