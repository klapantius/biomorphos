namespace biomorphos.library.topology
{
    /// <summary>
    /// Represents coordinates in a grid or topology. Can be implemented for 2D, 3D, or other systems.
    /// </summary>
    public interface ICoordinates
    {
        /// <summary>
        /// Returns the dimension count (e.g., 2 for 2D, 3 for 3D).
        /// </summary>
        int Dimensions { get; }

        /// <summary>
        /// Gets the value of the coordinate at the given dimension index.
        /// </summary>
        int this[int index] { get; }

        /// <summary>
        /// Returns a new coordinates object offset by the given deltas.
        /// </summary>
        ICoordinates Offset(params int[] deltas);
    }
}
