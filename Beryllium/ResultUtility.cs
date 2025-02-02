using CarboxylicLithium;

namespace CarboxylicBeryllium;

public static class ResultUtility
{
    /// <summary>
    /// Try a running a certain action X number of times. There are intervals between each try,
    /// and the action is only run if the previous try failed. Once the action succeeds, the loop
    /// ends.
    ///
    /// The action should return true if it succeeded, false if it failed.
    ///
    /// After the X number of the times, and the action still returns false, the failure action
    /// is called and the error is returned as a Result.
    /// </summary>
    /// <param name="x">Number of times to try</param>
    /// <param name="action">The function to try</param>
    /// <param name="failureAction">The function to generate the Exception when X number of tries is reached and no success</param>
    /// <param name="delayMilliseconds">Delay between each try</param>
    /// <returns></returns>
    public static async Task<Result<Unit>> TryFor(
        this int x,
        Func<int, Task<bool>> action,
        Func<Exception> failureAction,
        int delayMilliseconds = 1000
    )
    {
        var tries = 0;
        var done = false;
        while (!done)
        {
            if (tries >= x)
                return failureAction.Invoke();
            done = await action.Invoke(tries);
            await Task.Delay(delayMilliseconds);
            tries++;
        }
        return new Unit();
    }
}
