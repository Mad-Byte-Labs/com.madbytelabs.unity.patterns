using MadByteLabs.Patterns;

namespace MadByteLabs.Patterns.Samples.Command
{
    /// <summary>
    /// Provides counter command behavior for the pattern demonstration or tests.
    /// </summary>
    internal sealed class CounterCommand : ICommand
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        public int Value { get; private set; }

        /// <summary>
        /// Executes the operation. Failures propagate without rolling back partial side effects.
        /// </summary>
        public void Execute()
        {
            Value++;
        }

        /// <summary>
        /// Reverses the most recent successful operation, if one is available.
        /// </summary>
        public void Undo()
        {
            Value--;
        }
    }
}
