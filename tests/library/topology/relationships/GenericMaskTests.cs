using System.Collections.Generic;
using NUnit.Framework;
using biomorphos.library.topology.relationships;

namespace biomorphos.tests.library.topology.relationships
{
    [TestFixture]
    public class GenericMaskTests
    {
        // ...removed DummyCoordinates, now using shared test utility...

        private class TestMask : GenericMask<TestUtils.DummyCoordinates>
        {
            public TestMask(IEnumerable<int[]> mask) : base(mask) { }
        }

        [Test]
        public void GetRelated_ReturnsCorrectOffsets_2D()
        {
            // 2D mask: right, down, left, up
            var mask = new List<int[]> {
                new[] { 1, 0 },
                new[] { 0, 1 },
                new[] { -1, 0 },
                new[] { 0, -1 }
            };
            var rel = new TestMask(mask);
            var origin = new TestUtils.DummyCoordinates(5, 5);
            var expected = new List<TestUtils.DummyCoordinates> {
                new(6, 5),
                new(5, 6),
                new(4, 5),
                new(5, 4)
            };
            CollectionAssert.AreEquivalent(expected, rel.GetRelated(origin));
        }

        [Test]
        public void GetRelated_EmptyMask_ReturnsEmpty()
        {
            var rel = new TestMask(new List<int[]>());
            var origin = new TestUtils.DummyCoordinates(0, 0);
            CollectionAssert.IsEmpty(rel.GetRelated(origin));
        }

        [Test]
        public void GetRelated_3D_Mask()
        {
            // 3D mask: +x, +y, +z
            var mask = new List<int[]> {
                new[] { 1, 0, 0 },
                new[] { 0, 1, 0 },
                new[] { 0, 0, 1 }
            };
            var rel = new TestMask(mask);
            var origin = new TestUtils.DummyCoordinates(1, 2, 3);
            var expected = new List<TestUtils.DummyCoordinates> {
                new(2, 2, 3),
                new(1, 3, 3),
                new(1, 2, 4)
            };
            CollectionAssert.AreEquivalent(expected, rel.GetRelated(origin));
        }
    }
}
