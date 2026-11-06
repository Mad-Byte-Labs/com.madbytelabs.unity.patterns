using System;
using UnityEngine;

namespace MadByteLabs.Patterns
{
    /// <summary>
    /// Instantiates a required Unity component prefab. The caller owns created instances.
    /// </summary>
    /// <typeparam name="T">The type of values used by this contract.</typeparam>
    public sealed class PrefabFactory<T> : IFactory<T> where T : UnityEngine.Component
    {
        private readonly T m_prefab;
        private readonly Transform m_parent;

        /// <summary>
        /// Initializes a new PrefabFactory instance with explicit dependencies.
        /// </summary>
        /// <param name="prefab">The required live component prefab to instantiate.</param>
        /// <param name="parent">The optional parent for new instances, or null for no parent.</param>
        public PrefabFactory(T prefab, Transform parent = null)
        {
            if (prefab == null)
            {
                throw new ArgumentNullException(nameof(prefab));
            }

            m_prefab = prefab;
            m_parent = parent;
        }

        /// <summary>
        /// Creates a value using the configured creation strategy. The caller owns the result.
        /// </summary>
        /// <returns>The newly created value or Unity object instance.</returns>
        public T Create() => UnityEngine.Object.Instantiate(m_prefab, m_parent);
    }
}
