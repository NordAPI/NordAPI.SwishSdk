using System.Security.Cryptography;
using System.Text;
using NordAPI.Swish.Internal;

namespace NordAPI.Swish.Webhooks;

/// <summary>
/// Verifierar en webhook-signatur baserat på timestamp och body.
/// Canonical: "{timestamp}\n{body}"
/// </summary>
public static class WebhookSignatureVerifier
{
    public static bool Verify(
        string providedSignatureHex,
        string secret,
        string body,
        long timestampUnixSeconds,
        TimeSpan tolerance,
        ISystemClock? clock = null)
    {
        var now = (clock ?? new SystemClock()).UtcNow;
        var eventTime = DateTimeOffset.FromUnixTimeSeconds(timestampUnixSeconds);

        if (now - eventTime > tolerance) return false; // anti-replay fönster

        var canonical = $"{timestampUnixSeconds}\n{body}";
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret));
        var expectedBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(canonical));
        var expectedHex = Convert.ToHexString(expectedBytes);

        return CryptographicOperations.FixedTimeEquals(
            Encoding.ASCII.GetBytes(providedSignatureHex),
            Encoding.ASCII.GetBytes(expectedHex));
    }
}
