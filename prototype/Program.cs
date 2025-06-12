using System;
using System.IO;
using System.Text;

namespace CellCultureSimulator
{
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
