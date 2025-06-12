using System;

namespace CellCultureSimulator
{
    public class CellGrid
    {
        public readonly int Size;
        public CellState[,] Grid { get; private set; }
        private readonly NeighborhoodTemplate _template;
        private readonly ICellTransitionRule _transitionRule = new ClassicTransitionRule();

        public CellGrid(int size, NeighborhoodTemplate template)
        {
            Size = size;
            Grid = new CellState[size, size];
            _template = template;
        }

        public void InitializeCenterAlive()
        {
            Grid[Size / 2, Size / 2] = CellState.Alive;
        }

        public CellGrid NextIteration()
        {
            var newGrid = new CellGrid(Size, _template);
            Array.Copy(Grid, newGrid.Grid, Grid.Length);

            // Phase 1: Prepare births
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    int aliveNeighbors = CountAliveNeighbors(x, y);
                    newGrid.Grid[x, y] = _transitionRule.GetNextState(Grid[x, y], aliveNeighbors);
                }
            }
            return newGrid;
        }

        private bool HasAliveNeighbor(int x, int y)
        {
            int center = _template.Size / 2;
            for (int dx = 0; dx < _template.Size; dx++)
            {
                for (int dy = 0; dy < _template.Size; dy++)
                {
                    if (dx == center && dy == center) continue;
                    if (!_template.Mask[dx, dy]) continue;
                    int nx = x + dx - center;
                    int ny = y + dy - center;
                    if (nx >= 0 && nx < Size && ny >= 0 && ny < Size)
                    {
                        if (Grid[nx, ny] == CellState.Alive)
                            return true;
                    }
                }
            }
            return false;
        }

        private int CountAliveNeighbors(int x, int y)
        {
            int count = 0;
            int center = _template.Size / 2;
            for (int dx = 0; dx < _template.Size; dx++)
            {
                for (int dy = 0; dy < _template.Size; dy++)
                {
                    if (dx == center && dy == center) continue;
                    if (!_template.Mask[dx, dy]) continue;
                    int nx = x + dx - center;
                    int ny = y + dy - center;
                    if (nx >= 0 && nx < Size && ny >= 0 && ny < Size)
                    {
                        if (Grid[nx, ny] == CellState.Alive)
                            count++;
                    }
                }
            }
            return count;
        }
    }
}
