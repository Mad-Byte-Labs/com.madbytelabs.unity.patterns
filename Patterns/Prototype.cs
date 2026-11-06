using System;

namespace MadByteLabs.Patterns
{
    /// <summary>
    /// Clones Unity objects using Instantiate semantics. The caller owns and destroys each clone.
    /// </summary>
    public static class Prototype
    {
        /// <summary>
        /// Creates a copy according to the prototype's cloning contract.
        /// </summary>
        /// <typeparam name="T">The type of values used by this contract.</typeparam>
        /// <param name="source">The required live Unity object to clone.</param>
        /// <returns>A new clone owned by the caller.</returns>
        public static T Clone<T>(T source)
            where T : UnityEngine.Object
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return UnityEngine.Object.Instantiate(source);
        }
    }
}
