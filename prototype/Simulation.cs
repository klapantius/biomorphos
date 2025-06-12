namespace CellCultureSimulator
{
    public class Simulation
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
}
