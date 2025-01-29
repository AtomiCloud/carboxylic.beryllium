using System.Text.Json;
using System.Text.Json.Serialization;

namespace CarboxylicBeryllium;

public static class Utility
{
    public const string StandardDateFormat = "yyyy-MM-dd";
    public const string StandardTimeFormat = "HH:mm:ss";

    public static DateOnly ToDate(this string date) =>
        DateOnly.ParseExact(date, StandardDateFormat);

    public static TimeOnly ToTime(this string time) =>
        TimeOnly.ParseExact(time, StandardTimeFormat);

    public static string ToStandardDateFormat(this DateOnly date) =>
        date.ToString(StandardDateFormat);

    public static string ToStandardTimeFormat(this TimeOnly time) =>
        time.ToString(StandardTimeFormat);

    public static Uri ToUri(this string uri) => new(uri);

    public static string ToJson<T>(this T obj)
    {
        return JsonSerializer.Serialize(obj);
    }

    public static T ToObj<T>(this string json)
    {
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        return JsonSerializer.Deserialize<T>(json, options)!;
    }
}
