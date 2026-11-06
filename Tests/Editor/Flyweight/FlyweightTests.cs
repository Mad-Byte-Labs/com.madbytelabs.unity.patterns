using MadByteLabs.Patterns;
using NUnit.Framework;

namespace MadByteLabs.Patterns.Tests.Flyweight
{
    /// <summary>
    /// Provides flyweight tests behavior for the pattern demonstration or tests.
    /// </summary>
    public sealed class FlyweightTests
    {
        /// <summary>
        /// Verifies that flyweight creates once per key.
        /// </summary>
        [Test]
        public void FlyweightCreatesOncePerKey()
        {
            int calls = 0;
            var cache = new FlyweightCache<int, object>(key =>
            {
                calls++;
                return new object();
            });
            Assert.AreSame(cache.Get(1), cache.Get(1));
            Assert.AreEqual(1, calls);
        }
    }
}
