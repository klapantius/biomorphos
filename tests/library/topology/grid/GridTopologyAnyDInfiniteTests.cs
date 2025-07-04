using System.Linq;
using biomorphos.library.topology;
using biomorphos.library.topology.gridTopologies;
using NUnit.Framework;

namespace biomorphos.library.tests.topology.grid
{
    public class DummyCoordinates : ICoordinates
    {
        public int[] Values { get; }
        public DummyCoordinates(params int[] values) => Values = values;
        public int Dimension => Values.Length;
        public int this[int index] => Values[index];
        public ICoordinates Offset(params int[] deltas)
        {
            var result = new int[Values.Length];
            for (int i = 0; i < Values.Length; i++)
                result[i] = Values[i] + (i < deltas.Length ? deltas[i] : 0);
            return new DummyCoordinates(result);
        }
        public override bool Equals(object obj) => obj is DummyCoordinates dc && Values.SequenceEqual(dc.Values);
        public override int GetHashCode() => Values.Aggregate(0, (a, v) => a ^ v.GetHashCode());
    }

    [TestFixture]
    public class GridTopologyAnyDInfiniteTests
    {
        [Test]
        public void Normalize_ReturnsInput_Unchanged()
        {
            var coord = new DummyCoordinates(1, 2, 3);
            var grid = new GridTopologyAnyDInfinite<DummyCoordinates>();
            var result = grid.Normalize(coord);
            Assert.AreSame(coord, result);
        }
    }
}
