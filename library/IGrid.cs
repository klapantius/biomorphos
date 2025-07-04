using biomorphos.library.storage;
using biomorphos.library.topology;

namespace biomorphos.library
{
    /// <summary>
    /// Interface for a grid that maintains a storage of cells, a grid topology, and a relationship topology.
    /// All components use the same ICoordinates implementation.
    /// </summary>
    public interface IGrid
    {
        /// <summary>
        /// The storage for cells in the grid.
        /// </summary>
        IStorage Storage { get; }

        /// <summary>
        /// The topology of the grid (boundaries, normalization, etc).
        /// </summary>
        IGridTopology GridTopology { get; }

        /// <summary>
        /// The relationship topology (e.g., neighborhood) for cell connections.
        /// </summary>
        IRelationshipTopology RelationshipTopology { get; }

        /// <summary>
        /// An enumerable of all cells in the grid.
        /// </summary>
        IEnumerable<ICell> Cells { get; }
        
        /// <summary>
        /// Returns the related cells for the given cell. The coordinates will be calculated
        /// by the relationship topology and normalized to the grid.
        /// </summary>
        /// <param name="cell">The cell for which to find related coordinates.</param>
        /// <returns>An enumerable of valid, normalized coordinates of related cells.</returns>
        IEnumerable<ICell> GetRelated(ICell cell);
    }
}
