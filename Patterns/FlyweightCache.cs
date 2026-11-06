using System.Collections.Generic;
using System;

namespace MadByteLabs.Patterns
{
    /// <summary>
    /// Caches shared values by key. Keep cached data immutable and instance state outside the cache.
    /// </summary>
    /// <typeparam name="TKey">The key type used by this contract.</typeparam>
    /// <typeparam name="TValue">The type of values used by this contract.</typeparam>
    public sealed class FlyweightCache<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> m_values = new Dictionary<TKey, TValue>();
        private readonly Func<TKey, TValue> m_create;

        /// <summary>
        /// Initializes a new FlyweightCache instance with explicit dependencies.
        /// </summary>
        /// <param name="create">The required delegate that creates a value on demand.</param>
        public FlyweightCache(Func<TKey, TValue> create)
        {
            m_create = create ?? throw new ArgumentNullException(nameof(create));
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        public int Count => m_values.Count;

        /// <summary>
        /// Returns the cached value, creating and retaining it on the first request for a key.
        /// </summary>
        /// <param name="key">The key identifying the shared value.</param>
        /// <returns>The shared value associated with the key.</returns>
        public TValue Get(TKey key)
        {
            if (!m_values.TryGetValue(key, out var value))
            {
                value = m_create(key);
                m_values.Add(key, value);
            }

            return value;
        }

        /// <summary>
        /// Releases stored references without disposing or destroying referenced objects.
        /// </summary>
        public void Clear() => m_values.Clear();
    }
}
