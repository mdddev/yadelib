using System.Text.Json.Serialization;
namespace Yadelib.Helper;

public sealed class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateOnly.FromDateTime(reader.GetDateTime());
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("O", CultureInfo.InvariantCulture));
    }
}

public class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
{
    private const string _TIME_FORMAT = "HH:mm:ss.FFFFFFF";

    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var txt = reader.GetString();
        if (txt == null)
        {
            throw new ArgumentNullException(nameof(reader.GetString));
        }
        else
        {
            return TimeOnly.ParseExact(txt, _TIME_FORMAT, CultureInfo.InvariantCulture);
        }
    }

    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(_TIME_FORMAT, CultureInfo.InvariantCulture));
    }
}