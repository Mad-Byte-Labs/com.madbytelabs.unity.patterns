using System;
using MadByteLabs.Patterns;
using NUnit.Framework;

namespace MadByteLabs.Patterns.Tests.EventQueue
{
    /// <summary>
    /// Provides event queue tests behavior for the pattern demonstration or tests.
    /// </summary>
    public sealed class EventQueueTests
    {
        /// <summary>
        /// Verifies that queue defers events enqueued by handler.
        /// </summary>
        [Test]
        public void QueueDefersEventsEnqueuedByHandler()
        {
            var queue = new EventQueue<int>();
            using (queue.Dispatched.Subscribe(x => queue.Enqueue(x + 1)))
            {
                queue.Enqueue(1);
                Assert.AreEqual(1, queue.Drain());
                Assert.AreEqual(1, queue.Count);
            }
        }

        /// <summary>
        /// Verifies that queue recovers after handler throws.
        /// </summary>
        [Test]
        public void QueueRecoversAfterHandlerThrows()
        {
            var queue = new EventQueue<int>();
            var subscription = queue.Dispatched.Subscribe(x => throw new Exception());
            queue.Enqueue(1);
            Assert.Throws<Exception>(() => queue.Drain());
            subscription.Dispose();
            queue.Enqueue(2);
            Assert.AreEqual(1, queue.Drain());
        }
    }
}
