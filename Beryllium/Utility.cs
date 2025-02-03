using System.Text;
using System.Text.Json;

namespace CarboxylicBeryllium;

public static class Utility
{
    public const string StandardDateFormat = "yyyy-MM-dd";
    public const string StandardTimeFormat = "HH:mm:ss";

    /// <summary>
    /// Convert a Date String to a DateOnly object
    /// </summary>
    /// <param name="date">Date String in standard format, yyyy-MM-dd</param>
    /// <returns>DateOnly</returns>
    public static DateOnly ToDate(this string date) =>
        DateOnly.ParseExact(date, StandardDateFormat);

    /// <summary>
    /// Convert a Time String to a TimeOnly object
    /// </summary>
    /// <param name="time">Time String in standard format, HH:mm:ss</param>
    /// <returns>TimeOnly</returns>
    /// <returns></returns>
    public static TimeOnly ToTime(this string time) =>
        TimeOnly.ParseExact(time, StandardTimeFormat);

    /// <summary>
    /// Convert a DateOnly object to a Date String in standard format, yyyy-MM-dd
    /// </summary>
    /// <param name="date">The DateOnly object to convert</param>
    /// <returns>string</returns>
    public static string ToStandardDateFormat(this DateOnly date) =>
        date.ToString(StandardDateFormat);

    /// <summary>
    /// Convert a TimeOnly object to a Time String in standard format, HH:mm:ss
    /// </summary>
    /// <param name="time">The TimeOnly object to convert</param>
    /// <returns>string</returns>
    public static string ToStandardTimeFormat(this TimeOnly time) =>
        time.ToString(StandardTimeFormat);

    /// <summary>
    /// Convert a String to a Uri object
    /// </summary>
    /// <param name="uri">the Uri String</param>
    /// <returns>Uri</returns>
    public static Uri ToUri(this string uri) => new(uri);

    /// <summary>
    /// Convert any CLR object to a JSON string
    /// </summary>
    /// <param name="obj">CLR Object</param>
    /// <typeparam name="T">Shape (type) of the object</typeparam>
    /// <returns>JSON string</returns>
    public static string ToJson<T>(this T obj)
    {
        return JsonSerializer.Serialize(obj);
    }

    /// <summary>
    /// Convert a JSON string to a CLR object
    /// </summary>
    /// <param name="json">JSON string</param>
    /// <typeparam name="T">Shape (type) of the object</typeparam>
    /// <returns>CLR Object</returns>
    public static T ToObj<T>(this string json)
    {
        return JsonSerializer.Deserialize<T>(json)!;
    }

    /// <summary>
    /// Encodes the specified plain text using Base64, defaults to UTF8 encoding
    /// </summary>
    /// <param name="plainText">The plain text.</param>
    /// <param name="encoding"> The encoding format to use. Defaults to UTF8</param>
    /// <returns></returns>
    public static string Base64Encode(this string plainText, Encoding? encoding)
    {
        var enc = encoding ?? Encoding.UTF8;
        var plainTextBytes = enc.GetBytes(plainText);
        return Convert.ToBase64String(plainTextBytes);
    }

    /// <summary>
    /// Decodes the specified base64 encoded string, defaults to UTF8 encoding
    /// </summary>
    /// <param name="base64EncodedData">The base64 encoded data.</param>
    /// <param name="encoding">The encoding format to use. Defaults to UTF8</param>
    /// <returns></returns>
    public static string Base64Decode(this string base64EncodedData, Encoding? encoding)
    {
        var enc = encoding ?? Encoding.UTF8;
        var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
        return enc.GetString(base64EncodedBytes);
    }

    /// <summary>
    /// Converts object to HTTP Message for HTTP Clients to use
    /// </summary>
    /// <param name="req">The object to be converted into HTTP Message</param>
    /// <param name="method">HTTP Method (GET, POST, PUT, DELETE, ...)</param>
    /// <param name="endpoint">Endpoint to send the URL to</param>
    /// <returns></returns>
    public static HttpRequestMessage ToMessage(this object req, HttpMethod method, string endpoint)
    {
        var content = req.ToStringRequest();
        return new HttpRequestMessage(method, endpoint) { Content = content };
    }

    /// <summary>
    /// Convert object to a StringContent
    /// </summary>
    /// <param name="req">The object to be converted into StringContent</param>
    /// <returns>String content of JSON format of object</returns>
    public static StringContent ToStringRequest(this object req)
    {
        return new StringContent(req.ToJson(), Encoding.UTF8, "application/json");
    }
}
