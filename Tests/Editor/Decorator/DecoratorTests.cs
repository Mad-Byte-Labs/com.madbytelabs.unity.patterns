using MadByteLabs.Patterns;
using NUnit.Framework;

namespace MadByteLabs.Patterns.Tests.Decorator
{
    /// <summary>
    /// Provides decorator tests behavior for the pattern demonstration or tests.
    /// </summary>
    public sealed class DecoratorTests
    {
        /// <summary>
        /// Verifies that decorator order is significant.
        /// </summary>
        [Test]
        public void DecoratorOrderIsSignificant()
        {
            Assert.AreEqual(30, new MultiplyStat(new AddStat(new ConstantStat(10), 5), 2).Value);
            Assert.AreEqual(25, new AddStat(new MultiplyStat(new ConstantStat(10), 2), 5).Value);
        }
    }
}
