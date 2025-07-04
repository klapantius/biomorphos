namespace biomorphos.library.topology.relationships
{
    /// <summary>
    /// A generic, mask-based implementation of <see cref="IRelationshipTopology{TCoordinates}"/>.
    /// The mask defines the relative coordinate offsets of neighbors for any given cell.
    /// This class is intended to be used as a base for specific relationship topologies (e.g., VonNeumann, Moore).
    /// </summary>
    /// <typeparam name="TCoordinates">The coordinate type implementing <see cref="ICoordinates"/>.</typeparam>
    public abstract class GenericMask<TCoordinates> : IRelationshipTopology<TCoordinates>
        where TCoordinates : ICoordinates
    {
        /// <summary>
        /// The set of relative coordinate offsets that define the neighborhood mask.
        /// Each offset is an array of integers, one per dimension.
        /// </summary>
        protected readonly IReadOnlyList<int[]> Mask;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericMask{TCoordinates}"/> class.
        /// </summary>
        /// <param name="mask">A list of relative coordinate offsets (as int arrays) representing the neighborhood mask.</param>
        protected GenericMask(IEnumerable<int[]> mask)
        {
            if (mask == null) throw new ArgumentNullException(nameof(mask));
            Mask = new List<int[]>(mask).AsReadOnly();
        }

        /// <summary>
        /// Returns the related coordinates (e.g., neighbors) of the given coordinate, as defined by the mask.
        /// </summary>
        /// <param name="origin">The coordinate whose related coordinates are to be found.</param>
        /// <returns>An enumerable of related coordinates.</returns>
        public virtual IEnumerable<TCoordinates> GetRelated(TCoordinates origin)
        {
            foreach (var offset in Mask)
            {
                // origin.Offset returns ICoordinates, so cast to TCoordinates
                yield return (TCoordinates)origin.Offset(offset);
            }
        }
    }
}
