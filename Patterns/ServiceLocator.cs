using System.Collections.Generic;
using System;

namespace MadByteLabs.Patterns
{
    /// <summary>
    /// Stores services by their registered contract type. The registering owner manages service disposal.
    /// </summary>
    public sealed class ServiceLocator
    {
        private readonly Dictionary<Type, object> m_services = new Dictionary<Type, object>();

        /// <summary>
        /// Registers a required service by its contract type. Duplicate contracts are rejected.
        /// </summary>
        /// <typeparam name="T">The type of values used by this contract.</typeparam>
        /// <param name="service">The required service instance to register.</param>
        public void Register<T>(T service)
            where T : class
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (m_services.ContainsKey(typeof(T)))
            {
                throw new InvalidOperationException("Service already registered: " + typeof(T));
            }

            m_services.Add(typeof(T), service);
        }

        /// <summary>
        /// Retrieves a registered service. Throws if the requested contract is not registered.
        /// </summary>
        /// <typeparam name="T">The type of values used by this contract.</typeparam>
        /// <returns>The service registered under the requested contract.</returns>
        public T Get<T>()
            where T : class
        {
            if (!TryGet<T>(out var service))
            {
                throw new InvalidOperationException("Service not registered: " + typeof(T));
            }

            return service;
        }

        /// <summary>
        /// Attempts to retrieve a service without throwing when the contract is unregistered.
        /// </summary>
        /// <typeparam name="T">The type of values used by this contract.</typeparam>
        /// <param name="service">The service instance; null is returned when no registration exists in TryGet.</param>
        /// <returns>True when the contract is registered; otherwise false and the output service is null.</returns>
        public bool TryGet<T>(out T service)
            where T : class
        {
            bool found = m_services.TryGetValue(typeof(T), out var value);
            service = found ? (T)value : null;
            return found;
        }

        /// <summary>
        /// Removes a service registration without disposing the service.
        /// </summary>
        /// <typeparam name="T">The type of values used by this contract.</typeparam>
        /// <returns>True if a registration was removed; otherwise false.</returns>
        public bool Unregister<T>()
            where T : class => m_services.Remove(typeof(T));

        /// <summary>
        /// Releases stored references without disposing or destroying referenced objects.
        /// </summary>
        public void Clear() => m_services.Clear();
    }
}
