using System.Text;

namespace CellCultureSimulator
{
    enum CellState
    {
        NonExistent,
        WillBeBorn,
        Alive,
        WillDie
    }

    class Program
    {
        const int Size = 16;
        const int Iterations = 9;

        static void Main()
        {
            var gridHistory = new CellState[Iterations][,];
            gridHistory[0] = InitializeGrid();

            for (int i = 1; i < Iterations; i++)
            {
                gridHistory[i] = ComputeNextIteration(gridHistory[i - 1]);
            }

            GenerateHtmlOutput(gridHistory);
        }

        static CellState[,] InitializeGrid()
        {
            var grid = new CellState[Size, Size];
            grid[Size / 2, Size / 2] = CellState.Alive;
            return grid;
        }

        static CellState[,] ComputeNextIteration(CellState[,] previousGrid)
        {
            var newGrid = (CellState[,])previousGrid.Clone();

            // Phase 1: Prepare births
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    if (previousGrid[x, y] == CellState.NonExistent)
                    {
                        if (HasAliveNeighbor(x, y, previousGrid))
                        {
                            newGrid[x, y] = CellState.WillBeBorn;
                        }
                    }
                }
            }

            // Phase 2: State transitions
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    switch (newGrid[x, y])
                    {
                        case CellState.WillBeBorn:
                            newGrid[x, y] = CellState.Alive;
                            break;
                        case CellState.Alive:
                            if (CountAliveNeighbors(x, y, previousGrid) < 3)
                            {
                                newGrid[x, y] = CellState.WillDie;
                            }
                            break;
                        case CellState.WillDie:
                            newGrid[x, y] = CellState.NonExistent;
                            break;
                    }
                }
            }

            return newGrid;
        }

        static bool HasAliveNeighbor(int x, int y, CellState[,] grid)
        {
            foreach (var (dx, dy) in new[] { (-1, 0), (1, 0), (0, -1), (0, 1) })
            {
                int nx = x + dx;
                int ny = y + dy;
                if (nx >= 0 && nx < Size && ny >= 0 && ny < Size)
                {
                    if (grid[nx, ny] == CellState.Alive)
                        return true;
                }
            }
            return false;
        }

        static int CountAliveNeighbors(int x, int y, CellState[,] grid)
        {
            int count = 0;
            foreach (var (dx, dy) in new[] { (-1, 0), (1, 0), (0, -1), (0, 1) })
            {
                int nx = x + dx;
                int ny = y + dy;
                if (nx >= 0 && nx < Size && ny >= 0 && ny < Size)
                {
                    if (grid[nx, ny] == CellState.Alive)
                        count++;
                }
            }
            return count;
        }

        static void GenerateHtmlOutput(CellState[][,] gridHistory)
        {
            var html = new StringBuilder();
            html.AppendLine("<html><head><style>");
            html.AppendLine(".grid { border-collapse: collapse; margin: 20px; }");
            html.AppendLine("td { width: 15px; height: 15px; border: 1px solid #ddd; }");
            html.AppendLine("</style></head><body>");

            foreach (var grid in gridHistory)
            {
                html.AppendLine("<table class='grid'>");
                for (int x = 0; x < Size; x++)
                {
                    html.AppendLine("<tr>");
                    for (int y = 0; y < Size; y++)
                    {
                        var color = grid[x, y] switch
                        {
                            CellState.NonExistent => "white",
                            CellState.WillBeBorn => "yellow",
                            CellState.Alive => "green",
                            CellState.WillDie => "saddlebrown",
                            _ => "white"
                        };
                        html.AppendLine($"<td style='background-color:{color}'></td>");
                    }
                    html.AppendLine("</tr>");
                }
                html.AppendLine("</table>");
            }

            html.AppendLine("</body></html>");
            File.WriteAllText("simulation.html", html.ToString());
        }
    }
}
