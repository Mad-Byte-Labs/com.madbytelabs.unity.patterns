using System;

namespace MadByteLabs.Patterns
{
    /// <summary>
    /// Coordinates state lifecycle calls. Nested entry and exit transitions are rejected.
    /// </summary>
    public sealed class StateMachine
    {
        /// <summary>
        /// Gets the current.
        /// </summary>
        public IState Current { get; private set; }

        private bool m_transitioning;

        /// <summary>
        /// Exits the current state and enters the next state. Passing null stops the machine.
        /// </summary>
        /// <param name="next">The next state, or null to leave the machine inactive.</param>
        public void Change(IState next)
        {
            if (m_transitioning)
            {
                throw new InvalidOperationException("Nested state transitions are not supported.");
            }

            if (ReferenceEquals(Current, next))
            {
                return;
            }

            m_transitioning = true;
            try
            {
                Current?.Exit();
                Current = next;
                Current?.Enter();
            }
            finally
            {
                m_transitioning = false;
            }
        }

        /// <summary>
        /// Advances the active state's simulation by the supplied time step.
        /// </summary>
        /// <param name="deltaTimeSeconds">The simulation time step in seconds, supplied by the caller.</param>
        public void Tick(float deltaTimeSeconds) => Current?.Tick(deltaTimeSeconds);
    }
}
