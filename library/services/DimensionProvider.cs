namespace biomorphos.library.services
{
    public interface IDimensionProvider
    {
        int[] Dimensions { get; }
        bool ValidateDimensionCount(int count);
    }

    /// <summary>
    /// Provides the number of dimensions and their sizes for different grid topologies.<br/>
    /// Use the <see cref="DimensionProviderFactory"/> to get/create the instance.<br/>
    /// Tests may use the Create() method to maintain an encapsulated instance.
    /// </summary>
    public class DimensionProvider : IDimensionProvider
    {
        /// <summary>
        /// The count() or length of this array describes the number of dimensions.<br/>
        /// Each element of the array describes the size of the corresponding dimension.<br/>
        /// A -1 value indicates that the dimension is unbounded (infinite) in that direction.<br/>
        /// For example, a 2D grid with sizes [10, -1] has 10 cells in the first dimension<br/>
        /// and is infinite in the second dimension.
        /// </summary>
        public int[] Dimensions { get; }

        /// <summary>
        /// create the provider with specific sizes for each dimension
        /// </summary>
        /// <param name="dimensions"></param>
        internal DimensionProvider(params int[] dimensions)
        {
            if (dimensions == null || dimensions.Length == 0)
            {
                throw new ArgumentException("Dimensions must be a non-empty array.", nameof(dimensions));
            }
            Dimensions = dimensions;
        }

        /// <summary>
        /// create the provider with the number of dimensions, all set to -1 (unbounded)
        /// </summary>
        internal DimensionProvider(int dimensionCount)
        {
            if (dimensionCount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(dimensionCount), "Dimension count cannot be negative.");
            }
            Dimensions = new int[dimensionCount];
            for (int i = 0; i < dimensionCount; i++)
            {
                Dimensions[i] = -1; // unbounded}
            }
        }

        public bool ValidateDimensionCount(int count)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Dimension count cannot be negative.");
            }
            return Dimensions.Length == count;
        }
    }
}