using System.Collections.Generic;

namespace biomorphos.library.topology
{
    /// <summary>
    /// Describes the topology of the grid, including boundaries, dimensions, and coordinate normalization.
    /// Implementations depend on the applied ICoordinates system.
    /// </summary>
    public interface IGridTopology
    {
        /// <summary>
        /// Returns the dimension count of the grid (e.g., 2 for 2D, 3 for 3D).
        /// </summary>
        int Dimensions { get; }

        /// <summary>
        /// Returns the size of the grid in each dimension.
        /// </summary>
        int[] Sizes { get; }

        /// <summary>
        /// Normalizes a coordinate so that it is valid for the grid (e.g., wrap-around, clamping).
        /// </summary>
        /// <param name="coord">The coordinate to normalize.</param>
        /// <returns>A valid coordinate for the grid.</returns>
        ICoordinates Normalize(ICoordinates coord);

        /// <summary>
        /// Returns the related coordinates (e.g., neighbors) for a given coordinate, normalized for the grid.
        /// </summary>
        /// <param name="origin">The coordinate for which to find related coordinates.</param>
        /// <returns>An enumerable of valid, normalized related coordinates.</returns>
        IEnumerable<ICoordinates> GetRelated(ICoordinates origin);
    }
}
