namespace NordAPI.BankID.Tests;

/// <summary>
/// Minimal smoke tests to ensure the BankID test project is discovered and runs in CI.
/// </summary>
public sealed class SmokeTests
{
    /// <summary>
    /// Basic sanity test to verify the test project loads and executes.
    /// </summary>
    [Fact]
    public void BankIdTestProjectLoads()
    {
        Assert.True(true);
    }
}
