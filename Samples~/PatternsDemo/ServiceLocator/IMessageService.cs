namespace MadByteLabs.Patterns.Samples.ServiceLocator
{
    /// <summary>
    /// Provides imessage service behavior for the pattern demonstration or tests.
    /// </summary>
    internal interface IMessageService
    {
        /// <summary>
        /// Performs write for this instance.
        /// </summary>
        /// <param name="message">The message used by this operation.</param>
        void Write(string message);
    }
}
