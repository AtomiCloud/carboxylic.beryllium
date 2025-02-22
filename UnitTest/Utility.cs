﻿using System.Text;
using System.Text.Json;
using CarboxylicBeryllium;
using FluentAssertions;

namespace UnitTest;

public class UtilityTests
{
    // ==============
    //  StandardDateFormat & StandardTimeFormat Constants
    // ==============
    [Fact]
    public void StandardDateFormat_should_Be_yyyy_MM_dd()
    {
        Utility.StandardDateFormat.Should().Be("yyyy-MM-dd");
    }

    [Fact]
    public void StandardTimeFormat_should_Be_HH_mm_ss()
    {
        Utility.StandardTimeFormat.Should().Be("HH:mm:ss");
    }

    // ==============
    //     ToDate
    // ==============
    private class ToDate_should_Convert_String_To_DateOnly_Data : TheoryData<string, DateOnly>
    {
        public ToDate_should_Convert_String_To_DateOnly_Data()
        {
            Add("2025-01-30", new DateOnly(2025, 1, 30)); // Today's date
            Add("2024-12-31", new DateOnly(2024, 12, 31)); // End of year
            Add("2025-01-01", new DateOnly(2025, 1, 1)); // Start of year
            Add("2000-02-29", new DateOnly(2000, 2, 29)); // Leap year
            Add("1999-03-01", new DateOnly(1999, 3, 1)); // Past date
            Add("2099-04-05", new DateOnly(2099, 4, 5)); // Future date
        }
    }

    // Utility.ToDate should convert string in standard format to DateOnly
    [Theory]
    [ClassData(typeof(ToDate_should_Convert_String_To_DateOnly_Data))]
    public void ToDate_should_Convert_String_To_DateOnly(string input, DateOnly expected)
    {
        // Arrange is handled by TheoryData

        // Act
        var actual = input.ToDate();

        // Assert
        actual.Should().Be(expected);
    }

    private class ToDate_should_Throw_FormatException_For_InvalidFormat_Data : TheoryData<string>
    {
        public ToDate_should_Throw_FormatException_For_InvalidFormat_Data()
        {
            Add("30-01-2025"); // DD-MM-YYYY format
            Add("2025/01/30"); // YYYY/MM/DD format
            Add("20250130"); // YYYYMMDD format
            Add("invalid-date"); // Invalid date string
        }
    }

    // Utility.ToDate should throw FormatException for invalid date formats
    [Theory]
    [ClassData(typeof(ToDate_should_Throw_FormatException_For_InvalidFormat_Data))]
    public void ToDate_should_Throw_FormatException_For_InvalidFormat(string input)
    {
        // Arrange is handled by TheoryData

        // Act
        Action act = () => input.ToDate();

        // Assert
        act.Should().Throw<FormatException>();
    }

    [Fact]
    public void ToDate_should_Throw_ArgumentNullException_For_NullInput()
    {
        // Arrange
        string input = null!; // Using null! to explicitly allow null for testing

        // Act
        Action act = () => input.ToDate();

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ToDate_should_Throw_FormatException_For_EmptyInput()
    {
        // Arrange
        string input = "";

        // Act
        Action act = () => input.ToDate();

        // Assert
        act.Should().Throw<FormatException>();
    }

    // ==============
    //     ToTime
    // ==============
    private class ToTime_should_Convert_String_To_TimeOnly_Data : TheoryData<string, TimeOnly>
    {
        public ToTime_should_Convert_String_To_TimeOnly_Data()
        {
            Add("10:00:00", new TimeOnly(10, 0, 0)); // Current hour
            Add("23:59:59", new TimeOnly(23, 59, 59)); // End of day
            Add("00:00:00", new TimeOnly(0, 0, 0)); // Start of day
            Add("01:02:03", new TimeOnly(1, 2, 3)); // произвольное время
            Add("12:34:56", new TimeOnly(12, 34, 56)); // Midday time
            Add("18:00:00", new TimeOnly(18, 0, 0)); // Evening time
        }
    }

    // Utility.ToTime should convert string in standard format to TimeOnly
    [Theory]
    [ClassData(typeof(ToTime_should_Convert_String_To_TimeOnly_Data))]
    public void ToTime_should_Convert_String_To_TimeOnly(string input, TimeOnly expected)
    {
        // Arrange is handled by TheoryData

        // Act
        var actual = input.ToTime();

        // Assert
        actual.Should().Be(expected);
    }

    private class ToTime_should_Throw_FormatException_For_InvalidFormat_Data : TheoryData<string>
    {
        public ToTime_should_Throw_FormatException_For_InvalidFormat_Data()
        {
            Add("10:00"); // Missing seconds
            Add("100000"); // No separators
            Add("10-00-00"); // Date separators
            Add("invalid-time"); // Invalid time string
        }
    }

    // Utility.ToTime should throw FormatException for invalid time formats
    [Theory]
    [ClassData(typeof(ToTime_should_Throw_FormatException_For_InvalidFormat_Data))]
    public void ToTime_should_Throw_FormatException_For_InvalidFormat(string input)
    {
        // Arrange is handled by TheoryData

        // Act
        Action act = () => input.ToTime();

        // Assert
        act.Should().Throw<FormatException>();
    }

    [Fact]
    public void ToTime_should_Throw_ArgumentNullException_For_NullInput()
    {
        // Arrange
        string input = null!; // Using null! to explicitly allow null for testing

        // Act
        Action act = () => input.ToTime();

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ToTime_should_Throw_FormatException_For_EmptyInput()
    {
        // Arrange
        string input = "";

        // Act
        Action act = () => input.ToTime();

        // Assert
        act.Should().Throw<FormatException>();
    }

    // ==============
    //  ToStandardDateFormat
    // ==============
    private class ToStandardDateFormat_should_Convert_DateOnly_To_StandardDateFormat_Data
        : TheoryData<DateOnly, string>
    {
        public ToStandardDateFormat_should_Convert_DateOnly_To_StandardDateFormat_Data()
        {
            Add(new DateOnly(2025, 1, 30), "2025-01-30"); // Today's date
            Add(new DateOnly(2024, 12, 31), "2024-12-31"); // End of year
            Add(new DateOnly(2025, 1, 1), "2025-01-01"); // Start of year
            Add(new DateOnly(2000, 2, 29), "2000-02-29"); // Leap year
            Add(new DateOnly(1999, 3, 1), "1999-03-01"); // Past date
            Add(new DateOnly(2099, 4, 5), "2099-04-05"); // Future date
        }
    }

    // Utility.ToStandardDateFormat should convert DateOnly to string in standard format
    [Theory]
    [ClassData(typeof(ToStandardDateFormat_should_Convert_DateOnly_To_StandardDateFormat_Data))]
    public void ToStandardDateFormat_should_Convert_DateOnly_To_StandardDateFormat(
        DateOnly input,
        string expected
    )
    {
        // Arrange is handled by TheoryData

        // Act
        var actual = input.ToStandardDateFormat();

        // Assert
        actual.Should().Be(expected);
    }

    // ==============
    // ToStandardTimeFormat
    // ==============
    private class ToStandardTimeFormat_should_Convert_TimeOnly_To_StandardTimeFormat_Data
        : TheoryData<TimeOnly, string>
    {
        public ToStandardTimeFormat_should_Convert_TimeOnly_To_StandardTimeFormat_Data()
        {
            Add(new TimeOnly(10, 0, 0), "10:00:00"); // Current hour
            Add(new TimeOnly(23, 59, 59), "23:59:59"); // End of day
            Add(new TimeOnly(0, 0, 0), "00:00:00"); // Start of day
            Add(new TimeOnly(1, 2, 3), "01:02:03"); // произвольное время
            Add(new TimeOnly(12, 34, 56), "12:34:56"); // Mid-day time
            Add(new TimeOnly(18, 0, 0), "18:00:00"); // Evening time
        }
    }

    // Utility.ToStandardTimeFormat should convert TimeOnly to string in standard format
    [Theory]
    [ClassData(typeof(ToStandardTimeFormat_should_Convert_TimeOnly_To_StandardTimeFormat_Data))]
    public void ToStandardTimeFormat_should_Convert_TimeOnly_To_StandardTimeFormat(
        TimeOnly input,
        string expected
    )
    {
        // Arrange is handled by TheoryData

        // Act
        var actual = input.ToStandardTimeFormat();

        // Assert
        actual.Should().Be(expected);
    }

    // ==============
    //     ToUri
    // ==============
    private class ToUri_should_Convert_String_To_Uri_Data : TheoryData<string, Uri>
    {
        public ToUri_should_Convert_String_To_Uri_Data()
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
            Add(
                "https://atomi.cloud/test?query=true",
                new Uri("https://atomi.cloud/test?query=true")
            );
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
        }
    }

    // Utility.ToUri should convert string to Uri object
    [Theory]
    [ClassData(typeof(ToUri_should_Convert_String_To_Uri_Data))]
    public void ToUri_should_Convert_String_To_Uri(string input, Uri expected)
    {
        // Arrange is handled by TheoryData

        // Act
        var actual = input.ToUri();

        // Assert
        actual.Should().BeEquivalentTo(expected); // Using BeEquivalentTo for Uri comparison
    }

    [Fact]
    public void ToUri_should_Throw_ArgumentNullException_For_NullInput()
    {
        // Arrange
        string input = null!;

        // Act
        Action act = () => input.ToUri();

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    // ==============
    //     ToJson
    // ==============
    private class ToJson_should_Convert_Any_To_JsonString_Data : TheoryData<object, string>
    {
        public ToJson_should_Convert_Any_To_JsonString_Data()
        {
            Add(new { key = "value" }, """{"key":"value"}""");
            Add(new { key = 1 }, """{"key":1}""");
            Add(new { key = true }, """{"key":true}""");
            Add(
                new { key = new List<object> { "value", 1, true } },
                """{"key":["value",1,true]}"""
            );
            Add(123, "123"); // Integer
            Add("hello", "\"hello\""); // String
            Add(true, "true"); // Boolean
            Add(new DateTime(2025, 01, 30, 10, 0, 0, DateTimeKind.Utc), "\"2025-01-30T10:00:00Z\""); // DateTime
        }
    }

    // Utility.ToJson should convert any object to JSON string
    [Theory]
    [ClassData(typeof(ToJson_should_Convert_Any_To_JsonString_Data))]
    public void ToJson_should_Convert_Any_To_JsonString(object input, string expected)
    {
        // Arrange is handled by TheoryData

        // Act
        var actual = input.ToJson();

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void ToJson_should_Handle_NullInput()
    {
        // Arrange
        object input = null!;

        // Act
        var actual = input.ToJson();

        // Assert
        actual.Should().Be("null"); // Null input should serialize to "null"
    }

    // ==============
    //     ToObj
    // ==============
    private class ToObj_String_should_Convert_JsonString_To_String_Data : TheoryData<string, string>
    {
        public ToObj_String_should_Convert_JsonString_To_String_Data()
        {
            Add("\"string\"", "string");
            Add("\"string with spaces\"", "string with spaces");
            Add("\"\"", ""); // Empty string
        }
    }

    // Utility.ToObj<string> should convert JSON string to string
    [Theory]
    [ClassData(typeof(ToObj_String_should_Convert_JsonString_To_String_Data))]
    public void ToObj_String_should_Convert_JsonString_To_String(string input, string expected)
    {
        // Arrange is handled by TheoryData
        // Act
        var actual = input.ToObj<string>();

        // Assert
        actual.Should().Be(expected);
    }

    private class ToObj_Int_should_Convert_JsonString_To_Int_Data : TheoryData<string, int>
    {
        public ToObj_Int_should_Convert_JsonString_To_Int_Data()
        {
            Add("123", 123);
            Add("0", 0);
            Add("-456", -456);
        }
    }

    // Utility.ToObj<int> should convert JSON string to int
    [Theory]
    [ClassData(typeof(ToObj_Int_should_Convert_JsonString_To_Int_Data))]
    public void ToObj_Int_should_Convert_JsonString_To_Int(string input, int expected)
    {
        // Arrange is handled by TheoryData
        // Act
        var actual = input.ToObj<int>();

        // Assert
        actual.Should().Be(expected);
    }

    private class ToObj_Bool_should_Convert_JsonString_To_Bool_Data : TheoryData<string, bool>
    {
        public ToObj_Bool_should_Convert_JsonString_To_Bool_Data()
        {
            Add("true", true);
            Add("false", false);
        }
    }

    // Utility.ToObj<bool> should convert JSON string to bool
    [Theory]
    [ClassData(typeof(ToObj_Bool_should_Convert_JsonString_To_Bool_Data))]
    public void ToObj_Bool_should_Convert_JsonString_To_Bool(string input, bool expected)
    {
        // Arrange is handled by TheoryData
        // Act
        var actual = input.ToObj<bool>();

        // Assert
        actual.Should().Be(expected);
    }

    private class ToObj_ListString_should_Convert_JsonString_To_ListString_Data
        : TheoryData<string, List<string>>
    {
        public ToObj_ListString_should_Convert_JsonString_To_ListString_Data()
        {
            Add("[\"string1\", \"string2\"]", ["string1", "string2"]);
            Add("[]", []); // Empty list
        }
    }

    // Utility.ToObj<List<string>> should convert JSON string to List<string>
    [Theory]
    [ClassData(typeof(ToObj_ListString_should_Convert_JsonString_To_ListString_Data))]
    public void ToObj_ListString_should_Convert_JsonString_To_ListString(
        string input,
        List<string> expected
    )
    {
        // Arrange is handled by TheoryData
        // Act
        var actual = input.ToObj<List<string>>();

        // Assert
        actual.Should().BeEquivalentTo(expected); // Use BeEquivalentTo for lists
    }

    private class ToObj_DictionaryStringString_should_Convert_JsonString_To_DictionaryStringString_Data
        : TheoryData<string, Dictionary<string, string>>
    {
        public ToObj_DictionaryStringString_should_Convert_JsonString_To_DictionaryStringString_Data()
        {
            Add(
                """{"key1":"value1", "key2":"value2"}""",
                new Dictionary<string, string>() { { "key1", "value1" }, { "key2", "value2" } }
            );
            Add("{}", []);
        }
    }

    // Utility.ToObj<Dictionary<string, string>> should convert JSON string to Dictionary<string, string>
    [Theory]
    [ClassData(
        typeof(ToObj_DictionaryStringString_should_Convert_JsonString_To_DictionaryStringString_Data)
    )]
    public void ToObj_DictionaryStringString_should_Convert_JsonString_To_DictionaryStringString(
        string input,
        Dictionary<string, string> expected
    )
    {
        // Arrange is handled by TheoryData
        // Act
        var actual = input.ToObj<Dictionary<string, string>>();

        // Assert
        actual.Should().BeEquivalentTo(expected); // Use BeEquivalentTo for dictionaries
    }

    [Fact]
    public void ToObj_should_Throw_JsonException_For_InvalidJson()
    {
        // Arrange
        string input = "invalid json";

        // Act
        Action act = () => input.ToObj<object>();

        // Assert
        act.Should().Throw<JsonException>();
    }

    [Fact]
    public void ToObj_should_Throw_ArgumentNullException_For_NullInput()
    {
        // Arrange
        string input = null!;

        // Act
        Action act = () => input.ToObj<object>();

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ToObj_should_Throw_JsonException_For_EmptyInput()
    {
        // Arrange
        string input = "";

        // Act
        Action act = () => input.ToObj<object>();

        // Assert
        act.Should().Throw<JsonException>();
    }

    // ============================================
    //  Base64Encode method tests
    // ============================================

    // Data for Base64Encode method happy path
    private class Base64Encode_Should_ReturnBase64String_Data
        : TheoryData<string, Encoding?, string>
    {
        public Base64Encode_Should_ReturnBase64String_Data()
        {
            // Default encoding (UTF8)
            Add("Hello, World!", null, "SGVsbG8sIFdvcmxkIQ=="); // Basic test
            Add("12345", null, "MTIzNDU="); // Numeric string
            Add("", null, ""); // Empty string
            Add(" ", null, "IA=="); // Single space

            // Custom encoding
            Add("Hello, World!", Encoding.ASCII, "SGVsbG8sIFdvcmxkIQ=="); // ASCII
            Add("世界", Encoding.UTF8, "5LiW55WM"); // UTF8 with non-ASCII chars
        }
    }

    [Theory]
    [ClassData(typeof(Base64Encode_Should_ReturnBase64String_Data))]
    public void Base64Encode_Should_ReturnBase64String(
        string input,
        Encoding? encoding,
        string expected
    )
    {
        // Act
        var result = input.Base64Encode(encoding);

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void Base64Encode_Should_ThrowArgumentNullException_For_NullInput()
    {
        // Arrange
        string input = null!;

        // Act
        Action act = () => input.Base64Encode(Encoding.UTF8);

        // Assert
        act.Should()
            .Throw<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 's')");
    }

    // ============================================
    //    Base64Decode method tests
    // ============================================

    // Data for Base64Decode method happy path
    private class Base64Decode_Should_ReturnPlainString_Data : TheoryData<string, Encoding?, string>
    {
        public Base64Decode_Should_ReturnPlainString_Data()
        {
            // Default encoding (UTF8)
            Add("SGVsbG8sIFdvcmxkIQ==", null, "Hello, World!"); // Basic test
            Add("MTIzNDU=", null, "12345"); // Numeric string
            Add("", null, ""); // Empty string
            Add("IA==", null, " "); // Single space

            // Custom encoding
            Add("SGVsbG8sIFdvcmxkIQ==", Encoding.ASCII, "Hello, World!"); // ASCII
            Add("5Li76ZmG", Encoding.UTF8, "主陆"); // UTF8 with non-ASCII chars
        }
    }

    [Theory]
    [ClassData(typeof(Base64Decode_Should_ReturnPlainString_Data))]
    public void Base64Decode_Should_ReturnPlainString(
        string input,
        Encoding? encoding,
        string expected
    )
    {
        // Act
        var result = input.Base64Decode(encoding);

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void Base64Decode_Should_ThrowFormatException_For_InvalidBase64String()
    {
        // Arrange
        var invalidBase64 = "Invalid Base64 Data";

        // Act
        Action act = () => invalidBase64.Base64Decode(Encoding.UTF8);

        // Assert
        act.Should().Throw<FormatException>();
    }

    private class MockRequest
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
    }

    // ============================================
    // 1) ToStringRequest method tests
    // ============================================

    // Data for ToStringRequest method happy path
    private class ToStringRequest_Should_ReturnStringContent_Data
        : TheoryData<object, StringContent>
    {
        public ToStringRequest_Should_ReturnStringContent_Data()
        {
            Add(
                new MockRequest { Name = "John", Age = 30 },
                new StringContent("""{"Name":"John","Age":30}""", Encoding.UTF8, "application/json")
            );
            Add(
                new MockRequest { Name = string.Empty, Age = 0 },
                new StringContent("""{"Name":"","Age":0}""", Encoding.UTF8, "application/json")
            );
            Add(
                new { SomeProp = 123 },
                new StringContent("""{"SomeProp":123}""", Encoding.UTF8, "application/json")
            );
            Add(
                "Hello, World!",
                new StringContent(
                    """
                    "Hello, World!"
                    """,
                    Encoding.UTF8,
                    "application/json"
                )
            );
            Add(12345, new StringContent("12345", Encoding.UTF8, "application/json"));
        }
    }

    [Theory]
    [ClassData(typeof(ToStringRequest_Should_ReturnStringContent_Data))]
    public void ToStringRequest_Should_ReturnStringContent(object input, StringContent expected)
    {
        // Act
        var actual = input.ToStringRequest();

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    // ============================================
    // 2) ToMessage method tests
    // ============================================

    // Data for ToMessage method happy path
    private class ToMessage_Should_ReturnHttpRequestMessage_Data
        : TheoryData<object, HttpMethod, string, HttpRequestMessage>
    {
        public ToMessage_Should_ReturnHttpRequestMessage_Data()
        {
            // Add(new MockRequest { Name = "Alice", Age = 25 }, HttpMethod.Post, "https://example.com/",
            //     new HttpRequestMessage(HttpMethod.Post,"https://example.com/")
            //     {
            //         Content = new StringContent("""{"Name":"Alice","Age":25}""", Encoding.UTF8, "application/json")
            //     });
            Add(
                new MockRequest { Name = "Bob", Age = 22 },
                HttpMethod.Put,
                "/api/users",
                new HttpRequestMessage(HttpMethod.Put, "/api/users")
                {
                    Content = new StringContent(
                        """{"Name":"Bob","Age":22}""",
                        Encoding.UTF8,
                        "application/json"
                    ),
                }
            );

            Add(
                12345,
                HttpMethod.Delete,
                "https://example.com/delete",
                new HttpRequestMessage(HttpMethod.Delete, "https://example.com/delete")
                {
                    Content = new StringContent("12345", Encoding.UTF8, "application/json"),
                }
            );
        }
    }

    [Theory]
    [ClassData(typeof(ToMessage_Should_ReturnHttpRequestMessage_Data))]
    public void ToMessage_Should_ReturnHttpRequestMessage(
        object input,
        HttpMethod method,
        string endpoint,
        HttpRequestMessage expected
    )
    {
        // Act
        var actual = input.ToMessage(method, endpoint);

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    // ====================
    // G Method (ToGuid)
    // ====================

    private class G_Should_ParseValidGuidStrings_Data : TheoryData<string, Guid>
    {
        public G_Should_ParseValidGuidStrings_Data()
        {
            Add(
                "d2719a91-ea3e-4ec6-aec7-52f0c41cf09a",
                Guid.Parse("d2719a91-ea3e-4ec6-aec7-52f0c41cf09a")
            );
            Add(
                "3f4b3cd4-645f-48b0-b897-c96de3c021b2",
                Guid.Parse("3f4b3cd4-645f-48b0-b897-c96de3c021b2")
            );
            Add(
                "04e233b5-9c17-4514-89c2-c2b0c3e99829",
                Guid.Parse("04e233b5-9c17-4514-89c2-c2b0c3e99829")
            );
        }
    }

    [Theory]
    [ClassData(typeof(G_Should_ParseValidGuidStrings_Data))]
    public void G_Should_ParseValidGuidStrings(string input, Guid expected)
    {
        // Act
        var actual = input.G();

        // Assert
        actual
            .Should()
            .Be(expected, "G should correctly parse valid GUID strings into Guid objects");
    }

    // ==========================
    // Invalid or Empty Strings
    // ==========================

    [Fact]
    public void G_Should_ThrowFormatException_ForInvalidGuidStrings()
    {
        // Arrange
        var input = "Invalid-Guid-String";

        // Act
        Action act = () => input.G();

        // Assert
        act.Should().Throw<FormatException>("Invalid GUID strings should cause a FormatException");
    }

    [Fact]
    public void G_Should_ThrowFormatException_ForEmptyString()
    {
        // Arrange
        var input = "";

        // Act
        Action act = () => input.G();

        // Assert
        act.Should()
            .Throw<FormatException>(
                "Empty strings are not valid GUIDs and should throw a FormatException"
            );
    }
}
