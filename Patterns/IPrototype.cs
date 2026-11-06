namespace MadByteLabs.Patterns
{
    /// <summary>
    /// Defines a cloning contract. Implementations specify whether referenced data is shared or copied.
    /// </summary>
    /// <typeparam name="T">The type of values used by this contract.</typeparam>
    public interface IPrototype<out T>
    {
        /// <summary>
        /// Creates a copy according to the prototype's cloning contract.
        /// </summary>
        /// <returns>A new clone owned by the caller.</returns>
        T Clone();
    }
}
