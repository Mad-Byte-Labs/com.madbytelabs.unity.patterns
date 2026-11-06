using System;

namespace MadByteLabs.Patterns
{
    /// <summary>
    /// Defines a statistic that wraps another statistic.
    /// </summary>
    public abstract class StatDecorator : IStat
    {
        /// <summary>
        /// Gets the inner.
        /// </summary>
        protected IStat Inner { get; }

        /// <summary>
        /// Initializes a new StatDecorator instance with explicit dependencies.
        /// </summary>
        /// <param name="inner">The required statistic to wrap.</param>
        protected StatDecorator(IStat inner)
        {
            Inner = inner ?? throw new ArgumentNullException(nameof(inner));
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public abstract float Value { get; }
    }
}
