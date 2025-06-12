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

    class CellGrid
    {
        public readonly int Size;
        public CellState[,] Grid { get; private set; }

        public CellGrid(int size)
        {
            Size = size;
            Grid = new CellState[size, size];
        }

        public void InitializeCenterAlive()
        {
            Grid[Size / 2, Size / 2] = CellState.Alive;
        }

        public CellGrid NextIteration()
        {
            var newGrid = new CellGrid(Size);
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
            foreach (var (dx, dy) in new[] { (-1, 0), (1, 0), (0, -1), (0, 1) })
            {
                int nx = x + dx;
                int ny = y + dy;
                if (nx >= 0 && nx < Size && ny >= 0 && ny < Size)
                {
                    if (Grid[nx, ny] == CellState.Alive)
                        return true;
                }
            }
            return false;
        }

        private int CountAliveNeighbors(int x, int y)
        {
            int count = 0;
            foreach (var (dx, dy) in new[] { (-1, 0), (1, 0), (0, -1), (0, 1) })
            {
                int nx = x + dx;
                int ny = y + dy;
                if (nx >= 0 && nx < Size && ny >= 0 && ny < Size)
                {
                    if (Grid[nx, ny] == CellState.Alive)
                        count++;
                }
            }
            return count;
        }
    }

    class Simulation
    {
        public int Size { get; }
        public int Iterations { get; }
        public CellGrid[] GridHistory { get; }

        public Simulation(int size, int iterations)
        {
            Size = size;
            Iterations = iterations;
            GridHistory = new CellGrid[iterations];
        }

        public void Run()
        {
            var initialGrid = new CellGrid(Size);
            initialGrid.InitializeCenterAlive();
            GridHistory[0] = initialGrid;

            for (int i = 1; i < Iterations; i++)
            {
                GridHistory[i] = GridHistory[i - 1].NextIteration();
            }
        }
    }

    interface IRenderer
    {
        void Render(CellGrid[] gridHistory, string fileName = "simulation.html");
    }

    class HtmlRenderer : IRenderer
    {
        public void Render(CellGrid[] gridHistory, string fileName = "simulation.html")
        {
            var html = new StringBuilder();
            html.AppendLine("<html><head><style>");
            html.AppendLine(".grid { border-collapse: collapse; margin: 20px; }");
            html.AppendLine("td { width: 15px; height: 15px; border: 1px solid #ddd; }");
            html.AppendLine("</style></head><body>");

            foreach (var cellGrid in gridHistory)
            {
                var grid = cellGrid.Grid;
                html.AppendLine("<table class='grid'>");
                for (int x = 0; x < cellGrid.Size; x++)
                {
                    html.AppendLine("<tr>");
                    for (int y = 0; y < cellGrid.Size; y++)
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
            File.WriteAllText(fileName, html.ToString());
        }
    }

    class Program
    {
        const int Size = 16;
        const int Iterations = 9;

        static void Main()
        {
            var simulation = new Simulation(Size, Iterations);
            simulation.Run();
            IRenderer renderer = new HtmlRenderer();
            renderer.Render(simulation.GridHistory);
        }
    }
}
