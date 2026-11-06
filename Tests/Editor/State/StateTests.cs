using System.Collections.Generic;
using MadByteLabs.Patterns;
using NUnit.Framework;

namespace MadByteLabs.Patterns.Tests.State
{
    /// <summary>
    /// Provides state tests behavior for the pattern demonstration or tests.
    /// </summary>
    public sealed class StateTests
    {
        /// <summary>
        /// Verifies that state exits before next entry.
        /// </summary>
        [Test]
        public void StateExitsBeforeNextEntry()
        {
            var log = new List<string>();
            var machine = new StateMachine();
            var firstState = new RecordingState("a", log);
            machine.Change(firstState);
            machine.Change(firstState);
            machine.Change(new RecordingState("b", log));
            machine.Change(null);
            CollectionAssert.AreEqual(new[] { "a+", "a-", "b+", "b-" }, log);
        }
    }
}
