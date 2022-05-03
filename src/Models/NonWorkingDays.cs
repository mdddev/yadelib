namespace Yadelib.Models;

public class NonWorkingDays
{
    public GermanState GermanPublicHolidays { get; set; } = GermanState.Bund;
    public IEnumerable<DayOfWeek> Weekend { get; set; } = new DayOfWeek[] { DayOfWeek.Saturday, DayOfWeek.Sunday };
    public IEnumerable<DateOnly> CustomNonWorkingsDays { get; set; } = Enumerable.Empty<DateOnly>();
    public bool EuropeanTARGET { get; set; }
}