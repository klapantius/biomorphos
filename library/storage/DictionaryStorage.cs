using biomorphos.library.topology;

namespace biomorphos.library.storage
{
    /// <summary>
    /// Dictionary-based storage for cells, supporting any ICoordinates implementation.
    /// Suitable for sparse or unbounded grids.
    /// </summary>
    public class DictionaryStorage<TCoord> : IStorage<TCoord> where TCoord : ICoordinates
    {
        private readonly Dictionary<TCoord, ICell> _dict = new();

        public void Add(TCoord coordinates, ICell cell)
        {
            _dict[coordinates] = cell;
        }

        public ICell? Get(TCoord coordinates)
        {
            _dict.TryGetValue(coordinates, out var cell);
            return cell;
        }

        public void Remove(TCoord coordinates)
        {
            _dict.Remove(coordinates);
        }
    }
}
