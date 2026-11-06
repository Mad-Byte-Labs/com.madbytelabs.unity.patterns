namespace MadByteLabs.Patterns
{
    /// <summary>
    /// Exposes a computed numeric statistic that can be composed with decorators.
    /// </summary>
    public interface IStat
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        float Value { get; }
    }
}
