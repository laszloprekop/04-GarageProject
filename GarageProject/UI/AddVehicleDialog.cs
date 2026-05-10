using GarageProject.Application;
using GarageProject.Domain;
using Terminal.Gui.ViewBase;
using Terminal.Gui.Views;

namespace GarageProject.UI;

public class AddVehicleDialog : Dialog
{
    public AddVehicleDialog(GarageHandler garageHandler, Action onParked)
    {
        Title = "Add Vehicle";
        Width = 50;
        Height = 20;

        var typeLabel = new Label { Text = "Type:", X = 2, Y = 1 };
        var regLabel = new Label { Text = "Reg. No:", X = 2, Y = 5 };
        var colorLabel = new Label { Text = "Color:", X = 2, Y = 8 };
        var wheelsLabel = new Label { Text = "Wheels:", X = 2, Y = 11 };
        var errorLabel = new Label { Text = "", X = 2, Y = 14, Width = Dim.Fill(2), SchemeName = "Error" };

        var typeList = new DropDownList() { X = 2, Y = 2, Width = 20 };
        typeList.Source = new ListWrapper<string>(["Car", "Motorcycle", "Bus", "Airplane", "Boat"]);

        var regField = new TextField { X = 2, Y = 6, Width = 24 };
        var colorField = new TextField { X = 2, Y = 9, Width = 20 };
        var wheelsField = new TextField { X = 2, Y = 12, Width = 6 };


        var okButton = new Button

        {
            Text = "_OK",
            IsDefault = true,
            X = Pos.Center() - 7, Y = Pos.AnchorEnd(1)
        };

        okButton.Accepted += (s, e) =>
        {
            var reg = regField.Text.Trim().ToUpper() ?? "";
            if (string.IsNullOrEmpty(reg))
            {
                errorLabel.Text = "Registration number is required";
                return;
            }

            if (!int.TryParse(wheelsField.Text, out var wheels))
            {
                errorLabel.Text = "Wheels must be a whole number";
                return;
            }

            var type = "Car"; // placeholder
            var color = colorField.Text.Trim() ?? "";

            Vehicle? vehicle = type switch
            {
                "Motorcycle" => new Motorcycle(reg, color, wheels, FuelType.Gasoline, 0),
                "Bus" => new Bus(reg, color, wheels, 0),
                "Airplane" => new Airplane(reg, color, wheels, 0, FuelType.Other),
                "Boat" => new Boat(reg, color, wheels, 0),
                _ => new Car(reg, color, wheels, FuelType.Gasoline)
            };

            if (!garageHandler.ParkVehicle(vehicle))
            {
                errorLabel.Text = "Garage is full or reg. no already in use";
                return;
            }

            onParked();
            RequestStop();
        };

        var cancelButton = new Button
        {
            Text = "_Cancel",
            X = Pos.Center() + 2, Y = Pos.AnchorEnd(1)
        };
        cancelButton.Accepted += (s, e) => RequestStop();

        Add(typeLabel, typeList,
            regLabel, regField,
            colorLabel, colorField,
            wheelsLabel, wheelsField,
            errorLabel, okButton, cancelButton);
    }
}
