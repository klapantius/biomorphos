using System.Linq;

namespace biomorphos.library.topology.coordinates
{
    /// <summary>
    /// A generic vector implementation for any number of dimensions, implementing ICoordinates.
    /// </summary>
    public class Cartesian : ICoordinates
    {
        public int[] Values { get; }
        public Cartesian(params int[] values) => Values = values;
        public int Dimensions => Values.Length;
        public int this[int index] => Values[index];
        public ICoordinates Offset(params int[] deltas)
        {
            var result = new int[Values.Length];
            for (int i = 0; i < Values.Length; i++)
                result[i] = Values[i] + (i < deltas.Length ? deltas[i] : 0);
            return new Cartesian(result);
        }
        public override bool Equals(object? obj) => obj is Cartesian v && Values.SequenceEqual(v.Values);
        public override int GetHashCode() => Values.Aggregate(0, (a, v) => a ^ v.GetHashCode());
    }
}
