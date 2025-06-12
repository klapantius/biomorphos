using System;

namespace CellCultureSimulator
{
    public class NeighborhoodTemplate
    {
        public int Size { get; }
        public bool[,] Mask { get; }
        public (int dx, int dy) Center => (Size / 2, Size / 2);

        public NeighborhoodTemplate(bool[,] mask)
        {
            if (mask.GetLength(0) != mask.GetLength(1))
                throw new ArgumentException("Neighborhood template must be square.");
            if (mask.GetLength(0) % 2 == 0)
                throw new ArgumentException("Neighborhood template size must be odd.");
            Size = mask.GetLength(0);
            Mask = mask;
        }
    }
}
