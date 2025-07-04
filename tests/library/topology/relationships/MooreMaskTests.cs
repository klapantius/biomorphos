using System.Collections.Generic;
using NUnit.Framework;
using biomorphos.library.topology.relationships;

namespace biomorphos.tests.library.topology.relationships
{
    [TestFixture]
    public class MooreMaskTests
    {
        [Test]
        public void MooreMask_2D_Returns8Neighbors()
        {
            var moore = new MooreMask<TestUtils.DummyCoordinates>(2);
            var origin = new TestUtils.DummyCoordinates(0, 0);
            var expected = new HashSet<TestUtils.DummyCoordinates>
            {
                new(-1, -1), new(-1, 0), new(-1, 1),
                new(0, -1), /*origin */ new(0, 1),
                new(1, -1), new(1, 0), new(1, 1)
            };
            var actual = new HashSet<TestUtils.DummyCoordinates>(moore.GetRelated(origin));
            Assert.AreEqual(8, actual.Count);
            Assert.IsTrue(expected.SetEquals(actual), "Moore neighbors do not match expected set.");
        }

        [Test]
        public void MooreMask_3D_Returns26Neighbors()
        {
            var moore = new MooreMask<TestUtils.DummyCoordinates>(3);
            var origin = new TestUtils.DummyCoordinates(1, 1, 1);
            var actual = new HashSet<TestUtils.DummyCoordinates>(moore.GetRelated(origin));
            Assert.AreEqual(26, actual.Count);
            // Check that origin is not included
            Assert.IsFalse(actual.Contains(new TestUtils.DummyCoordinates(1, 1, 1)));
        }
    }
}
