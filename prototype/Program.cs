using System;
using CellCultureSimulator;

namespace CellCultureSimulator
{
    class Program
    {
        const int Size = 16;
        const int Iterations = 9;

        static void Main(string[] args)
        {
            // Command line: --neighborhood moore or --neighborhood vonneumann
            string neighborhood = "vonneumann";
            if (args.Length >= 2 && args[0] == "--neighborhood")
            {
                neighborhood = args[1].ToLower();
            }

            // Define a 3x3 von Neumann neighborhood (up, down, left, right)
            var vonNeumannMask = new bool[3, 3]
            {
                { false, true, false },
                { true,  false, true },
                { false, true, false }
            };

            // Define a 3x3 Moore neighborhood (all 8 surrounding cells)
            var mooreMask = new bool[3, 3]
            {
                { true,  true,  true },
                { true,  false, true },
                { true,  true,  true }
            };

            NeighborhoodTemplate template = neighborhood switch
            {
                "moore" => new NeighborhoodTemplate(mooreMask),
                _ => new NeighborhoodTemplate(vonNeumannMask)
            };

            var simulation = new Simulation(Size, Iterations, template);
            simulation.Run();
            IRenderer renderer = new HtmlRenderer();
            renderer.Render(simulation.GridHistory);
        }
    }
}
