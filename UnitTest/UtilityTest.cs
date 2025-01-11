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
    public void Convert_String_To_DateOnly(string input, DateOnly expected)
    {
        input.ToDate().Should().Be(expected);
    }
}
