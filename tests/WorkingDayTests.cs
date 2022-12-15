namespace Yadelib.Tests;

internal class NextWorkingDayTest
{
    public NonWorkingDays Nwd { get; set; } = null!;
    public DateOnly Current { get; set; }
    public DateOnly Expected { get; set; }
}

public class WorkingDayTests
{
    [Theory]
    [MemberData(nameof(_data))]
    public void Should_correctly_calculate_next_working_day(NonWorkingDays nwd, DateOnly current, DateOnly expected)
    {
        var actual = current.GetNextWorkingDay(nwd);
        Assert.Equal(expected, actual);
    }

    private static IEnumerable<object[]> _data()
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), "Data/NextWorkingDays.json");
        var json = File.ReadAllText(path);
        var opt = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var tests = JsonSerializer.Deserialize<List<NextWorkingDayTest>>(json, opt);

        ArgumentNullException.ThrowIfNull(tests, nameof(tests));

        foreach (var test in tests)
        {
            yield return new object[]
            {
                test.Nwd,
                test.Current,
                test.Expected
            };
        }
    }
}