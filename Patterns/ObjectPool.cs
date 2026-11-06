using System.Collections.Generic;
using System;

namespace MadByteLabs.Patterns
{
    /// <summary>
    /// Reuses reference objects with validated leases. Return every lease before disposal; callbacks must not reenter.
    /// </summary>
    /// <typeparam name="T">The type of values used by this contract.</typeparam>
    public sealed class ObjectPool<T> : IDisposable where T : class
    {
        private readonly Stack<T> m_idle = new Stack<T>();
        private readonly HashSet<T> m_leased = new HashSet<T>(new ReferenceIdentityComparer<T>());
        private readonly Func<T> m_create;
        private readonly Action<T> m_onRent;
        private readonly Action<T> m_onReturn;
        private readonly Action<T> m_onDestroy;
        private readonly int m_capacity;
        private bool m_disposed;

        /// <summary>
        /// Gets the number of retained inactive objects.
        /// </summary>
        public int CountInactive => m_idle.Count;

        /// <summary>
        /// Gets the number of active leases that must be returned before disposal.
        /// </summary>
        public int CountActive => m_leased.Count;

        /// <summary>
        /// Initializes a new ObjectPool instance with explicit dependencies.
        /// </summary>
        /// <param name="create">The required delegate that creates a value on demand.</param>
        /// <param name="onRent">An optional activation callback; it must not reenter this pool.</param>
        /// <param name="onReturn">An optional reset callback; it must not reenter this pool.</param>
        /// <param name="onDestroy">
        /// An optional cleanup callback for discarded objects; required to destroy Unity objects.
        /// </param>
        /// <param name="capacity">
        /// The nonnegative maximum number of inactive objects retained, not a limit on active leases.
        /// </param>
        public ObjectPool(
            Func<T> create,
            Action<T> onRent = null,
            Action<T> onReturn = null,
            Action<T> onDestroy = null,
            int capacity = 128)
        {
            m_create = create ?? throw new ArgumentNullException(nameof(create));
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity));
            }

            m_onRent = onRent;
            m_onReturn = onReturn;
            m_onDestroy = onDestroy;
            m_capacity = capacity;
        }

        /// <summary>
        /// Acquires an object and invokes its rent callback. The caller must return the lease exactly once.
        /// </summary>
        /// <returns>An active object lease that must be returned to this pool.</returns>
        public T Rent()
        {
            if (m_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }

            T item = m_idle.Count > 0 ? m_idle.Pop() : m_create();
            if (item == null)
            {
                throw new InvalidOperationException("Factory returned null.");
            }

            if (!m_leased.Add(item))
            {
                throw new InvalidOperationException("Factory returned an already leased object.");
            }

            try
            {
                m_onRent?.Invoke(item);
            }
            catch
            {
                m_leased.Remove(item);
                m_onDestroy?.Invoke(item);
                throw;
            }

            return item;
        }

        /// <summary>
        /// Releases an active lease. Foreign objects and repeated returns are rejected.
        /// </summary>
        /// <param name="item">The active object lease owned by this pool.</param>
        public void Return(T item)
        {
            if (m_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }

            if (item == null || !m_leased.Remove(item))
            {
                throw new InvalidOperationException("Object does not belong to active leases.");
            }

            try
            {
                m_onReturn?.Invoke(item);
            }
            catch
            {
                m_onDestroy?.Invoke(item);
                throw;
            }

            if (m_idle.Count < m_capacity)
            {
                m_idle.Push(item);
            }
            else
            {
                m_onDestroy?.Invoke(item);
            }
        }

        /// <summary>
        /// Destroys inactive objects. Active leases prevent disposal; cleanup failures are aggregated after all
        /// attempts.
        /// </summary>
        public void Dispose()
        {
            if (m_disposed)
            {
                return;
            }

            if (m_leased.Count != 0)
            {
                throw new InvalidOperationException("Return active objects before disposing the pool.");
            }

            m_disposed = true;
            List<Exception> errors = null;
            while (m_idle.Count > 0)
            {
                T item = m_idle.Pop();
                try
                {
                    m_onDestroy?.Invoke(item);
                }
                catch (Exception error)
                {
                    if (errors == null)
                    {
                        errors = new List<Exception>();
                    }

                    errors.Add(error);
                }
            }

            if (errors != null)
            {
                throw new AggregateException(errors);
            }
        }
    }
}
