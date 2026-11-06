using System;
using MadByteLabs.Patterns;
using NUnit.Framework;

namespace MadByteLabs.Patterns.Tests.ServiceLocator
{
    /// <summary>
    /// Provides service locator tests behavior for the pattern demonstration or tests.
    /// </summary>
    public sealed class ServiceLocatorTests
    {
        /// <summary>
        /// Verifies that services reject duplicates and clear.
        /// </summary>
        [Test]
        public void ServicesRejectDuplicatesAndClear()
        {
            var locator = new MadByteLabs.Patterns.ServiceLocator();
            locator.Register<object>(new object());
            Assert.Throws<InvalidOperationException>(() => locator.Register<object>(new object()));
            locator.Clear();
            Assert.Throws<InvalidOperationException>(() => locator.Get<object>());
        }
    }
}
