using System.Collections.Generic;

namespace MadByteLabs.Patterns
{
    /// <summary>
    /// Compares pooled objects by reference identity rather than value equality.
    /// </summary>
    /// <typeparam name="T">The type of values used by this contract.</typeparam>
    internal sealed class ReferenceIdentityComparer<T> : IEqualityComparer<T>
    {
        /// <summary>
        /// Determines whether two references identify the same object.
        /// </summary>
        /// <param name="x">The first object reference, which may be null.</param>
        /// <param name="y">The second object reference, which may be null.</param>
        /// <returns>True when both references identify the same object; otherwise false.</returns>
        public bool Equals(T x, T y) => ReferenceEquals(x, y);

        /// <summary>
        /// Returns a hash code based on reference identity.
        /// </summary>
        /// <param name="value">The non-null object whose reference identity is hashed.</param>
        /// <returns>The object's identity hash code.</returns>
        public int GetHashCode(T value) => System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(value);
    }
}
