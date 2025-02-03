using CarboxylicBeryllium;
using FluentAssertions;

namespace UnitTest;

public class DictionaryUtilityTests
{
    // ==============
    // 2) ToNullable
    // ==============

    private class ToNullable_Should_HandleVariousDataTypes_Data
        : TheoryData<
            IDictionary<string, int>, // Input dictionary
            Dictionary<string, int?> // Expected dictionary with nullable values
        >
    {
        public ToNullable_Should_HandleVariousDataTypes_Data()
        {
            Add(
                new Dictionary<string, int>
                {
                    { "A", 1 },
                    { "B", 2 },
                    { "C", 3 },
                },
                new Dictionary<string, int?>
                {
                    { "A", 1 },
                    { "B", 2 },
                    { "C", 3 },
                }
            ); // Simple dictionary

            Add(
                new Dictionary<string, int>(), // Empty dictionary
                []
            ); // Empty dictionary test case

            Add(
                new Dictionary<string, int> { { "Key", 100 } },
                new Dictionary<string, int?> { { "Key", 100 } }
            ); // Single item case
        }
    }

    [Theory]
    [ClassData(typeof(ToNullable_Should_HandleVariousDataTypes_Data))]
    public void ToNullable_Should_HandleVariousDataTypes(
        IDictionary<string, int> input,
        Dictionary<string, int?> expected
    )
    {
        // Act
        var actual = input.ToNullable();

        // Assert
        actual
            .Should()
            .BeEquivalentTo(
                expected,
                "ToNullable should convert to nullable values without data loss"
            );
    }

    // ==============
    // 3) GetRef
    // ==============

    private class GetRef_Should_ReturnValueOrNull_Data
        : TheoryData<
            Dictionary<string, string>, // Input dictionary
            string, // Key to retrieve
            string? // Expected value
        >
    {
        public GetRef_Should_ReturnValueOrNull_Data()
        {
            Add(
                new Dictionary<string, string> { { "A", "Apple" }, { "B", "Banana" } },
                "A",
                "Apple"
            ); // Key exists in dictionary

            Add(new Dictionary<string, string> { { "A", "Apple" }, { "B", "Banana" } }, "C", null); // Key does not exist

            Add(
                [], // Empty dictionary
                "A",
                null
            ); // Empty dictionary case
        }
    }

    [Theory]
    [ClassData(typeof(GetRef_Should_ReturnValueOrNull_Data))]
    public void GetRef_Should_ReturnValueOrNull(
        Dictionary<string, string> input,
        string key,
        string? expected
    )
    {
        // Act
        var actual = input.GetRef(key);

        // Assert
        actual.Should().Be(expected, $"GetRef should retrieve '{expected}' for the key '{key}'");
    }

    // ==============
    // 4) GetVal
    // ==============

    private class GetVal_Should_ReturnValueOrDefault_Data
        : TheoryData<
            Dictionary<string, int>, // Input dictionary
            string, // Key to retrieve
            int // Expected value
        >
    {
        public GetVal_Should_ReturnValueOrDefault_Data()
        {
            Add(new Dictionary<string, int> { { "A", 10 }, { "B", 20 } }, "A", 10); // Key exists in dictionary

            Add(new Dictionary<string, int> { { "A", 10 }, { "B", 20 } }, "C", 0); // Key does not exist

            Add(
                [], // Empty dictionary
                "A",
                0
            ); // Empty dictionary case
        }
    }

    [Theory]
    [ClassData(typeof(GetVal_Should_ReturnValueOrDefault_Data))]
    public void GetVal_Should_ReturnValueOrDefault(
        Dictionary<string, int> input,
        string key,
        int expected
    )
    {
        // Act
        var actual = input.GetVal(key);

        // Assert
        actual.Should().Be(expected, $"GetVal should retrieve '{expected}' for the key '{key}'");
    }

    // ================================
    // Struct Edge Case: Custom Struct
    // ================================

    private readonly struct CustomStruct(int value) : IComparable
    {
        private readonly int Value = value;

        public int CompareTo(object? obj) => Value.CompareTo(((CustomStruct)obj!).Value);
    }

    [Fact]
    public void GetVal_Should_HandleCustomStructs()
    {
        // Arrange
        var dictionary = new Dictionary<string, CustomStruct>
        {
            { "X", new CustomStruct(42) },
            { "Y", new CustomStruct(99) },
        };
        var expected = new CustomStruct(99);

        // Act
        var actual = dictionary.GetVal("Y");
        var nonExistent = dictionary.GetVal("Z");

        // Assert
        actual
            .Should()
            .BeEquivalentTo(expected, "GetVal should retrieve the correct CustomStruct value");
        nonExistent
            .Should()
            .Be(
                default(CustomStruct),
                "GetVal should return the default for CustomStruct when key is missing"
            );
    }

    // ====================
    // 5) MapVal
    // ====================

    private class MapVal_Should_TransformValues_Data
        : TheoryData<
            Dictionary<string, int>, // Input dictionary
            Func<int, string>, // Mapping function
            Dictionary<string, string> // Expected dictionary after transformation
        >
    {
        public MapVal_Should_TransformValues_Data()
        {
            Add(
                new Dictionary<string, int>
                {
                    { "A", 1 },
                    { "B", 2 },
                    { "C", 3 },
                },
                val => $"Value-{val}",
                new Dictionary<string, string>
                {
                    { "A", "Value-1" },
                    { "B", "Value-2" },
                    { "C", "Value-3" },
                }
            ); // Basic transformation

            Add(
                [], // Empty dictionary
                val => $"Transformed-{val}",
                []
            ); // Empty dictionary case

            Add(
                new Dictionary<string, int> { { "X", 10 }, { "Y", 20 } },
                val => (val * val).ToString(),
                new Dictionary<string, string> { { "X", "100" }, { "Y", "400" } }
            ); // Custom transformation function (square and stringify)
        }
    }

    [Theory]
    [ClassData(typeof(MapVal_Should_TransformValues_Data))]
    public void MapVal_Should_TransformValues(
        Dictionary<string, int> input,
        Func<int, string> mapFunction,
        Dictionary<string, string> expected
    )
    {
        // Act
        var actual = input.MapVal(mapFunction);

        // Assert
        actual
            .Should()
            .BeEquivalentTo(
                expected,
                "MapVal should correctly transform values while keeping keys unchanged"
            );
    }

    // ============================
    // Edge Case: Exception in Map
    // ============================

    [Fact]
    public void MapVal_Should_HandleExceptionsInMappingFunction()
    {
        // Arrange
        var input = new Dictionary<string, int>
        {
            { "A", 1 },
            { "B", 2 },
            { "C", 3 },
        };

        // Act
        Action act = () => input.MapVal((Func<int, string>)ThrowingMap);

        // Assert
        act.Should()
            .Throw<InvalidOperationException>()
            .WithMessage("Test Exception", "Mapping function exceptions should be propagated");
        return;

        static string ThrowingMap(int _) => throw new InvalidOperationException("Test Exception");
    }

    // ====================
    // 6) MapKey
    // ====================

    private class MapKey_Should_TransformKeys_Data
        : TheoryData<
            Dictionary<string, int>, // Input dictionary
            Func<string, int, string>, // Mapping function
            Dictionary<string, int> // Expected dictionary after key transformation
        >
    {
        public MapKey_Should_TransformKeys_Data()
        {
            Add(
                new Dictionary<string, int>
                {
                    { "A", 10 },
                    { "B", 20 },
                    { "C", 30 },
                },
                (key, value) => $"{key}-{value}",
                new Dictionary<string, int>
                {
                    { "A-10", 10 },
                    { "B-20", 20 },
                    { "C-30", 30 },
                }
            ); // Basic transformation using key-value combination

            Add(
                [], // Empty dictionary
                (key, _) => $"Transformed-{key}",
                []
            ); // Empty dictionary use case

            Add(
                new Dictionary<string, int> { { "Alpha", 1 }, { "Beta", 2 } },
                (key, _) => key.ToUpper(),
                new Dictionary<string, int> { { "ALPHA", 1 }, { "BETA", 2 } }
            ); // Uppercasing keys
        }
    }

    [Theory]
    [ClassData(typeof(MapKey_Should_TransformKeys_Data))]
    public void MapKey_Should_TransformKeys(
        Dictionary<string, int> input,
        Func<string, int, string> mapFunction,
        Dictionary<string, int> expected
    )
    {
        // Act
        var actual = input.MapKey(mapFunction);

        // Assert
        actual
            .Should()
            .BeEquivalentTo(
                expected,
                "MapKey should correctly transform keys while keeping values unchanged"
            );
    }

    // ==================================================
    // Edge Case: Duplicate Keys in Resulting Dictionary
    // ==================================================

    [Fact]
    public void MapKey_Should_ThrowException_WhenMappedKeysAreNotUnique()
    {
        // Arrange
        var input = new Dictionary<string, int>
        {
            { "A", 10 },
            { "B", 20 },
            { "C", 30 },
        };

        // Act
        Action act = () => input.MapKey(DuplicateKeyMapping);

        // Assert
        act.Should().Throw<ArgumentException>("Duplicate keys are not allowed in dictionaries");
        return;

        static string DuplicateKeyMapping(string s, int i) => "DUPLICATE";
    }

    // ====================
    // 7) MapKey (Simple)
    // ====================

    private class MapKey_Simple_Should_TransformKeys_Data
        : TheoryData<
            Dictionary<int, string>, // Input dictionary
            Func<int, string>, // Mapping function for keys
            Dictionary<string, string> // Expected dictionary after key transformation
        >
    {
        public MapKey_Simple_Should_TransformKeys_Data()
        {
            Add(
                new Dictionary<int, string>
                {
                    { 1, "One" },
                    { 2, "Two" },
                    { 3, "Three" },
                },
                key => $"Key-{key}",
                new Dictionary<string, string>
                {
                    { "Key-1", "One" },
                    { "Key-2", "Two" },
                    { "Key-3", "Three" },
                }
            ); // Transform by prefixing string to key

            Add(
                [], // Empty dictionary
                key => key.ToString(),
                []
            ); // Empty dictionary case

            Add(
                new Dictionary<int, string> { { 100, "Hundred" }, { 200, "Two Hundred" } },
                key => $"ID-{key + key}",
                new Dictionary<string, string>
                {
                    { "ID-200", "Hundred" },
                    { "ID-400", "Two Hundred" },
                }
            ); // Add mapped transformations
        }
    }

    [Theory]
    [ClassData(typeof(MapKey_Simple_Should_TransformKeys_Data))]
    public void MapKey_Simple_Should_TransformKeys(
        Dictionary<int, string> input,
        Func<int, string> mapFunction,
        Dictionary<string, string> expected
    )
    {
        // Act
        var actual = input.MapKey(mapFunction);

        // Assert
        actual
            .Should()
            .BeEquivalentTo(
                expected,
                "MapKey should transform keys while keeping values unchanged"
            );
    }

    // ==================================================
    // Edge Case: Duplicate Keys in Resulting Dictionary
    // ==================================================

    [Fact]
    public void MapKey_Simple_Should_ThrowException_WhenMappedKeysAreNotUnique()
    {
        // Arrange
        var input = new Dictionary<int, string>
        {
            { 1, "One" },
            { 2, "Two" },
            { 3, "Three" },
        };

        // Act
        Action act = () => input.MapKey((Func<int, string>)DuplicateKeyMapping);

        // Assert
        act.Should().Throw<ArgumentException>("Duplicate keys are not allowed in dictionaries");
        return;

        static string DuplicateKeyMapping(int _) => "DUPLICATE";
    }

    // ====================
    // 8) ChooseVal
    // ====================

    private class ChooseVal_Should_CreateDictionaryFromCollection_Data
        : TheoryData<
            IEnumerable<string>, // Input collection
            Func<string, int>, // Mapping function for values
            Dictionary<string, int> // Expected dictionary
        >
    {
        public ChooseVal_Should_CreateDictionaryFromCollection_Data()
        {
            Add(
                ["A", "BB", "CCC"],
                str => str.Length, // Use string length as value
                new Dictionary<string, int>
                {
                    { "A", 1 },
                    { "BB", 2 },
                    { "CCC", 3 },
                }
            ); // Basic transformation

            Add(
                [], // Empty collection
                str => str.Length,
                []
            ); // Empty collection case

            Add(
                ["X", "Y", "Z"],
                str => str[0], // Use ASCII value of the first char as the value
                new Dictionary<string, int>
                {
                    { "X", 88 },
                    { "Y", 89 },
                    { "Z", 90 },
                }
            ); // ASCII transformation
        }
    }

    [Theory]
    [ClassData(typeof(ChooseVal_Should_CreateDictionaryFromCollection_Data))]
    public void ChooseVal_Should_CreateDictionaryFromCollection(
        IEnumerable<string> input,
        Func<string, int> valFunc,
        Dictionary<string, int> expected
    )
    {
        // Act
        var actual = input.ChooseVal(valFunc);

        // Assert
        actual
            .Should()
            .BeEquivalentTo(
                expected,
                "ChooseVal should produce a dictionary using elements as keys and mapped values"
            );
    }

    // ==================================================
    // Edge Case: Duplicate Keys in Input Collection
    // ==================================================

    [Fact]
    public void ChooseVal_Should_ThrowException_WhenInputCollectionIncludesDuplicateElements()
    {
        // Arrange
        var input = new[] { "Apple", "Apple", "Banana" }; // Duplicate key: "Apple"

        // Act
        Action act = () => input.ChooseVal((Func<string, int>)ValFunc);

        // Assert
        act.Should()
            .Throw<ArgumentException>(
                "Duplicate keys are not allowed when transforming collections to dictionaries"
            );
        return;

        static int ValFunc(string str) => str.Length;
    }

    // ====================
    // 9) ChooseKey
    // ====================

    private class ChooseKey_Should_CreateDictionaryFromCollection_Data
        : TheoryData<
            IEnumerable<string>, // Input collection
            Func<string, string>, // Mapping function for keys
            Dictionary<string, string> // Expected dictionary
        >
    {
        public ChooseKey_Should_CreateDictionaryFromCollection_Data()
        {
            Add(
                ["Apple", "Banana", "Cherry"],
                fruit => fruit[..1], // Use the first letter of the string as key
                new Dictionary<string, string>
                {
                    { "A", "Apple" },
                    { "B", "Banana" },
                    { "C", "Cherry" },
                }
            ); // Basic case

            Add(
                [], // Empty collection
                fruit => fruit.ToUpper(),
                []
            ); // Empty collection case

            Add(
                ["Cat", "Worm", "Elephant"],
                animal => $"Key-{animal.Length}", // Use length of the string as part of the key
                new Dictionary<string, string>
                {
                    { "Key-3", "Cat" },
                    { "Key-4", "Worm" },
                    { "Key-8", "Elephant" },
                }
            ); // Complex key transformation
        }
    }

    [Theory]
    [ClassData(typeof(ChooseKey_Should_CreateDictionaryFromCollection_Data))]
    public void ChooseKey_Should_CreateDictionaryFromCollection(
        IEnumerable<string> input,
        Func<string, string> keyFunc,
        Dictionary<string, string> expected
    )
    {
        // Act
        var actual = input.ChooseKey(keyFunc);

        // Assert
        actual
            .Should()
            .BeEquivalentTo(
                expected,
                "ChooseKey should produce a dictionary using transformed keys and input elements as values"
            );
    }

    // ==================================================
    // Edge Case: Duplicate Keys in Input Collection
    // ==================================================

    [Fact]
    public void ChooseKey_Should_ThrowException_WhenMappedKeysAreNotUnique()
    {
        // Arrange
        var input = new[] { "Apple", "Apricot", "Avocado" }; // Duplicate key: first letter "A"

        // Act
        Action act = () => input.ChooseKey((Func<string, string>)DuplicateKeyGen);

        // Assert
        act.Should()
            .Throw<ArgumentException>("Duplicate keys are not allowed in the resulting dictionary");
        return;

        static string DuplicateKeyGen(string fruit) => fruit[..1];
    }

    // ====================
    // 10) Invert
    // ====================

    private class Invert_Should_SwapKeysAndValues_Data
        : TheoryData<
            Dictionary<string, int>, // Input dictionary
            Dictionary<int, string> // Expected inverted dictionary
        >
    {
        public Invert_Should_SwapKeysAndValues_Data()
        {
            Add(
                new Dictionary<string, int>
                {
                    { "A", 1 },
                    { "B", 2 },
                    { "C", 3 },
                },
                new Dictionary<int, string>
                {
                    { 1, "A" },
                    { 2, "B" },
                    { 3, "C" },
                }
            ); // Basic inversion

            Add(
                [], // Empty dictionary
                []
            ); // Empty dictionary case

            Add(
                new Dictionary<string, int> { { "X", 42 }, { "Y", 99 } },
                new Dictionary<int, string> { { 42, "X" }, { 99, "Y" } }
            ); // Simple two-element dictionary inversion
        }
    }

    [Theory]
    [ClassData(typeof(Invert_Should_SwapKeysAndValues_Data))]
    public void Invert_Should_SwapKeysAndValues(
        Dictionary<string, int> input,
        Dictionary<int, string> expected
    )
    {
        // Act
        var actual = input.Invert();

        // Assert
        actual.Should().BeEquivalentTo(expected, "Invert should correctly swap keys and values");
    }

    // ==================================================
    // Edge Case: Duplicate Values in Input Dictionary
    // ==================================================

    [Fact]
    public void Invert_Should_ThrowException_WhenInputContainsDuplicateValues()
    {
        // Arrange
        var input = new Dictionary<string, int>
        {
            { "A", 1 },
            { "B", 1 },
            { "C", 3 },
        }; // Duplicate value: 1

        // Act
        Action act = () => input.Invert();

        // Assert
        act.Should()
            .Throw<ArgumentException>(
                "Duplicate values in the input dictionary cannot produce unique keys in the inverted dictionary"
            );
    }

    // ====================
    // 11) GroupByKeys
    // ====================

    private class GroupByKeys_Should_GroupCollection_Data
        : TheoryData<
            IEnumerable<string>, // Input collection
            Func<string, char>, // Key-mapping function (e.g., first character)
            Func<string, string>, // Value-mapping function (e.g., identity or transformation)
            Dictionary<char, IEnumerable<string>> // Expected dictionary
        >
    {
        public GroupByKeys_Should_GroupCollection_Data()
        {
            Add(
                ["Apple", "Apricot", "Banana", "Blueberry", "Cherry"],
                fruit => fruit[0], // Key = first character
                fruit => fruit, // Value = identity (untransformed)
                new Dictionary<char, IEnumerable<string>>
                {
                    { 'A', ["Apple", "Apricot"] },
                    { 'B', ["Banana", "Blueberry"] },
                    { 'C', ["Cherry"] },
                }
            ); // Basic case with grouping by first character

            Add(
                [], // Empty collection
                fruit => fruit[0],
                fruit => fruit,
                []
            ); // Empty collection case

            Add(
                ["Cat", "Dog", "Elephant", "Cow", "Duck"],
                animal => animal[0], // Group by first character
                animal => animal.ToUpper(), // Transform values to uppercase
                new Dictionary<char, IEnumerable<string>>
                {
                    { 'C', ["CAT", "COW"] },
                    { 'D', ["DOG", "DUCK"] },
                    { 'E', ["ELEPHANT"] },
                }
            ); // Complex case with uppercased values
        }
    }

    [Theory]
    [ClassData(typeof(GroupByKeys_Should_GroupCollection_Data))]
    public void GroupByKeys_Should_GroupCollection(
        IEnumerable<string> input,
        Func<string, char> keyFunc,
        Func<string, string> valFunc,
        Dictionary<char, IEnumerable<string>> expected
    )
    {
        // Act
        var actual = input.GroupByKeys(keyFunc, valFunc);

        // Assert
        actual
            .Should()
            .BeEquivalentTo(
                expected,
                "GroupByKeys should correctly group elements by keys and transform their values"
            );
    }

    // ==================================================
    // Edge Case: Duplicate Elements in Input Collection
    // ==================================================

    [Fact]
    public void GroupByKeys_Should_HandleDuplicateElementsCorrectly()
    {
        // Arrange
        var input = new[] { "Apple", "Apple", "Banana", "Banana" }; // Duplicate elements in input
        char KeyFunc(string fruit) => fruit[0];
        string ValFunc(string fruit) => fruit;

        var expected = new Dictionary<char, IEnumerable<string>>
        {
            { 'A', ["Apple", "Apple"] }, // Both "Apple" instances grouped under 'A'
            { 'B', ["Banana", "Banana"] }, // Both "Banana" instances grouped under 'B'
        };

        // Act
        var actual = input.GroupByKeys((Func<string, char>)KeyFunc, (Func<string, string>)ValFunc);

        // Assert
        actual
            .Should()
            .BeEquivalentTo(
                expected,
                "GroupByKeys should correctly handle duplicate elements by grouping them under their respective keys"
            );
    }
}
