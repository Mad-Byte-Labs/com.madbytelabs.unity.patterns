using MadByteLabs.Patterns;
using NUnit.Framework;

namespace MadByteLabs.Patterns.Tests.Command
{
    /// <summary>
    /// Provides command tests behavior for the pattern demonstration or tests.
    /// </summary>
    public sealed class CommandTests
    {
        /// <summary>
        /// Verifies that history branches invalidate redo.
        /// </summary>
        [Test]
        public void HistoryBranchesInvalidateRedo()
        {
            var history = new CommandHistory();
            var command = new CounterCommand();
            history.Execute(command);
            history.Undo();
            history.Redo();
            Assert.AreEqual(1, command.Value);
            history.Undo();
            history.Execute(command);
            Assert.IsFalse(history.Redo());
        }
    }
}
