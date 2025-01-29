using System.Dynamic;
using CarboxylicBeryllium;
using FluentAssertions;
using Xunit.Sdk;

namespace UnitTest;

// Define TheoryData classes for type safety
public class StringToDateOnlyData : TheoryData<string, DateOnly>
{
    public StringToDateOnlyData()
    {
        Add("2100-01-01", new DateOnly(2100, 1, 1));
        Add("2120-01-02", new DateOnly(2120, 1, 2));
        Add("2036-01-03", new DateOnly(2036, 1, 3));
        Add("2022-11-04", new DateOnly(2022, 11, 4));
        Add("2022-01-05", new DateOnly(2022, 1, 5));
        Add("2022-03-06", new DateOnly(2022, 3, 6));
        Add("2024-01-07", new DateOnly(2024, 1, 7));
        Add("2024-12-08", new DateOnly(2024, 12, 8));
        Add("2024-01-09", new DateOnly(2024, 1, 9));
        Add("2024-07-10", new DateOnly(2024, 7, 10));
        Add("2025-01-11", new DateOnly(2025, 1, 11));
        Add("2025-01-12", new DateOnly(2025, 1, 12));
        Add("2025-01-13", new DateOnly(2025, 1, 13));
        Add("2025-01-14", new DateOnly(2025, 1, 14));
        Add("2025-01-15", new DateOnly(2025, 1, 15));
        Add("2025-01-16", new DateOnly(2025, 1, 16));
        Add("1997-01-17", new DateOnly(1997, 1, 17));
        Add("1200-03-28", new DateOnly(1200, 03, 28));
        Add("3200-03-29", new DateOnly(3200, 03, 29));
        Add("1200-03-30", new DateOnly(1200, 03, 30));
        Add("1200-04-01", new DateOnly(1200, 04, 01));
        Add("1347-04-02", new DateOnly(1347, 04, 02));
    }
}

public class StringToTimeOnlyData : TheoryData<string, TimeOnly>
{
    public StringToTimeOnlyData()
    {
        Add("00:00:00", new TimeOnly(0, 0, 0));
        Add("01:02:03", new TimeOnly(1, 2, 3));
        Add("04:05:06", new TimeOnly(4, 5, 6));
        Add("07:08:09", new TimeOnly(7, 8, 9));
        Add("10:11:12", new TimeOnly(10, 11, 12));
        Add("13:14:15", new TimeOnly(13, 14, 15));
        Add("16:17:18", new TimeOnly(16, 17, 18));
        Add("19:20:21", new TimeOnly(19, 20, 21));
        Add("22:23:24", new TimeOnly(22, 23, 24));
        Add("23:59:59", new TimeOnly(23, 59, 59));
        Add("00:00:01", new TimeOnly(0, 0, 1));
        Add("00:00:02", new TimeOnly(0, 0, 2));
        Add("00:00:03", new TimeOnly(0, 0, 3));
        Add("00:00:04", new TimeOnly(0, 0, 4));
        Add("00:00:05", new TimeOnly(0, 0, 5));
        Add("00:00:06", new TimeOnly(0, 0, 6));
        Add("00:00:07", new TimeOnly(0, 0, 7));
        Add("00:00:08", new TimeOnly(0, 0, 8));
        Add("00:00:09", new TimeOnly(0, 0, 9));
        Add("00:00:10", new TimeOnly(0, 0, 10));
        Add("00:00:11", new TimeOnly(0, 0, 11));
        Add("00:00:12", new TimeOnly(0, 0, 12));
        Add("00:00:13", new TimeOnly(0, 0, 13));
        Add("00:00:14", new TimeOnly(0, 0, 14));
        Add("00:00:15", new TimeOnly(0, 0, 15));
        Add("00:00:50", new TimeOnly(0, 0, 50));
        Add("00:00:51", new TimeOnly(0, 0, 51));
        Add("00:00:52", new TimeOnly(0, 0, 52));
        Add("00:00:53", new TimeOnly(0, 0, 53));
        Add("00:00:54", new TimeOnly(0, 0, 54));
        Add("00:00:55", new TimeOnly(0, 0, 55));
        Add("00:00:56", new TimeOnly(0, 0, 56));
        Add("00:00:57", new TimeOnly(0, 0, 57));
        Add("00:00:58", new TimeOnly(0, 0, 58));
        Add("00:00:59", new TimeOnly(0, 0, 59));
        Add("00:01:00", new TimeOnly(0, 1, 0));
    }
}

public class DateOnlyToStringData : TheoryData<DateOnly, string>
{
    public DateOnlyToStringData()
    {
        Add(new DateOnly(2100, 1, 1), "2100-01-01");
        Add(new DateOnly(2120, 1, 2), "2120-01-02");
        Add(new DateOnly(2036, 1, 3), "2036-01-03");
        Add(new DateOnly(2022, 11, 4), "2022-11-04");
        Add(new DateOnly(2022, 1, 5), "2022-01-05");
        Add(new DateOnly(2022, 3, 6), "2022-03-06");
        Add(new DateOnly(2024, 1, 7), "2024-01-07");
        Add(new DateOnly(2024, 12, 8), "2024-12-08");
        Add(new DateOnly(2024, 1, 9), "2024-01-09");
        Add(new DateOnly(2024, 7, 10), "2024-07-10");
        Add(new DateOnly(2025, 1, 11), "2025-01-11");
        Add(new DateOnly(2025, 1, 12), "2025-01-12");
        Add(new DateOnly(2025, 1, 13), "2025-01-13");
        Add(new DateOnly(2025, 1, 14), "2025-01-14");
        Add(new DateOnly(2025, 1, 15), "2025-01-15");
        Add(new DateOnly(2025, 1, 16), "2025-01-16");
        Add(new DateOnly(1997, 1, 17), "1997-01-17");
        Add(new DateOnly(1200, 03, 28), "1200-03-28");
        Add(new DateOnly(3200, 03, 29), "3200-03-29");
        Add(new DateOnly(1200, 03, 30), "1200-03-30");
        Add(new DateOnly(1200, 04, 01), "1200-04-01");
        Add(new DateOnly(1347, 04, 02), "1347-04-02");
    }
}

public class TimeOnlyToStringData : TheoryData<TimeOnly, string>
{
    public TimeOnlyToStringData()
    {
        Add(new TimeOnly(0, 0, 0), "00:00:00");
        Add(new TimeOnly(1, 2, 3), "01:02:03");
        Add(new TimeOnly(4, 5, 6), "04:05:06");
        Add(new TimeOnly(7, 8, 9), "07:08:09");
        Add(new TimeOnly(10, 11, 12), "10:11:12");
        Add(new TimeOnly(13, 14, 15), "13:14:15");
        Add(new TimeOnly(16, 17, 18), "16:17:18");
        Add(new TimeOnly(19, 20, 21), "19:20:21");
        Add(new TimeOnly(22, 23, 24), "22:23:24");
        Add(new TimeOnly(23, 59, 59), "23:59:59");
        Add(new TimeOnly(0, 0, 1), "00:00:01");
        Add(new TimeOnly(0, 0, 2), "00:00:02");
        Add(new TimeOnly(0, 0, 3), "00:00:03");
        Add(new TimeOnly(0, 0, 4), "00:00:04");
        Add(new TimeOnly(0, 0, 5), "00:00:05");
        Add(new TimeOnly(0, 0, 6), "00:00:06");
        Add(new TimeOnly(0, 0, 7), "00:00:07");
        Add(new TimeOnly(0, 0, 8), "00:00:08");
        Add(new TimeOnly(0, 0, 9), "00:00:09");
        Add(new TimeOnly(0, 0, 10), "00:00:10");
        Add(new TimeOnly(0, 0, 11), "00:00:11");
        Add(new TimeOnly(0, 0, 12), "00:00:12");
        Add(new TimeOnly(0, 0, 13), "00:00:13");
        Add(new TimeOnly(0, 0, 14), "00:00:14");
        Add(new TimeOnly(0, 0, 15), "00:00:15");
        Add(new TimeOnly(0, 0, 50), "00:00:50");
        Add(new TimeOnly(0, 0, 51), "00:00:51");
        Add(new TimeOnly(0, 0, 52), "00:00:52");
        Add(new TimeOnly(0, 0, 53), "00:00:53");
        Add(new TimeOnly(0, 0, 54), "00:00:54");
        Add(new TimeOnly(0, 0, 55), "00:00:55");
        Add(new TimeOnly(0, 0, 56), "00:00:56");
        Add(new TimeOnly(0, 0, 57), "00:00:57");
        Add(new TimeOnly(0, 0, 58), "00:00:58");
        Add(new TimeOnly(0, 0, 59), "00:00:59");
        Add(new TimeOnly(0, 1, 0), "00:01:00");
    }
}

public class StringToUriData : TheoryData<string, Uri>
{
    public StringToUriData()
    {
        // base host
        Add("https://atomi.cloud", new Uri("https://atomi.cloud"));
        // with port
        Add("https://atomi.cloud:8080", new Uri("https://atomi.cloud:8080"));
        // with ip
        Add("https://127.0.0.1", new Uri("https://127.0.0.1"));
        // with ip and port
        Add("https://127.0.0.1:8080", new Uri("https://127.0.0.1:8080"));
        // with path
        Add("https://atomi.cloud/test", new Uri("https://atomi.cloud/test"));
        // with query only
        Add("https://atomi.cloud?query=true", new Uri("https://atomi.cloud?query=true"));
        // with fragment only
        Add("https://atomi.cloud#fragment", new Uri("https://atomi.cloud#fragment"));
        // with path and fragment
        Add("https://atomi.cloud/test#fragment", new Uri("https://atomi.cloud/test#fragment"));
        // with path and query
        Add("https://atomi.cloud/test?query=true", new Uri("https://atomi.cloud/test?query=true"));
        // with path, query and fragment
        Add(
            "https://atomi.cloud/test?query=true#fragment",
            new Uri("https://atomi.cloud/test?query=true#fragment")
        );
        // with path, query, fragment and port
        Add(
            "https://atomi.cloud:8080/test?query=true#fragment",
            new Uri("https://atomi.cloud:8080/test?query=true#fragment")
        );
        // with path, query, fragment and port
        Add(
            "https://atomi.cloud:8080/test?query=true#fragment",
            new Uri("https://atomi.cloud:8080/test?query=true#fragment")
        );
    }
}

public class StringToBoolData : TheoryData<string, bool>
{
    public StringToBoolData()
    {
        Add("true", true);
        Add("false", false);
    }
}

public class StringToIntData : TheoryData<string, int>
{
    public StringToIntData()
    {
        Add("1", 1);
        Add("-1", -1);
        Add("0", 0);
    }
}

public class StringToDoubleData : TheoryData<string, double>
{
    public StringToDoubleData()
    {
        Add("1.1", 1.1);
        Add("-1.1", -1.1);
        Add("0.0", 0.0);
    }
}

public class StringToStringData : TheoryData<string, string>
{
    public StringToStringData()
    {
        Add("\"string\"", "string");
        Add("\"string with \\\" quote\"", "string with \" quote");
        Add("\"string with \\\\ backslash\"", "string with \\ backslash");
        Add("\"string with \\n new line\"", "string with \n new line");
        Add("\"string with \\r carriage return\"", "string with \r carriage return");
        Add("\"string with \\t tab\"", "string with \t tab");
        Add("\"string with \\u0020 space\"", "string with \u0020 space");
    }
}

public class StringToFloatData : TheoryData<string, float>
{
    public StringToFloatData()
    {
        Add("1.1", 1.1f);
        Add("-1.1", -1.1f);
        Add("0.0", 0.0f);
    }
}

public class StringToDecimalData : TheoryData<string, decimal>
{
    public StringToDecimalData()
    {
        Add("1.1", 1.1m);
        Add("-1.1", -1.1m);
        Add("0.0", 0.0m);
    }
}

public class StringToLongData : TheoryData<string, long>
{
    public StringToLongData()
    {
        Add("1", 1L);
        Add("-1", -1L);
        Add("0", 0L);
    }
}

public class StringToShortData : TheoryData<string, short>
{
    public StringToShortData()
    {
        Add("1", 1);
        Add("-1", -1);
        Add("0", 0);
    }
}

public class StringToByteData : TheoryData<string, byte>
{
    public StringToByteData()
    {
        Add("1", (byte)1);
        Add("0", (byte)0);
    }
}

public class StringToSByteData : TheoryData<string, sbyte>
{
    public StringToSByteData()
    {
        Add("1", 1);
        Add("-1", -1);
        Add("0", 0);
    }
}

public class StringToCharData : TheoryData<string, char>
{
    public StringToCharData()
    {
        Add("\"a\"", 'a');
        Add("\"A\"", 'A');
        Add("\"1\"", '1');
    }
}

public class StringToDateTimeData : TheoryData<string, DateTime>
{
    public StringToDateTimeData()
    {
        Add("\"2000-01-01T00:00:00\"", new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc));
        Add("\"2000-01-01T00:00:00.000\"", new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc));
        Add("\"2000-01-01T00:00:00.001\"", new DateTime(2000, 1, 1, 0, 0, 0, 1, DateTimeKind.Utc));
        Add("\"2000-01-01T00:00:00.010\"", new DateTime(2000, 1, 1, 0, 0, 0, 10, DateTimeKind.Utc));
        Add(
            "\"2000-01-01T00:00:00.100\"",
            new DateTime(2000, 1, 1, 0, 0, 0, 100, DateTimeKind.Utc)
        );
        Add(
            "\"2000-01-01T00:00:00.200\"",
            new DateTime(2000, 1, 1, 0, 0, 0, 200, DateTimeKind.Utc)
        );
        Add(
            "\"2000-01-01T00:00:00.300\"",
            new DateTime(2000, 1, 1, 0, 0, 0, 300, DateTimeKind.Utc)
        );
        Add(
            "\"2000-01-01T00:00:00.400\"",
            new DateTime(2000, 1, 1, 0, 0, 0, 400, DateTimeKind.Utc)
        );
        Add(
            "\"2000-01-01T00:00:00.500\"",
            new DateTime(2000, 1, 1, 0, 0, 0, 500, DateTimeKind.Utc)
        );
        Add(
            "\"2000-01-01T00:00:00.600\"",
            new DateTime(2000, 1, 1, 0, 0, 0, 600, DateTimeKind.Utc)
        );
        Add(
            "\"2000-01-01T00:00:00.700\"",
            new DateTime(2000, 1, 1, 0, 0, 0, 700, DateTimeKind.Utc)
        );
        Add(
            "\"2000-01-01T00:00:00.800\"",
            new DateTime(2000, 1, 1, 0, 0, 0, 800, DateTimeKind.Utc)
        );
        Add(
            "\"2000-01-01T00:00:00.900\"",
            new DateTime(2000, 1, 1, 0, 0, 0, 900, DateTimeKind.Utc)
        );
        Add("\"2000-01-01T00:00:01.000\"", new DateTime(2000, 1, 1, 0, 0, 1, DateTimeKind.Utc));
        Add("\"2000-01-01T00:00:01.001\"", new DateTime(2000, 1, 1, 0, 0, 1, 1, DateTimeKind.Utc));
        Add("\"2000-01-01T00:00:01.010\"", new DateTime(2000, 1, 1, 0, 0, 1, 10, DateTimeKind.Utc));
        Add(
            "\"2000-01-01T00:00:01.100\"",
            new DateTime(2000, 1, 1, 0, 0, 1, 100, DateTimeKind.Utc)
        );
        Add(
            "\"2000-01-01T00:00:01.200\"",
            new DateTime(2000, 1, 1, 0, 0, 1, 200, DateTimeKind.Utc)
        );
        Add(
            "\"2000-01-01T00:00:01.300\"",
            new DateTime(2000, 1, 1, 0, 0, 1, 300, DateTimeKind.Utc)
        );
        Add(
            "\"2000-01-01T00:00:01.400\"",
            new DateTime(2000, 1, 1, 0, 0, 1, 400, DateTimeKind.Utc)
        );
        Add(
            "\"2000-01-01T00:00:01.500\"",
            new DateTime(2000, 1, 1, 0, 0, 1, 500, DateTimeKind.Utc)
        );
        Add(
            "\"2000-01-01T00:00:01.600\"",
            new DateTime(2000, 1, 1, 0, 0, 1, 600, DateTimeKind.Utc)
        );
        Add(
            "\"2000-01-01T00:00:01.700\"",
            new DateTime(2000, 1, 1, 0, 0, 1, 700, DateTimeKind.Utc)
        );
        Add(
            "\"2000-01-01T00:00:01.800\"",
            new DateTime(2000, 1, 1, 0, 0, 1, 800, DateTimeKind.Utc)
        );
        Add(
            "\"2000-01-01T00:00:01.900\"",
            new DateTime(2000, 1, 1, 0, 0, 1, 900, DateTimeKind.Utc)
        );
        Add("\"2000-01-01T00:00:02.000\"", new DateTime(2000, 1, 1, 0, 0, 2, DateTimeKind.Utc));
    }
}

public class StringToUintData : TheoryData<string, uint>
{
    public StringToUintData()
    {
        Add("1", 1u);
        Add("0", 0u);
    }
}

public class StringToULongData : TheoryData<string, ulong>
{
    public StringToULongData()
    {
        Add("1", 1ul);
        Add("0", 0ul);
    }
}

public class StringToUShortData : TheoryData<string, ushort>
{
    public StringToUShortData()
    {
        Add("1", 1);
        Add("0", 0);
    }
}

public class StringToListData : TheoryData<string, List<object>>
{
    public StringToListData()
    {
        Add("[]", []);
    }
}

public class StringToArrayData : TheoryData<string, object[]>
{
    public StringToArrayData()
    {
        Add("[]", []);
    }
}

public class StringToStringListData : TheoryData<string, List<string>>
{
    public StringToStringListData()
    {
        Add("[]", []);
        Add("[\"string\", \"string2\"]", ["string", "string2"]);
        // multiple values
        Add("[\"string\", \"string2\", \"string3\"]", ["string", "string2", "string3"]);
    }
}

public class StringToIntListData : TheoryData<string, List<int>>
{
    public StringToIntListData()
    {
        Add("[]", []);
        Add("[1, 2, 3]", [1, 2, 3]);
        // multiple values
        Add("[1, 2, 3, 4]", [1, 2, 3, 4]);
    }
}

public class StringToDictionaryObjectData : TheoryData<string, Dictionary<string, object>>
{
    public StringToDictionaryObjectData()
    {
        Add("{}", []);
    }
}

public class StringToDictionaryStringData : TheoryData<string, Dictionary<string, string>>
{
    public StringToDictionaryStringData()
    {
        Add("{}", []);
        Add("{\"key\": \"value\"}", new Dictionary<string, string> { { "key", "value" } });
        // multiple values
        Add(
            "{\"key\": \"value\", \"key2\": \"value2\"}",
            new Dictionary<string, string> { { "key", "value" }, { "key2", "value2" } }
        );
    }
}

public class StringToDictionaryIntData : TheoryData<string, Dictionary<string, int>>
{
    public StringToDictionaryIntData()
    {
        Add("{}", []);
        Add("{\"key\": 1}", new Dictionary<string, int> { { "key", 1 } });
        Add(
            "{\"key\": 1, \"key2\": 2}",
            new Dictionary<string, int> { { "key", 1 }, { "key2", 2 } }
        );
        // multiple values
        Add(
            "{\"key\": 1, \"key2\": 2, \"key3\": 3}",
            new Dictionary<string, int>
            {
                { "key", 1 },
                { "key2", 2 },
                { "key3", 3 },
            }
        );
    }
}

public class AnyToJsonStringData : TheoryData<object, string>
{
    public AnyToJsonStringData()
    {
        Add(new object(), "{}");
        Add(new { key = "value" }, """{"key":"value"}""");
        Add(new { key = 1 }, """{"key":1}""");
        Add(new { key = true }, """{"key":true}""");
        Add(new { key = new { key = "value" } }, """{"key":{"key":"value"}}""");
        Add(new { key = new List<object> { "value", 1, true } }, """{"key":["value",1,true]}""");
        Add(
            new { key = new Dictionary<string, object> { { "key", "value" } } },
            """{"key":{"key":"value"}}"""
        );
        Add(new { key = new object[] { "value", 1, true } }, """{"key":["value",1,true]}""");
        // date
        Add(
            new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            """
            "2000-01-01T00:00:00Z"
            """
        );
        Add(
            new DateTime(2000, 1, 1, 0, 0, 0, 1, DateTimeKind.Utc),
            """
            "2000-01-01T00:00:00.001Z"
            """
        );
        // primitives
        Add(
            "hello world",
            """
            "hello world"
            """
        );
        Add(1, "1");
        Add(true, "true");
        Add(false, "false");
        Add(1L, "1");
        Add(1.1, "1.1");
        Add(-1.1, "-1.1");
        Add(0.0, "0");
        Add((sbyte)1, "1");
        Add(
            'a',
            """
            "a"
            """
        );
        // enumerable, list, arrays and collections
        Add(new List<object> { "value", 1, true }, """["value",1,true]""");
        Add(new List<string> { "value", "value2" }, """["value","value2"]""");
        Add(new List<int> { 1, 2, 3 }, @"[1,2,3]");
        Add(new Dictionary<string, object> { { "key", "value" } }, """{"key":"value"}""");
        Add(new Dictionary<string, string> { { "key", "value" } }, """{"key":"value"}""");
        Add(new Dictionary<string, int> { { "key", 1 } }, """{"key":1}""");
        Add(new object[] { "value", 1, true }, """["value",1,true]""");
        Add(new object[] { "value", "value2" }, """["value","value2"]""");
        Add(new object[] { 1, 2, 3 }, "[1,2,3]");
    }
}

public class UtilityTest
{
    [Fact]
    public void Standard_Date_And_Time_Format()
    {
        Utility.StandardDateFormat.Should().Be("yyyy-MM-dd");
        Utility.StandardTimeFormat.Should().Be("HH:mm:ss");
    }

    [Theory]
    [ClassData(typeof(StringToDateOnlyData))]
    public void ToDate_Should_Convert_String_To_DateOnly(string input, DateOnly expected)
    {
        input.ToDate().Should().Be(expected);
    }

    [Theory]
    [ClassData(typeof(StringToTimeOnlyData))]
    public void ToTime_Should_Convert_String_To_TimeOnly(string input, TimeOnly expected)
    {
        input.ToTime().Should().Be(expected);
    }

    [Theory]
    [ClassData(typeof(DateOnlyToStringData))]
    public void ToStandardDateFormat_Should_Convert_DateOnly_To_Standard_Date_Format(
        DateOnly input,
        string expected
    )
    {
        input.ToStandardDateFormat().Should().Be(expected);
    }

    [Theory]
    [ClassData(typeof(TimeOnlyToStringData))]
    public void ToStandardTimeFormat_Should_Convert_TimeOnly_To_Standard_Time_Format(
        TimeOnly input,
        string expected
    )
    {
        input.ToStandardTimeFormat().Should().Be(expected);
    }

    [Theory]
    [ClassData(typeof(StringToUriData))]
    public void ToUri_Should_Convert_String_To_Uri(string input, Uri expected)
    {
        input.ToUri().Should().Be(expected);
    }

    [Theory]
    [ClassData(typeof(StringToBoolData))]
    public void ToObj_Should_Convert_String_To_Bool(string input, bool expected)
    {
        var obj = input.ToObj<bool>();
        obj.Should().Be(expected);
    }

    [Theory]
    [ClassData(typeof(StringToIntData))]
    public void ToObj_Should_Convert_String_To_Int(string input, int expected)
    {
        var obj = input.ToObj<int>();
        obj.Should().Be(expected);
    }

    [Theory]
    [ClassData(typeof(StringToDoubleData))]
    public void ToObj_Should_Convert_String_To_Double(string input, double expected)
    {
        var obj = input.ToObj<double>();
        obj.Should().Be(expected);
    }

    [Theory]
    [ClassData(typeof(StringToStringData))]
    public void ToObj_Should_Convert_String_To_String(string input, string expected)
    {
        var obj = input.ToObj<string>();
        obj.Should().Be(expected);
    }

    [Theory]
    [ClassData(typeof(StringToFloatData))]
    public void ToObj_Should_Convert_String_To_Float(string input, float expected)
    {
        var obj = input.ToObj<float>();
        obj.Should().Be(expected);
    }

    [Theory]
    [ClassData(typeof(StringToDecimalData))]
    public void ToObj_Should_Convert_String_To_Decimal(string input, decimal expected)
    {
        var obj = input.ToObj<decimal>();
        obj.Should().Be(expected);
    }

    [Theory]
    [ClassData(typeof(StringToLongData))]
    public void ToObj_Should_Convert_String_To_Long(string input, long expected)
    {
        var obj = input.ToObj<long>();
        obj.Should().Be(expected);
    }

    [Theory]
    [ClassData(typeof(StringToShortData))]
    public void ToObj_Should_Convert_String_To_Short(string input, short expected)
    {
        var obj = input.ToObj<short>();
        obj.Should().Be(expected);
    }

    [Theory]
    [ClassData(typeof(StringToByteData))]
    public void ToObj_Should_Convert_String_To_Byte(string input, byte expected)
    {
        var obj = input.ToObj<byte>();
        obj.Should().Be(expected);
    }

    [Theory]
    [ClassData(typeof(StringToSByteData))]
    public void ToObj_Should_Convert_String_To_SByte(string input, sbyte expected)
    {
        var obj = input.ToObj<sbyte>();
        obj.Should().Be(expected);
    }

    [Theory]
    [ClassData(typeof(StringToCharData))]
    public void ToObj_Should_Convert_String_To_Char(string input, char expected)
    {
        var obj = input.ToObj<char>();
        obj.Should().Be(expected);
    }

    [Theory]
    [ClassData(typeof(StringToDateTimeData))]
    public void ToObj_Should_Convert_String_To_DateTime(string input, DateTime expected)
    {
        var obj = input.ToObj<DateTime>();
        obj.Should().Be(expected);
    }

    [Theory]
    [ClassData(typeof(StringToUintData))]
    public void ToObj_Should_Convert_String_To_Uint(string input, uint expected)
    {
        var obj = input.ToObj<uint>();
        obj.Should().Be(expected);
    }

    [Theory]
    [ClassData(typeof(StringToULongData))]
    public void ToObj_Should_Convert_String_To_ULong(string input, ulong expected)
    {
        var obj = input.ToObj<ulong>();
        obj.Should().Be(expected);
    }

    [Theory]
    [ClassData(typeof(StringToUShortData))]
    public void ToObj_Should_Convert_String_To_UShort(string input, ushort expected)
    {
        var obj = input.ToObj<ushort>();
        obj.Should().Be(expected);
    }

    [Theory]
    [ClassData(typeof(StringToListData))]
    public void ToObj_Should_Convert_String_To_List(string input, List<object> expected)
    {
        var obj = input.ToObj<List<object>>();
        obj.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [ClassData(typeof(StringToArrayData))]
    public void ToObj_Should_Convert_String_To_Array(string input, object[] expected)
    {
        var obj = input.ToObj<object[]>();
        obj.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [ClassData(typeof(StringToStringListData))]
    public void ToObj_Should_Convert_String_To_StringList(string input, List<string> expected)
    {
        var obj = input.ToObj<List<string>>();
        obj.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [ClassData(typeof(StringToIntListData))]
    public void ToObj_Should_Convert_String_To_IntList(string input, List<int> expected)
    {
        var obj = input.ToObj<List<int>>();
        obj.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [ClassData(typeof(StringToDictionaryObjectData))]
    public void ToObj_Should_Convert_String_To_Dictionary(
        string input,
        Dictionary<string, object> expected
    )
    {
        var obj = input.ToObj<Dictionary<string, object>>();
        obj.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [ClassData(typeof(StringToDictionaryStringData))]
    public void ToObj_Should_Convert_String_To_Dictionary_String(
        string input,
        Dictionary<string, string> expected
    )
    {
        var obj = input.ToObj<Dictionary<string, string>>();
        obj.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [ClassData(typeof(StringToDictionaryIntData))]
    public void ToObj_Should_Convert_String_To_Dictionary_Int(
        string input,
        Dictionary<string, int> expected
    )
    {
        var obj = input.ToObj<Dictionary<string, int>>();
        obj.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [ClassData(typeof(AnyToJsonStringData))]
    public void ToJson_Should_Convert_Any_To_Json_String(object input, string expected)
    {
        var obj = input.ToJson();
        obj.Should().Be(expected);
    }
}
