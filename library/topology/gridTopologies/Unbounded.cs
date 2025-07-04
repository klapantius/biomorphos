namespace biomorphos.library.topology.gridTopologies
{
    /// <summary>
    /// An infinite grid topology implementation for any number of dimensions.
    /// The Normalize method is a null-function (returns the input coordinate).
    /// </summary>
    public class Unbounded<TCoord> : IGridTopology<TCoord> where TCoord : ICoordinates
    {
        public TCoord Normalize(TCoord coord)
        {
            // No normalization needed for infinite grid
            return coord;
        }
    }
}
