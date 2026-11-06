namespace MadByteLabs.Patterns
{
    /// <summary>
    /// Defines a reversible operation. Implementations must undo their own successful execution.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Executes the operation. Failures propagate without rolling back partial side effects.
        /// </summary>
        void Execute();

        /// <summary>
        /// Reverses this command's successful execution. Implementations define a meaningful inverse.
        /// </summary>
        void Undo();
    }
}
