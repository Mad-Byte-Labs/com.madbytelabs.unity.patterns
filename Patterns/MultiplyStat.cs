namespace MadByteLabs.Patterns
{
    /// <summary>
    /// Multiplies the wrapped statistic by a fixed factor.
    /// </summary>
    public sealed class MultiplyStat : StatDecorator
    {
        private readonly float m_multiplier;

        /// <summary>
        /// Initializes a new MultiplyStat instance with explicit dependencies.
        /// </summary>
        /// <param name="inner">The required statistic to wrap.</param>
        /// <param name="multiplier">The factor applied to the wrapped value.</param>
        public MultiplyStat(IStat inner, float multiplier) : base(inner)
        {
            m_multiplier = multiplier;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public override float Value => Inner.Value * m_multiplier;
    }
}
