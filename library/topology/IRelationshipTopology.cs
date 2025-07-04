using System.Collections.Generic;

namespace biomorphos.library.topology
{
    /// <summary>
    /// Describes the relationship (e.g., neighborhood) between cells in a grid.
    /// Implementations depend on the applied ICoordinates system.
    /// </summary>
    public interface IRelationshipTopology
    {
        /// <summary>
        /// Returns the related coordinates (e.g., neighbors) for a given coordinate.
        /// </summary>
        /// <param name="origin">The coordinate for which to find related coordinates.</param>
        /// <returns>An enumerable of related coordinates.</returns>
        IEnumerable<ICoordinates> GetRelated(ICoordinates origin);
    }
}
