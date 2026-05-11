using System.Data;
using GarageProject.Application;
using GarageProject.Domain;
using Terminal.Gui.Input;
using Terminal.Gui.ViewBase;
using Terminal.Gui.Views;

namespace GarageProject.UI;

public class SearchDialog: Dialog
{
    private readonly GarageHandler _garageHandler;
    private readonly TextField _registrationNumberField;
    private readonly Button _cancelButton;

    public SearchDialog(GarageHandler garageHandler)
    {
        _garageHandler = garageHandler;
        Title = "Search by Registration Number";
        Width = 46;
        Height = 9;

        Add(new Label { Text = "Registration Number (partial OK):", X = 2, Y = 1 });
        _registrationNumberField = new TextField { X = 2, Y = 3, Width = 30 };

        var searchButton = new Button { Text = "_Search", IsDefault = true, X = Pos.Center() - 8, Y = Pos.AnchorEnd(1) };

        _cancelButton = new Button{Text = "_Cancel", X = Pos.Center() + 2, Y = Pos.AnchorEnd(1)};
        _cancelButton.Accepted += (_, _) => RequestStop();

        Add(_registrationNumberField, searchButton, _cancelButton);
    }

    protected override bool OnAccepting(CommandEventArgs args)
    {
        View? src = null;
        args.Context?.Source?.TryGetTarget(out src);
        if (src == _cancelButton) return base.OnAccepting(args);

        var term = _registrationNumberField.Text ?? "";
        var matches = _garageHandler.GetAll()
            .Where(v => v.RegistrationNumber.Contains(term, StringComparison.OrdinalIgnoreCase))
            .ToList();

        var dt = BuildResultsTable(matches);
        RequestStop();
        App!.Run(new ResultsDialog($"Results for \"{term}\" - {matches.Count} found", dt), ex => false);
        return true;
    }

    private static DataTable BuildResultsTable(IEnumerable<Vehicle> vehicles)
    {
        var dt = new DataTable();
        dt.Columns.Add("Reg. No");
        dt.Columns.Add("Type");
        dt.Columns.Add("Color");
        dt.Columns.Add("Wheels");
        foreach (var v in vehicles)
        {
            dt.Rows.Add(v.RegistrationNumber, v.GetType().Name, v.Color, v.NumberOfWheels);
        }
        return dt;
    }
}
