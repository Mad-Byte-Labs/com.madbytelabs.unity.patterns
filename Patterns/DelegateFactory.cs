using System;

namespace MadByteLabs.Patterns
{
    /// <summary>
    /// Creates values using a caller-provided construction delegate.
    /// </summary>
    /// <typeparam name="T">The type of values used by this contract.</typeparam>
    public sealed class DelegateFactory<T> : IFactory<T>
    {
        private readonly Func<T> m_create;

        /// <summary>
        /// Initializes a new DelegateFactory instance with explicit dependencies.
        /// </summary>
        /// <param name="create">The required delegate that creates a value on demand.</param>
        public DelegateFactory(Func<T> create)
        {
            m_create = create ?? throw new ArgumentNullException(nameof(create));
        }

        /// <summary>
        /// Creates a value using the configured creation strategy. The caller owns the result.
        /// </summary>
        /// <returns>The newly created value or Unity object instance.</returns>
        public T Create() => m_create();
    }
}
