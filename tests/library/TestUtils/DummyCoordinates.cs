using biomorphos.library.topology;
using System;
using System.Diagnostics;
using System.Linq;

namespace biomorphos.tests.library.TestUtils
{
    /// <summary>
    /// Simple test implementation of ICoordinates for unit testing purposes.
    /// </summary>
    [DebuggerDisplay("{ToString(),nq}")]
    public class DummyCoordinates : ICoordinates
    {
        private readonly int[] _coords;
        public DummyCoordinates(params int[] coords) { _coords = coords; }
        public int Dimensions => _coords.Length;
        public int this[int index] => _coords[index];
        public ICoordinates Offset(params int[] deltas)
        {
            int[] result = new int[_coords.Length];
            for (int i = 0; i < _coords.Length; i++)
                result[i] = _coords[i] + deltas[i];
            return new DummyCoordinates(result);
        }
        public override bool Equals(object obj)
        {
            if (obj is DummyCoordinates other && other.Dimensions == Dimensions)
            {
                for (int i = 0; i < Dimensions; i++)
                    if (_coords[i] != other[i]) return false;
                return true;
            }
            return false;
        }
        public override int GetHashCode() => HashCode.Combine(_coords.Length, _coords.Aggregate(0, (a, v) => a * 31 + v));

        public override string ToString() => $"[{string.Join(", ", _coords)}]";
    }
}
