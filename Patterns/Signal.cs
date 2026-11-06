using System;

namespace MadByteLabs.Patterns
{
    /// <summary>
    /// Publishes synchronous notifications in subscription order. Dispose subscriptions when their owner stops
    /// listening.
    /// </summary>
    /// <typeparam name="T">The type of values used by this contract.</typeparam>
    public sealed class Signal<T>
    {
        private event Action<T> Published;

        /// <summary>
        /// Adds a listener and returns its subscription. Listener failures stop the current publication.
        /// </summary>
        /// <param name="handler">The required listener to invoke for each publication.</param>
        /// <returns>An idempotent subscription token owned by the caller.</returns>
        public IDisposable Subscribe(Action<T> handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            Published += handler;
            return new Subscription(() => Published -= handler);
        }

        /// <summary>
        /// Notifies the current listeners synchronously. Listener failures propagate.
        /// </summary>
        /// <param name="value">The value to publish, enqueue, or use as a base statistic.</param>
        public void Publish(T value) => Published?.Invoke(value);

        /// <summary>
        /// Releases stored references without disposing or destroying referenced objects.
        /// </summary>
        public void Clear() => Published = null;
    }
}
