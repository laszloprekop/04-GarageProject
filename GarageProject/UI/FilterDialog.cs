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


        Add(new Label { Text = "Type (blank = any):", X = 2, Y = 1 });
        Add(new Label { Text = "Color (blank = any):", X = 2, Y = 4 });
        Add(new Label { Text = "Min. wheels:", X = 2, Y = 7 });

        _typeField = new TextField { X = 2, Y = 2, Width = Dim.Fill() };
        _colorField = new TextField { X = 2, Y = 5, Width = Dim.Fill() };
        _wheelsField = new TextField { X = 2, Y = 8, Width = Dim.Fill() };
        _errorLabel = new Label { Text = "", X = 2, Y = 10, Width = Dim.Fill(2), SchemeName = "Error" };

        var filterButton = new Button { Text = "_Filter", X = Pos.Center() - 8, Y = Pos.AnchorEnd(1) };
        _cancelButton = new Button { Text = "_Cancel", X = Pos.Center() + 2, Y = Pos.AnchorEnd(1) };
        _cancelButton.Accepted += (_, _) => RequestStop();

        Add(_typeField, _colorField, _wheelsField, _errorLabel, filterButton, _cancelButton);
    }

    protected override bool OnAccepting(CommandEventArgs args)
    {
        throw new NotImplementedException();
    }
};
