namespace CellCultureSimulator
{
    public class Simulation
    {
        public int Size { get; }
        public int Iterations { get; }
        public CellGrid[] GridHistory { get; }
        public NeighborhoodTemplate Template { get; }

        public Simulation(int size, int iterations, NeighborhoodTemplate template)
        {
            Size = size;
            Iterations = iterations;
            GridHistory = new CellGrid[iterations];
            Template = template;
        }

        public void Run()
        {
            var initialGrid = new CellGrid(Size, Template);
            initialGrid.InitializeCenterAlive();
            GridHistory[0] = initialGrid;

            for (int i = 1; i < Iterations; i++)
            {
                GridHistory[i] = GridHistory[i - 1].NextIteration();
            }
        }
    }
}
