using System.Collections.Generic;
using System;

namespace MadByteLabs.Patterns
{
    /// <summary>
    /// Defers notifications until Drain. Events enqueued during dispatch wait for a later drain.
    /// </summary>
    /// <typeparam name="T">The type of values used by this contract.</typeparam>
    public sealed class EventQueue<T>
    {
        private readonly Queue<T> m_pending = new Queue<T>();
        private bool m_draining;

        /// <summary>
        /// Gets the dispatched.
        /// </summary>
        public Signal<T> Dispatched { get; } = new Signal<T>();

        /// <summary>
        /// Gets the count.
        /// </summary>
        public int Count => m_pending.Count;

        /// <summary>
        /// Appends an event for later dispatch.
        /// </summary>
        /// <param name="value">The value to publish, enqueue, or use as a base statistic.</param>
        public void Enqueue(T value) => m_pending.Enqueue(value);

        /// <summary>
        /// Dispatches up to the initial event count and budget. A failed event is consumed; later events remain queued.
        /// </summary>
        /// <param name="maxEvents">The nonnegative maximum number of events to process in this call.</param>
        /// <returns>The number of events dispatched successfully before the call returns.</returns>
        public int Drain(int maxEvents = int.MaxValue)
        {
            if (maxEvents < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxEvents));
            }

            if (m_draining)
            {
                throw new InvalidOperationException("Already draining.");
            }

            int count = Math.Min(maxEvents, m_pending.Count), processed = 0;
            m_draining = true;
            try
            {
                while (processed < count && m_pending.Count > 0)
                {
                    var value = m_pending.Dequeue();
                    processed++;
                    Dispatched.Publish(value);
                }
            }
            finally
            {
                m_draining = false;
            }

            return processed;
        }

        /// <summary>
        /// Releases stored references without disposing or destroying referenced objects.
        /// </summary>
        public void Clear() => m_pending.Clear();
    }
}
