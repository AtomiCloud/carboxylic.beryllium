using System.Dynamic;
using CarboxylicBeryllium;
using FluentAssertions;

namespace UnitTest;

public class UtilityTest
{
    [Fact]
    public void Standard_Date_And_Time_Format()
    {
        Utility.StandardDateFormat.Should().Be("yyyy-MM-dd");
        Utility.StandardTimeFormat.Should().Be("HH:mm:ss");
    }

    public static IEnumerable<object[]> ConvertStringToDateOnlyData =>
        [
            ["2100-01-01", new DateOnly(2100, 1, 1)],
            ["2120-01-02", new DateOnly(2120, 1, 2)],
            ["2036-01-03", new DateOnly(2036, 1, 3)],
            ["2022-11-04", new DateOnly(2022, 11, 4)],
            ["2022-01-05", new DateOnly(2022, 1, 5)],
            ["2022-03-06", new DateOnly(2022, 3, 6)],
            ["2024-01-07", new DateOnly(2024, 1, 7)],
            ["2024-12-08", new DateOnly(2024, 12, 8)],
            ["2024-01-09", new DateOnly(2024, 1, 9)],
            ["2024-07-10", new DateOnly(2024, 7, 10)],
            ["2025-01-11", new DateOnly(2025, 1, 11)],
            ["2025-01-12", new DateOnly(2025, 1, 12)],
            ["2025-01-13", new DateOnly(2025, 1, 13)],
            ["2025-01-14", new DateOnly(2025, 1, 14)],
            ["2025-01-15", new DateOnly(2025, 1, 15)],
            ["2025-01-16", new DateOnly(2025, 1, 16)],
            ["1997-01-17", new DateOnly(1997, 1, 17)],
            ["1200-03-28", new DateOnly(1200, 03, 28)],
            ["3200-03-29", new DateOnly(3200, 03, 29)],
            ["1200-03-30", new DateOnly(1200, 03, 30)],
            ["1200-04-01", new DateOnly(1200, 04, 01)],
            ["1347-04-02", new DateOnly(1347, 04, 02)],
        ];

    [Theory]
    [MemberData(nameof(ConvertStringToDateOnlyData))]
    public void ToDate_Should_Convert_String_To_DateOnly(string input, DateOnly expected)
    {
        input.ToDate().Should().Be(expected);
    }

    public static IEnumerable<object[]> ConvertStringToTimeOnlyData =>
        [
            ["00:00:00", new TimeOnly(0, 0, 0)],
            ["01:02:03", new TimeOnly(1, 2, 3)],
            ["04:05:06", new TimeOnly(4, 5, 6)],
            ["07:08:09", new TimeOnly(7, 8, 9)],
            ["10:11:12", new TimeOnly(10, 11, 12)],
            ["13:14:15", new TimeOnly(13, 14, 15)],
            ["16:17:18", new TimeOnly(16, 17, 18)],
            ["19:20:21", new TimeOnly(19, 20, 21)],
            ["22:23:24", new TimeOnly(22, 23, 24)],
            ["23:59:59", new TimeOnly(23, 59, 59)],
            ["00:00:01", new TimeOnly(0, 0, 1)],
            ["00:00:02", new TimeOnly(0, 0, 2)],
            ["00:00:03", new TimeOnly(0, 0, 3)],
            ["00:00:04", new TimeOnly(0, 0, 4)],
            ["00:00:05", new TimeOnly(0, 0, 5)],
            ["00:00:06", new TimeOnly(0, 0, 6)],
            ["00:00:07", new TimeOnly(0, 0, 7)],
            ["00:00:08", new TimeOnly(0, 0, 8)],
            ["00:00:09", new TimeOnly(0, 0, 9)],
            ["00:00:10", new TimeOnly(0, 0, 10)],
            ["00:00:11", new TimeOnly(0, 0, 11)],
            ["00:00:12", new TimeOnly(0, 0, 12)],
            ["00:00:13", new TimeOnly(0, 0, 13)],
            ["00:00:14", new TimeOnly(0, 0, 14)],
            ["00:00:15", new TimeOnly(0, 0, 15)],
            ["00:00:50", new TimeOnly(0, 0, 50)],
            ["00:00:51", new TimeOnly(0, 0, 51)],
            ["00:00:52", new TimeOnly(0, 0, 52)],
            ["00:00:53", new TimeOnly(0, 0, 53)],
            ["00:00:54", new TimeOnly(0, 0, 54)],
            ["00:00:55", new TimeOnly(0, 0, 55)],
            ["00:00:56", new TimeOnly(0, 0, 56)],
            ["00:00:57", new TimeOnly(0, 0, 57)],
            ["00:00:58", new TimeOnly(0, 0, 58)],
            ["00:00:59", new TimeOnly(0, 0, 59)],
            ["00:01:00", new TimeOnly(0, 1, 0)],
        ];

    [Theory]
    [MemberData(nameof(ConvertStringToTimeOnlyData))]
    public void ToTime_Should_Convert_String_To_TimeOnly(string input, TimeOnly expected)
    {
        input.ToTime().Should().Be(expected);
    }

    public static IEnumerable<object[]> ConvertDateOnlyToString =>
        [
            [new DateOnly(2100, 1, 1), "2100-01-01"],
            [new DateOnly(2120, 1, 2), "2120-01-02"],
            [new DateOnly(2036, 1, 3), "2036-01-03"],
            [new DateOnly(2022, 11, 4), "2022-11-04"],
            [new DateOnly(2022, 1, 5), "2022-01-05"],
            [new DateOnly(2022, 3, 6), "2022-03-06"],
            [new DateOnly(2024, 1, 7), "2024-01-07"],
            [new DateOnly(2024, 12, 8), "2024-12-08"],
            [new DateOnly(2024, 1, 9), "2024-01-09"],
            [new DateOnly(2024, 7, 10), "2024-07-10"],
            [new DateOnly(2025, 1, 11), "2025-01-11"],
            [new DateOnly(2025, 1, 12), "2025-01-12"],
            [new DateOnly(2025, 1, 13), "2025-01-13"],
            [new DateOnly(2025, 1, 14), "2025-01-14"],
            [new DateOnly(2025, 1, 15), "2025-01-15"],
            [new DateOnly(2025, 1, 16), "2025-01-16"],
            [new DateOnly(1997, 1, 17), "1997-01-17"],
            [new DateOnly(1200, 03, 28), "1200-03-28"],
            [new DateOnly(3200, 03, 29), "3200-03-29"],
            [new DateOnly(1200, 03, 30), "1200-03-30"],
            [new DateOnly(1200, 04, 01), "1200-04-01"],
            [new DateOnly(1347, 04, 02), "1347-04-02"],
        ];

    [Theory]
    [MemberData(nameof(ConvertDateOnlyToString))]
    public void ToStandardDateFormat_Should_Convert_DateOnly_To_Standard_Date_Format(
        DateOnly input,
        string expected
    )
    {
        input.ToStandardDateFormat().Should().Be(expected);
    }

    public static IEnumerable<object[]> ConvertTimeOnlyToString =>
        [
            [new TimeOnly(0, 0, 0), "00:00:00"],
            [new TimeOnly(1, 2, 3), "01:02:03"],
            [new TimeOnly(4, 5, 6), "04:05:06"],
            [new TimeOnly(7, 8, 9), "07:08:09"],
            [new TimeOnly(10, 11, 12), "10:11:12"],
            [new TimeOnly(13, 14, 15), "13:14:15"],
            [new TimeOnly(16, 17, 18), "16:17:18"],
            [new TimeOnly(19, 20, 21), "19:20:21"],
            [new TimeOnly(22, 23, 24), "22:23:24"],
            [new TimeOnly(23, 59, 59), "23:59:59"],
            [new TimeOnly(0, 0, 1), "00:00:01"],
            [new TimeOnly(0, 0, 2), "00:00:02"],
            [new TimeOnly(0, 0, 3), "00:00:03"],
            [new TimeOnly(0, 0, 4), "00:00:04"],
            [new TimeOnly(0, 0, 5), "00:00:05"],
            [new TimeOnly(0, 0, 6), "00:00:06"],
            [new TimeOnly(0, 0, 7), "00:00:07"],
            [new TimeOnly(0, 0, 8), "00:00:08"],
            [new TimeOnly(0, 0, 9), "00:00:09"],
            [new TimeOnly(0, 0, 10), "00:00:10"],
            [new TimeOnly(0, 0, 11), "00:00:11"],
            [new TimeOnly(0, 0, 12), "00:00:12"],
            [new TimeOnly(0, 0, 13), "00:00:13"],
            [new TimeOnly(0, 0, 14), "00:00:14"],
            [new TimeOnly(0, 0, 15), "00:00:15"],
            [new TimeOnly(0, 0, 50), "00:00:50"],
            [new TimeOnly(0, 0, 51), "00:00:51"],
            [new TimeOnly(0, 0, 52), "00:00:52"],
            [new TimeOnly(0, 0, 53), "00:00:53"],
            [new TimeOnly(0, 0, 54), "00:00:54"],
            [new TimeOnly(0, 0, 55), "00:00:55"],
            [new TimeOnly(0, 0, 56), "00:00:56"],
            [new TimeOnly(0, 0, 57), "00:00:57"],
            [new TimeOnly(0, 0, 58), "00:00:58"],
            [new TimeOnly(0, 0, 59), "00:00:59"],
            [new TimeOnly(0, 1, 0), "00:01:00"],
        ];

    [Theory]
    [MemberData(nameof(ConvertTimeOnlyToString))]
    public void ToStandardTimeFormat_Should_Convert_TimeOnly_To_Standard_Time_Format(
        TimeOnly input,
        string expected
    )
    {
        input.ToStandardTimeFormat().Should().Be(expected);
    }

    public static IEnumerable<object[]> ConvertStringToUriData =>
        [
            // base host
            ["https://atomi.cloud", new Uri("https://atomi.cloud")],
            // with port
            ["https://atomi.cloud:8080", new Uri("https://atomi.cloud:8080")],
            // with ip
            ["https://127.0.0.1", new Uri("https://127.0.0.1")],
            // with ip and port
            ["https://127.0.0.1:8080", new Uri("https://127.0.0.1:8080")],
            // with path
            ["https://atomi.cloud/test", new Uri("https://atomi.cloud/test")],
            // with query only
            ["https://atomi.cloud?query=true", new Uri("https://atomi.cloud?query=true")],
            // with fragment only
            ["https://atomi.cloud#fragment", new Uri("https://atomi.cloud#fragment")],
            // with path and fragment
            ["https://atomi.cloud/test#fragment", new Uri("https://atomi.cloud/test#fragment")],
            // with path and query
            [
                "https://atomi.cloud/test?query=true",
                new Uri("https://atomi.cloud/test?query=true"),
            ],
            // with path, query and fragment
            [
                "https://atomi.cloud/test?query=true#fragment",
                new Uri("https://atomi.cloud/test?query=true#fragment"),
            ],
            // with path, query, fragment and port
            [
                "https://atomi.cloud:8080/test?query=true#fragment",
                new Uri("https://atomi.cloud:8080/test?query=true#fragment"),
            ],
            // with path, query, fragment and port
            [
                "https://atomi.cloud:8080/test?query=true#fragment",
                new Uri("https://atomi.cloud:8080/test?query=true#fragment"),
            ],
        ];

    [Theory]
    [MemberData(nameof(ConvertStringToUriData))]
    public void ToUri_Should_Convert_String_To_Uri(string input, Uri expected)
    {
        input.ToUri().Should().Be(expected);
    }

    public static IEnumerable<object[]> ConvertStringToBool =>
        [
            ["true", true],
            ["false", false],
        ];

    [Theory]
    [MemberData(nameof(ConvertStringToBool))]
    public void ToObj_Should_Convert_String_To_Bool(string input, bool expected)
    {
        var obj = input.ToObj<bool>();
        obj.Should().Be(expected);
    }

    public static IEnumerable<object[]> ConvertStringToInt =>
        [
            ["1", 1],
            ["-1", -1],
            ["0", 0],
        ];

    [Theory]
    [MemberData(nameof(ConvertStringToInt))]
    public void ToObj_Should_Convert_String_To_Int(string input, int expected)
    {
        var obj = input.ToObj<int>();
        obj.Should().Be(expected);
    }

    public static IEnumerable<object[]> ConvertStringToDouble =>
        [
            ["1.1", 1.1],
            ["-1.1", -1.1],
            ["0.0", 0.0],
        ];

    [Theory]
    [MemberData(nameof(ConvertStringToDouble))]
    public void ToObj_Should_Convert_String_To_Double(string input, double expected)
    {
        var obj = input.ToObj<double>();
        obj.Should().Be(expected);
    }

    public static IEnumerable<object[]> ConvertStringToString =>
        [
            ["\"string\"", "string"],
            ["\"string with \\\" quote\"", "string with \" quote"],
            ["\"string with \\\\ backslash\"", "string with \\ backslash"],
            ["\"string with \\n new line\"", "string with \n new line"],
            ["\"string with \\r carriage return\"", "string with \r carriage return"],
            ["\"string with \\t tab\"", "string with \t tab"],
            ["\"string with \\u0020 space\"", "string with \u0020 space"],
        ];

    [Theory]
    [MemberData(nameof(ConvertStringToString))]
    public void ToObj_Should_Convert_String_To_String(string input, string expected)
    {
        var obj = input.ToObj<string>();
        obj.Should().Be(expected);
    }

    public static IEnumerable<object[]> ConvertStringToFloat =>
        [
            ["1.1", 1.1f],
            ["-1.1", -1.1f],
            ["0.0", 0.0f],
        ];

    [Theory]
    [MemberData(nameof(ConvertStringToFloat))]
    public void ToObj_Should_Convert_String_To_Float(string input, float expected)
    {
        var obj = input.ToObj<float>();
        obj.Should().Be(expected);
    }

    public static IEnumerable<object[]> ConvertStringToDecimal =>
        [
            ["1.1", 1.1m],
            ["-1.1", -1.1m],
            ["0.0", 0.0m],
        ];

    [Theory]
    [MemberData(nameof(ConvertStringToDecimal))]
    public void ToObj_Should_Convert_String_To_Decimal(string input, decimal expected)
    {
        var obj = input.ToObj<decimal>();
        obj.Should().Be(expected);
    }

    public static IEnumerable<object[]> ConvertStringToLong =>
        [
            ["1", 1L],
            ["-1", -1L],
            ["0", 0L],
        ];

    [Theory]
    [MemberData(nameof(ConvertStringToLong))]
    public void ToObj_Should_Convert_String_To_Long(string input, long expected)
    {
        var obj = input.ToObj<long>();
        obj.Should().Be(expected);
    }

    public static IEnumerable<object[]> ConvertStringToShort =>
        [
            ["1", 1],
            ["-1", -1],
            ["0", 0],
        ];

    [Theory]
    [MemberData(nameof(ConvertStringToShort))]
    public void ToObj_Should_Convert_String_To_Short(string input, short expected)
    {
        var obj = input.ToObj<short>();
        obj.Should().Be(expected);
    }

    public static IEnumerable<object[]> ConvertStringToByte =>
        [
            ["1", (byte)1],
            ["0", (byte)0],
        ];

    [Theory]
    [MemberData(nameof(ConvertStringToByte))]
    public void ToObj_Should_Convert_String_To_Byte(string input, byte expected)
    {
        var obj = input.ToObj<byte>();
        obj.Should().Be(expected);
    }

    public static IEnumerable<object[]> ConvertStringToSByte =>
        [
            ["1", 1],
            ["-1", -1],
            ["0", 0],
        ];

    [Theory]
    [MemberData(nameof(ConvertStringToSByte))]
    public void ToObj_Should_Convert_String_To_SByte(string input, sbyte expected)
    {
        var obj = input.ToObj<sbyte>();
        obj.Should().Be(expected);
    }

    public static IEnumerable<object[]> ConvertStringToChar =>
        [
            ["\"a\"", 'a'],
            ["\"A\"", 'A'],
            ["\"1\"", '1'],
        ];

    [Theory]
    [MemberData(nameof(ConvertStringToChar))]
    public void ToObj_Should_Convert_String_To_Char(string input, char expected)
    {
        var obj = input.ToObj<char>();
        obj.Should().Be(expected);
    }

    public static IEnumerable<object[]> ConvertStringToDateTime =>
        [
            ["\"2000-01-01T00:00:00\"", new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc)],
            ["\"2000-01-01T00:00:00.000\"", new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc)],
            ["\"2000-01-01T00:00:00.001\"", new DateTime(2000, 1, 1, 0, 0, 0, 1, DateTimeKind.Utc)],
            [
                "\"2000-01-01T00:00:00.010\"",
                new DateTime(2000, 1, 1, 0, 0, 0, 10, DateTimeKind.Utc),
            ],
            [
                "\"2000-01-01T00:00:00.100\"",
                new DateTime(2000, 1, 1, 0, 0, 0, 100, DateTimeKind.Utc),
            ],
            [
                "\"2000-01-01T00:00:00.200\"",
                new DateTime(2000, 1, 1, 0, 0, 0, 200, DateTimeKind.Utc),
            ],
            [
                "\"2000-01-01T00:00:00.300\"",
                new DateTime(2000, 1, 1, 0, 0, 0, 300, DateTimeKind.Utc),
            ],
            [
                "\"2000-01-01T00:00:00.400\"",
                new DateTime(2000, 1, 1, 0, 0, 0, 400, DateTimeKind.Utc),
            ],
            [
                "\"2000-01-01T00:00:00.500\"",
                new DateTime(2000, 1, 1, 0, 0, 0, 500, DateTimeKind.Utc),
            ],
            [
                "\"2000-01-01T00:00:00.600\"",
                new DateTime(2000, 1, 1, 0, 0, 0, 600, DateTimeKind.Utc),
            ],
            [
                "\"2000-01-01T00:00:00.700\"",
                new DateTime(2000, 1, 1, 0, 0, 0, 700, DateTimeKind.Utc),
            ],
            [
                "\"2000-01-01T00:00:00.800\"",
                new DateTime(2000, 1, 1, 0, 0, 0, 800, DateTimeKind.Utc),
            ],
            [
                "\"2000-01-01T00:00:00.900\"",
                new DateTime(2000, 1, 1, 0, 0, 0, 900, DateTimeKind.Utc),
            ],
            ["\"2000-01-01T00:00:01.000\"", new DateTime(2000, 1, 1, 0, 0, 1, DateTimeKind.Utc)],
            ["\"2000-01-01T00:00:01.001\"", new DateTime(2000, 1, 1, 0, 0, 1, 1, DateTimeKind.Utc)],
            [
                "\"2000-01-01T00:00:01.010\"",
                new DateTime(2000, 1, 1, 0, 0, 1, 10, DateTimeKind.Utc),
            ],
            [
                "\"2000-01-01T00:00:01.100\"",
                new DateTime(2000, 1, 1, 0, 0, 1, 100, DateTimeKind.Utc),
            ],
            [
                "\"2000-01-01T00:00:01.200\"",
                new DateTime(2000, 1, 1, 0, 0, 1, 200, DateTimeKind.Utc),
            ],
            [
                "\"2000-01-01T00:00:01.300\"",
                new DateTime(2000, 1, 1, 0, 0, 1, 300, DateTimeKind.Utc),
            ],
            [
                "\"2000-01-01T00:00:01.400\"",
                new DateTime(2000, 1, 1, 0, 0, 1, 400, DateTimeKind.Utc),
            ],
            [
                "\"2000-01-01T00:00:01.500\"",
                new DateTime(2000, 1, 1, 0, 0, 1, 500, DateTimeKind.Utc),
            ],
            [
                "\"2000-01-01T00:00:01.600\"",
                new DateTime(2000, 1, 1, 0, 0, 1, 600, DateTimeKind.Utc),
            ],
            [
                "\"2000-01-01T00:00:01.700\"",
                new DateTime(2000, 1, 1, 0, 0, 1, 700, DateTimeKind.Utc),
            ],
            [
                "\"2000-01-01T00:00:01.800\"",
                new DateTime(2000, 1, 1, 0, 0, 1, 800, DateTimeKind.Utc),
            ],
            [
                "\"2000-01-01T00:00:01.900\"",
                new DateTime(2000, 1, 1, 0, 0, 1, 900, DateTimeKind.Utc),
            ],
            ["\"2000-01-01T00:00:02.000\"", new DateTime(2000, 1, 1, 0, 0, 2, DateTimeKind.Utc)],
        ];

    [Theory]
    [MemberData(nameof(ConvertStringToDateTime))]
    public void ToObj_Should_Convert_String_To_DateTime(string input, DateTime expected)
    {
        var obj = input.ToObj<DateTime>();
        obj.Should().Be(expected);
    }

    public static IEnumerable<object[]> ConvertStringToUint =>
        [
            ["1", 1u],
            ["0", 0u],
        ];

    [Theory]
    [MemberData(nameof(ConvertStringToUint))]
    public void ToObj_Should_Convert_String_To_Uint(string input, uint expected)
    {
        var obj = input.ToObj<uint>();
        obj.Should().Be(expected);
    }

    public static IEnumerable<object[]> ConvertStringToULong =>
        [
            ["1", 1ul],
            ["0", 0ul],
        ];

    [Theory]
    [MemberData(nameof(ConvertStringToULong))]
    public void ToObj_Should_Convert_String_To_ULong(string input, ulong expected)
    {
        var obj = input.ToObj<ulong>();
        obj.Should().Be(expected);
    }

    public static IEnumerable<object[]> ConvertStringToUShort =>
        [
            ["1", 1u],
            ["0", 0u],
        ];

    [Theory]
    [MemberData(nameof(ConvertStringToUShort))]
    public void ToObj_Should_Convert_String_To_UShort(string input, ushort expected)
    {
        var obj = input.ToObj<ushort>();
        obj.Should().Be(expected);
    }

    public static IEnumerable<object[]> ConvertStringToList =>
        [
            ["[]", new List<object>()],
        ];

    [Theory]
    [MemberData(nameof(ConvertStringToList))]
    public void ToObj_Should_Convert_String_To_List(string input, List<object> expected)
    {
        var obj = input.ToObj<List<object>>();
        obj.Should().BeEquivalentTo(expected);
    }

    public static IEnumerable<object[]> ConvertStringToArray =>
        [
            ["[]", new object[] { }],
        ];

    [Theory]
    [MemberData(nameof(ConvertStringToArray))]
    public void ToObj_Should_Convert_String_To_Array(string input, object[] expected)
    {
        var obj = input.ToObj<object[]>();
        obj.Should().BeEquivalentTo(expected);
    }

    public static IEnumerable<object[]> ConvertStringToStringList =>
        [
            ["[]", new List<string>()],
            ["[\"string\", \"string2\"]", new List<string> { "string", "string2" }],
            // multiple values
            [
                "[\"string\", \"string2\", \"string3\"]",
                new List<string> { "string", "string2", "string3" },
            ],
        ];

    [Theory]
    [MemberData(nameof(ConvertStringToStringList))]
    public void ToObj_Should_Convert_String_To_StringList(string input, List<string> expected)
    {
        var obj = input.ToObj<List<string>>();
        obj.Should().BeEquivalentTo(expected);
    }

    public static IEnumerable<object[]> ConvertStringToIntList =>
        [
            ["[]", new List<int>()],
            ["[1, 2, 3]", new List<int> { 1, 2, 3 }],
            // multiple values
            ["[1, 2, 3, 4]", new List<int> { 1, 2, 3, 4 }],
        ];

    [Theory]
    [MemberData(nameof(ConvertStringToIntList))]
    public void ToObj_Should_Convert_String_To_IntList(string input, List<int> expected)
    {
        var obj = input.ToObj<List<int>>();
        obj.Should().BeEquivalentTo(expected);
    }

    public static IEnumerable<object[]> ConvertStringToDictionaryObject =>
        [
            ["{}", new Dictionary<string, object>()],
        ];

    [Theory]
    [MemberData(nameof(ConvertStringToDictionaryObject))]
    public void ToObj_Should_Convert_String_To_Dictionary(
        string input,
        Dictionary<string, object> expected
    )
    {
        var obj = input.ToObj<Dictionary<string, object>>();
        obj.Should().BeEquivalentTo(expected);
    }

    public static IEnumerable<object[]> ConvertStringToDictionaryString =>
        [
            ["{}", new Dictionary<string, string>()],
            ["{\"key\": \"value\"}", new Dictionary<string, string> { { "key", "value" } }],
            // multiple values
            [
                "{\"key\": \"value\", \"key2\": \"value2\"}",
                new Dictionary<string, string> { { "key", "value" }, { "key2", "value2" } },
            ],
        ];

    [Theory]
    [MemberData(nameof(ConvertStringToDictionaryString))]
    public void ToObj_Should_Convert_String_To_Dictionary_String(
        string input,
        Dictionary<string, string> expected
    )
    {
        var obj = input.ToObj<Dictionary<string, string>>();
        obj.Should().BeEquivalentTo(expected);
    }

    public static IEnumerable<object[]> ConvertStringToDictionaryInt =>
        [
            ["{}", new Dictionary<string, int>()],
            ["{\"key\": 1}", new Dictionary<string, int> { { "key", 1 } }],
            [
                "{\"key\": 1, \"key2\": 2}",
                new Dictionary<string, int> { { "key", 1 }, { "key2", 2 } },
            ],
            // multiple values
            [
                "{\"key\": 1, \"key2\": 2, \"key3\": 3}",
                new Dictionary<string, int>
                {
                    { "key", 1 },
                    { "key2", 2 },
                    { "key3", 3 },
                },
            ],
        ];

    [Theory]
    [MemberData(nameof(ConvertStringToDictionaryInt))]
    public void ToObj_Should_Convert_String_To_Dictionary_Int(
        string input,
        Dictionary<string, int> expected
    )
    {
        var obj = input.ToObj<Dictionary<string, int>>();
        obj.Should().BeEquivalentTo(expected);
    }

    public static IEnumerable<object[]> ConvertAnyToJsonString =>
        [
            [new object(), @"{}"],
            [new { key = "value" }, """{"key":"value"}"""],
            [new { key = 1 }, """{"key":1}"""],
            [new { key = true }, """{"key":true}"""],
            [new { key = new { key = "value" } }, """{"key":{"key":"value"}}"""],
            [new { key = new List<object> { "value", 1, true } }, """{"key":["value",1,true]}"""],
            [
                new { key = new Dictionary<string, object> { { "key", "value" } } },
                """{"key":{"key":"value"}}""",
            ],
            [new { key = new object[] { "value", 1, true } }, """{"key":["value",1,true]}"""],
            // date
            [
                new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                """
                    "2000-01-01T00:00:00Z"
                    """,
            ],
            [
                new DateTime(2000, 1, 1, 0, 0, 0, 1, DateTimeKind.Utc),
                """
                    "2000-01-01T00:00:00.001Z"
                    """,
            ],
            // primitives
            [
                "hello world",
                """
                    "hello world"
                    """,
            ],
            [1, "1"],
            [true, "true"],
            [false, "false"],
            [1L, "1"],
            [1.1, "1.1"],
            [-1.1, "-1.1"],
            [0.0, "0"],
            [(sbyte)1, "1"],
            [
                'a',
                """
                    "a"
                    """,
            ],
            // enumerable, list, arrays and collections
            [new List<object> { "value", 1, true }, """["value",1,true]"""],
            [new List<string> { "value", "value2" }, """["value","value2"]"""],
            [new List<int> { 1, 2, 3 }, @"[1,2,3]"],
            [new Dictionary<string, object> { { "key", "value" } }, """{"key":"value"}"""],
            [new Dictionary<string, string> { { "key", "value" } }, """{"key":"value"}"""],
            [new Dictionary<string, int> { { "key", 1 } }, """{"key":1}"""],
            [new object[] { "value", 1, true }, """["value",1,true]"""],
            [new object[] { "value", "value2" }, """["value","value2"]"""],
            [new object[] { 1, 2, 3 }, "[1,2,3]"],
        ];

    [Theory]
    [MemberData(nameof(ConvertAnyToJsonString))]
    public void ToJson_Should_Convert_Any_To_Json_String(object input, string expected)
    {
        var obj = input.ToJson();
        obj.Should().Be(expected);
    }
}
