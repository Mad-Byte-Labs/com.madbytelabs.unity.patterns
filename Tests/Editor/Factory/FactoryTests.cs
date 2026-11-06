using MadByteLabs.Patterns;
using NUnit.Framework;

namespace MadByteLabs.Patterns.Tests.Factory
{
    /// <summary>
    /// Provides factory tests behavior for the pattern demonstration or tests.
    /// </summary>
    public sealed class FactoryTests
    {
        /// <summary>
        /// Verifies that factory creates distinct objects.
        /// </summary>
        [Test]
        public void FactoryCreatesDistinctObjects()
        {
            var factory = new DelegateFactory<object>(() => new object());
            Assert.AreNotSame(factory.Create(), factory.Create());
        }
    }
}
