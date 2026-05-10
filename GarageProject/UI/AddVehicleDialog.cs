using System.Collections.ObjectModel;
using GarageProject.Application;
using GarageProject.Domain;
using Terminal.Gui.Input;
using Terminal.Gui.ViewBase;
using Terminal.Gui.Views;

namespace GarageProject.UI;

public class AddVehicleDialog : Dialog
{
    private static readonly string[] VehicleTypes = ["Car", "Motorcycle", "Bus", "Airplane", "Boat"];

    private readonly GarageHandler _handler;
    private readonly Action _onParked;
    private readonly DropDownList _typeList;
    private readonly TextField _regField;
    private readonly TextField _colorField;
    private readonly TextField _wheelsField;
    private readonly Label _errorLabel;
    private readonly Button _cancelBtn;

    public AddVehicleDialog(GarageHandler handler, Action onParked)
    {
        _handler = handler;
        _onParked = onParked;

        Title = "Add Vehicle";
        Width = 50;
        Height = 20;

        var typeLabel   = new Label { Text = "Type (F4/Space to open):", X = 2, Y = 1 };
        var regLabel    = new Label { Text = "Reg. No:",                  X = 2, Y = 4 };
        var colorLabel  = new Label { Text = "Color:",                    X = 2, Y = 7 };
        var wheelsLabel = new Label { Text = "Wheels:",                   X = 2, Y = 10 };
        _errorLabel = new Label { Text = "", X = 2, Y = 13, Width = Dim.Fill(2), SchemeName = "Error" };

        _typeList = new DropDownList
        {
            X = 2, Y = 2, Width = 22,
            ReadOnly = true,
            Source = new ListWrapper<string>(new ObservableCollection<string>(VehicleTypes)),
            Value = VehicleTypes[0],
        };

        _regField    = new TextField { X = 2, Y = 5,  Width = 24 };
        _colorField  = new TextField { X = 2, Y = 8,  Width = 20 };
        _wheelsField = new TextField { X = 2, Y = 11, Width = 6 };

        var okBtn = new Button { Text = "_OK", X = Pos.Center() - 7, Y = Pos.AnchorEnd(1) };

        _cancelBtn = new Button { Text = "_Cancel", X = Pos.Center() + 2, Y = Pos.AnchorEnd(1) };
        _cancelBtn.Accepted += (_, _) => RequestStop();

        Add(typeLabel, _typeList,
            regLabel, _regField,
            colorLabel, _colorField,
            wheelsLabel, _wheelsField,
            _errorLabel, okBtn, _cancelBtn);
    }

    // Override OnAccepting instead of subscribing to this.Accepting.
    // Dialog<TResult>.OnAccepting calls RequestStop() BEFORE raising the Accepting event,
    // so e.Handled = true in the event cannot prevent the close. Returning true from
    // OnAccepting without calling base prevents RequestStop() entirely.
    protected override bool OnAccepting(CommandEventArgs args)
    {
        View? src = null;
        args.Context?.Source?.TryGetTarget(out src);

        if (src == _cancelBtn)
            return base.OnAccepting(args);

        var reg = _regField.Text.Trim().ToUpper();
        if (string.IsNullOrEmpty(reg))
        {
            _errorLabel.Text = "Registration number is required";
            _errorLabel.SetNeedsDraw();
            return true;
        }

        if (!int.TryParse(_wheelsField.Text, out var wheels))
        {
            _errorLabel.Text = "Wheels must be a whole number";
            _errorLabel.SetNeedsDraw();
            return true;
        }

        var type  = _typeList.Value ?? VehicleTypes[0];
        var color = _colorField.Text.Trim();

        Vehicle vehicle = type switch
        {
            "Motorcycle" => new Motorcycle(reg, color, wheels, FuelType.Gasoline, 0),
            "Bus"        => new Bus(reg, color, wheels, 0),
            "Airplane"   => new Airplane(reg, color, wheels, 0, FuelType.Other),
            "Boat"       => new Boat(reg, color, wheels, 0),
            _            => new Car(reg, color, wheels, FuelType.Gasoline)
        };

        if (!_handler.ParkVehicle(vehicle))
        {
            _errorLabel.Text = "Garage is full or reg. no already in use";
            _errorLabel.SetNeedsDraw();
            return true;
        }

        _onParked();
        return base.OnAccepting(args);
    }
}
