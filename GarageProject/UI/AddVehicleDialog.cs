using System.Collections.ObjectModel;
using GarageProject.Application;
using GarageProject.Domain;
using Terminal.Gui.ViewBase;
using Terminal.Gui.Views;

namespace GarageProject.UI;

public class AddVehicleDialog : Dialog
{
    private static readonly string[] VehicleTypes = ["Car", "Motorcycle", "Bus", "Airplane", "Boat"];

    public AddVehicleDialog(GarageHandler garageHandler, Action onParked)
    {
        Title = "Add Vehicle";
        Width = 50;
        Height = 20;

        var typeLabel   = new Label { Text = "Type (F4/Space to open):", X = 2, Y = 1 };
        var regLabel    = new Label { Text = "Reg. No:",                  X = 2, Y = 4 };
        var colorLabel  = new Label { Text = "Color:",                    X = 2, Y = 7 };
        var wheelsLabel = new Label { Text = "Wheels:",                   X = 2, Y = 10 };
        var errorLabel  = new Label { Text = "", X = 2, Y = 13, Width = Dim.Fill(2), SchemeName = "Error" };

        var typeList = new DropDownList
        {
            X = 2, Y = 2, Width = 22,
            ReadOnly = true,
            Source = new ListWrapper<string>(new ObservableCollection<string>(VehicleTypes)),
            Value = VehicleTypes[0],
        };

        var regField    = new TextField { X = 2, Y = 5,  Width = 24 };
        var colorField  = new TextField { X = 2, Y = 8,  Width = 20 };
        var wheelsField = new TextField { X = 2, Y = 11, Width = 6 };

        var okButton = new Button
        {
            Text = "_OK",
            X = Pos.Center() - 7, Y = Pos.AnchorEnd(1)
        };

        var cancelButton = new Button
        {
            Text = "_Cancel",
            X = Pos.Center() + 2, Y = Pos.AnchorEnd(1)
        };
        cancelButton.Accepted += (_, _) => RequestStop();

        // Subscribe to the Dialog's own Accepting event — this fires for BOTH Enter-in-field
        // and OK button click, so validation runs regardless of how the user submits
        this.Accepting += (_, e) =>
        {
            // Let Cancel button through without validation
            if (e.Context?.Source?.TryGetTarget(out var src) == true && src == cancelButton)
                return;

            var reg = regField.Text.Trim().ToUpper();
            if (string.IsNullOrEmpty(reg))
            {
                errorLabel.Text = "Registration number is required";
                errorLabel.SetNeedsDraw();
                e.Handled = true;
                return;
            }

            if (!int.TryParse(wheelsField.Text, out var wheels))
            {
                errorLabel.Text = "Wheels must be a whole number";
                errorLabel.SetNeedsDraw();
                e.Handled = true;
                return;
            }

            var type  = typeList.Value ?? VehicleTypes[0];
            var color = colorField.Text.Trim();

            Vehicle vehicle = type switch
            {
                "Motorcycle" => new Motorcycle(reg, color, wheels, FuelType.Gasoline, 0),
                "Bus"        => new Bus(reg, color, wheels, 0),
                "Airplane"   => new Airplane(reg, color, wheels, 0, FuelType.Other),
                "Boat"       => new Boat(reg, color, wheels, 0),
                _            => new Car(reg, color, wheels, FuelType.Gasoline)
            };

            if (!garageHandler.ParkVehicle(vehicle))
            {
                errorLabel.Text = "Garage is full or reg. no already in use";
                errorLabel.SetNeedsDraw();
                e.Handled = true;
                return;
            }

            onParked();
            RequestStop();
            e.Handled = true;
        };

        Add(typeLabel, typeList,
            regLabel, regField,
            colorLabel, colorField,
            wheelsLabel, wheelsField,
            errorLabel, okButton, cancelButton);
    }
}
