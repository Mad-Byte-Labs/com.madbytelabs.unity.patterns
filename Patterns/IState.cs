namespace MadByteLabs.Patterns
{
    /// <summary>
    /// Defines entry, simulation, and exit operations for a state.
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// Initializes behavior when the state becomes active.
        /// </summary>
        void Enter();

        /// <summary>
        /// Advances the active state's simulation by the supplied time step.
        /// </summary>
        /// <param name="deltaTimeSeconds">The simulation time step in seconds, supplied by the caller.</param>
        void Tick(float deltaTimeSeconds);

        /// <summary>
        /// Releases behavior when the state becomes inactive.
        /// </summary>
        void Exit();
    }
}
