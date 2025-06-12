using System.IO;
using System.Text;

namespace CellCultureSimulator
{
    public class HtmlRenderer : IRenderer
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
}
