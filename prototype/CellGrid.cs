using System;

namespace CellCultureSimulator
{
    public class CellGrid
    {
        public readonly int Size;
        public CellState[,] Grid { get; private set; }
        private readonly NeighborhoodTemplate _template;

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
                    if (Grid[x, y] == CellState.NonExistent)
                    {
                        if (HasAliveNeighbor(x, y))
                        {
                            newGrid.Grid[x, y] = CellState.WillBeBorn;
                        }
                    }
                }
            }

            // Phase 2: State transitions
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    switch (newGrid.Grid[x, y])
                    {
                        case CellState.WillBeBorn:
                            newGrid.Grid[x, y] = CellState.Alive;
                            break;
                        case CellState.Alive:
                            if (CountAliveNeighbors(x, y) < 3)
                            {
                                newGrid.Grid[x, y] = CellState.WillDie;
                            }
                            break;
                        case CellState.WillDie:
                            newGrid.Grid[x, y] = CellState.NonExistent;
                            break;
                    }
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
