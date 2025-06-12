namespace CellCultureSimulator
{
    /// <summary>
    /// Implements the classic rule: a cell is born if it has at least one alive neighbor, and an alive cell survives if it has at least 3 alive neighbors.
    /// </summary>
    public class ClassicTransitionRule : ICellTransitionRule
    {
        public CellState GetNextState(CellState current, int aliveNeighbors)
        {
            switch (current)
            {
                case CellState.NonExistent:
                    return aliveNeighbors > 0 ? CellState.WillBeBorn : CellState.NonExistent;
                case CellState.WillBeBorn:
                    return CellState.Alive;
                case CellState.Alive:
                    return aliveNeighbors < 3 ? CellState.WillDie : CellState.Alive;
                case CellState.WillDie:
                    return CellState.NonExistent;
                default:
                    return current;
            }
        }
    }
}
