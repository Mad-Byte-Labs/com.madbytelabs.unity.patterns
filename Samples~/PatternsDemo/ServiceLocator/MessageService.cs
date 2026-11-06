using UnityEngine;

namespace MadByteLabs.Patterns.Samples.ServiceLocator
{
    /// <summary>
    /// Provides message service behavior for the pattern demonstration or tests.
    /// </summary>
    internal sealed class MessageService : IMessageService
    {
        /// <summary>
        /// Performs write for this instance.
        /// </summary>
        /// <param name="message">The message used by this operation.</param>
        public void Write(string message)
        {
            Debug.Log(message);
        }
    }
}
