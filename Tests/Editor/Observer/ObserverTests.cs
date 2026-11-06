using MadByteLabs.Patterns;
using NUnit.Framework;

namespace MadByteLabs.Patterns.Tests.Observer
{
    /// <summary>
    /// Provides observer tests behavior for the pattern demonstration or tests.
    /// </summary>
    public sealed class ObserverTests
    {
        /// <summary>
        /// Verifies that subscription disposal is idempotent.
        /// </summary>
        [Test]
        public void SubscriptionDisposalIsIdempotent()
        {
            int sum = 0;
            var signal = new Signal<int>();
            var token = signal.Subscribe(x => sum += x);
            signal.Publish(2);
            token.Dispose();
            token.Dispose();
            signal.Publish(5);
            Assert.AreEqual(2, sum);
        }
    }
}
