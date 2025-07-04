namespace biomorphos.library.topology.relationships
{
    /// <summary>
    /// Implements the Moore neighborhood topology (all adjacent cells, including diagonals) for any dimension.
    /// </summary>
    /// <typeparam name="TCoordinates">The coordinate type implementing <see cref="ICoordinates"/>.</typeparam>
    public class MooreMask<TCoordinates> : GenericMask<TCoordinates>
        where TCoordinates : ICoordinates
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MooreMask{TCoordinates}"/> class.
        /// </summary>
        /// <param name="dimensions">The number of dimensions for the neighborhood.</param>
        public MooreMask(int dimensions)
            : base(BuildMooreMask(dimensions))
        {
        }

        private static IEnumerable<int[]> BuildMooreMask(int dimensions)
        {
            // Generate all combinations of -1, 0, 1 for each dimension, except the all-zeros vector (the origin itself)
            int total = (int)Math.Pow(3, dimensions);
            for (int i = 0; i < total; i++)
            {
                int[] offset = new int[dimensions];
                int n = i;
                bool allZero = true;
                for (int d = 0; d < dimensions; d++)
                {
                    int v = (n % 3) - 1;
                    offset[d] = v;
                    if (v != 0) allZero = false;
                    n /= 3;
                }
                if (!allZero)
                    yield return offset;
            }
        }
    }
}
