using UnityEngine;

namespace MadByteLabs.Patterns
{
    /// <summary>
    /// Registers an explicitly placed scene instance. Use a dedicated root GameObject and initialization hooks.
    /// </summary>
    /// <typeparam name="T">The type of values used by this contract.</typeparam>
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        /// <summary>
        /// Gets the registered scene instance, or null before initialization and after destruction.
        /// </summary>
        public static T Instance { get; private set; }

        /// <summary>
        /// Gets whether the dedicated root GameObject survives scene changes.
        /// </summary>
        protected virtual bool PersistBetweenScenes => true;

        /// <summary>
        /// Registers this instance and rejects duplicates. Override OnInitialized instead of replacing this callback.
        /// </summary>
        protected virtual void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = (T)this;
            if (PersistBetweenScenes)
            {
                DontDestroyOnLoad(gameObject);
            }

            OnInitialized();
        }

        /// <summary>
        /// Clears the registered instance and invokes its release hook.
        /// </summary>
        protected virtual void OnDestroy()
        {
            if (Instance != this)
            {
                return;
            }

            Instance = null;
            OnReleased();
        }

        /// <summary>
        /// Runs once after this instance is registered. Override to initialize owned behavior.
        /// </summary>
        protected virtual void OnInitialized()
        {
        }

        /// <summary>
        /// Runs when the registered instance is destroyed. Override to release owned behavior.
        /// </summary>
        protected virtual void OnReleased()
        {
        }
    }
}
