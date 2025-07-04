using biomorphos.library.topology;

namespace biomorphos.library.storage
{
    /// <summary>
    /// Interface for cell storage, providing add, get, and remove operations by coordinates.
    /// </summary>
    public interface IStorage<TCoord> where TCoord : ICoordinates
    {
        /// <summary>
        /// Adds or replaces a cell at the given coordinates.
        /// </summary>
        void Add(TCoord coordinates, ICell cell);

        /// <summary>
        /// Gets the cell at the given coordinates, or null if not present.
        /// </summary>
        ICell? Get(TCoord coordinates);

        /// <summary>
        /// Removes the cell at the given coordinates, if present.
        /// </summary>
        void Remove(TCoord coordinates);
    }
}
