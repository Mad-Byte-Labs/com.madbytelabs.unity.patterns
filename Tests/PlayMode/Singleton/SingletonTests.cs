using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine;

namespace MadByteLabs.Patterns.Tests.Singleton
{
    /// <summary>
    /// Provides singleton tests behavior for the pattern demonstration or tests.
    /// </summary>
    public sealed class SingletonTests
    {
        /// <summary>
        /// Verifies that registers instance and clears it on destruction.
        /// </summary>
        /// <returns>An enumerator that waits for Unity to complete object destruction.</returns>
        [UnityTest]
        public IEnumerator RegistersInstanceAndClearsItOnDestruction()
        {
            var owner = new GameObject("Singleton test");
            var instance = owner.AddComponent<TestSession>();
            try
            {
                Assert.AreSame(instance, TestSession.Instance);
            }
            finally
            {
                Object.Destroy(owner);
            }

            yield return null;
            Assert.IsTrue(TestSession.Instance == null);
        }
    }
}
