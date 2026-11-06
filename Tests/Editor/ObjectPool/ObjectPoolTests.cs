using System;
using MadByteLabs.Patterns;
using NUnit.Framework;

namespace MadByteLabs.Patterns.Tests.ObjectPool
{
    /// <summary>
    /// Provides object pool tests behavior for the pattern demonstration or tests.
    /// </summary>
    public sealed class ObjectPoolTests
    {
        /// <summary>
        /// Verifies that pool reuses and rejects foreign and double returns.
        /// </summary>
        [Test]
        public void PoolReusesAndRejectsForeignAndDoubleReturns()
        {
            using (var pool = new ObjectPool<object>(() => new object()))
            {
                var item = pool.Rent();
                Assert.Throws<InvalidOperationException>(() => pool.Return(new object()));
                pool.Return(item);
                Assert.Throws<InvalidOperationException>(() => pool.Return(item));
                var reusedItem = pool.Rent();
                Assert.AreSame(item, reusedItem);
                pool.Return(reusedItem);
            }
        }

        /// <summary>
        /// Verifies that pool destroys overflow and requires returned leases.
        /// </summary>
        [Test]
        public void PoolDestroysOverflowAndRequiresReturnedLeases()
        {
            int destroyed = 0;
            var pool = new ObjectPool<object>(() => new object(), onDestroy: item => destroyed++, capacity: 1);
            var firstLease = pool.Rent();
            var secondLease = pool.Rent();
            Assert.Throws<InvalidOperationException>(() => pool.Dispose());
            pool.Return(firstLease);
            pool.Return(secondLease);
            pool.Dispose();
            Assert.AreEqual(2, destroyed);
            Assert.Throws<ObjectDisposedException>(() => pool.Rent());
        }
    }
}
