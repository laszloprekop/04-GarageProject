using System.Data;
using GarageProject.Application;
using Terminal.Gui.Input;
using Terminal.Gui.ViewBase;
using Terminal.Gui.Views;

namespace GarageProject.UI;

public class FilterDialog : Dialog
{
    private readonly GarageHandler _garageHandler;
    private readonly TextField _typeField;
    private readonly TextField _colorField;
    private readonly TextField _wheelsField;
    private readonly Label _errorLabel;
    private readonly Button _cancelButton;

    public FilterDialog(GarageHandler garageHandler)
    {
        _garageHandler = garageHandler;
        Title = "Filter Vehicles";
        Width = 46;
        Height = 16;

        Add(new Label { Text = "Type (blank = any):",      X = 2, Y = 1 });
        Add(new Label { Text = "Color (blank = any):",     X = 2, Y = 4 });
        Add(new Label { Text = "Min. wheels (blank = any):", X = 2, Y = 7 });

        _typeField   = new TextField { X = 2, Y = 2, Width = Dim.Fill() };
        _colorField  = new TextField { X = 2, Y = 5, Width = Dim.Fill() };
        _wheelsField = new TextField { X = 2, Y = 8, Width = Dim.Fill() };
        _errorLabel  = new Label { Text = "", X = 2, Y = 10, Width = Dim.Fill(2), SchemeName = "Error" };

        var filterButton = new Button { Text = "_Filter", IsDefault = true, X = Pos.Center() - 8, Y = Pos.AnchorEnd(1) };
        _cancelButton    = new Button { Text = "_Cancel",                   X = Pos.Center() + 2, Y = Pos.AnchorEnd(1) };
        _cancelButton.Accepted += (_, _) => RequestStop();

        Add(_typeField, _colorField, _wheelsField, _errorLabel, filterButton, _cancelButton);
    }

    protected override bool OnAccepting(CommandEventArgs args)
    {
        View? src = null;
        args.Context?.Source?.TryGetTarget(out src);
        if (src == _cancelButton) return base.OnAccepting(args);

        var type  = _typeField.Text?.Trim();
        var color = _colorField.Text?.Trim();

        int? minWheels = null;
        var wheelsText = _wheelsField.Text?.Trim();
        if (!string.IsNullOrEmpty(wheelsText))
        {
            if (!int.TryParse(wheelsText, out var parsed) || parsed < 0)
            {
                _errorLabel.Text = parsed < 0 ? "Wheels cannot be negative." : "Wheels must be a whole number.";
                _errorLabel.SetNeedsDraw();
                return true;
            }
            minWheels = parsed;
        }

        var results = _garageHandler.Filter(type, color, minWheels).ToList();
        var dt = new DataTable();
        dt.Columns.Add("Reg. no");
        dt.Columns.Add("Type");
        dt.Columns.Add("Color");
        dt.Columns.Add("Wheels");

        foreach (var vehicle in results)
            dt.Rows.Add(vehicle.RegistrationNumber, vehicle.GetType().Name, vehicle.Color, vehicle.NumberOfWheels);

        RequestStop();
        App!.Run(new ResultsDialog($"Filter results — {results.Count} found", dt), ex => false);
        return true;
    }
}
