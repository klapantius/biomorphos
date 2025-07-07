using biomorphos.library.services;
using biomorphos.library.topology;

namespace biomorphos.library.storage
{
    /// <summary>
    /// Dense array-based storage for cells, supporting only non-negative integer coordinates in all dimensions.
    /// Suitable for small, dense fix-size grids.
    /// </summary>
    public class DenseArrayStorage<TCoord> : IStorage<TCoord> where TCoord : ICoordinates
    {
        private readonly int[] _dimensions;
        private readonly ICell?[] _cells;

        /// <summary>
        /// Initializes a new instance of the <see cref="DenseArrayStorage{TCoord}"/> class.
        /// </summary>
        /// <param name="dimensionProvider">The size of the grid in each dimension (length = number of dimensions).</param>
        public DenseArrayStorage(IDimensionProvider dimensionProvider)
        {
            if (dimensionProvider == null)
            {
                throw new ArgumentNullException(nameof(dimensionProvider), "Dimension provider cannot be null.");
            }
            _dimensions = (int[])dimensionProvider.Dimensions.Clone();
            int total = 1;
            foreach (var s in _dimensions)
            {
                if (s <= 0) throw new ArgumentException("All sizes must be positive.");
                total *= s;
            }
            _cells = new ICell?[total];
        }

        internal int GetIndex(TCoord coord)
        {
            if (coord.Dimensions != _dimensions.Length)
                throw new ArgumentException($"Coordinate dimensions ({coord.Dimensions}) do not match storage dimensions ({_dimensions.Length}).");
            int index = 0;
            int stride = 1;
            for (int d = 0; d < _dimensions.Length; d++)
            {
                int v = coord[d];
                if (v < 0 || v >= _dimensions[d])
                    throw new ArgumentOutOfRangeException($"Coordinate value {v} out of bounds for dimension {d} (size {_dimensions[d]}).\nCoordinate: {coord}");
                index += v * stride;
                stride *= _dimensions[d];
            }
            return index;
        }

        public void Add(TCoord coordinates, ICell cell)
        {
            _cells[GetIndex(coordinates)] = cell;
        }

        public ICell? Get(TCoord coordinates)
        {
            return _cells[GetIndex(coordinates)];
        }

        public void Remove(TCoord coordinates)
        {
            _cells[GetIndex(coordinates)] = null;
        }
    }
}
