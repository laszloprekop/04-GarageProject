using GarageProject.Application;
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

        // ...fields for vehicle

        var okButton = new Button
        {
            Text = "_OK",
            IsDefault = true,
            X = Pos.Center() - 7, Y = Pos.AnchorEnd(1)
        };

        okButton.Accepted += (s, e) => onParked();

        var cancelButton = new Button
        {
            Text = "_Cancel",
            X = Pos.Center() + 2, Y = Pos.AnchorEnd(1)
        };
        cancelButton.Accepted += (s, e) => RequestStop();

        Add(okButton, cancelButton);
    }
}
