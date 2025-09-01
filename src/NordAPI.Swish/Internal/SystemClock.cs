namespace NordAPI.Swish.Internal;

public interface ISystemClock
{
    DateTimeOffset UtcNow { get; }
}

public sealed class SystemClock : ISystemClock
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}

public static class Nonce
{
    public static string New() => Convert.ToHexString(Guid.NewGuid().ToByteArray());
}
