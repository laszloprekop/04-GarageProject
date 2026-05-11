using System.Data;
using Terminal.Gui.ViewBase;
using Terminal.Gui.Views;

namespace GarageProject.UI;

public class ResultsDialog : Dialog
{
    public ResultsDialog(string title, DataTable results)
    {
        Title = title;
        Width = 64;
        Height = 18;

        var table = new TableView
        {
            X = 1, Y = 0,
            Width = Dim.Fill(1),
            Height = Dim.Fill(2),
            FullRowSelect = true,
        };
        table.Table = new DataTableSource(results);

        var closeButton = new Button
        {
            X = Pos.Center(), Y = Pos.AnchorEnd(1),
            Text = "Close",
        };
        closeButton.Accepted += (_, _) => RequestStop();

        Add(table, closeButton);
    }
}
