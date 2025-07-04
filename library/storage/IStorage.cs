using biomorphos.library.topology;

namespace biomorphos.library.storage
{
    /// <summary>
    /// Interface for cell storage, providing add, get, and remove operations by coordinates.
    /// </summary>
    public interface IStorage
    {
        /// <summary>
        /// Adds or replaces a cell at the given coordinates.
        /// </summary>
        void Add(ICoordinates coordinates, ICell cell);

        /// <summary>
        /// Gets the cell at the given coordinates, or null if not present.
        /// </summary>
        ICell? Get(ICoordinates coordinates);

        /// <summary>
        /// Removes the cell at the given coordinates, if present.
        /// </summary>
        void Remove(ICoordinates coordinates);
    }
}
