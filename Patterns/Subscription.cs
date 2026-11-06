using System;

namespace MadByteLabs.Patterns
{
    /// <summary>
    /// Owns an idempotent callback that releases an observer subscription.
    /// </summary>
    internal sealed class Subscription : IDisposable
    {
        private Action m_release;

        /// <summary>
        /// Initializes a new Subscription instance with explicit dependencies.
        /// </summary>
        /// <param name="release">The callback that releases the subscription.</param>
        internal Subscription(Action release)
        {
            m_release = release;
        }

        /// <summary>
        /// Releases owned resources. Repeated successful disposal has no effect.
        /// </summary>
        public void Dispose()
        {
            var action = m_release;
            m_release = null;
            action?.Invoke();
        }
    }
}
