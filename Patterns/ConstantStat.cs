namespace MadByteLabs.Patterns
{
    /// <summary>
    /// Provides an immutable base value for a statistic.
    /// </summary>
    public sealed class ConstantStat : IStat
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        public float Value { get; }

        /// <summary>
        /// Initializes a new ConstantStat instance with explicit dependencies.
        /// </summary>
        /// <param name="value">The value to publish, enqueue, or use as a base statistic.</param>
        public ConstantStat(float value)
        {
            Value = value;
        }
    }
}
