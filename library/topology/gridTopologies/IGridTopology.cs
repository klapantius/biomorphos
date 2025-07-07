namespace biomorphos.library.topology
{
    /// <summary>
    /// Describes the topology of the grid, including boundaries, dimensions, and coordinate normalization.
    /// Implementations depend on the applied ICoordinates system.
    /// </summary>
    public interface IGridTopology<TCoord> where TCoord : ICoordinates
    {
        TCoord Normalize(TCoord coord);
    }
}
