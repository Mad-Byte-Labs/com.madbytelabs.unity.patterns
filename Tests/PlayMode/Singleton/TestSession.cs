namespace MadByteLabs.Patterns.Tests.Singleton
{
    /// <summary>
    /// Provides test session behavior for the pattern demonstration or tests.
    /// </summary>
    public sealed class TestSession : MadByteLabs.Patterns.Singleton<TestSession>
    {
        /// <summary>
        /// Gets whether this scene-scoped instance survives scene changes.
        /// </summary>
        protected override bool PersistBetweenScenes => false;
    }
}
