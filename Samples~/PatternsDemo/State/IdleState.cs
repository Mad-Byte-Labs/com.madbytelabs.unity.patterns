using MadByteLabs.Patterns;
using UnityEngine;

namespace MadByteLabs.Patterns.Samples.State
{
    /// <summary>
    /// Provides idle state behavior for the pattern demonstration or tests.
    /// </summary>
    internal sealed class IdleState : IState
    {
        /// <summary>
        /// Initializes behavior when the state becomes active.
        /// </summary>
        public void Enter()
        {
            Debug.Log("State: entered idle");
        }

        /// <summary>
        /// Advances the active state's simulation by the supplied time step.
        /// </summary>
        /// <param name="deltaTimeSeconds">The simulation time step in seconds, supplied by the caller.</param>
        public void Tick(float deltaTimeSeconds)
        {
        }

        /// <summary>
        /// Releases behavior when the state becomes inactive.
        /// </summary>
        public void Exit()
        {
            Debug.Log("State: exited idle");
        }
    }
}
