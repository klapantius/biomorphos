namespace CellCultureSimulator
{
    public interface ICellTransitionRule
    {
        /// <summary>
        /// Determines the next state of a cell based on its current state and the number of alive neighbors.
        /// </summary>
        /// <param name="current">The current state of the cell.</param>
        /// <param name="aliveNeighbors">The number of alive neighbors.</param>
        /// <returns>The next state of the cell.</returns>
        CellState GetNextState(CellState current, int aliveNeighbors);
    }
}
