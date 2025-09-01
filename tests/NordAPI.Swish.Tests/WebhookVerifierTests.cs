using System;
using Xunit;
using NordAPI.Swish.Webhooks;
using NordAPI.Swish.Security.Http;

namespace NordAPI.Swish.Tests
{
    public class WebhookVerifierTests
    {
        [Fact]
        public void Verify_SucceedsWithinTolerance()
        {
            var body = "{\"ok\":true}";
            var secret = "s3cr3t";
            var ts = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            var canonical = $"{ts}\n{body}";
            var sig = HmacSigningHandler.ComputeHmac(secret, canonical);

            var ok = WebhookSignatureVerifier.Verify(sig, secret, body, ts, TimeSpan.FromMinutes(5));
            Assert.True(ok);
        }
    }
}
