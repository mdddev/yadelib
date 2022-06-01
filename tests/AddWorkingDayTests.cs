namespace Yadelib.Tests;

internal class AddWorkingDaysTestData
{
    public DateOnly Now { get; set; }
    public int Add { get; set; }
    public DateOnly Expected { get; set; }
    public NonWorkingDays? Nwd { get; set; }
}

public class AddWorkingDayTests
{
    [Theory]
    [MemberData(nameof(_data))]
    public void Should_correctly_calculate_next_working_day(DateOnly now, int add, DateOnly expected, NonWorkingDays nwd)
    {
        var actual = now.AddWorkingDays(add, nwd);
        Assert.Equal(expected, actual);
    }

    private static IEnumerable<object[]> _data()
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), "Data/AddWorkingDays.json");
        var json = File.ReadAllText(path);
        var opt = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        opt.Converters.Add(new DateOnlyJsonConverter());
        var testsData = JsonSerializer.Deserialize<List<AddWorkingDaysTestData>>(json, opt);

        ArgumentNullException.ThrowIfNull(testsData, nameof(testsData));

        foreach (var data in testsData)
        {
            yield return new object[]
            {
                data.Now,
                data.Add,
                data.Expected,
                data.Nwd ?? new ()
            };
        }
    }
}