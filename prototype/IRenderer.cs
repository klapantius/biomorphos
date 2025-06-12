namespace CellCultureSimulator
{
    public interface IRenderer
    {
        void Render(CellGrid[] gridHistory, string fileName = "simulation.html");
    }
}
