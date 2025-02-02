using CarboxylicBeryllium;
using CarboxylicLithium;
using FluentAssertions;

namespace UnitTest;

public class ResultUtilityTests
{
    // ==============
    // 1) TryFor
    // ==============

    private class TryFor_Should_SucceedBeforeMaxTries_Data
        : TheoryData<int, Task<bool>[], Result<Unit>>
    {
        public TryFor_Should_SucceedBeforeMaxTries_Data()
        {
            Add(3, [Task.FromResult(true)], new Unit());
            Add(3, [Task.FromResult(false), Task.FromResult(true)], new Unit());
            Add(
                3,
                [Task.FromResult(false), Task.FromResult(false), Task.FromResult(true)],
                new Unit()
            );
        }
    }

    // TryFor should succeed if action succeeds before the maximum number of tries
    [Theory]
    [ClassData(typeof(TryFor_Should_SucceedBeforeMaxTries_Data))]
    public async Task TryFor_Should_SucceedBeforeMaxTries(
        int maxTries,
        Task<bool>[] actions,
        Result<Unit> expected
    )
    {
        // Arrange
        var callIndex = 0;
        Task<bool> Action(int _) => actions[callIndex++];

        // Act
        var actual = await maxTries.TryFor(Action, () => new Exception("Failure"), 5);

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    private class TryFor_Should_FailAtMaxTries_Data
        : TheoryData<int, Task<bool>[], Func<Exception>, Result<Unit>>
    {
        public TryFor_Should_FailAtMaxTries_Data()
        {
            // Fails exactly at max tries
            Add(
                3,
                [Task.FromResult(false), Task.FromResult(false), Task.FromResult(false)],
                () => new ApplicationException("failed"),
                new ApplicationException("failed")
            );
            Add(
                5,
                [
                    Task.FromResult(false),
                    Task.FromResult(false),
                    Task.FromResult(false),
                    Task.FromResult(false),
                    Task.FromResult(false),
                ],
                () => new InvalidOperationException("failed"),
                new InvalidOperationException("failed")
            );
        }
    }

    // TryFor should fail exactly at the maximum number of tries
    [Theory]
    [ClassData(typeof(TryFor_Should_FailAtMaxTries_Data))]
    public async Task TryFor_Should_FailAtMaxTries(
        int maxTries,
        Task<bool>[] actions,
        Func<Exception> failureAction,
        Result<Unit> expected
    )
    {
        // Arrange
        var callIndex = 0;
        Task<bool> Action(int _) => actions[callIndex++];

        // Act
        var actual = await maxTries.TryFor(Action, failureAction, 5);

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    private class TryFor_Should_HandleNoTries_Data : TheoryData<int, Exception, Result<Unit>>
    {
        public TryFor_Should_HandleNoTries_Data()
        {
            // Zero tries should invoke failure immediately
            Add(0, new Exception("No try attempt"), new Exception("No try attempt"));
        }
    }

    // TryFor should handle zero tries correctly
    [Theory]
    [ClassData(typeof(TryFor_Should_HandleNoTries_Data))]
    public async Task TryFor_Should_HandleNoTries(
        int maxTries,
        Exception failure,
        Result<Unit> expected
    )
    {
        var threshold = -100;
        // Arrange
        Task<bool> Action(int i) => i < threshold ? Task.FromResult(true) : Task.FromResult(false);

        // Act
        var actual = await maxTries.TryFor(Action, () => failure);

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }
}
