namespace MadByteLabs.Patterns
{
    /// <summary>
    /// Defines creation without exposing a concrete construction strategy.
    /// </summary>
    /// <typeparam name="T">The type of values used by this contract.</typeparam>
    public interface IFactory<out T>
    {
        /// <summary>
        /// Creates a value using the configured creation strategy. The caller owns the result.
        /// </summary>
        /// <returns>The newly created value or Unity object instance.</returns>
        T Create();
    }
}
