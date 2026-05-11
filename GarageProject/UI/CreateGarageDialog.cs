using GarageProject.Application;
using Terminal.Gui.Input;
using Terminal.Gui.ViewBase;
using Terminal.Gui.Views;

namespace GarageProject.UI;

public class CreateGarageDialog : Dialog
{
    private readonly GarageHandler _handler;
    private readonly Action _onCreated;
    private readonly TextField _capacityField;
    private readonly Label _errorLabel;
    private readonly Button _cancelButton;

    public CreateGarageDialog(GarageHandler handler, Action onCreated)
    {
        _handler = handler;
        _onCreated = onCreated;
        Title = "Create Garage";
        Width = 40;
        Height = 9;

        Add(new Label { Text = "Capacity: (number of Spaces", X = 2, Y = 1 });
        _capacityField = new TextField { X = 2, Y = 3, Width = 10, Text = "15" };
        _errorLabel = new Label { X = 2, Y = 5, Width = Dim.Fill(2), SchemeName = "Error" };

        var okButton = new Button { Text = "_Create", X = Pos.Center() - 7, Y = Pos.AnchorEnd(1) };
        _cancelButton = new Button { Text = "_Cancel", X = Pos.Center() + 2, Y = Pos.AnchorEnd(1) };
        _cancelButton.Accepted += (_, _) => RequestStop();

        Add(_capacityField, _errorLabel, okButton, _cancelButton);
    }

    protected override bool OnAccepting(CommandEventArgs args)
    {
        View? src = null;
        args.Context?.Source?.TryGetTarget(out src);
        if (src == _cancelButton) return base.OnAccepting(args);

        if (!int.TryParse(_capacityField.Text, out var capacity) || capacity < 1)
        {
            _errorLabel.Text = "Enter a whole greater than 0.";
            _errorLabel.SetNeedsDraw();
            return true;
        }

        _handler.CreateGarage(capacity);
        _onCreated();
        return base.OnAccepting(args);
    }
}
