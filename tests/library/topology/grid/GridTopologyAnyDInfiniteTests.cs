using System.Linq;
using biomorphos.library.topology;
using biomorphos.library.topology.coordinates;
using biomorphos.library.topology.gridTopologies;
using NUnit.Framework;

namespace biomorphos.library.tests.topology.grid
{
    [TestFixture]
    public class GridTopologyAnyDInfiniteTests
    {
        [Test]
        public void Normalize_ReturnsInput_Unchanged()
        {
            var coord = new Cartesian(1, 2, 3);
            var grid = new GridTopologyAnyDInfinite<Cartesian>();
            var result = grid.Normalize(coord);
            Assert.AreSame(coord, result);
        }
    }
}
