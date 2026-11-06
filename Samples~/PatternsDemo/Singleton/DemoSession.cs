using MadByteLabs.Patterns;

namespace MadByteLabs.Patterns.Samples.Singleton
{
    /// <summary>
    /// Provides demo session behavior for the pattern demonstration or tests.
    /// </summary>
    public sealed class DemoSession : Singleton<DemoSession>
    {
        /// <summary>
        /// Gets whether this scene-scoped instance survives scene changes.
        /// </summary>
        protected override bool PersistBetweenScenes => false;
    }
}
