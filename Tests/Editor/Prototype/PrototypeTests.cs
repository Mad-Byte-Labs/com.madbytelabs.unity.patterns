using NUnit.Framework;
using UnityEngine;

namespace MadByteLabs.Patterns.Tests.Prototype
{
    /// <summary>
    /// Provides prototype tests behavior for the pattern demonstration or tests.
    /// </summary>
    public sealed class PrototypeTests
    {
        /// <summary>
        /// Verifies that clone creates independent unity object.
        /// </summary>
        [Test]
        public void CloneCreatesIndependentUnityObject()
        {
            var source = new GameObject("Prototype source");
            var clone = MadByteLabs.Patterns.Prototype.Clone(source);
            try
            {
                Assert.AreNotSame(source, clone);
                Assert.AreEqual(source.name + "(Clone)", clone.name);
                clone.name="Changed clone";
                Assert.AreEqual("Prototype source", source.name);
            }
            finally
            {
                Object.DestroyImmediate(source);
                Object.DestroyImmediate(clone);
            }
        }
    }
}
