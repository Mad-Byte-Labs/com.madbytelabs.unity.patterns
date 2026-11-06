using System.Collections.Generic;
using MadByteLabs.Patterns;

namespace MadByteLabs.Patterns.Tests.State
{
    /// <summary>
    /// Provides recording state behavior for the pattern demonstration or tests.
    /// </summary>
    internal sealed class RecordingState : IState
    {
        private readonly string m_name;
        private readonly List<string> m_log;

        /// <summary>
        /// Initializes a new RecordingState instance with explicit dependencies.
        /// </summary>
        /// <param name="name">The name used by this operation.</param>
        /// <param name="log">The log used by this operation.</param>
        internal RecordingState(string name, List<string> log)
        {
            m_name = name;
            m_log = log;
        }

        /// <summary>
        /// Initializes behavior when the state becomes active.
        /// </summary>
        public void Enter() => m_log.Add(m_name + "+");

        /// <summary>
        /// Releases behavior when the state becomes inactive.
        /// </summary>
        public void Exit() => m_log.Add(m_name + "-");

        /// <summary>
        /// Advances the active state's simulation by the supplied time step.
        /// </summary>
        /// <param name="deltaTimeSeconds">The simulation time step in seconds.</param>
        public void Tick(float deltaTimeSeconds)
        {
        }
    }
}
