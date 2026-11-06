namespace MadByteLabs.Patterns
{
    /// <summary>
    /// Adds a fixed amount to the wrapped statistic.
    /// </summary>
    public sealed class AddStat : StatDecorator
    {
        private readonly float m_amount;

        /// <summary>
        /// Initializes a new AddStat instance with explicit dependencies.
        /// </summary>
        /// <param name="inner">The required statistic to wrap.</param>
        /// <param name="amount">The amount to add to the wrapped value.</param>
        public AddStat(IStat inner, float amount) : base(inner)
        {
            m_amount = amount;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public override float Value => Inner.Value + m_amount;
    }
}
